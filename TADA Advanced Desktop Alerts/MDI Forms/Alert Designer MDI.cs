using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TADA_Advanced_Desktop_Alerts.MDI_Forms;

namespace TADA_Advanced_Desktop_Alerts.Creation_Forms
{
    public partial class Alert_Designer_MDI : Form
    {
        TADAAlert mainAlert;

        Bitmap previewImage;

        Color COLOR_TAB = Color.White;
        Color COLOR_IMAGE = Color.Yellow;
        Color COLOR_STREAMER_TEXT = Color.Green;
        Color COLOR_VIEWER_TEXT = Color.OrangeRed;

        private double MaximumTime;

        private List <Tuple <TADAAlertImage, TabPage>> imageTabPages = new List<Tuple<TADAAlertImage, TabPage>>();
        private List <Tuple <TADAAlertAudio, TabPage>> audioTabPages = new List<Tuple<TADAAlertAudio, TabPage>>();
        private List <Tuple <TADAAlertText, TabPage>> textTabPages = new List<Tuple<TADAAlertText, TabPage>>();
        private List<Tuple<TADAAlertCondition, TabPage>> conditionTabPages = new List<Tuple<TADAAlertCondition, TabPage>>();

        private Dictionary<string, string> ipaDictionary = MainTADAForm.mainTADAForm.IpaDictionary();
        
        internal Alert_Designer_MDI(TADAAlert originalAlert)
        {
            InitializeComponent();

            this.mainAlert = originalAlert;

            ConfigureMenuItems();

            FillAlertTypeComboBox();
            FillMacroKeysComboBox();

            this.MdiChildActivate += ShowOrHideMdiTabs;
            this.tab_ChildMDIForms.SelectedIndexChanged += SwitchTabPage;

            previewImage = new Bitmap(pb_Preview.Width, pb_Preview.Height);
            Graphics previewGraphics = Graphics.FromImage(previewImage);
            previewGraphics.FillRectangle(Brushes.Black, new Rectangle(new Point(0, 0), previewImage.Size));
            pb_Preview.Image = previewImage;
            pb_Preview.Update();

            pb_Preview.MouseUp += SendCoordinatesToChildForm;

            trk_Time.ValueChanged += TrackbarPositionChanged;

            cmb_AlertType.SelectedIndexChanged += UpdateAlertType;
            txt_AlertName.LostFocus += UpdateAlertName;

            dg_Images.CellContentClick += CellEditClicked;
            dg_Audio.CellContentClick += CellEditClicked;
            dg_StreamerMessages.CellContentClick += CellEditClicked;
            dg_ViewerMessages.CellContentClick += CellEditClicked;
            dg_Conditions.CellContentClick += CellEditClicked;

            dg_Images.UserDeletingRow += RowDeleted;
            dg_Audio.UserDeletingRow += RowDeleted;
            dg_StreamerMessages.UserDeletingRow += RowDeleted;
            dg_ViewerMessages.UserDeletingRow += RowDeleted;
            dg_Conditions.UserDeletingRow += RowDeleted;

            dg_Images.UserDeletingRow += UpdateAlertContents;
            dg_Audio.UserDeletingRow += UpdateAlertContents;
            dg_StreamerMessages.UserDeletingRow += UpdateAlertContents;
            dg_ViewerMessages.UserDeletingRow += UpdateAlertContents;
            dg_Conditions.UserDeletingRow += UpdateAlertContents;

            dg_Images.RowPostPaint += DrawRowNumber;
            dg_Audio.RowPostPaint += DrawRowNumber;
            dg_StreamerMessages.RowPostPaint += DrawRowNumber;
            dg_ViewerMessages.RowPostPaint += DrawRowNumber;
            dg_Conditions.RowPostPaint += DrawRowNumber;

            chk_PreviewImage.CheckedChanged += delegate { UpdatePreview(); };
            chk_PreviewStreamerText.CheckedChanged += delegate { UpdatePreview(); };
            chk_PreviewViewerText.CheckedChanged += delegate { UpdatePreview(); };
            numud_PreviewImage.ValueChanged += delegate { UpdatePreview(); };
            numud_PreviewStreamer.ValueChanged += delegate { UpdatePreview(); };
            numud_PreviewViewer.ValueChanged += delegate { UpdatePreview(); };

            mtxt_GlobalCooldown.TextChanged += UpdateCooldowns;
            mtxt_UserCooldown.TextChanged += UpdateCooldowns;
            
            foreach (TADAAlertImage image in this.mainAlert.Images)
            {
                this.alertImageBindingSource.Add(image);
            }

            foreach (TADAAlertAudio audio in this.mainAlert.Audios)
            {
                this.alertAudioBindingSource.Add(audio);
            }

            foreach (TADAAlertText text in this.mainAlert.StreamerTexts)
            {
                this.streamerAlertTextBindingSource.Add(text);
            }

            foreach (TADAAlertText text in this.mainAlert.ViewerTexts)
            {
                this.viewerAlertTextBindingSource.Add(text);
            }

            foreach (TADAAlertCondition condition in this.mainAlert.Conditions)
            {
                this.alertConditionBindingSource.Add(condition);
            }

            mtxt_GlobalCooldown.Text = General.StringFromTimeSpan(this.mainAlert.globalCooldown);
            mtxt_UserCooldown.Text = General.StringFromTimeSpan(this.mainAlert.userCooldown);

            btn_TestAlert.Click += TestAlert;

            UpdatePreview();
            UpdateAlertPieceCounts();

            this.cmb_AlertType.Text = this.mainAlert.AlertType;
            this.txt_AlertName.Text = this.mainAlert.AlertName;
        }

