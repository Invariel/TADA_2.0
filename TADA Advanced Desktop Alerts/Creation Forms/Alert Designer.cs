using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
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

            btn_CreateAudio.Click += CreateAudio;
            btn_CreateImage.Click += CreateImage;
            btn_CreateYour.Click += CreateYour;
            btn_CreateViewer.Click += CreateViewer;

            btn_TestAlert.Click += TestAlert;
            btn_Save.Click += SaveAlert;

            dg_Images.CellClick += EditExistingEntry;
            dg_Audio.CellClick += EditExistingEntry;
            dg_StreamerMessages.CellClick += EditExistingEntry;
            dg_ViewerMessages.CellClick += EditExistingEntry;

            dg_Images.RowsAdded += UpdateMaximum;
            dg_Audio.RowsAdded += UpdateMaximum;
            dg_StreamerMessages.RowsAdded += UpdateMaximum;
            dg_ViewerMessages.RowsAdded += UpdateMaximum;

            dg_Images.RowsRemoved += UpdateMaximum;
            dg_Audio.RowsRemoved += UpdateMaximum;
            dg_StreamerMessages.RowsRemoved += UpdateMaximum;
            dg_ViewerMessages.RowsRemoved += UpdateMaximum;

            numud_TestAudio.GotFocus += UpdateMaximum;
            numud_TestImage.GotFocus += UpdateMaximum;
            numud_TestStreamerMessage.GotFocus += UpdateMaximum;
            numud_TestViewerMessage.GotFocus += UpdateMaximum;

            if (this.currentAlert != null)
            {
                LoadExisting();
            }
        }

        private void LoadExisting()
        {
            BindingSource audioSource = dg_Audio.DataSource as BindingSource;
            BindingSource imageSource = dg_Images.DataSource as BindingSource;
            BindingSource streamerSource = dg_StreamerMessages.DataSource as BindingSource;
            BindingSource viewerSource = dg_ViewerMessages.DataSource as BindingSource;

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

        private void EditExistingEntry(object sender, DataGridViewCellEventArgs e)
        {
            if ((sender as DataGridView).Columns[e.ColumnIndex] is DataGridViewButtonColumn)
            {
                BindingSource bindingSource = (sender as DataGridView).DataSource as BindingSource;

                if (sender == dg_Images)
                {
                    if (this.currentImageForm == null || this.currentImageForm.IsDisposed)
                    {
                        this.currentImageForm = new ImageCreationForm(this.animationWindow, bindingSource.Current as TADAAlertImage);
                    }
                    this.currentImageForm.Show();
                }
                else if (sender == dg_Audio)
                {
                    if (this.currentAudioForm == null || this.currentAudioForm.IsDisposed)
                    {
                        this.currentAudioForm = new AudioCreationForm(this.animationWindow, bindingSource.Current as TADAAlertAudio);
                    }
                    this.currentAudioForm.Show();
                }
                else if (sender == dg_StreamerMessages)
                {
                    if (this.currentStreamerForm == null || this.currentStreamerForm.IsDisposed)
                    {
                        this.currentStreamerForm = new TextCreationForm(this.animationWindow, bindingSource.Current as TADAAlertText, true);
                    }
                    this.currentStreamerForm.Show();
                }
                else if (sender == dg_ViewerMessages)
                {
                    if (this.currentViewerForm == null || this.currentViewerForm.IsDisposed)
                    {
                        this.currentViewerForm = new TextCreationForm(this.animationWindow, bindingSource.Current as TADAAlertText, false);
                    }
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

        private void CreateAudio(object sender, EventArgs e)
        {
            BindingSource bindingSource = dg_Audio.DataSource as BindingSource;
            Creation_Forms.AudioCreationForm audioCreationForm = new AudioCreationForm(this.animationWindow, bindingSource.AddNew() as TADAAlertAudio);
            audioCreationForm.Show();
        }

        private void CreateImage(object sender, EventArgs e)
        {
            BindingSource bindingSource = dg_Images.DataSource as BindingSource;
            Creation_Forms.ImageCreationForm imageCreationForm = new ImageCreationForm(this.animationWindow, bindingSource.AddNew() as TADAAlertImage);
            imageCreationForm.Show();
        }

        private void CreateYour(object sender, EventArgs e)
        {
            BindingSource bindingSource = dg_StreamerMessages.DataSource as BindingSource;
            Creation_Forms.TextCreationForm textCreationForm = new TextCreationForm(this.animationWindow, bindingSource.AddNew() as TADAAlertText, true);
            textCreationForm.Show();
        }

        private void CreateViewer(object sender, EventArgs e)
        {
            BindingSource bindingSource = dg_ViewerMessages.DataSource as BindingSource;
            Creation_Forms.TextCreationForm textCreationForm = new TextCreationForm(this.animationWindow, bindingSource.AddNew() as TADAAlertText, false);
            textCreationForm.Show();
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
                Tier = dom_SubTier.Text,
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

                streamerText.Message = streamerMessage;
                streamerText.ReadMessage = streamerReadMessage;
            }

            if (whichViewerMessage > 0)
            {
                viewerText = (dg_ViewerMessages.DataSource as BindingSource)[whichViewerMessage - 1] as TADAAlertText;
                viewerText.Message = txt_ViewerMessage.Text;
            }

            PlayingAlert playingAlert = new PlayingAlert(image, audio, streamerText, viewerText);
            this.animationWindow.AddPlayingAlert(playingAlert);
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
