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
        private static TADAAnimationWindow animationWindowSingleton;

        public List<PlayingAlert> playingAlerts = new List<PlayingAlert>();
        public List<PlayingAlert> keyboardAlerts = new List<PlayingAlert>();

        private List<Creation_Forms.MainWaypointForm> listeningDesignWindows = new List<Creation_Forms.MainWaypointForm>();
        private MainTADAForm listeningMainForm;

        private Graphics myGraphics;

        public static TADAAnimationWindow AnimationWindow
        {
            get
            {
                if (animationWindowSingleton == null)
                {
                    animationWindowSingleton = new TADAAnimationWindow();
                }

                return animationWindowSingleton;
            }
        }

        public Graphics graphics
        {
            get { return this.myGraphics; }
        }

        public TADAAnimationWindow()
        {
            InitializeComponent();

            this.Width = 1280;
            this.Height = 1024;

            this.DoubleBuffered = true;
            
            myGraphics = this.CreateGraphics();

            this.SizeChanged += OnSizeChanged;
            this.ClientSizeChanged += OnSizeChanged;
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

        public Size Listen(Creation_Forms.MainWaypointForm form)
        {
            if (!listeningDesignWindows.Contains(form))
            {
                listeningDesignWindows.Add(form);
            }

            return this.ClientSize;
        }

        public Size Listen(MainTADAForm form)
        {
            if (listeningMainForm == null)
            {
                listeningMainForm = form;
            }

            return this.ClientSize;
        }

        public void Silence(Creation_Forms.MainWaypointForm form)
        {
            this.listeningDesignWindows.Remove(form);
        }

        public void OnSizeChanged(object sender, EventArgs e)
        {
            foreach (Creation_Forms.MainWaypointForm form in listeningDesignWindows)
            {
                form.SetAnimationWindowSize(this.ClientSize);
            }

            if (listeningMainForm != null &&
                (listeningMainForm != sender ||
                (sender is NumericUpDown && (sender as NumericUpDown).ParentForm != listeningMainForm)))
            {
                // Then it probably wasn't the main form, so let's tell it our size changed too.
                listeningMainForm.SetAnimationWindowSize(this.ClientSize);
            }
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            foreach (PlayingAlert alert in playingAlerts)
            {
                alert.DrawElements(e.Graphics);
            }
        }

        public void AddPlayingAlert (PlayingAlert playingAlert)
        {
            if (!this.playingAlerts.Contains(playingAlert))
            {
                this.playingAlerts.Add(playingAlert);
            }
        }

        public void RemovePlayingAlert(PlayingAlert playingAlert)
        {
            this.playingAlerts.Remove(playingAlert);
        }
    }
}