        private void ConfigureMenuItems ()
        {
            this.ts_NewImageVideo.Click += NewImageVideoForm;
            this.ts_NewStreamerAudio.Click += NewAudioForm;
            this.ts_NewStreamerText.Click += NewStreamerTextForm;
            this.ts_NewViewerText.Click += NewViewerTextForm;
            this.ts_NewCondition.Click += NewConditionForm;

            this.ts_Save.Click += UpdateAlertContents;
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

            General.ResizeDropdown(cmb_AlertType);
        }

        private void FillMacroKeysComboBox()
        {
            foreach (Keys key in Enum.GetValues(typeof(Keys)))
            {
                String keyname = Enum.GetName(typeof(Keys), key);

                if ((key & Keys.Modifiers) == 0 && key != Keys.KeyCode && !cmb_MacroKey.Items.Contains(keyname))
                {
                    cmb_MacroKey.Items.Add(keyname);
                }

            }

            cmb_MacroKey.DropDownWidth = cmb_MacroKey.Items.Cast<string>().Max(x => TextRenderer.MeasureText(x, cmb_MacroKey.Font).Width);
            cmb_MacroKey.Text = "None";
        }

        private void DrawRowNumber(object sender, DataGridViewRowPostPaintEventArgs e)
        {
            DataGridView dataGrid = sender as DataGridView;

            if (dataGrid.Rows[e.RowIndex].IsNewRow)
            {
                return;
            }

            String rowNumber = String.Format ("{0,5}  ", e.RowIndex + 1);

            StringFormat alignment = new StringFormat()
            {
                Alignment = StringAlignment.Far,
                LineAlignment = StringAlignment.Center
            };

            Rectangle headerBounds = new Rectangle(e.RowBounds.Left, e.RowBounds.Top, dataGrid.RowHeadersWidth, e.RowBounds.Height);
            e.Graphics.DrawString(rowNumber, this.Font, SystemBrushes.ControlText, headerBounds, alignment);
        }

