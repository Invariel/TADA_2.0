using System;
using System.Collections.Generic;
using System.Drawing;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Shell;
using System.Xml;

/*
 * Special thanks to
 * GrizzlyGuyGaming
 * TheCyberMonk
 * TransientGamers
 * EdwardIgnis
 * ceriam
 * PyurJenius
 * BrokeAndStoked -- Maybe OBS integration?
 * Brawli (of OverboredGaming)
 * AggyTheSalty -- users trigger stuff from chat [keyword -> alert + cooldown]
 * Shatterglasslol
 * HelloBoing
 * TheHunterWild 
 * Vinc3ntVega
 * PurpleElephantGaming
 * Unkle_J
 * SunderSeries
 * OldManSethus
 * BackgroundGuy02
 * SkaF1aw
 * 
 * for advice and opinions
 */

/*
 * Testers
 * 
 * Trogdor
 * ixirsii
 * Troupster
 * PyurJenius
 * Unkle_J
 */

namespace TADA_Advanced_Desktop_Alerts
{
    public partial class MainTADAForm : Form
    {
        const String TADAVersion = "0.0.5";

        bool blockUpdates = false;

        Creation_Forms.AlertCreationForm _alertCreationForm;
        private TADAAnimationWindow animationWindow;

        internal WebSocketServer webSocketServer;

        private static MainTADAForm _mainTADAForm;

        public static MainTADAForm mainTADAForm
        {
            get { return _mainTADAForm; }
        }

        public TADAAnimationWindow AnimationWindow
        {
            get
            {
                if (this.animationWindow == null)
                {
                    this.animationWindow = new TADAAnimationWindow();
                }

                return this.animationWindow;
            }
        }
        
        public String ChannelName()
        {
            return txt_ChannelName.Text;
        }

        public String OauthToken()
        {
            return txt_AccessToken.Text;
        }

        public MainTADAForm()
        {
            InitializeComponent();

            this.AnimationWindow.Show();

            btn_SaveManifest.Click += btn_SaveManifest_Click;
            btn_LoadManifest.Click += btn_LoadManifest_Click;
            btn_MergeManifest.Click += btn_MergeManifest_Click;

            btn_SaveChannelDetails.Click += btn_SaveChannelDetails_Click;
            btn_LoadChannelDetails.Click += btn_LoadChannelDetails_Click;

            btn_GetAccessToken.Click += GetAccessToken;

            numud_AnimationWindowSizeX.ValueChanged += ResizeAnimationWindow;
            numud_AnimationWindowSizeY.ValueChanged += ResizeAnimationWindow;

            txt_BackgroundARGB.TextChanged += UpdateARGB;
            numud_bgR.ValueChanged += UpdateARGB;
            numud_bgG.ValueChanged += UpdateARGB;
            numud_bgB.ValueChanged += UpdateARGB;
            btn_ColourPicker.Click += (sender, e) => General.ColourPicker(numud_bgR, numud_bgG, numud_bgB, null);

            txt_AccessToken.TextChanged += PasswordMask;

            dg_Alerts.CellContentClick += NewOrEditEntry;
            dg_Alerts.RowsAdded += DefaultButtonValue;

            btn_Connect.Click += ConnectToTwitch;

            this.FormClosing += StopWebSocketServer;

            if (System.IO.File.Exists(General.SUBDIRECTORY + General.DEFAULT_ALERTS))
            {
                try
                {
                    LoadManifest(General.SUBDIRECTORY + General.DEFAULT_ALERTS);
                }
                catch (Exception e)
                {
                    System.Windows.Forms.MessageBox.Show(null,
                                                         "An error occurred trying to automatically load default.tada.  Please remove the file, or otherwise deal with the issue." +
                                                         Environment.NewLine +
                                                         Environment.NewLine +
                                                         "Original error: " + e.Message,
                                                         "Error While Loading default.tada",
                                                         MessageBoxButtons.OK,
                                                         MessageBoxIcon.Error);
                }
            }

            if (System.IO.File.Exists(General.SUBDIRECTORY + General.DEFAULT_OAUTH))
            {
                LoadChannelConfiguration(General.SUBDIRECTORY + General.DEFAULT_OAUTH);
            }

            blockUpdates = true;
            Size animationWindowSize = this.animationWindow.GetClientSize();
            numud_AnimationWindowSizeX.Value = animationWindowSize.Width;
            numud_AnimationWindowSizeY.Value = animationWindowSize.Height;
            this.animationWindow.BackColor = Color.FromArgb(255, Convert.ToInt32(numud_bgR.Value), Convert.ToInt32(numud_bgG.Value), Convert.ToInt32(numud_bgB.Value));
            blockUpdates = false;

            new TimePeriod(1);

            _mainTADAForm = this;
            webSocketServer = new WebSocketServer(21573);
        }

