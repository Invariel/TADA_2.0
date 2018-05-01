using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TADA_Advanced_Desktop_Alerts
{
    public partial class TADAAnimationWindow : Form
    {
        public PlayingAlert playingAlert = null;

        public Graphics myGraphics;

        List<Creation_Forms.MainWaypointForm> listeningWaypointForms = new List<Creation_Forms.MainWaypointForm>();
        MainTADAForm listeningMainForm;

        public TADAAnimationWindow()
        {
            InitializeComponent();

            this.Width = 1280;
            this.Height = 1024;

            this.DoubleBuffered = true;
            
            myGraphics = this.CreateGraphics();

            this.SizeChanged += TellObservers;
        }

        // Testing Refresh instead of Invalidate to hopefully force a full form refresh.
        public void InvalidateMe()
        {
            if (!this.IsDisposed)
            {
                if (this.InvokeRequired)
                {
                    this.Invoke(new MethodInvoker(Refresh));
                }
                else
                {
                    this.Refresh();
                }
            }
        }

        public void Listen(Creation_Forms.MainWaypointForm form)
        {
            listeningWaypointForms.Add(form);
        }

        public void Listen(MainTADAForm form)
        {
            listeningMainForm = form;
        }

        public void Silence(Creation_Forms.MainWaypointForm form)
        {
            listeningWaypointForms.Remove(form);
        }

        public void TellObservers(object sender, EventArgs e)
        {
            foreach (Creation_Forms.MainWaypointForm form in listeningWaypointForms)
            {
                form.SetAnimationWindowSize(this.ClientSize.Width, this.ClientSize.Height);
            }

            listeningMainForm.SetAnimationWindowSize(this.ClientSize.Width, this.ClientSize.Height);
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            if (this.playingAlert != null)
            {
                this.playingAlert.DrawElements(e.Graphics);
            }
        }

        public void RemovePlayingAlert(PlayingAlert playingAlert)
        {
        }
    }
}
