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
    public partial class Alert_Designer_MDI : Form
    {
        Bitmap previewImage;

        public Alert_Designer_MDI()
        {
            InitializeComponent();

            this.MdiChildActivate += ManageChildFormTabPages;
            this.tab_ChildMDIForms.SelectedIndexChanged += SwitchTabPage;

            this.ts_NewImageVideo.Click += NewImageVideoForm;

            previewImage = new Bitmap(pb_Preview.Width, pb_Preview.Height);
            Graphics previewGraphics = Graphics.FromImage(previewImage);
            previewGraphics.FillRectangle(Brushes.Black, new Rectangle(new Point(0, 0), previewImage.Size));
            pb_Preview.Image = previewImage;
            pb_Preview.Update();

            ConfigureMenuItems();
        }

        private void ConfigureMenuItems ()
        {
            //ts_NewImageVideo.Click += delegate { NewAlertPiece(new TADAAlertImage()); };
        }

        private void ManageChildFormTabPages (object sender, EventArgs e)
        {
            if (this.ActiveMdiChild == null)
            {
                tab_ChildMDIForms.Visible = false;
            }
            else
            {
                this.ActiveMdiChild.WindowState = FormWindowState.Maximized;
                if (this.ActiveMdiChild.Tag == null)
                {
                    // Add a tabPage to the tabControl (tab_ChildMDIForms) with an appropriate caption.
                    TabPage newTabPage = new TabPage(this.ActiveMdiChild.Text); // Note: Add a listener to update this tab's name by the child.
                    newTabPage.Tag = this.ActiveMdiChild;
                    newTabPage.Parent = tab_ChildMDIForms;
                    tab_ChildMDIForms.SelectedTab = newTabPage;
                    this.ActiveMdiChild.Tag = newTabPage;
                    this.ActiveMdiChild.FormClosed += new FormClosedEventHandler(ChildFormClosed);
                }
            }

            if (!tab_ChildMDIForms.Visible)
            {
                tab_ChildMDIForms.Visible = true;
            }
        }

        private void ChildFormClosed (object sender, FormClosedEventArgs e)
        {
            ((sender as Form).Tag as TabPage).Dispose();
        }

        private void SwitchTabPage (object sender, EventArgs e)
        {
            if (tab_ChildMDIForms.SelectedTab != null && tab_ChildMDIForms.SelectedTab.Tag != null)
            {
                (tab_ChildMDIForms.SelectedTab.Tag as Form).Select();
            }
        }

        //private void NewAlertItem ()
        private void NewImageVideoForm(object sender, EventArgs e)
        {
            _TADAAlertImage newAlertImage = alertImageBindingSource.AddNew() as _TADAAlertImage;

            Form newImageForm = new MDI_Forms.Image_Designer_MDI(newAlertImage, true, null);
            newImageForm.MdiParent = this;
            newImageForm.Text = "Image/Video      ";
            newImageForm.Show();
        }

        private void OpenFile(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
            openFileDialog.Filter = "Text Files (*.txt)|*.txt|All Files (*.*)|*.*";
            if (openFileDialog.ShowDialog(this) == DialogResult.OK)
            {
                string FileName = openFileDialog.FileName;
            }
        }

        private void SaveAsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
            saveFileDialog.Filter = "Text Files (*.txt)|*.txt|All Files (*.*)|*.*";
            if (saveFileDialog.ShowDialog(this) == DialogResult.OK)
            {
                string FileName = saveFileDialog.FileName;
            }
        }

        private void ExitToolsStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void CutToolStripMenuItem_Click(object sender, EventArgs e)
        {
        }

        private void CopyToolStripMenuItem_Click(object sender, EventArgs e)
        {
        }

        private void PasteToolStripMenuItem_Click(object sender, EventArgs e)
        {
        }

        private void CascadeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LayoutMdi(MdiLayout.Cascade);
        }

        private void TileVerticalToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LayoutMdi(MdiLayout.TileVertical);
        }

        private void TileHorizontalToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LayoutMdi(MdiLayout.TileHorizontal);
        }

        private void ArrangeIconsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LayoutMdi(MdiLayout.ArrangeIcons);
        }

        private void CloseAllToolStripMenuItem_Click(object sender, EventArgs e)
        {
            foreach (Form childForm in MdiChildren)
            {
                childForm.Close();
            }
        }

        private void tab_ChildMDIForms_DrawItem(object sender, DrawItemEventArgs e)
        {
            e.Graphics.DrawString("x", e.Font, Brushes.Black, e.Bounds.Right - 15, e.Bounds.Top + 4);
            e.Graphics.DrawString(this.tab_ChildMDIForms.TabPages[e.Index].Text, e.Font, Brushes.Black, e.Bounds.Left + 12, e.Bounds.Top + 4);
            e.DrawFocusRectangle();
        }

        private void tab_ChildMDIForms_MouseUp(object sender, MouseEventArgs e)
        {
            Rectangle r = tab_ChildMDIForms.GetTabRect(this.tab_ChildMDIForms.SelectedIndex);
            Rectangle closeButton = new Rectangle(r.Right - 15, r.Top + 4, 9, 7);
            if (closeButton.Contains(e.Location))
            {
                (this.tab_ChildMDIForms.SelectedTab.Tag as Form).Close();   // This raises the form's Closed event, which, in turn, deletes the tab.
            }

            if (this.tab_ChildMDIForms.TabCount == 0)
            {
                this.tab_ChildMDIForms.Visible = false;
            }
        }

        protected void CompressPoint(ref PointF point, float widthRatio, float heightRatio)
        {
            point.X = point.X * widthRatio;
            point.Y = point.Y * heightRatio;
        }

        private void CompressPoints(List<PointF> points, float widthRatio, float heightRatio)
        {
            for (int i = 0; i < points.Count; ++i)
            {
                PointF point = points[i];
                CompressPoint(ref point, widthRatio, heightRatio);
                points[i] = point;
            }
        }

        // The expectation here is that DrawWaypoints() is called from the time tracker-being-moved code, which first draws a black background rectangle.
        // The expectation is also that the images are drawn in the order "image, streamer text, viewer text".
        private void DrawWaypoints (WaypointContainer waypoints, PointF unmodifiedLocation, SizeF unmodifiedSize, float currentAngle, float currentMagnification, float currentOpacity, Boolean aroundCenter, Bitmap itemToDraw)
        {
            if (waypoints.Waypoints.Count > 0)
            {
                float widthRatio = pb_Preview.Image.Width * 1.0f / MainTADAForm.mainTADAForm.AnimationWindow.ClientSize.Width;
                float heightRatio = pb_Preview.Image.Height * 1.0f / MainTADAForm.mainTADAForm.AnimationWindow.ClientSize.Height;

                PointF currentPosition = waypoints.Waypoints[0].P1;

                Graphics imageGraphics = Graphics.FromImage(previewImage);

                for (int currentWaypoint = 1; currentWaypoint < waypoints.Waypoints.Count; ++ currentWaypoint)
                {
                    Waypoint waypoint = waypoints.Waypoints[currentWaypoint];

                    // Every iteration we want a fresh P0 and P1 because they aren't compressed yet to fit our screen.
                    PointF P0, P1, C0, C1;

                    P0 = currentPosition;
                    P1 = waypoint.P1;

                    if (waypoint.IsCurve)
                    {
                        C0 = waypoint.C0;
                        C1 = waypoint.C1;
                    }
                    else
                    {
                        Bezier.CalculateLinearControlPoints(P0, P1, out C0, out C1);
                    }

                    // Interestingly, compressing points like this does still give the same shape of Bezier curve.
                    CompressPoints(new List<PointF>() { P0, P1, C0, C1 }, widthRatio, heightRatio);

                    imageGraphics.DrawBezier(Pens.White, P0, C0, C1, P1);
                }

                if (unmodifiedLocation != null && itemToDraw != null)
                {
                    CompressPoint(ref unmodifiedLocation, widthRatio, heightRatio);
                    unmodifiedSize.Width *= widthRatio;
                    unmodifiedSize.Height *= heightRatio;

                    imageGraphics.TranslateTransform(unmodifiedLocation.X + (aroundCenter ? unmodifiedSize.Width / 2 : 0.0f), unmodifiedLocation.Y + (aroundCenter ? unmodifiedSize.Height / 2 : 0.0f));
                    imageGraphics.RotateTransform(currentAngle);
                    imageGraphics.DrawImage(itemToDraw, Point.Empty);
                    imageGraphics.RotateTransform(-currentAngle);
                    imageGraphics.TranslateTransform(-unmodifiedLocation.X - (aroundCenter ? unmodifiedSize.Width / 2 : 0.0f), -unmodifiedLocation.Y - (aroundCenter ? unmodifiedSize.Height / 2 : 0.0f));
                }
            }
        }

        public void UpdatePreview ()
        {
            // Update the preview image based on the child MDI forms that are being monitored.
        }
    }
}
