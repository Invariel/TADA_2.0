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
        internal List<PlayingAlert> pendingAlerts = new List<PlayingAlert>();
        internal List<PlayingAlert> keyboardAlerts = new List<PlayingAlert>();

        internal PlayingAlert nowPlaying = null;

        private Played_Alerts playedAlerts;

        private Graphics myGraphics;

        public bool pauseAlerts = false;

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

        public Size GetClientSize()
        {
            return this.ClientSize;
        }

        public void OnSizeChanged(object sender, EventArgs e)
        {
            if (sender is NumericUpDown && (sender as NumericUpDown).ParentForm != MainTADAForm.mainTADAForm)
            {
                MainTADAForm.mainTADAForm.SetAnimationWindowSize(this.ClientSize);
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

        internal void AlertFinished(PlayingAlert alert)
        {
            if (alert == nowPlaying)
            {
                this.nowPlaying = null;
                DecideWhatToPlay();
            }
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            if (this.nowPlaying != null)
            {
                try
                {
                    this.nowPlaying.DrawElements(e.Graphics);
                }
                catch (System.ArgumentException ex)
                {
                    // Just catch this possibility.  Let it go.
                }
            }
        }

        public bool TogglePause()
        {
            pauseAlerts = !pauseAlerts;
            DecideWhatToPlay();
            return pauseAlerts;
        }

        internal void AddPlayingAlert(PlayingAlert playingAlert, bool isTest = false)
        {
            this.pendingAlerts.Add(playingAlert);
            if (isTest || this.keyboardAlerts.Count >= 0)
            {
                DecideWhatToPlay();
            }
            else
            {
                // Give it a short rest between alerts.
                System.Threading.Thread.Sleep(2500);
                DecideWhatToPlay();
            }
        }

        private void DecideWhatToPlay ()
        {
            if (this.nowPlaying == null && !this.pauseAlerts)
            {
                if (keyboardAlerts.Count > 0)
                {
                    // Then we've got to play the macro.
                    this.nowPlaying = keyboardAlerts[0];
                    keyboardAlerts.RemoveAt(0);
                }
                else if (pendingAlerts.Count > 0)
                {
                    this.nowPlaying = pendingAlerts[0];
                    pendingAlerts.RemoveAt(0);
                }
                else if (this.playedAlerts != null && this.playedAlerts.HasAlertsWaiting())
                {
                    ListedAlert alertToPlay = this.playedAlerts.PlayFirstPendingAlert();
                    this.nowPlaying = new PlayingAlert(alertToPlay.alertImage ?? null, alertToPlay.alertAudio ?? null, alertToPlay.alertStreamerText ?? null, alertToPlay.alertViewerText ?? null, alertToPlay.subObject);
                }

                if (this.nowPlaying != null)
                {
                    this.nowPlaying.Play();
                }

            }
        }
    }
}
