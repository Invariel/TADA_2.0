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

        public TADAAnimationWindow()
        {
            InitializeComponent();

            this.Width = 1280;
            this.Height = 1024;

            this.DoubleBuffered = true;
            myGraphics = this.CreateGraphics();
        }

        public void InvalidateMe()
        {
            if (!this.IsDisposed)
            {
                if (this.InvokeRequired)
                {
                    this.Invoke(new MethodInvoker(Invalidate));
                }
                else
                {
                    this.Invalidate();
                }
            }
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