        private void StopWebSocketServer(object sender, FormClosingEventArgs e)
        {
            webSocketServer.StopListening();
        }

        private void GetAccessToken(object sender, EventArgs e)
        {
            // channel_subscription - for PubSub to be able to read when someone subscribes
            // channel_check_subscription - to check if a user is subscribed
            System.Diagnostics.Process.Start("https://api.twitch.tv/kraken/oauth2/authorize?client_id=" +
                                             General.CLIENT_ID +
                                             "&redirect_uri=" +
                                             General.AUTH_URI +
                                             "&response_type=token&" +
                                             "scope=chat_login channel_subscriptions channel_check_subscription");
        }

        private void PasswordMask(object sender, EventArgs e)
        {
            if (String.IsNullOrEmpty(txt_AccessToken.Text))
            {
                txt_AccessToken.Text = "Paste Access Token Here";
                txt_AccessToken.PasswordChar = '\0';
            }
            else
            {
                txt_AccessToken.PasswordChar = '*';
            }
        }

        private void UpdateARGB(object sender, EventArgs e)
        {
            if (!blockUpdates)
            {
                blockUpdates = true;
                if (sender == txt_BackgroundARGB)
                {
                    String colorString = txt_BackgroundARGB.Text;
                    while (colorString.Length < 7)
                    {
                        colorString += "0";
                    }

                    numud_bgR.Value = Convert.ToInt32(colorString.Substring(1, 2), 16);
                    numud_bgG.Value = Convert.ToInt32(colorString.Substring(3, 2), 16);
                    numud_bgB.Value = Convert.ToInt32(colorString.Substring(5, 2), 16);
                }
                else
                {
                    txt_BackgroundARGB.Text = String.Format("#{0:X2}{1:X2}{2:X2}", Convert.ToInt32(numud_bgR.Value), Convert.ToInt32(numud_bgG.Value), Convert.ToInt32(numud_bgB.Value));
                }

                blockUpdates = false;
                ColorAnimationWindow();
            }
        }

        private void ColorAnimationWindow()
        {
            if (!blockUpdates)
            {
                blockUpdates = true;

                int r = Convert.ToInt32(numud_bgR.Value);
                int g = Convert.ToInt32(numud_bgG.Value);
                int b = Convert.ToInt32(numud_bgB.Value);

                this.animationWindow.BackColor = Color.FromArgb(r, g, b);

                blockUpdates = false;
            }
        }

        private void ResizeAnimationWindow(object sender, EventArgs e)
        {
            if (!blockUpdates)
            {
                blockUpdates = true;

                this.animationWindow.ClientSize = new Size(Convert.ToInt32(numud_AnimationWindowSizeX.Value), Convert.ToInt32(numud_AnimationWindowSizeY.Value));

                blockUpdates = false;
            }
        }

        public void SetAnimationWindowSize (int width, int height)
        {
            if (!blockUpdates)
            {
                blockUpdates = true;

                numud_AnimationWindowSizeX.Value = width;
                numud_AnimationWindowSizeY.Value = height;

                blockUpdates = false;
            }
        }

        public void SetAnimationWindowSize(Size size)
        {
            if (!blockUpdates)
            {
                blockUpdates = true;

                numud_AnimationWindowSizeX.Value = size.Width;
                numud_AnimationWindowSizeY.Value = size.Height;

                blockUpdates = false;
            }
        }

        private void HideAnimationWindow(object sender, EventArgs e)
        {
            if (this.animationWindow.Location.X < 0)
            {
                this.animationWindow.Location = new Point(0, 0);
                btn_ShowHideAnimationWindow.Text = "Hide Animation Window";
            }
            else
            {
                this.animationWindow.Location = new Point(-5000, -5000);
                btn_ShowHideAnimationWindow.Text = "Show Animation Window";
            }
        }

        private void DefaultButtonValue(object sender, DataGridViewRowsAddedEventArgs e)
        {
            DataGridViewRow r = (sender as DataGridView).Rows[e.RowIndex];
            for (int cell = 0; cell < r.Cells.Count; ++cell)
            {
                if (r.Cells[cell] is DataGridViewButtonCell)
                {
                    r.Cells[cell].Value = "New";
                }
            }
        }

