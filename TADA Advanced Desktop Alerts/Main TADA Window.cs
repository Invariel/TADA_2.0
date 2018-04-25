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

namespace TADA_Advanced_Desktop_Alerts
{
    public partial class MainTADAForm : Form
    {
        TADAAnimationWindow mainAnimationWindow = new TADAAnimationWindow();

        public MainTADAForm()
        {
            InitializeComponent();

            this.mainAnimationWindow.Show();

            btn_SaveManifest.Click += btn_SaveManifest_Click;
            btn_LoadManifest.Click += btn_LoadManifest_Click;
            btn_MergeManifest.Click += btn_MergeManifest_Click;
        }

        private void btn_CreateAlert_Click(object sender, EventArgs e)
        {
            // Obviously this will need to change.

            Creation_Forms.MainWaypointForm mainWaypointForm = new Creation_Forms.MainWaypointForm();
            mainWaypointForm.Show();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Creation_Forms.ImageCreationForm imageCreationForm = new Creation_Forms.ImageCreationForm(this.mainAnimationWindow, null);
            imageCreationForm.Show();
        }

        private void btn_Text_Click(object sender, EventArgs e)
        {
            Creation_Forms.TextCreationForm textCreationForm = new Creation_Forms.TextCreationForm(this.mainAnimationWindow, null, true);
            textCreationForm.Show();
        }

        private void btn_Audio_Click(object sender, EventArgs e)
        {
            Creation_Forms.AudioCreationForm audioCreationForm = new Creation_Forms.AudioCreationForm(this.mainAnimationWindow, null);
            audioCreationForm.Show();
        }

        private void btn_CreateAlertForm_Click(object sender, EventArgs e)
        {
            Creation_Forms.AlertCreationForm alertCreationForm = new Creation_Forms.AlertCreationForm(this, this.mainAnimationWindow, (dg_Alerts.DataSource as BindingSource).AddNew() as TADAAlert);
            alertCreationForm.Show();
        }

        private void btn_SaveManifest_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.DefaultExt = "tada";
            saveFileDialog.Filter = "TADA Configuration Files (*.tada)|*.tada|All Files (*.*)|*.*";
            saveFileDialog.FilterIndex = 0;
            saveFileDialog.InitialDirectory = Environment.CurrentDirectory;
            saveFileDialog.RestoreDirectory = true;
            saveFileDialog.CreatePrompt = false;
            saveFileDialog.OverwritePrompt = true;

            DialogResult result = saveFileDialog.ShowDialog();

            if (result == DialogResult.OK)
            {
                XmlWriterSettings xmlWriterSettings = new XmlWriterSettings();
                xmlWriterSettings.Encoding = Encoding.UTF8;
                xmlWriterSettings.Indent = true;
                xmlWriterSettings.NewLineOnAttributes = false;

                using (System.IO.FileStream fileStream = new System.IO.FileStream(saveFileDialog.FileName, System.IO.FileMode.Create))
                {
                    XmlWriter xmlWriter = XmlWriter.Create(fileStream, xmlWriterSettings);
                    xmlWriter.WriteStartDocument();
                    using (BindingSource b = dg_Alerts.DataSource as BindingSource)
                    {
                        foreach (TADAAlert a in b)
                        {
                            a.WriteXml(xmlWriter);
                        }
                    }
                    xmlWriter.WriteEndDocument();
                    xmlWriter.Flush();
                    xmlWriter.Close();
                }
            }
        }

        private void btn_LoadManifest_Click(object sender, EventArgs e)
        {
        }

        private void btn_MergeManifest_Click(object sender, EventArgs e)
        {
        }
    }
}