        private void UpdateAlertContents (object sender, EventArgs e)
        {
            foreach (Tuple<TADAAlertImage, TabPage> t in imageTabPages)
            {
                ((t.Item2 as TabPage).Tag as MDI_Forms.Image_Designer_MDI).FillImportComboBox();
            }

            foreach (Tuple<TADAAlertAudio, TabPage> t in audioTabPages)
            {
                ((t.Item2 as TabPage).Tag as MDI_Forms.Audio_Designer_MDI).FillImportComboBox();
            }

            foreach (Tuple<TADAAlertText, TabPage> t in textTabPages)
            {
                ((t.Item2 as TabPage).Tag as MDI_Forms.Text_Designer_MDI).FillImportComboBox();
            }

            UpdateAlertPieceCounts();
        }

        private void UpdateCooldowns(object sender, EventArgs e)
        {
            if (!(sender as MaskedTextBox).Focused)
            {
                if (sender == mtxt_GlobalCooldown)
                {
                    this.mainAlert.globalCooldown = General.TimeSpanFromString(mtxt_GlobalCooldown.Text);
                }
                else if (sender == mtxt_UserCooldown)
                {
                    this.mainAlert.userCooldown = General.TimeSpanFromString(mtxt_UserCooldown.Text);
                }
            }
        }

        private void UpdateAlertPieceCounts ()
        {
            numud_PreviewImage.Maximum = this.mainAlert.ImageCount;
            numud_PreviewStreamer.Maximum = this.mainAlert.StreamerCount;
            numud_PreviewViewer.Maximum = this.mainAlert.ViewerCount;

            numud_TestAudio.Maximum = this.mainAlert.AudioCount;
            numud_TestImage.Maximum = this.mainAlert.ImageCount;
            numud_TestStreamerMessage.Maximum = this.mainAlert.StreamerCount;
            numud_TestViewerMessage.Maximum = this.mainAlert.ViewerCount;
        }

        private String GetCaption(String type, int number)
        {
            return String.Format("{0} {1}        ", type, number);
        }

        private void AddTab(TADAAlertImage alertImage, TabPage newTab)
        {
            imageTabPages.Add(new Tuple<TADAAlertImage, TabPage>(alertImage, newTab));
        }

        private void AddTab(TADAAlertAudio alertAudio, TabPage newTab)
        {
            audioTabPages.Add(new Tuple<TADAAlertAudio, TabPage>(alertAudio, newTab));
        }

        private void AddTab(TADAAlertText alertText, TabPage newTab)
        {
            textTabPages.Add(new Tuple<TADAAlertText, TabPage>(alertText, newTab));
        }

        private TabPage TabExists(TADAAlertImage alertImage)
        {
            Tuple<TADAAlertImage, TabPage> tuple = imageTabPages.Find(tp => tp.Item1 == alertImage);
            return tuple?.Item2 ?? null;
        }

        private TabPage TabExists(TADAAlertAudio alertAudio)
        {
            Tuple<TADAAlertAudio, TabPage> tuple = audioTabPages.Find(tp => tp.Item1 == alertAudio);
            return tuple?.Item2 ?? null;
        }

        private TabPage TabExists(TADAAlertText alertText)
        {
            Tuple<TADAAlertText, TabPage> tuple = textTabPages.Find(tp => tp.Item1 == alertText);
            return tuple?.Item2 ?? null;
        }

        private TabPage TabExists (TADAAlertCondition alertCondition)
        {
            Tuple<TADAAlertCondition, TabPage> tuple = conditionTabPages.Find(tp => tp.Item1 == alertCondition);
            return tuple?.Item2 ?? null;
        }

        private void UpdateAlertType (object sender, EventArgs e)
        {
            this.mainAlert.AlertType = cmb_AlertType.Text;

            if (cmb_AlertType.Text == "Macro")
            {
                grp_Macro.Visible = true;
                SaveMacroSettings(sender, e);
            }
            else
            {
                grp_Macro.Visible = false;
                DeleteMacroSettings();
            }

            foreach (Tuple <TADAAlertCondition, TabPage> tuple in conditionTabPages)
            {
                (tuple.Item2.Tag as MDI_Forms.Condition_Designer_MDI).AlertTypeOptions(General.alertStrings[this.mainAlert.AlertType]);
            }
        }

