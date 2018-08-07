using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TADA_Advanced_Desktop_Alerts.Creation_Forms
{
    public partial class AlertCreationForm : Form
    {
        private MainTADAForm parentWindow;
        private TADAAnimationWindow animationWindow;
        private ImageCreationForm currentImageForm;
        private AudioCreationForm currentAudioForm;
        private TextCreationForm currentStreamerForm;
        private TextCreationForm currentViewerForm;
        private TADAAlert currentAlert;

        public AlertCreationForm(MainTADAForm parentWindow, TADAAlert tadaAlert)
        {
            InitializeComponent();

            this.parentWindow = parentWindow;
            this.animationWindow = TADAAnimationWindow.AnimationWindow;
            this.currentAlert = tadaAlert;

            foreach (String alertType in General.alertTypes.Values)
            {
                cmb_AlertType.Items.Add(alertType);
            }

            btn_TestAlert.Click += TestAlert;
            btn_Save.Click += SaveAlert;

            dg_Images.CellContentClick += CellEditClicked;
            dg_Audio.CellContentClick += CellEditClicked;
            dg_StreamerMessages.CellContentClick += CellEditClicked;
            dg_ViewerMessages.CellContentClick += CellEditClicked;

            dg_Images.RowsAdded += UpdateMaximum;
            dg_Audio.RowsAdded += UpdateMaximum;
            dg_StreamerMessages.RowsAdded += UpdateMaximum;
            dg_ViewerMessages.RowsAdded += UpdateMaximum;

            dg_Images.RowsRemoved += UpdateMaximum;
            dg_Audio.RowsRemoved += UpdateMaximum;
            dg_StreamerMessages.RowsRemoved += UpdateMaximum;
            dg_ViewerMessages.RowsRemoved += UpdateMaximum;

            dg_Images.RowsAdded += DefaultButtonValue;
            dg_Audio.RowsAdded += DefaultButtonValue;
            dg_StreamerMessages.RowsAdded += DefaultButtonValue;
            dg_ViewerMessages.RowsAdded += DefaultButtonValue;

            numud_TestAudio.GotFocus += UpdateMaximum;
            numud_TestImage.GotFocus += UpdateMaximum;
            numud_TestStreamerMessage.GotFocus += UpdateMaximum;
            numud_TestViewerMessage.GotFocus += UpdateMaximum;

            if (this.currentAlert != null)
            {
                LoadExisting();
            }
        }

        private void DefaultButtonValue (object sender, DataGridViewRowsAddedEventArgs e)
        {
            DataGridViewRow r = (sender as DataGridView).Rows[e.RowIndex];
            for (int cell = 0; cell < r.Cells.Count; ++ cell)
            {
                if (r.Cells[cell] is DataGridViewButtonCell)
                {
                    r.Cells[cell].Value = "New";
                }
            }
        }

        private void LoadExisting()
        {
            BindingSource audioSource = dg_Audio.DataSource as BindingSource;
            BindingSource imageSource = dg_Images.DataSource as BindingSource;
            BindingSource streamerSource = dg_StreamerMessages.DataSource as BindingSource;
            BindingSource viewerSource = dg_ViewerMessages.DataSource as BindingSource;

            cmb_AlertType.Text = this.currentAlert.AlertType;
            txt_AlertName.Text = this.currentAlert.AlertName;

            audioSource.Clear();
            foreach (TADAAlertAudio a in this.currentAlert.Audios)
            {
                audioSource.Add(a);
            }

            imageSource.Clear();
            foreach (TADAAlertImage i in this.currentAlert.Images)
            {
                imageSource.Add(i);
            }

            streamerSource.Clear();
            foreach (TADAAlertText t in this.currentAlert.StreamerTexts)
            {
                streamerSource.Add(t);
            }

            viewerSource.Clear();
            foreach (TADAAlertText t in this.currentAlert.ViewerTexts)
            {
                viewerSource.Add(t);
            }
        }

        private void CellEditClicked(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex >= 0 && e.RowIndex >= 0 && (sender as DataGridView).Columns[e.ColumnIndex] is DataGridViewButtonColumn)
            {
                DataGridView dataGrid = (sender as DataGridView);
                BindingSource bindingSource = dataGrid.DataSource as BindingSource;
                object editObject;

                DataGridView dgView = (sender as DataGridView);
                if (dataGrid.Rows[e.RowIndex].IsNewRow)
                {
                    bindingSource.CancelEdit();
                    editObject = bindingSource.AddNew();
                }
                else
                {
                    editObject = bindingSource.Current;
                }

                if (dataGrid == dg_Images)
                {
                    if (this.currentImageForm == null || this.currentImageForm.IsDisposed)
                    {
                        this.currentImageForm = new ImageCreationForm(this.animationWindow, editObject as TADAAlertImage);
                    }
                    this.currentImageForm.Show();
                }
                else if (dataGrid == dg_Audio)
                {
                    if (this.currentAudioForm == null || this.currentAudioForm.IsDisposed)
                    {
                        this.currentAudioForm = new AudioCreationForm(this.animationWindow, editObject as TADAAlertAudio);
                    }
                    this.currentAudioForm.Show();
                }
                else if (dataGrid == dg_StreamerMessages)
                {
                    if (this.currentStreamerForm == null || this.currentStreamerForm.IsDisposed)
                    {
                        this.currentStreamerForm = new TextCreationForm(this.animationWindow, editObject as TADAAlertText, true);
                    }
                    this.currentStreamerForm.Show();
                }
                else if (dataGrid == dg_ViewerMessages)
                {
                    if (this.currentViewerForm == null || this.currentViewerForm.IsDisposed)
                    {
                        this.currentViewerForm = new TextCreationForm(this.animationWindow, editObject as TADAAlertText, false);
                    }
                    this.currentViewerForm.Show();
                }
            }
        }

        private void UpdateMaximum (object sender, EventArgs e)
        {
            Tuple<NumericUpDown, DataGridView>[] pairing = new Tuple<NumericUpDown, DataGridView>[]
            {
                new Tuple<NumericUpDown, DataGridView> (numud_TestAudio, dg_Audio),
                new Tuple<NumericUpDown, DataGridView> (numud_TestImage, dg_Images),
                new Tuple<NumericUpDown, DataGridView> (numud_TestStreamerMessage, dg_StreamerMessages),
                new Tuple<NumericUpDown, DataGridView> (numud_TestViewerMessage, dg_ViewerMessages)
            };

            foreach (Tuple<NumericUpDown, DataGridView> tuple in pairing)
            {
                tuple.Item1.Maximum = tuple.Item2.Rows.Count - 1;
                tuple.Item1.Minimum = 0;
            }
        }

        private void TestAlert(object sender, EventArgs e)
        {
            int whichAudio = Convert.ToInt32(numud_TestAudio.Value);
            int whichImage = Convert.ToInt32(numud_TestImage.Value);
            int whichStreamerMessage = Convert.ToInt32(numud_TestStreamerMessage.Value);
            int whichViewerMessage = Convert.ToInt32(numud_TestViewerMessage.Value);

            TADAAlertImage image = null;
            TADAAlertAudio audio = null;
            TADAAlertText streamerText = null;
            TADAAlertText viewerText = null;

            SubstitutionObject subObject = new SubstitutionObject
            {
                ViewerName = txt_ViewerName.Text,
                RecipientName = txt_RecipientName.Text,
                Amount = Convert.ToInt32(numud_Amount.Value),
                Tier = General.stringToTwitchLibSubPlan[dom_SubTier.Text],
                TierName = txt_SubTierName.Text
            };

            if (whichAudio > 0)
            {
                audio = (dg_Audio.DataSource as BindingSource)[whichAudio - 1] as TADAAlertAudio;
            }

            if (whichImage > 0)
            {
                image = (dg_Images.DataSource as BindingSource)[whichImage - 1] as TADAAlertImage;
            }

            if (whichStreamerMessage > 0)
            {
                streamerText = (dg_StreamerMessages.DataSource as BindingSource)[whichStreamerMessage - 1] as TADAAlertText;

                String streamerMessage = streamerText.Message;
                String streamerReadMessage = streamerText.ReadMessage;

                General.PerformSubstitutions(ref streamerMessage, subObject);
                General.PerformSubstitutions(ref streamerReadMessage, subObject);

                streamerText.SetMessage(streamerMessage);
                streamerText.SetReadMessage(streamerReadMessage);
            }

            if (whichViewerMessage > 0)
            {
                viewerText = (dg_ViewerMessages.DataSource as BindingSource)[whichViewerMessage - 1] as TADAAlertText;
                viewerText.SetMessage(txt_ViewerMessage.Text);
            }

            PlayingAlert playingAlert = new PlayingAlert(image, audio, streamerText, viewerText);
            this.animationWindow.AddPlayingAlert(playingAlert, true);
            playingAlert.Play();
            this.animationWindow.Show();
        }

        private void SaveAlert(object sender, EventArgs e)
        {
            this.currentAlert.Audios.Clear();
            this.currentAlert.Images.Clear();
            this.currentAlert.StreamerTexts.Clear();
            this.currentAlert.ViewerTexts.Clear();

            foreach (TADAAlertImage image in (dg_Images.DataSource as BindingSource))
            {
                this.currentAlert.Images.Add(image);
            }

            foreach (TADAAlertAudio audio in (dg_Audio.DataSource as BindingSource))
            {
                this.currentAlert.Audios.Add(audio);
            }

            foreach (TADAAlertText text in (dg_StreamerMessages.DataSource as BindingSource))
            {
                this.currentAlert.StreamerTexts.Add(text);
            }

            foreach (TADAAlertText text in (dg_ViewerMessages.DataSource as BindingSource))
            {
                this.currentAlert.ViewerTexts.Add(text);
            }

            foreach (TADAAlertCondition condition in (dg_Conditions.DataSource as BindingSource))
            {
                this.currentAlert.Conditions.Add(condition);
            }

            this.currentAlert.AlertName = txt_AlertName.Text;
            this.currentAlert.AlertType = cmb_AlertType.Text;

            this.parentWindow.Show();
            this.Close();
        }
    }
}
