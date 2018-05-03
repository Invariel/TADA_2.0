using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
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
 */

 // Add a button that makes the window "disappear" --> get rendered over at position (-3000, -3000).  Test this with OBS to make sure things still render.
 // Figure out why window size is wrong.

namespace TADA_Advanced_Desktop_Alerts
{
    public partial class MainTADAForm : Form
    {
        const String TADAVersion = "0.0.5";

        List<Creation_Forms.MainWaypointForm> animationWindowListeners = new List<Creation_Forms.MainWaypointForm>();

        TADAAnimationWindow mainAnimationWindow;

        bool blockUpdates = false;

        public MainTADAForm()
        {
            InitializeComponent();

            this.mainAnimationWindow = TADAAnimationWindow.AnimationWindow;
            this.mainAnimationWindow.Show();

            btn_SaveManifest.Click += btn_SaveManifest_Click;
            btn_LoadManifest.Click += btn_LoadManifest_Click;
            btn_MergeManifest.Click += btn_MergeManifest_Click;

            numud_AnimationWindowSizeX.ValueChanged += ResizeAnimationWindow;
            numud_AnimationWindowSizeY.ValueChanged += ResizeAnimationWindow;

            txt_BackgroundARGB.TextChanged += UpdateARGB;
            numud_bgR.ValueChanged += UpdateARGB;
            numud_bgG.ValueChanged += UpdateARGB;
            numud_bgB.ValueChanged += UpdateARGB;

            txt_AccessToken.TextChanged += PasswordMask;

            dg_Alerts.CellClick += EditExistingEntry;

            btn_Connect.Click += Btn_Connect_Click;

            // This is where the default manifest file would be loaded.
            if (System.IO.File.Exists("./default.tada"))
            {
                try
                {
                    LoadManifest("./default.tada");
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

            ResizeAnimationWindow(null, null);
            new TimePeriod(1);
        }

        private void Btn_Connect_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Not yet, sorry.");
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
                    txt_BackgroundARGB.Text = String.Format("#{0:X2}{1:X2}{2:X2}", numud_bgR.Value, numud_bgG.Value, numud_bgR.Value);
                }

                ColorAnimationWindow();
                blockUpdates = false;
            }
        }

        private void ColorAnimationWindow()
        {
            if (this.mainAnimationWindow == null)
            {
                this.mainAnimationWindow = new TADAAnimationWindow();
                this.mainAnimationWindow.Show();
            }

            if (!blockUpdates)
            {
                blockUpdates = true;

                int r = Convert.ToInt32(numud_bgR.Value);
                int g = Convert.ToInt32(numud_bgG.Value);
                int b = Convert.ToInt32(numud_bgB.Value);

                this.mainAnimationWindow.BackColor = Color.FromArgb(r, g, b);

                blockUpdates = false;
            }
        }

        private void ResizeAnimationWindow(object sender, EventArgs e)
        {
            if (this.mainAnimationWindow == null)
            {
                this.mainAnimationWindow = new TADAAnimationWindow();
                this.mainAnimationWindow.Show();
            }
            
            if (!blockUpdates)
            {
                blockUpdates = true;

                this.mainAnimationWindow.ClientSize = new Size(Convert.ToInt32(numud_AnimationWindowSizeX.Value), Convert.ToInt32(numud_AnimationWindowSizeY.Value));

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
            if (this.mainAnimationWindow.Location.X < 0)
            {
                this.mainAnimationWindow.Location = new Point(0, 0);
                btn_ShowHideAnimationWindow.Text = "Hide Animation Window";
            }
            else
            {
                this.mainAnimationWindow.Location = new Point(-5000, -5000);
                btn_ShowHideAnimationWindow.Text = "Show Animation Window";
            }
        }

        private void btn_CreateAlertForm_Click(object sender, EventArgs e)
        {
            Creation_Forms.AlertCreationForm alertCreationForm = new Creation_Forms.AlertCreationForm(this, (dg_Alerts.DataSource as BindingSource).AddNew() as TADAAlert);
            alertCreationForm.Show();
        }

        private void EditExistingEntry(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex >= 0 && (sender as DataGridView).Columns[e.ColumnIndex] is DataGridViewButtonColumn)
            {
                BindingSource bindingSource = (sender as DataGridView).DataSource as BindingSource;

                Creation_Forms.AlertCreationForm alertCreationForm = new Creation_Forms.AlertCreationForm(this, bindingSource.Current as TADAAlert);
                alertCreationForm.Show();
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

                    if (!this.mainAnimationWindow.IsDisposed)
                    {
                        XmlHelper.WritePoint(xmlWriter, this.mainAnimationWindow.Location, "Window");
                    }

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
                                if (!this.mainAnimationWindow.IsDisposed)
                                {
                                    this.mainAnimationWindow.Location = point;
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
    }
}