        private void UpdateAlertName (object sender, EventArgs e)
        {
            this.mainAlert.AlertName = txt_AlertName.Text;
        }

        private void SaveMacroSettings(object sender, EventArgs e)
        {
            this.mainAlert.macroKey.Ctrl = chk_MacroCtrl.Checked;
            this.mainAlert.macroKey.Alt = chk_MacroAlt.Checked;
            this.mainAlert.macroKey.Shift = chk_MacroShift.Checked;
            this.mainAlert.macroKey.Key = (Keys)(Enum.Parse(typeof(Keys), cmb_MacroKey.Text));
        }

        private void DeleteMacroSettings()
        {
            this.mainAlert.macroKey.Ctrl = false;
            this.mainAlert.macroKey.Alt = false;
            this.mainAlert.macroKey.Shift = false;
            this.mainAlert.macroKey.Key = Keys.None;
        }

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
                    if (isNewObject)
                    {
                        this.mainAlert.Images.Add(editObject as TADAAlertImage);
                    }
                    else
                    {
                        existingTabPage = TabExists(editObject as TADAAlertImage);
                    }

                    if (existingTabPage == null || isNewObject)
                    {
                        CreateMDIForm(editObject as TADAAlertImage, GetCaption("Image", e.RowIndex));
                    }
                }
                else if (dataGrid == dg_Audio)
                {
                    if (isNewObject)
                    {
                        this.mainAlert.Audios.Add(editObject as TADAAlertAudio);
                    }
                    else
                    {
                        existingTabPage = TabExists(editObject as TADAAlertAudio);
                    }

                    if (existingTabPage == null || isNewObject)
                    {
                        CreateMDIForm(editObject as TADAAlertAudio, GetCaption("Audio", e.RowIndex));
                    }
                }
                else if (dataGrid == dg_StreamerMessages || dataGrid == dg_ViewerMessages)
                {
                    if (isNewObject)
                    {
                        if (dataGrid == dg_StreamerMessages)
                        {
                            this.mainAlert.StreamerTexts.Add(editObject as TADAAlertText);
                        }
                        else
                        {
                            this.mainAlert.ViewerTexts.Add(editObject as TADAAlertText);
                        }
                    }
                    else
                    {
                        existingTabPage = TabExists(editObject as TADAAlertText);
                    }

                    if (existingTabPage == null || isNewObject)
                    {
                        Boolean isViewer = (dataGrid == dg_ViewerMessages);

                        CreateMDIForm(editObject as TADAAlertText, GetCaption((isViewer ? "Viewer" : "Streamer"), e.RowIndex), isViewer);
                    }
                }
                else if (dataGrid == dg_Conditions)
                {
                    if (isNewObject)
                    {
                        this.mainAlert.Conditions.Add(editObject as TADAAlertCondition);
                    }
                    else
                    {
                        existingTabPage = TabExists(editObject as TADAAlertCondition);
                    }

                    if (existingTabPage == null || isNewObject)
                    {
                        CreateMDIForm(editObject as TADAAlertCondition, GetCaption("Condition", e.RowIndex));
                    }
                }

                if (existingTabPage != null)
                {
                    SwitchTabPage(existingTabPage);
                }

