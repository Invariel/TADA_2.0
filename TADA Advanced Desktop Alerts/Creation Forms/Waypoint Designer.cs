using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TADA_Advanced_Desktop_Alerts.Creation_Forms
{
    public partial class MainWaypointForm : Form
    {
        private double MIN_SAFE_RESOLUTION = 15.0;

        private bool disableUpdates;

        private Bitmap previewImage;

        private int drawWaypoint = 0;

        private float drawAngle = 0,
                      drawMagnification = 1,
                      drawOpacity = 1;

        private double drawTime;

        private PointF drawPosition = new PointF(0, 0);

        protected double totalTime = 0;

        protected SizeF itemSize = new SizeF(225, 150);
        protected SizeF animationWindowSize = new SizeF(1280, 1024);

        protected TADAAnimationWindow animationWindow;

        public MainWaypointForm()
        {
            InitializeComponent();
            ConfigureToolTips();

            previewImage = new Bitmap(pb_Preview.Width, pb_Preview.Height);
            pb_Preview.Image = previewImage;

            link_Keypress.LinkClicked += KeyCombinationHelp;
            dg_Waypoints.SelectionChanged += UpdateAdvancedWaypoint;
            dg_Waypoints.DefaultValuesNeeded += DefaultRowValues;
            dg_Waypoints.EditingControlShowing += NumericInputsOnly;
            dg_Waypoints.CurrentCellDirtyStateChanged += PresetValues;

            radio_Curve.CheckedChanged += SaveVariableState;
            radio_Line.CheckedChanged += SaveVariableState;

            numud_P0X.ValueChanged += SaveVariableState;
            numud_P0Y.ValueChanged += SaveVariableState;
            numud_C0X.ValueChanged += SaveVariableState;
            numud_C0Y.ValueChanged += SaveVariableState;
            numud_C1X.ValueChanged += SaveVariableState;
            numud_C1Y.ValueChanged += SaveVariableState;
            numud_P1X.ValueChanged += SaveVariableState;
            numud_P1Y.ValueChanged += SaveVariableState;

            chk_LeaveScreen.CheckedChanged += SaveVariableState;
            chk_BehindImage.CheckedChanged += SaveVariableState;
            chk_ReadMessage.CheckedChanged += SaveVariableState;

            chk_ChangeAngle.CheckedChanged += SaveVariableState;
            chk_ChangeMagnification.CheckedChanged += SaveVariableState;
            chk_ChangeOpacity.CheckedChanged += SaveVariableState;
            chk_ChangeBalance.CheckedChanged += SaveVariableState;
            chk_ChangeVolume.CheckedChanged += SaveVariableState;

            radio_AngleCenter.CheckedChanged += SaveVariableState;
            radio_AngleUpperLeft.CheckedChanged += SaveVariableState;
            radio_MagnifyCenter.CheckedChanged += SaveVariableState;
            radio_MagnifyUpperLeft.CheckedChanged += SaveVariableState;

            numud_TargetAngle.ValueChanged += SaveVariableState;
            numud_FramesAngle.ValueChanged += SaveVariableState;
            numud_DurationAngle.ValueChanged += SaveVariableState;

            numud_TargetMagnification.ValueChanged += SaveVariableState;
            numud_FramesMagnification.ValueChanged += SaveVariableState;
            numud_DurationMagnification.ValueChanged += SaveVariableState;

            numud_TargetOpacity.ValueChanged += SaveVariableState;
            numud_FramesOpacity.ValueChanged += SaveVariableState;
            numud_DurationOpacity.ValueChanged += SaveVariableState;

            numud_TargetBalance.ValueChanged += SaveVariableState;
            numud_FramesBalance.ValueChanged += SaveVariableState;
            numud_DurationBalance.ValueChanged += SaveVariableState;

            numud_TargetVolume.ValueChanged += SaveVariableState;
            numud_FramesVolume.ValueChanged += SaveVariableState;
            numud_DurationVolume.ValueChanged += SaveVariableState;

            txt_WindowTarget.TextChanged += SaveVariableState;
            txt_Keypresses.TextChanged += SaveVariableState;

            trk_Time.ValueChanged += UpdateTime;
            numud_Waypoints.ValueChanged += UpdateTime;

            btn_CopyWaypoints.Click += btn_CopyWaypoints_Click;
            btn_ShiftWaypoints.Click += btn_ShiftWaypoints_Click;
        }

        private void ConfigureToolTips()
        {
            General.ConfigureToolTip(grp_AdvancedWaypoint, "Advanced controls for the selected Waypoint.");
            General.ConfigureToolTip(grp_Coordinates, "This section lets you choose whether to have your item move in a line or along a curve, and where the control points for that curve are.");
            General.ConfigureToolTip(grp_Macro, "This section lets you define the window you want to send keypresses to once this Waypoint has completed.");
            General.ConfigureToolTip(grp_Preview, "This gives you a preview of how your item will move across the screen.  If you last used the Advanced Waypoints window, it will give you a preview of just that Waypoint.");

            General.ConfigureToolTip(tab_Angle, "This tab lets you rotate the item to the desired angle in the desired time with the chosen resolution.  This will not work for movie files.");
            General.ConfigureToolTip(tab_Magnification, "This tab lets you magnify (or shrink) the item to the desired size.  Magnification levels are all relative the the item's original size.");
            General.ConfigureToolTip(tab_Opacity, "This tab lets you choose how transparent or opaque your item should become.  This will not work for movie files.");
            General.ConfigureToolTip(tab_Balance, "This tab lets you shift the audio to the left (-1), right (1), or back to center (0) while this item plays.");
            General.ConfigureToolTip(tab_Volume, "This tab lets you change the volume dynamically during this waypoint.");
            General.ConfigureToolTip(tab_Options, "This tab lets you set additional options: whether to display behind the image, to leave the screen completely when moving to the edge, or to read text messages after this Waypoint.");

            General.ConfigureToolTip(lbl_ControlPoint1, "The coordinates for the first control point that defines the curve this item will follow.");
            General.ConfigureToolTip(lbl_ControlPoint2, "The coordinates for the second control point that defines the curve this item will follow.");
            General.ConfigureToolTip(lbl_Destination, "The coordinates for the destination of this Waypoint.");
            General.ConfigureToolTip(lbl_Initial, "The initial point for this Waypoint, which is the destination point of the previous Waypoint.");

            General.ConfigureToolTip(new List<Control>() { lbl_Keypresses, txt_Keypresses }, "The combination of keys to send to the selected window.");
            General.ConfigureToolTip(new List<Control>() { lbl_WindowTarget, txt_WindowTarget }, "The window to direct the keypress combination to.");

            General.ConfigureToolTip(new List<Control>() { numud_P0X, numud_P0Y }, "The coordinates from the previous Waypoint.");
            General.ConfigureToolTip(new List<Control>() { numud_P1X, numud_P1Y }, "The coordinates of the destination for this item.");
            General.ConfigureToolTip(new List<Control>() { numud_C0X, numud_C0Y }, "The coordinates of the first control point defining the curve.");
            General.ConfigureToolTip(new List<Control>() { numud_C1X, numud_C1Y }, "The coordinates of the second control point defining the curve.");

            General.ConfigureToolTip(chk_ChangeAngle, "If checked, this Waypoint also defines a rotation.");
            General.ConfigureToolTip(numud_TargetAngle, "The desired angle for the item to reach starting from its current angle.  After reaching the target, the item's angle will be redefined to 0 <= angle < 360.");
            General.ConfigureToolTip(numud_FramesAngle, "The number of animation frames rotating this item will take.");
            General.ConfigureToolTip(numud_DurationAngle, "The amount of time to take for this rotation to complete.");
            General.ConfigureToolTip(radio_AngleCenter, "If checked, this rotation is around the center of your item.");
            General.ConfigureToolTip(radio_AngleUpperLeft, "If checked, this rotation is around the item's upper-left corner.  This is the default behaviour.");
            
            General.ConfigureToolTip(chk_ChangeMagnification, "If checked, this Waypoint also defines a change in magnification.");
            General.ConfigureToolTip(numud_TargetMagnification, "The desired magnification for the item to reach, starting from its current state, but relative to its original size.");
            General.ConfigureToolTip(numud_FramesMagnification, "The number of animation frames changing this item's size will take.");
            General.ConfigureToolTip(numud_DurationMagnification, "The amount of time for this change in magnification to complete.");
            General.ConfigureToolTip(radio_MagnifyCenter, "When this feature is enabled, your item will expand and shrink from the center out.");
            General.ConfigureToolTip(radio_MagnifyUpperLeft, "When this feature is enabled, your item will expand and shrink down and right.  This is the default behaviour.");

            General.ConfigureToolTip(chk_ChangeOpacity, "If checked, this Waypoint also defines a change in opacity.");
            General.ConfigureToolTip(numud_TargetOpacity, "The desired level of opacity for the item to reach, starting from its current state.");
            General.ConfigureToolTip(numud_FramesOpacity, "The number of animation frames changing this item's opacity will take.");
            General.ConfigureToolTip(numud_DurationOpacity, "The amount of time for this change in opacity to complete.");

            General.ConfigureToolTip(chk_ChangeBalance, "If checked, this Waypoint also defines a change in audio balance.");
            General.ConfigureToolTip(numud_TargetBalance, "The desired audio balance to reach, starting from its current state, where -1 is left, 0 is center, and 1 is right.");
            General.ConfigureToolTip(numud_FramesBalance, "The number of times that the balance will change before reaching the desired audio balance.");
            General.ConfigureToolTip(numud_DurationBalance, "The amount of time for this change in audio balance to complete.");

            General.ConfigureToolTip(chk_ChangeVolume, "If checked, this Waypoint also defines a change in audio volume.");
            General.ConfigureToolTip(numud_TargetVolume, "The desired volume to reach, starting from its current state.");
            General.ConfigureToolTip(numud_FramesVolume, "The number of times that the volume will change before reaching the desired volume.");
            General.ConfigureToolTip(numud_DurationVolume, "The amount of time for this change in audio volume to complete.");

            General.ConfigureToolTip(btn_CopyWaypoints, "Copy the selected rows in the order that they were selected.  This will copy all of the associated Waypoint data.");
            General.ConfigureToolTip(btn_ShiftWaypoints, "Shift the selected rows in the horizontal and vertical directions as specified.  This will NOT affect control points.");
        }

        private void SaveVariableState(object sender, EventArgs e)
        {
            if (!disableUpdates)
            {
                BindingSource bindingSource = dg_Waypoints.DataSource as BindingSource;
                Waypoint w = bindingSource.Current as Waypoint;

                if (bindingSource.Position == 0)
                {
                    w.X = (float)(numud_P0X.Value);
                    w.Y = (float)(numud_P0Y.Value);
                }
                else
                {
                    w.X = (float)(numud_P1X.Value);
                    w.Y = (float)(numud_P1Y.Value);
                }

                w.IsCurve = radio_Curve.Checked;

                if (radio_Curve.Checked)
                {
                    w.C0 = new PointF((float)(numud_C0X.Value), (float)(numud_C0Y.Value));
                    w.C1 = new PointF((float)(numud_C1X.Value), (float)(numud_C1Y.Value));
                }

                w.LeaveScreen = chk_LeaveScreen.Checked;
                w.BehindImage = chk_BehindImage.Checked;
                w.ReadMessage = chk_ReadMessage.Checked;

                w.Angle.change = chk_ChangeAngle.Checked;
                w.Angle.duration = Convert.ToDouble(numud_DurationAngle.Value);
                w.Angle.frames = Convert.ToInt32(numud_FramesAngle.Value);
                w.Angle.target = Convert.ToDouble(numud_TargetAngle.Value);
                w.Angle.aroundCenter = radio_AngleCenter.Checked;

                w.Magnification.change = chk_ChangeMagnification.Checked;
                w.Magnification.duration = Convert.ToDouble(numud_DurationMagnification.Value);
                w.Magnification.frames = Convert.ToInt32(numud_FramesMagnification.Value);
                w.Magnification.target = Convert.ToDouble(numud_TargetMagnification.Value);
                w.Magnification.aroundCenter = radio_MagnifyCenter.Checked;

                w.Opacity.change = chk_ChangeOpacity.Checked;
                w.Opacity.duration = Convert.ToDouble(numud_DurationOpacity.Value);
                w.Opacity.frames = Convert.ToInt32(numud_FramesOpacity.Value);
                w.Opacity.target = Convert.ToDouble(numud_TargetOpacity.Value);

                w.Balance.change = chk_ChangeBalance.Checked;
                w.Balance.duration = Convert.ToDouble(numud_DurationBalance.Value);
                w.Balance.frames = Convert.ToInt32(numud_FramesBalance.Value);
                w.Balance.target = Convert.ToDouble(numud_TargetBalance.Value);

                w.Volume.change = chk_ChangeVolume.Checked;
                w.Volume.duration = Convert.ToDouble(numud_DurationVolume.Value);
                w.Volume.frames = Convert.ToInt32(numud_FramesVolume.Value);
                w.Volume.target = Convert.ToDouble(numud_TargetVolume.Value);

                w.WindowTarget = txt_WindowTarget.Text;
                w.Keypresses = txt_Keypresses.Text;

                UpdatePreview();
                CheckChecked();

                Tuple<Effect, String>[] tuples = new Tuple<Effect, String>[]
                {
                    new Tuple<Effect, String> (w.Angle, "angle"),
                    new Tuple<Effect, String> (w.Magnification, "magnification"),
                    new Tuple<Effect, String> (w.Opacity, "opacity"),
                    new Tuple<Effect, String> (w.Balance, "balance"),
                    new Tuple<Effect, String> (w.Volume, "volume")
                };

                foreach (Tuple<Effect, String> tuple in tuples)
                {
                    double time = tuple.Item1.duration * 1000 / tuple.Item1.frames;
                    if (tuple.Item1.change && time < MIN_SAFE_RESOLUTION && time > 0)
                    {
                        MessageBox.Show(null,
                                        String.Format("Your {0} effect may take longer than desired because {1:F3} is lower than the safe {2} ms resolution.",
                                                     tuple.Item2,
                                                     time,
                                                     MIN_SAFE_RESOLUTION),
                                        "May Take Longer Than Desired",
                                        MessageBoxButtons.OK,
                                        MessageBoxIcon.Warning);
                    }
                }
            }
        }

        private void CheckChecked()
        {
            numud_TargetAngle.Enabled = chk_ChangeAngle.Checked;
            numud_FramesAngle.Enabled = chk_ChangeAngle.Checked;
            numud_DurationAngle.Enabled = chk_ChangeAngle.Checked;

            numud_TargetMagnification.Enabled = chk_ChangeMagnification.Checked;
            numud_FramesMagnification.Enabled = chk_ChangeMagnification.Checked;
            numud_DurationMagnification.Enabled = chk_ChangeMagnification.Checked;

            numud_TargetOpacity.Enabled = chk_ChangeOpacity.Checked;
            numud_FramesOpacity.Enabled = chk_ChangeOpacity.Checked;
            numud_DurationOpacity.Enabled = chk_ChangeOpacity.Checked;

            numud_TargetBalance.Enabled = chk_ChangeBalance.Checked;
            numud_FramesBalance.Enabled = chk_ChangeBalance.Checked;
            numud_DurationBalance.Enabled = chk_ChangeBalance.Checked;

            numud_TargetVolume.Enabled = chk_ChangeVolume.Checked;
            numud_FramesVolume.Enabled = chk_ChangeVolume.Checked;
            numud_DurationVolume.Enabled = chk_ChangeVolume.Checked;
        }
        
        private void DefaultRowValues (object sender, DataGridViewRowEventArgs e)
        {
            e.Row.Cells["Presets"].Value = "Custom";
            e.Row.Cells["xDataGridViewTextBoxColumn"].Value = 0;
            e.Row.Cells["yDataGridViewTextBoxColumn"].Value = 0;
            e.Row.Cells["TransitionTimeDataGridViewTextBoxColumn"].Value = 0;
            e.Row.Cells["persistTimeDataGridViewTextBoxColumn"].Value = 0;
            e.Row.HeaderCell.Value = (e.Row.Index + 1).ToString();
            dg_Waypoints.AutoResizeRowHeadersWidth(DataGridViewRowHeadersWidthSizeMode.AutoSizeToAllHeaders);
        }

        private void NumericInputsOnly (object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            if (!disableUpdates)
            {
                DataGridViewCell cell = (sender as DataGridView).CurrentCell;
                if (cell.ColumnIndex != (sender as DataGridView).CurrentRow.Cells["Presets"].ColumnIndex)
                {
                    e.Control.KeyPress -= NumericCellKeyPress;

                    TextBox textBox = (e.Control as TextBox);
                    if (textBox != null)
                    {
                        textBox.KeyPress += NumericCellKeyPress;
                    }
                }
            }
        }

        private void NumericCellKeyPress(object sender, KeyPressEventArgs e)
        {
            TextBox textBox = (sender as TextBox);

            System.Globalization.NumberFormatInfo nfi = System.Threading.Thread.CurrentThread.CurrentCulture.NumberFormat;
            char decimalSeparator = nfi.CurrencyDecimalSeparator[0];

            if (char.IsDigit(e.KeyChar)) // If it's a digit, do the thing that makes sense - let them type it.
            {
                e.Handled = false;
            }
            else if (e.KeyChar.Equals('-')) // If it's a minus sign, that needs to be at the start of the value and only one can occur.
            {
                if (textBox.SelectionStart != 0 || textBox.Text.Contains('-'))
                {
                    e.Handled = true;
                }
            }
            else if (e.KeyChar.Equals(decimalSeparator)) // If it's the decimal separator character, only let there be one.  And prepend a 0 if necessary.
            {
                if (textBox.Text.Contains(decimalSeparator))
                {
                    e.Handled = true;
                }
                else if (textBox.Text.Length == 0)
                {
                    textBox.Text = "0.";
                    textBox.SelectionStart = textBox.Text.Length;
                    e.Handled = true;
                }
                else
                {
                    e.Handled = false;
                }
            }
            else
            {
                e.Handled = (!char.IsControl(e.KeyChar));
            }
        }

        private void PresetValues(object sender, EventArgs e)
        {
            DataGridView dataGridView = (sender as DataGridView);

            if (dataGridView.CurrentCell.OwningColumn.Name == "Presets")
            {
                float fullX = this.animationWindowSize.Width - this.itemSize.Width;
                float fullY = this.animationWindowSize.Height - this.itemSize.Height;
                float midX = fullX / 2;
                float midY = fullY / 2;

                DataGridViewRow row = dataGridView.CurrentCell.OwningRow;

                // Commit the edit, so that it updates the cell and lets the rest of this work.
                dataGridView.CommitEdit(DataGridViewDataErrorContexts.Commit);

                this.disableUpdates = true;

                switch (dataGridView.CurrentCell.Value)
                {
                    case "Upper Left":
                        row.Cells["xDataGridViewTextBoxColumn"].Value = 0;
                        row.Cells["yDataGridViewTextBoxColumn"].Value = 0;
                    break;
                    case "Upper Center":
                        row.Cells["xDataGridViewTextBoxColumn"].Value = midX;
                        row.Cells["yDataGridViewTextBoxColumn"].Value = 0;
                    break;
                    case "Upper Right":
                        row.Cells["xDataGridViewTextBoxColumn"].Value = fullX;
                        row.Cells["yDataGridViewTextBoxColumn"].Value = 0;
                    break;
                    case "Middle Left":
                        row.Cells["xDataGridViewTextBoxColumn"].Value = 0;
                        row.Cells["yDataGridViewTextBoxColumn"].Value = midY;
                    break;
                    case "Middle Center":
                        row.Cells["xDataGridViewTextBoxColumn"].Value = midX;
                        row.Cells["yDataGridViewTextBoxColumn"].Value = midY;
                    break;
                    case "Middle Right":
                        row.Cells["xDataGridViewTextBoxColumn"].Value = fullX;
                        row.Cells["yDataGridViewTextBoxColumn"].Value = midY;
                    break;
                    case "Bottom Left":
                        row.Cells["xDataGridViewTextBoxColumn"].Value = 0;
                        row.Cells["yDataGridViewTextBoxColumn"].Value = fullY;
                    break;
                    case "Bottom Center":
                        row.Cells["xDataGridViewTextBoxColumn"].Value = midX;
                        row.Cells["yDataGridViewTextBoxColumn"].Value = fullY;
                    break;
                    case "Bottom Right":
                        row.Cells["xDataGridViewTextBoxColumn"].Value = fullX;
                        row.Cells["yDataGridViewTextBoxColumn"].Value = fullY;
                    break;
                    default:
                    break;
                }

                this.disableUpdates = false;
            }
        }

        private void FinishedEditingCell(object sender, EventArgs e)
        {
            DataGridView dataGridView = (sender as DataGridView);
            
            if (!this.disableUpdates && (dataGridView.CurrentCell.OwningColumn.Name == "xDataGridViewTextBoxColumn" ||
                                         dataGridView.CurrentCell.OwningColumn.Name == "yDataGridViewTextBoxColumn"))
            {
                dataGridView.CurrentCell.OwningRow.Cells["Preset"].Value = "Custom";
            }
        }

        private void UpdateTime(object sender, EventArgs e)
        {
            BindingSource bindingSource = dg_Waypoints.DataSource as BindingSource;

            double currentTime = trk_Time.Value / 1000.0 * totalTime;

            double summedTime = 0;

            double prevAngle = 0, nextAngle = 0, elapsedAngle = 0, durationAngle = 0;
            double prevMagnification = 1, nextMagnification = 1, elapsedMagnification = 0, durationMagnification = 0;
            double prevOpacity = 100, nextOpacity = 100, elapsedOpacity = 0, durationOpacity = 0;

            drawAngle = 0;
            drawMagnification = 1;
            drawOpacity = 100;

            // Update the label.
            lbl_Time.Text = String.Format("{0:F2} s", currentTime);

            drawWaypoint = -1;

            // Do the heavy lifting to figure out which Waypoint this is in, and update the previous values accordingly.
            foreach (Waypoint w in bindingSource)
            {
                if (summedTime < currentTime)
                {
                    ++drawWaypoint;

                    double waypointDuration = Math.Min(w.TransitionTime + w.PersistTime, currentTime - summedTime);
                    summedTime += waypointDuration;

                    if (elapsedAngle < durationAngle)
                    {
                        if (!w.Angle.change) { elapsedAngle = Math.Min(elapsedAngle + waypointDuration, durationAngle); }
                        else
                        {
                            if (waypointDuration >= w.Angle.duration) { prevAngle = NormalizeAngle(w.Angle.target); nextAngle = prevAngle; elapsedAngle = 0; durationAngle = 0; }
                            else { prevAngle = NormalizeAngle(drawAngle); nextAngle = w.Angle.target; elapsedAngle = waypointDuration; durationAngle = w.Angle.duration; }
                        }
                    }
                    else if (w.Angle.change)
                    {
                        prevAngle = NormalizeAngle(drawAngle);
                        nextAngle = w.Angle.target;
                        durationAngle = w.Angle.duration;
                        elapsedAngle = Math.Min(durationAngle, waypointDuration);
                    }

                    if (elapsedMagnification < durationMagnification)
                    {
                        if (!w.Magnification.change) { elapsedMagnification = Math.Min(elapsedMagnification + waypointDuration, durationMagnification); }
                        else
                        {
                            if (waypointDuration >= w.Magnification.duration) { prevMagnification = w.Magnification.target; nextMagnification = prevMagnification; elapsedMagnification = 0; durationMagnification = 0; }
                            else { prevMagnification = drawMagnification; nextMagnification = w.Magnification.target; elapsedMagnification = waypointDuration; durationMagnification = w.Magnification.duration; }
                        }
                    }
                    else if (w.Magnification.change)
                    {
                        prevMagnification = drawMagnification;
                        nextMagnification = w.Magnification.target;
                        durationMagnification = w.Magnification.duration;
                        elapsedMagnification = Math.Min(durationMagnification, waypointDuration);
                    }

                    if (elapsedOpacity < durationOpacity)
                    {
                        if (!w.Opacity.change) { elapsedOpacity = Math.Min(elapsedOpacity + waypointDuration, durationOpacity); }
                        else
                        {
                            if (waypointDuration >= w.Opacity.duration) { prevOpacity = w.Opacity.target; nextOpacity = prevOpacity; elapsedOpacity = 0; durationOpacity = 0; }
                            else { prevOpacity = drawOpacity; nextOpacity = w.Opacity.target; elapsedOpacity = waypointDuration; durationOpacity = w.Opacity.duration; }
                        }
                    }
                    else if (w.Opacity.change)
                    {
                        prevOpacity = drawOpacity;
                        nextOpacity = w.Opacity.target;
                        durationOpacity = w.Opacity.duration;
                        elapsedOpacity = Math.Min(durationOpacity, waypointDuration);
                    }

                    drawAngle = (durationAngle <= 0 ? (float)prevAngle : (float)(elapsedAngle / durationAngle * (nextAngle - prevAngle) + prevAngle));
                    drawMagnification = (durationMagnification <= 0 ? (float)prevMagnification : (float)(elapsedMagnification / durationMagnification * (nextMagnification - prevMagnification) + prevMagnification));
                    drawOpacity = (durationOpacity <= 0 ? (float)prevOpacity : (float)(elapsedOpacity / durationOpacity * (nextOpacity - prevOpacity) + prevOpacity));

                    drawTime = waypointDuration;

                    if (w.TransitionTime + w.PersistTime > 0)
                    {
                        double t = waypointDuration / (w.TransitionTime + w.PersistTime);

                        drawPosition = Bezier.CalculateBezierPosition(drawPosition, w.C0, w.C1, w.P1, t);
                    }
                    else
                    {
                        drawPosition = w.P1;
                    }
                }
            }

            if (drawWaypoint == -1)
            {
                drawWaypoint = 0;
            }

            // Update the preview.
            UpdatePreview();
        }

        protected void SetItemSize (float width = 0, float height = 0)
        {
            if (width == 0 && height == 0)
            {
                itemSize.Width = 200;
                itemSize.Height = 150;
            }
            else
            {
                itemSize.Width = width;
                itemSize.Height = height;
            }
        }

        protected void SetItemSize (SizeF size)
        {
            itemSize = size;
        }

        protected void SetAnimationWindow (TADAAnimationWindow animationWindow)
        {
            this.animationWindow = animationWindow;
            animationWindow.Listen(this);
        }

        public void SetAnimationWindowSize (float width = 0, float height = 0)
        {
            if (width == 0 && height == 0)
            {
                animationWindowSize.Width = 1280;
                animationWindowSize.Height = 1024;
            }
            else
            {
                animationWindowSize.Width = width;
                animationWindowSize.Height = height;
            }
        }

        public void SetAnimationWindowSize (Size size)
        {
            animationWindowSize = size;
        }

        private double NormalizeAngle (double currentAngle)
        {
            while (currentAngle < 0)
            {
                currentAngle += 360;
            }

            return currentAngle % 360;
        }

        private void UpdateAdvancedWaypoint(object sender, EventArgs e)
        {
            disableUpdates = true;

            grp_AdvancedWaypoint.Enabled = !((sender as DataGridView).CurrentCell.OwningRow.IsNewRow);

            BindingSource source = (sender as DataGridView).DataSource as BindingSource;
            Waypoint w = source.Current as Waypoint;

            if (source.Position == 0)
            {
                // Dealing with the first Waypoint.  Set P0 and not P1.
                numud_P0X.Value = Convert.ToDecimal(w.X);
                numud_P0Y.Value = Convert.ToDecimal(w.Y);

                if (source.Count > 1)
                {
                    Waypoint w2 = source[1] as Waypoint;
                    numud_P1X.Value = Convert.ToDecimal(w2.P1.X);
                    numud_P1Y.Value = Convert.ToDecimal(w2.P1.Y);
                    numud_C0X.Value = Convert.ToDecimal(w2.C0.X);
                    numud_C0Y.Value = Convert.ToDecimal(w2.C0.Y);
                    numud_C1X.Value = Convert.ToDecimal(w2.C1.X);
                    numud_C1Y.Value = Convert.ToDecimal(w2.C1.Y);
                }

                radio_Curve.Enabled = false;
                radio_Line.Enabled = false;

                numud_P0X.Enabled = true;
                numud_P0Y.Enabled = true;
                numud_P1X.Enabled = false;
                numud_P1Y.Enabled = false;
                numud_C0X.Enabled = false;
                numud_C0Y.Enabled = false;
                numud_C1X.Enabled = false;
                numud_C1Y.Enabled = false;
            }
            else
            {
                // Dealing with any Waypoint after the first one.
                Waypoint w2 = source[source.Position - 1] as Waypoint;
                numud_P0X.Value = Convert.ToDecimal(w2.X);
                numud_P0Y.Value = Convert.ToDecimal(w2.Y);

                numud_C0X.Value = Convert.ToDecimal(w.C0.X);
                numud_C0Y.Value = Convert.ToDecimal(w.C0.Y);
                numud_C1X.Value = Convert.ToDecimal(w.C1.X);
                numud_C1Y.Value = Convert.ToDecimal(w.C1.Y);
                numud_P1X.Value = Convert.ToDecimal(w.P1.X);
                numud_P1Y.Value = Convert.ToDecimal(w.P1.Y);

                radio_Curve.Enabled = true;
                radio_Line.Enabled = true;

                numud_P0X.Enabled = false;
                numud_P0Y.Enabled = false;
                numud_P1X.Enabled = true;
                numud_P1Y.Enabled = true;
                numud_C0X.Enabled = true;
                numud_C0Y.Enabled = true;
                numud_C1X.Enabled = true;
                numud_C1Y.Enabled = true;
            }

            radio_Curve.Checked = w.IsCurve;
            radio_Line.Checked = !w.IsCurve;

            chk_LeaveScreen.Checked = w.LeaveScreen;
            chk_BehindImage.Checked = w.BehindImage;
            chk_ReadMessage.Checked = w.ReadMessage;

            chk_ChangeAngle.Checked = w.Angle.change;
            numud_TargetAngle.Value = Convert.ToDecimal(w.Angle.target);
            numud_FramesAngle.Value = Convert.ToInt32(w.Angle.frames);
            numud_DurationAngle.Value = Convert.ToDecimal(w.Angle.duration);
            radio_AngleCenter.Checked = w.Angle.aroundCenter;
            radio_AngleUpperLeft.Checked = !w.Angle.aroundCenter;

            chk_ChangeMagnification.Checked = w.Magnification.change;
            numud_TargetMagnification.Value = Convert.ToDecimal(w.Magnification.target);
            numud_FramesMagnification.Value = Convert.ToDecimal(w.Magnification.frames);
            numud_DurationMagnification.Value = Convert.ToDecimal(w.Magnification.duration);
            radio_MagnifyCenter.Checked = w.Magnification.aroundCenter;
            radio_MagnifyUpperLeft.Checked = !w.Magnification.aroundCenter;

            chk_ChangeOpacity.Checked = w.Opacity.change;
            numud_TargetOpacity.Value = Convert.ToDecimal(w.Opacity.target);
            numud_FramesOpacity.Value = Convert.ToDecimal(w.Opacity.frames);
            numud_DurationOpacity.Value = Convert.ToDecimal(w.Opacity.duration);

            chk_ChangeVolume.Checked = w.Volume.change;
            numud_TargetVolume.Value = Convert.ToDecimal(w.Volume.target);
            numud_FramesVolume.Value = Convert.ToDecimal(w.Volume.frames);
            numud_DurationVolume.Value = Convert.ToDecimal(w.Volume.duration);

            chk_ChangeBalance.Checked = w.Balance.change;
            numud_TargetBalance.Value = Convert.ToDecimal(w.Balance.target);
            numud_FramesBalance.Value = Convert.ToDecimal(w.Balance.frames);
            numud_DurationBalance.Value = Convert.ToDecimal(w.Balance.duration);

            txt_WindowTarget.Text = w.WindowTarget;
            txt_Keypresses.Text = w.Keypresses;

            SumWaypoints();

            UpdatePreview();

            CheckChecked();

            disableUpdates = false;
        }

        private void SumWaypoints()
        {
            BindingSource bindingSource = dg_Waypoints.DataSource as BindingSource;
            totalTime = 0;

            foreach (Waypoint w in bindingSource)
            {
                totalTime += w.TransitionTime;
                totalTime += w.PersistTime;
            }

            lbl_Time.Text = (trk_Time.Value / 1000.0 * totalTime).ToString();
            lbl_TotalWaypointTime.Text = String.Format("Total Alert Duration: {0:F2}s", totalTime);
        }

        private void btn_CopyWaypoints_Click(object sender, EventArgs e)
        {
            if (dg_Waypoints.SelectedRows.Count > 0)
            {
                var rows = dg_Waypoints.SelectedRows;

                for (int r = rows.Count - 1; r >= 0; -- r)
                {
                    Waypoint existingWaypoint = rows[r].DataBoundItem as Waypoint;
                    Waypoint newWaypoint = (dg_Waypoints.DataSource as BindingSource).AddNew() as Waypoint;

                    newWaypoint.P1 = existingWaypoint.P1;
                    newWaypoint.C0 = existingWaypoint.C0;
                    newWaypoint.C1 = existingWaypoint.C1;
                    newWaypoint.TransitionTime = existingWaypoint.TransitionTime;
                    newWaypoint.PersistTime = existingWaypoint.PersistTime;
                    newWaypoint.LeaveScreen = existingWaypoint.LeaveScreen;
                    newWaypoint.BehindImage = existingWaypoint.BehindImage;
                    newWaypoint.ReadMessage = existingWaypoint.ReadMessage;

                    newWaypoint.Angle.change = existingWaypoint.Angle.change;
                    newWaypoint.Angle.duration = existingWaypoint.Angle.duration;
                    newWaypoint.Angle.frames = existingWaypoint.Angle.frames;
                    newWaypoint.Angle.target = existingWaypoint.Angle.target;

                    newWaypoint.Magnification.change = existingWaypoint.Magnification.change;
                    newWaypoint.Magnification.duration = existingWaypoint.Magnification.duration;
                    newWaypoint.Magnification.frames = existingWaypoint.Magnification.frames;
                    newWaypoint.Magnification.target = existingWaypoint.Magnification.target;

                    newWaypoint.Opacity.change = existingWaypoint.Opacity.change;
                    newWaypoint.Opacity.duration = existingWaypoint.Opacity.duration;
                    newWaypoint.Opacity.frames = existingWaypoint.Opacity.frames;
                    newWaypoint.Opacity.target = existingWaypoint.Opacity.target;

                    newWaypoint.WindowTarget = existingWaypoint.WindowTarget;
                    newWaypoint.Keypresses = existingWaypoint.Keypresses;
                }

                SumWaypoints();
            }

            for (int r = 0; r < dg_Waypoints.Rows.Count; ++ r)
            {
                if (!dg_Waypoints.Rows[r].IsNewRow)
                {
                    dg_Waypoints.Rows[r].HeaderCell.Value = (r + 1).ToString();
                }
            }

            dg_Waypoints.AutoResizeRowHeadersWidth(DataGridViewRowHeadersWidthSizeMode.AutoSizeToAllHeaders);
        }

        private void btn_ShiftWaypoints_Click(object sender, EventArgs e)
        {
            if (dg_Waypoints.SelectedRows.Count > 0)
            {
                var rows = dg_Waypoints.SelectedRows;

                for (int row = rows.Count - 1; row >= 0; -- row)
                {
                    Waypoint r = rows[row].DataBoundItem as Waypoint;
                    float x = (float)numud_ShiftX.Value;
                    float y = (float)numud_ShiftY.Value;

                    r.X += (float)numud_ShiftX.Value;
                    r.Y += (float)numud_ShiftY.Value;
                }

                UpdatePreview();
            }
        }

        protected void UpdatePreview()
        {
            BindingSource bindingSource = dg_Waypoints.DataSource as BindingSource;

            if (bindingSource.Count <= 0)
            {
                return;
            }

            float widthRatio = pb_Preview.Width / (this.animationWindow != null ? this.animationWindow.Width : this.animationWindowSize.Width);
            float heightRatio = pb_Preview.Height / (this.animationWindow != null ? this.animationWindow.Height : this.animationWindowSize.Height);

            PointF P0 = (bindingSource[0] as Waypoint).P1;
            P0.X *= widthRatio;
            P0.Y *= heightRatio;

            Graphics graphics = Graphics.FromImage(previewImage);
            graphics.FillRectangle(Brushes.Black, new Rectangle(new Point(0, 0), new Size(pb_Preview.Width, pb_Preview.Height)));            

            for (int i = 0; i < bindingSource.Count; ++ i)
            {
                Waypoint w = bindingSource[i] as Waypoint;
                PointF C0, C1, P1;

                P1 = w.P1;
                P1.X *= widthRatio;
                P1.Y *= heightRatio;

                if (w.IsCurve)
                {
                    C0 = w.C0;
                    C1 = w.C1;

                    C0.X *= widthRatio;
                    C0.Y *= heightRatio;
                    C1.X *= widthRatio;
                    C1.Y *= heightRatio;
                }
                else
                {
                    Bezier.CalculateLinearControlPoints(P0, P1, out C0, out C1);
                }

                graphics.DrawBezier(Pens.White, P0, C0, C1, P1);
                graphics.DrawRectangle(Pens.Red, new Rectangle(Convert.ToInt32(Math.Floor(Convert.ToDouble(P0.X) - 2)), Convert.ToInt32(Math.Floor(Convert.ToDouble(P0.Y) - 2)), 5, 5));
                graphics.DrawRectangle(Pens.Red, new Rectangle(Convert.ToInt32(Math.Floor(Convert.ToDouble(P1.X) - 2)), Convert.ToInt32(Math.Floor(Convert.ToDouble(P1.Y) - 2)), 5, 5));

                if (i == drawWaypoint)
                {
                    PointF[] rectanglePoints = new PointF[] {
                        new PointF(0, 0),
                        new PointF(0, itemSize.Height * heightRatio),
                        new PointF(itemSize.Width * widthRatio, itemSize.Height * heightRatio),
                        new PointF(itemSize.Width * widthRatio, 0),
                        new PointF(0, 0)
                    };

                    double t;
                    if (w.TransitionTime == 0)
                    {
                        t = 1;
                    }
                    else
                    {
                        t = Math.Min(1, drawTime / w.TransitionTime);
                    }

                    float bezierX = Bezier.CalculateBezierCoordinate(P0.X, C0.X, C1.X, P1.X, (float)t);
                    float bezierY = Bezier.CalculateBezierCoordinate(P0.Y, C0.Y, C1.Y, P1.Y, (float)t);

                    Matrix m = new Matrix();
                    m.RotateAt(drawAngle, w.Angle.aroundCenter ? new PointF(itemSize.Width * widthRatio / 2.0f, itemSize.Height * heightRatio / 2.0f) : new PointF(0, 0));
                    m.Scale(drawMagnification, drawMagnification);
                    m.TransformPoints(rectanglePoints);
                    m.Reset();

                    m.Translate(bezierX, bezierY);
                    m.TransformPoints(rectanglePoints);

                    Color c = Color.FromArgb(Convert.ToInt32(Math.Floor(drawOpacity * 255 / 100)), Color.Green);
                    graphics.DrawLines(new Pen (c), rectanglePoints);
                }
                P0 = P1;
            }

            for (int i = 0; i < dg_Waypoints.Rows.Count; ++ i)
            {
                if (!dg_Waypoints.Rows[i].IsNewRow)
                {
                    foreach (DataGridViewCell c in dg_Waypoints.SelectedCells)
                    {
                        if (Math.Abs(i - c.RowIndex) <= numud_Waypoints.Value)
                        {
                            Waypoint waypoint = bindingSource[i] as Waypoint;
                            PointF P1 = waypoint.P1;
                            P1.X *= widthRatio;
                            P1.Y *= heightRatio;

                            int offsetX = (P1.X > pb_Preview.Width / 2 ? -10 : 5);
                            int offsetY = (P1.Y > pb_Preview.Height / 2 ? -10 : 5);
                            graphics.DrawString((i + 1).ToString(), new Font("Arial", 8.0f, FontStyle.Regular, GraphicsUnit.Point), Brushes.White, new PointF(P1.X + offsetX, P1.Y + offsetY));

                            break;
                        }
                    }
                }
            }

            pb_Preview.Image = previewImage;
            pb_Preview.Update();
        }

        private void KeyCombinationHelp(object sender, LinkLabelLinkClickedEventArgs e)
        {
            System.Diagnostics.Process.Start("https://msdn.microsoft.com/en-us/library/system.windows.forms.sendkeys.send(v=vs.110).aspx#Remarks");
        }

        protected WaypointContainer GetWaypoints()
        {
            var tempWaypoints = (dg_Waypoints.DataSource as BindingSource);

            WaypointContainer waypointContainer = new WaypointContainer();

            foreach (Waypoint w in tempWaypoints)
            {
                waypointContainer.AddWaypoint(w);
            }

            return waypointContainer;
        }

        protected void SetWaypoints(WaypointContainer waypointContainer)
        {
            if (waypointContainer != null)
            {
                BindingSource bindingSource = dg_Waypoints.DataSource as BindingSource;
                foreach (Waypoint waypoint in waypointContainer.Waypoints)
                {
                    bindingSource.Add(waypoint);
                }
            }
        }
    }
}