        private void NewOrEditEntry(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex >= 0 && e.RowIndex >= 0 && (sender as DataGridView).Columns[e.ColumnIndex] is DataGridViewButtonColumn)
            {
                if (_alertCreationForm == null || _alertCreationForm.IsDisposed)
                {
                    DataGridView dataGrid = (sender as DataGridView);
                    BindingSource bindingSource = dataGrid.DataSource as BindingSource;
                    TADAAlert editObject;

                    DataGridView dgView = (sender as DataGridView);
                    if (dataGrid.Rows[e.RowIndex].IsNewRow)
                    {
                        bindingSource.CancelEdit();
                        editObject = bindingSource.AddNew() as TADAAlert;
                    }
                    else
                    {
                        editObject = bindingSource.Current as TADAAlert;
                    }

                    _alertCreationForm = new Creation_Forms.AlertCreationForm(this, editObject);
                }

                _alertCreationForm.Show();
            }
        }

        private void btn_SaveManifest_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog
            {
                DefaultExt = "tada",
                Filter = "TADA Configuration Files (*.tada)|*.tada|All Files (*.*)|*.*",
                FilterIndex = 0,
                InitialDirectory = Environment.CurrentDirectory,
                RestoreDirectory = true,
                CreatePrompt = false,
                OverwritePrompt = true
            };

            DialogResult result = saveFileDialog.ShowDialog();