                UpdateAlertContents(sender, e);
            }
        }

        private void RowDeleted (object sender, DataGridViewRowCancelEventArgs e)
        {
            if (sender == dg_Images)
            {
                this.mainAlert.Images.Remove(alertImageBindingSource[e.Row.Index] as TADAAlertImage);
            }
            else if (sender == dg_Audio)
            {
                this.mainAlert.Audios.Remove(alertAudioBindingSource[e.Row.Index] as TADAAlertAudio);
            }
            else if (sender == dg_StreamerMessages)
            {
                this.mainAlert.StreamerTexts.Remove(streamerAlertTextBindingSource[e.Row.Index] as TADAAlertText);
            }
            else if (sender == dg_ViewerMessages)
            {
                this.mainAlert.ViewerTexts.Remove(viewerAlertTextBindingSource[e.Row.Index] as TADAAlertText);
            }
            else if (sender == dg_Conditions)
            {
                this.mainAlert.Conditions.Remove(alertConditionBindingSource[e.Row.Index] as TADAAlertCondition);
            }

            if (sender != dg_Conditions)
            {
                UpdateAlertContents(sender, e);
            }
        }

        private void ShowOrHideMdiTabs (object sender = null, EventArgs e = null)
        {
            tab_ChildMDIForms.Visible = !(this.ActiveMdiChild == null);
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

        private TabPage EncapsulateWithTab(Form contents, String caption)
        {
            contents.MdiParent = this;
            contents.Text = caption;
            contents.Show();

            this.ActiveMdiChild.WindowState = FormWindowState.Maximized;
            TabPage newTabPage = new TabPage(caption);
            newTabPage.Tag = contents;
            newTabPage.Parent = tab_ChildMDIForms;
            tab_ChildMDIForms.SelectedTab = newTabPage;
            this.ActiveMdiChild.Tag = newTabPage;
            this.ActiveMdiChild.FormClosed += new FormClosedEventHandler(ChildFormClosed);

            tab_ChildMDIForms.Visible = true;
            return newTabPage;
        }

        private void NewImageVideoForm(object sender = null, EventArgs e = null)
        {
            TADAAlertImage newAlertImage = alertImageBindingSource.AddNew() as TADAAlertImage;
            this.mainAlert.Images.Add(newAlertImage);
            CreateMDIForm(newAlertImage, GetCaption("Image", alertImageBindingSource.Count - 1));
        }

        private void NewAudioForm(object sender = null, EventArgs e = null)
        {
            TADAAlertAudio newAlertAudio = alertAudioBindingSource.AddNew() as TADAAlertAudio;
            this.mainAlert.Audios.Add(newAlertAudio);
            CreateMDIForm(newAlertAudio, GetCaption("Audio", alertAudioBindingSource.Count - 1));
        }

private void NewStreamerTextForm (object sender = null, EventArgs e = null)
        {
            TADAAlertText newAlertText = streamerAlertTextBindingSource.AddNew() as TADAAlertText;
            this.mainAlert.StreamerTexts.Add(newAlertText);
            CreateMDIForm(newAlertText, GetCaption("Streamer", streamerAlertTextBindingSource.Count - 1), false);
        }

        private void NewViewerTextForm (object sender = null, EventArgs e = null)
        {
            TADAAlertText newAlertText = viewerAlertTextBindingSource.AddNew() as TADAAlertText;
            this.mainAlert.ViewerTexts.Add(newAlertText);
            CreateMDIForm(newAlertText, GetCaption("Viewer", viewerAlertTextBindingSource.Count - 1), true);
        }

        private void NewConditionForm (object sender = null, EventArgs e = null)
        {
            TADAAlertCondition newCondition = alertConditionBindingSource.AddNew() as TADAAlertCondition;
            this.mainAlert.Conditions.Add(newCondition);
            CreateMDIForm(newCondition, GetCaption("Condition", alertConditionBindingSource.Count - 1));
        }

        private void CreateMDIForm (TADAAlertImage newAlertImage, String caption)
        {
            Form newImageForm = new MDI_Forms.Image_Designer_MDI(newAlertImage, this.mainAlert);
            TabPage surroundingTabPage = EncapsulateWithTab(newImageForm, caption);
            imageTabPages.Add(new Tuple<TADAAlertImage, TabPage>(newAlertImage, surroundingTabPage));
        }

        private void CreateMDIForm (TADAAlertAudio newAlertAudio, String caption)
        {
            Form newAudioForm = new MDI_Forms.Audio_Designer_MDI(newAlertAudio, this.mainAlert);
            TabPage surroundingTabPage = EncapsulateWithTab(newAudioForm, caption);
            audioTabPages.Add(new Tuple<TADAAlertAudio, TabPage>(newAlertAudio, surroundingTabPage));
        }

        private void CreateMDIForm (TADAAlertText newAlertText, String caption, Boolean isViewerAlert = false)
        {
            Form newTextForm = new MDI_Forms.Text_Designer_MDI(newAlertText, this.mainAlert, isViewerAlert);
            TabPage surroundingTabPage = EncapsulateWithTab(newTextForm, caption);
            textTabPages.Add(new Tuple<TADAAlertText, TabPage>(newAlertText, surroundingTabPage));
        }

        private void CreateMDIForm (TADAAlertCondition newAlertCondition, String caption)
        {
            Form newCaptionForm = new MDI_Forms.Condition_Designer_MDI(newAlertCondition, this.mainAlert);
            TabPage surroundingTabPage = EncapsulateWithTab(newCaptionForm, caption);
            conditionTabPages.Add(new Tuple<TADAAlertCondition, TabPage>(newAlertCondition, surroundingTabPage));
            (newCaptionForm as MDI_Forms.Condition_Designer_MDI).AlertTypeOptions(General.alertStrings[cmb_AlertType.Text]);
        }

        private void ChildFormClosed (object sender, FormClosedEventArgs e)
        {
            if (sender is MDI_Forms.Image_Designer_MDI)
            {
                imageTabPages.Remove(imageTabPages.Find(itp => itp.Item2 == (sender as Form).Tag as TabPage));
            }
            else if (sender is MDI_Forms.Audio_Designer_MDI)
            {
                audioTabPages.Remove(audioTabPages.Find(itp => itp.Item2 == (sender as Form).Tag as TabPage));
            }
            else if (sender is MDI_Forms.Text_Designer_MDI)
            {
                textTabPages.Remove(textTabPages.Find(itp => itp.Item2 == (sender as Form).Tag as TabPage));
            }
            else if (sender is MDI_Forms.Condition_Designer_MDI)
            {
                conditionTabPages.Remove(conditionTabPages.Find(itp => itp.Item2 == (sender as Form).Tag as TabPage));
            }

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

            // Need to check to see if this is the active tab or not.

            if (closeButton.Contains(e.Location))
            {
                // -=-=-=-=-=-=-=-=-=-  This is the place where we're closing the form.  Save things first.
                var w = (this.tab_ChildMDIForms.SelectedTab.Tag) as Waypoint_Designer_MDI;

                if (w is not null)
                {
                    w.isClosing = true;
                }

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

                bool deleteImage = false;

                unmodifiedSize.Width = Math.Max(1, unmodifiedSize.Width);
                unmodifiedSize.Height = Math.Max(1, unmodifiedSize.Height);

                if (itemToDraw == null)
                {
                    deleteImage = true;
                    itemToDraw = new Bitmap(Convert.ToInt32(unmodifiedSize.Width), Convert.ToInt32(unmodifiedSize.Height));
                    Graphics graphics = Graphics.FromImage(itemToDraw);
                    using (SolidBrush b = new SolidBrush(lineColour))
                    {
                        graphics.FillRectangle(b, new Rectangle(0, 0, itemToDraw.Width, itemToDraw.Height));
                    }
                }

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

                if (deleteImage)
                {
                    itemToDraw.Dispose();
                }
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

            foreach (TADAAlertImage image in alertImageBindingSource)
            {
                maximumTime = Math.Max(maximumTime, image.Waypoints.TotalDuration);
            }

            foreach (TADAAlertAudio audio in alertAudioBindingSource)
            {
                maximumTime = Math.Max(maximumTime, audio.Waypoints.TotalDuration);
                maximumTime = Math.Max(maximumTime, audio.Duration.TotalSeconds + audio.Delay.TotalSeconds);
            }

            foreach (TADAAlertText text in streamerAlertTextBindingSource)
            {
                maximumTime = Math.Max(maximumTime, text.Waypoints.TotalDuration);
            }

            foreach (TADAAlertText text in viewerAlertTextBindingSource)
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

        private void UpdateTimes()
        {
            // Maximum time of alert is the maximum of all alert durations.
            // Minimum time of alert is the maximum time of all minimums across alert parts.
            double minImage = this.mainAlert.ImageCount == 0 ? 0 : this.mainAlert.Images.Min(image => image.TotalDuration);
            double maxImage = this.mainAlert.ImageCount == 0 ? 0 : this.mainAlert.Images.Max(image => image.TotalDuration);
            double minAudio = this.mainAlert.AudioCount == 0 ? 0 : this.mainAlert.Audios.Min(audio => audio.TotalDuration);
            double maxAudio = this.mainAlert.AudioCount == 0 ? 0 : this.mainAlert.Audios.Max(audio => audio.TotalDuration);
            double minStreamerText = this.mainAlert.StreamerCount == 0 ? 0 : this.mainAlert.StreamerTexts.Min(text => text.TotalDuration);
            double maxStreamerText = this.mainAlert.StreamerCount == 0 ? 0 : this.mainAlert.StreamerTexts.Max(text => text.TotalDuration);
            double minViewerText = this.mainAlert.ViewerCount == 0 ? 0 : this.mainAlert.ViewerTexts.Min(text => text.TotalDuration);
            double maxViewerText = this.mainAlert.ViewerCount == 0 ? 0 : this.mainAlert.ViewerTexts.Max(text => text.TotalDuration);

            lbl_MaximumDurationCount.Text = String.Format("{0:0.0} s", new double[] { maxImage, maxAudio, maxStreamerText, maxViewerText }.Max());
            lbl_MinimumDurationCount.Text = String.Format("{0:0.0} s", new double[] { minImage, minAudio, minStreamerText, minViewerText }.Max());
            lbl_CombinationsCount.Text = String.Format("{0}", this.mainAlert.Combinations);
        }

        private void TestAlert (object sender, EventArgs e)
        {
            // Make an alert based on the items selected on the pane, send them to the window to test.
            TADAAlertImage image = null;
            TADAAlertAudio audio = null;
            TADAAlertText text = null;
            TADAAlertText response = null;

            SubstitutionObject subObject = new SubstitutionObject()
            {
                AlertType = cmb_AlertType.Text,
                Amount = Convert.ToInt32(numud_Amount.Value),
                Message = txt_ViewerMessage.Text,
                RecipientName = txt_RecipientName.Text,
                TierName = txt_SubTierName.Text,
                ViewerName = txt_ViewerName.Text
            };

            subObject.SetPlan(TierAndPlanConverter.StringToSubscriptionPlan(dom_SubTier.Text));

            if (numud_TestImage.Value > 0)
            {
                image = alertImageBindingSource[Convert.ToInt32(numud_TestImage.Value - 1)] as TADAAlertImage;
            }

            if (numud_TestAudio.Value > 0)
            {
                audio = alertAudioBindingSource[Convert.ToInt32(numud_TestAudio.Value - 1)] as TADAAlertAudio;
            }

            if (numud_TestStreamerMessage.Value > 0)
            {
                text = streamerAlertTextBindingSource[Convert.ToInt32(numud_TestStreamerMessage.Value - 1)] as TADAAlertText;

                text.SetMessage(text.Message);
                text.SetReadMessage(text.ReadMessage);
                text.SetVoicedMessage(General.PerformTTSSubstitutions(text.ReadMessage, subObject, ipaDictionary));
            }

            if (numud_TestViewerMessage.Value > 0)
            {
                response = viewerAlertTextBindingSource[Convert.ToInt32(numud_TestViewerMessage.Value - 1)] as TADAAlertText;

                response.SetMessage(txt_ViewerMessage.Text);
                response.SetReadMessage(txt_ViewerMessage.Text);
                response.SetVoicedMessage(General.PerformTTSSubstitutions(txt_ViewerMessage.Text, subObject, ipaDictionary));
            }

            PlayingAlert testAlert = new PlayingAlert(image, audio, text, response, subObject);
            MainTADAForm.mainTADAForm.AnimationWindow.AddPlayingAlert(testAlert, true);
        }


        // Update the preview image based on the child MDI forms that are being monitored.
        public void UpdatePreview ()
        {
            UpdateTrackbar();
            UpdateTimes();

            Graphics imageGraphics = Graphics.FromImage(previewImage);
            imageGraphics.FillRectangle(Brushes.Black, 0, 0, previewImage.Width, previewImage.Height);

            // Always monitor the active tab.  And know what type of tab is active.
            MDI_Forms.Waypoint_Designer_MDI activeTab = this.ActiveMdiChild as MDI_Forms.Waypoint_Designer_MDI;

            // If not active, draw the selected image
            if (!(activeTab is MDI_Forms.Image_Designer_MDI) && chk_PreviewImage.Checked)
            {
                int index = Convert.ToInt32(numud_PreviewImage.Value) - 1;
                if (index >= 0)
                {
                    TADAAlertImage alertImage = this.mainAlert.Images[index];
                    WaypointContainer waypointContainer = alertImage.Waypoints;

                    CurrentPosition currentPosition = waypointContainer.CalculatePositionFromTime(TrackbarTime());

                    if (currentPosition != null)
                    {
                        DrawWaypoints(waypointContainer, alertImage.Dimensions, null, currentPosition, COLOR_IMAGE);
                    }
                }
            }

            // Then, if not active, draw the selected Streamer Text
            if (!(activeTab is MDI_Forms.Text_Designer_MDI && this.mainAlert.StreamerTexts.Contains((activeTab as MDI_Forms.Text_Designer_MDI).alertText)) && chk_PreviewStreamerText.Checked)
            {
                int index = Convert.ToInt32(numud_PreviewStreamer.Value) - 1;
                if (index >= 0)
                {
                    TADAAlertText alertText = this.mainAlert.StreamerTexts[index];
                    WaypointContainer waypointContainer = alertText.Waypoints;

                    CurrentPosition currentPosition = waypointContainer.CalculatePositionFromTime(TrackbarTime());

                    if (currentPosition != null)
                    {
                        DrawWaypoints(waypointContainer, alertText.Dimensions, null, currentPosition, COLOR_STREAMER_TEXT);
                    }
                }
            }

            // Then, if not active, draw the selected Viewer Text
            if (!(activeTab is MDI_Forms.Text_Designer_MDI && this.mainAlert.ViewerTexts.Contains((activeTab as MDI_Forms.Text_Designer_MDI).alertText)) && chk_PreviewViewerText.Checked)
            {
                int index = Convert.ToInt32(numud_PreviewViewer.Value) - 1;
                if (index >= 0)
                {
                    TADAAlertText alertText = this.mainAlert.StreamerTexts[index];
                    WaypointContainer waypointContainer = alertText.Waypoints;

                    CurrentPosition currentPosition = waypointContainer.CalculatePositionFromTime(TrackbarTime());

                    if (currentPosition != null)
                    {
                        DrawWaypoints(waypointContainer, alertText.Dimensions, null, currentPosition, COLOR_VIEWER_TEXT);
                    }
                }
            }
            
            // Finally, so that it is on top, draw the active item.
            if (activeTab != null)
            {
                CurrentPosition currentPosition = activeTab.waypointContainer.CalculatePositionFromTime(TrackbarTime());
                if (currentPosition != null)
                {
                    DrawWaypoints(activeTab.GetWaypoints(), activeTab.PreviewItemSize, activeTab.PreviewBitmap, currentPosition, COLOR_TAB);
                }
            }
        }
    }
}
