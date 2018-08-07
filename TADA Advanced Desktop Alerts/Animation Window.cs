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

        public List<PlayingAlert> pendingAlerts = new List<PlayingAlert>();
        public List<PlayingAlert> keyboardAlerts = new List<PlayingAlert>();

        private PlayingAlert nowPlaying = null;

        private List<Creation_Forms.MainWaypointForm> listeningDesignWindows = new List<Creation_Forms.MainWaypointForm>();
        private Played_Alerts playedAlerts;
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

        public void SetPlayedAlerts (Played_Alerts alertForm)
        {
            this.playedAlerts = alertForm;
        }

        public void PendingAlert ()
        {
            this.DecideWhatToPlay();
        }

        public void AlertFinished(PlayingAlert alert)
        {
            if (alert == nowPlaying)
            {
                this.nowPlaying = null;
                DecideWhatToPlay();
            }
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            foreach (PlayingAlert alert in pendingAlerts)
            {
                alert.DrawElements(e.Graphics);
            }
        }

        public void AddPlayingAlert(PlayingAlert playingAlert, bool isTest = false)
        {
            this.pendingAlerts.Add(playingAlert);
            if (isTest || this.keyboardAlerts.Count >= 0)
            {
                DecideWhatToPlay();
            }
            else
            {
                // Then give it about five seconds.

                DecideWhatToPlay();
            }
        }

        private void DecideWhatToPlay ()
        {
            if (this.nowPlaying == null)
            {
                if (keyboardAlerts.Count > 0)
                {
                    // Then we've got to play the macro.
                    this.nowPlaying = keyboardAlerts[0];
                    keyboardAlerts.RemoveAt(0);
                    this.nowPlaying.Play();
                }
                else if (pendingAlerts.Count > 0)
                {
                    this.nowPlaying = pendingAlerts[0];
                    pendingAlerts.RemoveAt(0);
                    this.nowPlaying.Play();
                }
                else if (this.playedAlerts.HasAlertsWaiting())
                {
                    this.nowPlaying = this.playedAlerts.PlayFirstPendingAlert();
                    this.nowPlaying.Play();
                }
            }
        }
    }
}