            if (result == DialogResult.OK)
            {
                XmlWriterSettings xmlWriterSettings = new XmlWriterSettings
                {
                    Encoding = Encoding.UTF8,
                    Indent = true,
                    NewLineOnAttributes = false
                };

                using (System.IO.FileStream fileStream = new System.IO.FileStream(saveFileDialog.FileName, System.IO.FileMode.Create))
                {
                    XmlWriter xmlWriter = XmlWriter.Create(fileStream, xmlWriterSettings);
                    xmlWriter.WriteStartDocument();

                    xmlWriter.WriteStartElement("TADAConfiguration");
                    xmlWriter.WriteAttributeString("Version", TADAVersion);

                    xmlWriter.WriteStartElement("Window");
                    XmlHelper.WriteColor(xmlWriter, Color.FromArgb(255, Convert.ToInt32(numud_bgR.Value), Convert.ToInt32(numud_bgG.Value), Convert.ToInt32(numud_bgB.Value)), "Window");
                    XmlHelper.WriteSize(xmlWriter, new Size(Convert.ToInt32(numud_AnimationWindowSizeX.Value), Convert.ToInt32(numud_AnimationWindowSizeY.Value)), "Window");

                    if (!this.animationWindow.IsDisposed)
                    {
                        XmlHelper.WritePoint(xmlWriter, this.animationWindow.Location, "Window");
                    }

                    xmlWriter.WriteEndElement();

                    xmlWriter.WriteStartElement("FPS");
                    xmlWriter.WriteAttributeString("Value", (rad_60FPS.Checked ? "60" : "30"));
                    xmlWriter.WriteEndElement();

                    BindingSource b = dg_Alerts.DataSource as BindingSource;

                    foreach (TADAAlert a in b)
                    {
                        a.WriteXml(xmlWriter);
                    }

                    xmlWriter.WriteEndElement();
                    xmlWriter.WriteEndDocument();
                    xmlWriter.Flush();
                    xmlWriter.Close();
                }
            }
        }

        private void btn_LoadManifest_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog
            {
                Filter = "TADA Configuration Files (*.tada)|*.tada|All Files (*.*)|*.*",
                FilterIndex = 0,
                InitialDirectory = Environment.CurrentDirectory,
                CheckFileExists = true,
                RestoreDirectory = true
            };

            DialogResult result = openFileDialog.ShowDialog();

            if (result == DialogResult.OK && System.IO.File.Exists(openFileDialog.FileName))
            {
                LoadManifest(openFileDialog.FileName);
            }
        }

        private void btn_MergeManifest_Click(object sender, EventArgs e)
        {
        }

        private void LoadManifest(String filename)
        {
            XmlReaderSettings xmlReaderSettings = new XmlReaderSettings
            {
                IgnoreWhitespace = true,
                ValidationType = ValidationType.None
            };

            using (System.IO.FileStream fileStream = new System.IO.FileStream(filename, System.IO.FileMode.Open))
            {
                XmlReader xmlReader = XmlReader.Create(fileStream, xmlReaderSettings);
                while (xmlReader.Read())
                {
                    if (xmlReader.IsStartElement("Alert"))
                    {
                        TADAAlert newAlert = (this.dg_Alerts.DataSource as BindingSource).AddNew() as TADAAlert;
                        newAlert.ReadXml(xmlReader);
                    }
                    else if (xmlReader.IsStartElement("Window"))
                    {
                        while (xmlReader.Read())
                        {
                            if (xmlReader.IsStartElement("Color"))
                            {
                                XmlHelper.ReadColor(xmlReader, out Color color, out string name);
                                numud_bgR.Value = color.R;
                                numud_bgG.Value = color.G;
                                numud_bgB.Value = color.B;
                            }
                            else if (xmlReader.IsStartElement("Size"))
                            {
                                XmlHelper.ReadSize(xmlReader, out Size size, out string name);
                                numud_AnimationWindowSizeX.Value = size.Width;
                                numud_AnimationWindowSizeY.Value = size.Height;
                            }
                            else if (xmlReader.IsStartElement("Point"))
                            {
                                XmlHelper.ReadPoint(xmlReader, out Point point, out string name);
                                if (!this.animationWindow.IsDisposed)
                                {
                                    this.animationWindow.Location = point;
                                }
                            }
                            else if (xmlReader.IsStartElement("FPS"))
                            {
                                switch (xmlReader.GetAttribute("Value").ToString())
                                {
                                    case "60":
                                        rad_60FPS.Checked = true;
                                        break;
                                    case "30":
                                    default:
                                        rad_30FPS.Checked = true;
                                        break;
                                }
                            }
                            else if (!xmlReader.IsStartElement() && xmlReader.Name == "Window")
                            {
                                break;
                            }
                        }
                    }
                }
            }
        }

        private void btn_SaveChannelDetails_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog
            {
                DefaultExt = "cfg",
                Filter = "Channel Configuration Files (*.cfg)|*.cfg|All Files (*.*)|*.*",
                FilterIndex = 0,
                InitialDirectory = Environment.CurrentDirectory,
                RestoreDirectory = true,
                CreatePrompt = false,
                OverwritePrompt = true
            };

            DialogResult result = saveFileDialog.ShowDialog();

            if (result == DialogResult.OK)
            {
                using (System.IO.StreamWriter writer = new System.IO.StreamWriter(saveFileDialog.FileName, false))
                {
                    writer.WriteLine(txt_ChannelName.Text);
                    writer.WriteLine(txt_AccessToken.Text);
                }
            }
        }

        private void btn_LoadChannelDetails_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog
            {
                Filter = "Channel Configuration Files(*.cfg)|*.cfg|All Files (*.*)|*.*",
                DefaultExt = ".cfg",
                FilterIndex = 0,
                InitialDirectory = Environment.CurrentDirectory,
                CheckFileExists = true,
                RestoreDirectory = true
            };

            DialogResult result = openFileDialog.ShowDialog();

            if (result == DialogResult.OK && System.IO.File.Exists(openFileDialog.FileName))
            {
                LoadChannelConfiguration(openFileDialog.FileName);
            }
        }

        private void LoadChannelConfiguration(String filename)
        {
            using (System.IO.StreamReader reader = new System.IO.StreamReader(filename))
            {
                txt_ChannelName.Text = reader.ReadLine();
                txt_AccessToken.Text = reader.ReadLine();
            }
        }

        private void ConnectToTwitch(object sender, EventArgs e)
        {
            if (!String.IsNullOrEmpty(txt_ChannelName.Text) && !String.IsNullOrEmpty(txt_AccessToken.Text))
            {
                rad_30FPS.Enabled = false;
                rad_60FPS.Enabled = false;

                Played_Alerts playedAlerts = new Played_Alerts(txt_ChannelName.Text, txt_AccessToken.Text);
                playedAlerts.Show();
            }
            else if (String.IsNullOrEmpty(txt_ChannelName.Text))
            {
                MessageBox.Show(null, "No channel name entered.", "No Channel Entered", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else if (String.IsNullOrEmpty(txt_AccessToken.Text))
            {
                MessageBox.Show(null, "No Oauth code supplied.", "No Oauth Code", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public Dictionary<String, List<TADAAlert>> snapshotAlerts ()
        {
            Dictionary<String, List<TADAAlert>> dictionary = new Dictionary<string, List<TADAAlert>>();

            BindingSource definedAlerts = dg_Alerts.DataSource as BindingSource;
            foreach (TADAAlert alert in definedAlerts)
            {
                if (!dictionary.ContainsKey(alert.AlertType))
                {
                    dictionary.Add(alert.AlertType, new List<TADAAlert>());
                }

                dictionary[alert.AlertType].Add(alert);
            }

            return dictionary;
        }
    }
}
