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
        _TADAAlert mainAlert;

        Bitmap previewImage;

        Color COLOR_TAB = Color.White;
        Color COLOR_IMAGE = Color.Yellow;
        Color COLOR_STREAMER_TEXT = Color.Green;
        Color COLOR_VIEWER_TEXT = Color.OrangeRed;

        private double MaximumTime;

        private List <Tuple <_TADAAlertImage, TabPage>> imageTabPages = new List<Tuple<_TADAAlertImage, TabPage>>();
        private List <Tuple <_TADAAlertAudio, TabPage>> audioTabPages = new List<Tuple<_TADAAlertAudio, TabPage>>();
        private List <Tuple <_TADAAlertText, TabPage>> textTabPages = new List<Tuple<_TADAAlertText, TabPage>>();

        internal Alert_Designer_MDI(_TADAAlert originalAlert = null)
        {
            InitializeComponent();

            this.MdiChildActivate += ShowOrHideMdiTabs;
            this.tab_ChildMDIForms.SelectedIndexChanged += SwitchTabPage;

            previewImage = new Bitmap(pb_Preview.Width, pb_Preview.Height);
            Graphics previewGraphics = Graphics.FromImage(previewImage);
            previewGraphics.FillRectangle(Brushes.Black, new Rectangle(new Point(0, 0), previewImage.Size));
            pb_Preview.Image = previewImage;
            pb_Preview.Update();

            pb_Preview.MouseUp += SendCoordinatesToChildForm;

            trk_Time.ValueChanged += TrackbarPositionChanged;

            ConfigureMenuItems();

            FillAlertTypeComboBox();

            this.mainAlert = originalAlert;
            if (originalAlert != null)
            {
                // Then fill the four alert types, and conditions.
            }
        }

        private void ConfigureMenuItems ()
        {
            this.ts_NewImageVideo.Click += NewImageVideoForm;
//            this.ts_NewStreamerAudio.Click += NewAudioForm;
//            this.ts_NewStreamerText.Click += NewStreamerTextForm;
//            this.ts_NewViewerText.Click += NewViewerTextForm;
        }

        private void FillAlertTypeComboBox()
        {
            foreach (String alertType in General.alertTypes.Values)
            {
                if (General.alertStrings[alertType] != General.ALERTTYPES.NONE)
                {
                    cmb_AlertType.Items.Add(alertType);
                }
            }

            cmb_AlertType.DropDownWidth = cmb_AlertType.Items.Cast<string>().Max(x => TextRenderer.MeasureText(x, cmb_AlertType.Font).Width);
            cmb_AlertType.SelectedIndex = 0;
        }

        private String GetCaption(String type, int number)
        {
            return String.Format("{0} {1}     ", type, number);
        }

        private void AddTab(_TADAAlertImage alertImage, TabPage newTab)
        {
            imageTabPages.Add(new Tuple<_TADAAlertImage, TabPage>(alertImage, newTab));
        }

        private void AddTab(_TADAAlertAudio alertAudio, TabPage newTab)
        {
            audioTabPages.Add(new Tuple<_TADAAlertAudio, TabPage>(alertAudio, newTab));
        }

        private void AddTab(_TADAAlertText alertText, TabPage newTab)
        {
            textTabPages.Add(new Tuple<_TADAAlertText, TabPage>(alertText, newTab));
        }

        private TabPage TabExists(_TADAAlertImage alertImage)
        {
            Tuple<_TADAAlertImage, TabPage> tuple = imageTabPages.Find(tp => tp.Item1 == alertImage);
            return tuple?.Item2 ?? null;
        }

        private TabPage TabExists(_TADAAlertAudio alertAudio)
        {
            Tuple<_TADAAlertAudio, TabPage> tuple = audioTabPages.Find(tp => tp.Item1 == alertAudio);
            return tuple?.Item2 ?? null;
        }

        private TabPage TabExists(_TADAAlertText alertText)
        {
            Tuple<_TADAAlertText, TabPage> tuple = textTabPages.Find(tp => tp.Item1 == alertText);
            return tuple?.Item2 ?? null;
        }

        /*
         * What am I trying to do?
         * 
         * When a user clicks 'New <whatever>', it needs to create a new instance in the appropriate BindingSource.
         * Then that new instance gets embedded in a TabPage and that TabPage gets added to the appropriate List<>.
         * 
         * When a user clicks 'Edit', it has to look for that item in the appropriate TabPage list.
         * If there, make that TabPage active.
         * If not, make a new instance and such.
         */
        private void CellEditClicked(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex >= 0 && e.RowIndex >= 0 && (sender as DataGridView).Columns[e.ColumnIndex] is DataGridViewButtonColumn)
            {
                DataGridView dataGrid = (sender as DataGridView);
                BindingSource bindingSource = dataGrid.DataSource as BindingSource;

                bool isNewObject = false;
                object editObject;

                TabPage existingTabPage = null;

                DataGridView dgView = (sender as DataGridView);
                if (dataGrid.Rows[e.RowIndex].IsNewRow)
                {
                    bindingSource.CancelEdit();
                    editObject = bindingSource.AddNew();
                    isNewObject = true;
                }
                else
                {
                    editObject = bindingSource.Current;
                }

                if (dataGrid == dg_Images)
                {
                    if (!isNewObject)
                    {
                        existingTabPage = TabExists(editObject as _TADAAlertImage);
                    }

                    if (existingTabPage == null || isNewObject)
                    {
                        // Make a new tab page.
                    }
                    else
                    {
                        (existingTabPage.Tag as Form).Select();
                    }
                }
/*
                else if (dataGrid == dg_Audio)
                {
                    if (this.currentAudioForm == null || this.currentAudioForm.IsDisposed)
                    {
                        this.currentAudioForm = new AudioCreationForm(editObject as TADAAlertAudio, isNewObject);
                    }
                    this.currentAudioForm.Show();
                }
                else if (dataGrid == dg_StreamerMessages)
                {
                    if (this.currentStreamerForm == null || this.currentStreamerForm.IsDisposed)
                    {
                        this.currentStreamerForm = new TextCreationForm(editObject as TADAAlertText, true, isNewObject);
                    }
                    this.currentStreamerForm.Show();
                }
                else if (dataGrid == dg_ViewerMessages)
                {
                    if (this.currentViewerForm == null || this.currentViewerForm.IsDisposed)
                    {
                        this.currentViewerForm = new TextCreationForm(editObject as TADAAlertText, false, isNewObject);
                    }
                    this.currentViewerForm.Show();
                }
*/
            }
        }

        private void ShowOrHideMdiTabs (object sender = null, EventArgs e = null)
        {
            tab_ChildMDIForms.Visible = (this.ActiveMdiChild == null);
        }

        private void AssignTags ()
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

            if (!tab_ChildMDIForms.Visible)
            {
                tab_ChildMDIForms.Visible = true;
            }
        }

        //private void NewAlertItem ()
        private void NewImageVideoForm(object sender, EventArgs e)
        {
            _TADAAlertImage newAlertImage = alertImageBindingSource.AddNew() as _TADAAlertImage;

            Form newImageForm = new MDI_Forms.Image_Designer_MDI(newAlertImage, true, null);
            newImageForm.MdiParent = this;
            newImageForm.Text = GetCaption("Image", alertImageBindingSource.Count - 1);
            newImageForm.Show();

            AssignTags();
        }

        private void EncapsulateWithTab(_TADAAlertImage alertImage, String caption, bool isNew)
        {
            //thisAlert;
            Form newImageForm = new MDI_Forms.Image_Designer_MDI(alertImage, isNew, this.mainAlert);
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

        private void SwitchTabPage (TabPage tabpage)
        {
            (tabpage.Tag as Form).Select();
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

        // The expectation here is that DrawWaypoints() is called from the time tracker-being-moved code, which first draws a black background rectangle.
        // The expectation is also that the images are drawn in the order "image, streamer text, viewer text".
        private void DrawWaypoints (WaypointContainer waypoints, SizeF unmodifiedSize, Bitmap itemToDraw, CurrentPosition currentPosition, Color lineColour)
        {
            if (waypoints.Waypoints.Count > 0)
            {
                float widthRatio = pb_Preview.Image.Width * 1.0f / MainTADAForm.mainTADAForm.AnimationWindow.ClientSize.Width;
                float heightRatio = pb_Preview.Image.Height * 1.0f / MainTADAForm.mainTADAForm.AnimationWindow.ClientSize.Height;

                PointF currentEndpoint = waypoints.Waypoints[0].P1;

                Graphics imageGraphics = Graphics.FromImage(previewImage);

                for (int currentWaypoint = 1; currentWaypoint < waypoints.Waypoints.Count; ++ currentWaypoint)
                {
                    Waypoint waypoint = waypoints.Waypoints[currentWaypoint];

                    // Every iteration we want a fresh P0 and P1 because they aren't compressed yet to fit our screen.
                    PointF P0, P1, C0, C1;

                    P0 = currentEndpoint;
                    P1 = waypoint.P1;
                    currentEndpoint = P1;

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
                    CompressPoint(ref P0, widthRatio, heightRatio);
                    CompressPoint(ref P1, widthRatio, heightRatio);
                    CompressPoint(ref C0, widthRatio, heightRatio);
                    CompressPoint(ref C1, widthRatio, heightRatio);

                    using (Pen p = new Pen(lineColour))
                    {
                        imageGraphics.DrawBezier(p, P0, C0, C1, P1);
                    }
                }

                if (currentPosition.actuallyDraw &&
                    itemToDraw != null)
                {
                    PointF modifiedLocation = currentPosition.location;

                    CompressPoint(ref modifiedLocation, widthRatio, heightRatio);

                    SizeF modifiedSize = new SizeF(unmodifiedSize.Width * widthRatio * currentPosition.magnification, unmodifiedSize.Height * heightRatio * currentPosition.magnification);

                    // Opacity stuff.  Technically doesn't need to be done if opacity == 100, but whatever.
                    System.Drawing.Imaging.ColorMatrix colorMatrix = new System.Drawing.Imaging.ColorMatrix()
                    {
                        Matrix33 = (float)(currentPosition.opacity / 100.0)
                    };

                    System.Drawing.Imaging.ImageAttributes imageAttributes = new System.Drawing.Imaging.ImageAttributes();
                    imageAttributes.SetColorMatrix(colorMatrix, System.Drawing.Imaging.ColorMatrixFlag.Default, System.Drawing.Imaging.ColorAdjustType.Bitmap);

                    imageGraphics.TranslateTransform(modifiedLocation.X + (currentPosition.aroundCenter ? modifiedSize.Width / 2 : 0.0f), modifiedLocation.Y + (currentPosition.aroundCenter ? modifiedSize.Height / 2 : 0.0f));
                    imageGraphics.RotateTransform(currentPosition.angle);
                    imageGraphics.DrawImage(itemToDraw,
                                            new Rectangle(0, 0, Convert.ToInt32(modifiedSize.Width), Convert.ToInt32(modifiedSize.Height)),
                                            0,
                                            0,
                                            itemToDraw.Width,
                                            itemToDraw.Height,
                                            GraphicsUnit.Pixel,
                                            imageAttributes);

                    imageGraphics.RotateTransform(-currentPosition.angle);
                    imageGraphics.TranslateTransform(-modifiedLocation.X - (currentPosition.aroundCenter ? modifiedSize.Width / 2 : 0.0f), -modifiedLocation.Y - (currentPosition.aroundCenter ? modifiedSize.Height / 2 : 0.0f));
                }

                pb_Preview.Image = previewImage;
                pb_Preview.Update();
            }
        }

        private void SendCoordinatesToChildForm(object sender, MouseEventArgs e)
        {
            if (this.ActiveMdiChild == null ||
                (!System.Windows.Input.Keyboard.Modifiers.HasFlag(System.Windows.Input.ModifierKeys.Shift) &&
                 !System.Windows.Input.Keyboard.Modifiers.HasFlag(System.Windows.Input.ModifierKeys.Control) ) )
            {
                // Then do nothing and bow out gracefully.
            }
            else
            {
                // Calculate position of e.Location (or of Control.PointToClient(Cursor.Position) multiplied by the X/Y ratios to get the point.
                float ratioX = MainTADAForm.mainTADAForm.AnimationWindow.Width / (float)pb_Preview.Width;
                float ratioY = MainTADAForm.mainTADAForm.AnimationWindow.Height / (float)pb_Preview.Height;

                PointF modifiedLocation = new PointF((float)(Math.Round(e.Location.X * ratioX, 2)), (float)(Math.Round(e.Location.Y * ratioY, 2)));
                
                (this.ActiveMdiChild as MDI_Forms.Waypoint_Designer_MDI).AddWaypointFromImage(modifiedLocation, System.Windows.Input.Keyboard.Modifiers.HasFlag(System.Windows.Input.ModifierKeys.Control));
            }
        }

        private double TrackbarTime()
        {
            return trk_Time.Value / 1000.0 * this.MaximumTime;
        }

        private void TrackbarPositionChanged (object sender, EventArgs e)
        {
            lbl_Time.Text = TrackbarTime().ToString();
            this.UpdatePreview();
        }

        public void UpdateTrackbar ()
        {
            double timestamp = TrackbarTime();
            double maximumTime = 0;

            foreach (_TADAAlertImage image in alertImageBindingSource)
            {
                maximumTime = Math.Max(maximumTime, image.Waypoints.TotalDuration);
            }

            foreach (_TADAAlertAudio audio in alertAudioBindingSource)
            {
                maximumTime = Math.Max(maximumTime, audio.Waypoints.TotalDuration);
                maximumTime = Math.Max(maximumTime, audio.Duration.TotalSeconds + audio.Delay.TotalSeconds);
            }

            foreach (_TADAAlertText text in streamerAlertTextBindingSource)
            {
                maximumTime = Math.Max(maximumTime, text.Waypoints.TotalDuration);
            }

            foreach (_TADAAlertText text in viewerAlertTextBindingSource)
            {
                maximumTime = Math.Max(maximumTime, text.Waypoints.TotalDuration);
            }

            this.MaximumTime = maximumTime;

            // And then set the tracker to that spot if possible.
            if (maximumTime > 0)
            {
                trk_Time.Value = Convert.ToInt32(Math.Min(trk_Time.Maximum, timestamp / maximumTime * 1000.0));
                lbl_Time.Text = TrackbarTime().ToString();
            }
        }

        public void UpdatePreview ()
        {
            UpdateTrackbar();

            // Update the preview image based on the child MDI forms that are being monitored.
            Graphics imageGraphics = Graphics.FromImage(previewImage);
            imageGraphics.FillRectangle(Brushes.Black, 0, 0, previewImage.Width, previewImage.Height);
            
            MDI_Forms.Waypoint_Designer_MDI activeTab = this.ActiveMdiChild as MDI_Forms.Waypoint_Designer_MDI;

            // Always monitor the active tab.  And know what type of tab is active.
            if (activeTab != null)
            {
                CurrentPosition currentValues = activeTab.CalculatePositionFromTime(TrackbarTime(), 0, 0);
                if (currentValues != null)
                {
                    DrawWaypoints(activeTab.GetWaypoints(), activeTab.PreviewItemSize, activeTab.PreviewBitmap, currentValues, COLOR_TAB);
                }
            }

            // Then, if not active, draw the selected image

            // Then, if not active, draw the selected Streamer Text

            // Then, if not active, draw the selected Viewer Text
        }

        private void btn_Test_Click(object sender, EventArgs e)
        {
            System.Xml.XmlWriterSettings xmlWriterSettings = new System.Xml.XmlWriterSettings
            {
                Encoding = Encoding.UTF8,
                Indent = true,
                NewLineOnAttributes = false
            };

            using (System.IO.FileStream fileStream = new System.IO.FileStream(@"D:\Temp\xmlfile.xml", System.IO.FileMode.Create))
            {
                System.Xml.XmlWriter xmlWriter = System.Xml.XmlWriter.Create(fileStream, xmlWriterSettings);
                xmlWriter.WriteStartDocument();
                foreach (_TADAAlertImage image in alertImageBindingSource) { image.WriteXml(xmlWriter); }
                xmlWriter.WriteEndDocument();
                xmlWriter.Flush();
                xmlWriter.Close();
            }
        }
    }
}
