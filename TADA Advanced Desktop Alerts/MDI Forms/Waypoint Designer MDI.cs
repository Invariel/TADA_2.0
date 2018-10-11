using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TADA_Advanced_Desktop_Alerts.MDI_Forms
{
    public partial class Waypoint_Designer_MDI : Form
    {
        protected internal bool disableUpdates = false;

        public Waypoint_Designer_MDI(TADAAlert parentAlert = null)
        {
            InitializeComponent();

            dg_Waypoints.SelectionChanged += UpdateAdvancedWaypoint;
            dg_Waypoints.DefaultValuesNeeded += DefaultRowValues;
            dg_Waypoints.EditingControlShowing += NumericInputsOnly;

            btn_CopySelected.Click += btn_CopySelected_Click;
            btn_ShiftSelected.Click += btn_ShiftSelected_Click;

            radio_Curve.CheckedChanged += SaveVariableState;
            radio_Line.CheckedChanged += SaveVariableState;
            radio_FromCenter.CheckedChanged += SaveVariableState;
            radio_FromTopLeft.CheckedChanged += SaveVariableState;

            radio_Curve.CheckedChanged += ToggleCurve;
            radio_Line.CheckedChanged += ToggleCurve;

            numud_P0X.ValueChanged += SaveVariableState;
            numud_P0Y.ValueChanged += SaveVariableState;
            numud_C0X.ValueChanged += SaveVariableState;
            numud_C0Y.ValueChanged += SaveVariableState;
            numud_C1X.ValueChanged += SaveVariableState;
            numud_C1Y.ValueChanged += SaveVariableState;
            numud_P1X.ValueChanged += SaveVariableState;
            numud_P1Y.ValueChanged += SaveVariableState;

            chk_Angle.CheckedChanged += SaveVariableState;
            numud_DesiredAngle.ValueChanged += SaveVariableState;
            numud_AngleDuration.ValueChanged += SaveVariableState;
            numud_AngleFrames.ValueChanged += SaveVariableState;

            chk_Magnification.CheckedChanged += SaveVariableState;
            numud_DesiredMagnification.ValueChanged += SaveVariableState;
            numud_MagnificationDuration.ValueChanged += SaveVariableState;
            numud_MagnificationFrames.ValueChanged += SaveVariableState;

            chk_Opacity.CheckedChanged += SaveVariableState;
            numud_DesiredOpacity.ValueChanged += SaveVariableState;
            numud_OpacityDuration.ValueChanged += SaveVariableState;
            numud_OpacityFrames.ValueChanged += SaveVariableState;

            chk_Balance.CheckedChanged += SaveVariableState;
            numud_DesiredBalance.ValueChanged += SaveVariableState;
            numud_BalanceDuration.ValueChanged += SaveVariableState;
            numud_BalanceFrames.ValueChanged += SaveVariableState;

            chk_Volume.CheckedChanged += SaveVariableState;
            numud_DesiredVolume.ValueChanged += SaveVariableState;
            numud_VolumeDuration.ValueChanged += SaveVariableState;
            numud_VolumeFrames.ValueChanged += SaveVariableState;

            txt_Keypresses.LostFocus += SaveVariableState;
            txt_WindowTarget.LostFocus += SaveVariableState;

            if (parentAlert != null)
            {
                // Then populate cmb_ImportWaypoints with the other alert names and types.
                // One of the alerts is this one.  Need to figure out which.

                cmb_ImportWaypoints.DisplayMember = "Text";
                cmb_ImportWaypoints.ValueMember = "Waypoints";

            }

            ConfigureTooltips();
        }

        private void ConfigureTooltips()
        {
            General.ConfigureToolTip(lbl_WaypointDuration, "The total time that your Waypoints will run for.");

            General.ConfigureToolTip(new List<Control>() { lbl_InitialAngle, numud_InitialAngle, lbl_AngleDegrees }, "The initial angle for this element.");
            General.ConfigureToolTip(new List<Control>() { lbl_InitialMagnification, numud_InitialMagnification, lbl_InitialMagnificationX }, "The initial magnification for this element.");
            General.ConfigureToolTip(new List<Control>() { lbl_InitialOpacity, numud_InitialOpacity, lbl_InitialOpacityPercent }, "The initial opacity for this element.");

            General.ConfigureToolTip(btn_ImportWaypointsFrom, "Overwrite your Waypoint table with Waypoints from another item in this Alert.");
            General.ConfigureToolTip(cmb_ImportWaypoints, "Choose from the other items in this Alert to use as this item's Waypoints.");

            General.ConfigureToolTip(btn_CopySelected, "Copy the selected rows in the order that they were selected.  This will copy all of the associated Waypoint data.");
            General.ConfigureToolTip(btn_ShiftSelected, "Shift the selected rows in the horizontal and vertical directions as specified.  This will NOT affect control points.");
            General.ConfigureToolTip(numud_ShiftX, "The horizontal shift to be applied to all selected Waypoints once the Shift Selected Waypoints button is pressed.");
            General.ConfigureToolTip(numud_ShiftY, "The vertical shift to be applied to all selected Waypoints once the Shift Selected Waypoints button is pressed.");

            General.ConfigureToolTip(new List<Control>() { tab_AdvancedWaypointControls, tab_Advanced }, "Advanced controls for the selected Waypoint.");

            General.ConfigureToolTip(tab_Path, "Controls for drawing a curved path for your element.");
            General.ConfigureToolTip(radio_Curve, "Select this if you want your object to take a curved path instead of a straight one to the current Waypoint.");
            General.ConfigureToolTip(radio_Line, "Select this if you want your object to take a straight path instead of a curved one to the current Waypoint.");

            General.ConfigureToolTip(new List<Control>() { lbl_P0, numud_P0X, numud_P0Y }, "The coordinates of the previous Waypoint (or the current Waypoint if this is the first Waypoint).");
            General.ConfigureToolTip(new List<Control>() { lbl_P1, numud_P1X, numud_P1Y }, "The coordinates of the destination for this item.");
            General.ConfigureToolTip(new List<Control>() { lbl_C0, numud_C0X, numud_C0Y }, "The coordinates of the first control point defining the curve.");
            General.ConfigureToolTip(new List<Control>() { lbl_C1, numud_C1X, numud_C1Y }, "The coordinates of the second control point defining the curve.");

            General.ConfigureToolTip(tab_AngleAndMagnification, "Controls for adjusting your element's angle and magnification.  Video files cannot currently be rotated.");
            General.ConfigureToolTip(chk_Angle, "If checked, this Waypoint includes an angle change.");
            General.ConfigureToolTip(new List<Control>() { lbl_DesiredAngle, numud_DesiredAngle }, "The desired angle for the element to reach.  On reaching the target, the item's angle will be redefined to 0 <= angle < 360.");
            General.ConfigureToolTip(new List<Control>() { lbl_AngleFrames, numud_AngleFrames }, "The number of animation frames to take to rotate the element.");
            General.ConfigureToolTip(new List<Control>() { lbl_AngleDuration, numud_AngleDuration }, "How much time, in seconds, to complete this rotation.");

            General.ConfigureToolTip(chk_Magnification, "If checked, this Waypoint includes a change in magnification.");
            General.ConfigureToolTip(new List<Control>() { lbl_DesiredMagnification, numud_DesiredMagnification }, "The desired magnification for the element to reach.");
            General.ConfigureToolTip(new List<Control>() { lbl_MagnificationFrames, numud_MagnificationFrames }, "The number of animation frames to take to magnify the element.");
            General.ConfigureToolTip(new List<Control>() { lbl_MagnificationDuration, numud_MagnificationDuration }, "How much time, in seconds, to complete this magnification.");

            General.ConfigureToolTip(tab_Opacity, "Controls for changing your element's opacity.  Opacity cannot currently be changed on video files.");
            General.ConfigureToolTip(chk_Opacity, "If checked, this Waypoint includes a change in opacity.");
            General.ConfigureToolTip(new List<Control>() { lbl_DesiredOpacity, numud_DesiredOpacity }, "The desired opacity for the element to reach.");
            General.ConfigureToolTip(new List<Control>() { lbl_OpacityFrames, numud_OpacityFrames }, "The number of animation frames to take to fade the element.");
            General.ConfigureToolTip(new List<Control>() { lbl_OpacityDuration, numud_OpacityDuration }, "How much time, in seconds, to complete this fade.");

            General.ConfigureToolTip(tab_Sound, "Controls for adjustinug your element's sound balance and volume over the course of play.");
            General.ConfigureToolTip(chk_Balance, "If checked, this Waypoint includes a change in audio balance.");
            General.ConfigureToolTip(new List<Control>() { lbl_DesiredBalance, numud_DesiredBalance }, "The desired audio balance for the audio to play at, -1.0 for left, 1.0 for right.");
            General.ConfigureToolTip(new List<Control>() { lbl_BalanceFrames, numud_BalanceFrames }, "How many times the audio balance changes to reach the final balance value.");
            General.ConfigureToolTip(new List<Control>() { lbl_BalanceDuration, numud_BalanceDuration }, "How quickly to reach the desired balance, in seconds.");

            General.ConfigureToolTip(chk_Volume, "If checked, this Waypoint includes a change in audio volume.");
            General.ConfigureToolTip(new List<Control>() { lbl_DesiredVolume, numud_DesiredVolume }, "The desired volume for the audio to reach.");
            General.ConfigureToolTip(new List<Control>() { lbl_VolumeFrames, numud_VolumeFrames }, "How many times the volume changes to reach the final value.");
            General.ConfigureToolTip(new List<Control>() { lbl_VolumeDuration, numud_VolumeDuration }, "How quickly to reach the desired volume, in seconds.");

            General.ConfigureToolTip(tab_Macro, "Controls for directing keypresses at a given application at the end of this Waypoint.");
            General.ConfigureToolTip(new List<Control>() { lbl_WindowTarget, txt_WindowTarget }, "The exact name of the window to direct the keypress combination to.");
            General.ConfigureToolTip(new List<Control>() { lbl_Keypresses, txt_Keypresses }, "The combination of keys to send to the selected window.");
        }

        private void AddWaypointFromImage (PointF position, bool insertBefore)
        {
            Waypoint newWaypoint = new Waypoint(position, 0, 0, false, false, false);
            if (insertBefore)
            {
                if (dg_Waypoints.SelectedRows.Count == 1)
                {
                    waypointBindingSource.Insert(dg_Waypoints.SelectedRows[0].Index, newWaypoint);
                }
            }
            else
            {
                waypointBindingSource.Add(newWaypoint);
            }

            RenumberDataGrid();
        }

        private void btn_CopySelected_Click(object sender, EventArgs e)
        {
            if (dg_Waypoints.SelectedRows.Count > 0)
            {
                var rows = dg_Waypoints.SelectedRows;

                for (int r = rows.Count - 1; r >= 0; --r)
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

            for (int r = 0; r < dg_Waypoints.Rows.Count; ++r)
            {
                if (!dg_Waypoints.Rows[r].IsNewRow)
                {
                    dg_Waypoints.Rows[r].HeaderCell.Value = (r + 1).ToString();
                }
            }

            dg_Waypoints.AutoResizeRowHeadersWidth(DataGridViewRowHeadersWidthSizeMode.AutoSizeToAllHeaders);
        }

        private void btn_ShiftSelected_Click(object sender, EventArgs e)
        {
            if (dg_Waypoints.SelectedRows.Count > 0)
            {
                var rows = dg_Waypoints.SelectedRows;

                for (int row = rows.Count - 1; row >= 0; --row)
                {
                    Waypoint r = rows[row].DataBoundItem as Waypoint;
                    float x = (float)numud_ShiftX.Value;
                    float y = (float)numud_ShiftY.Value;

                    r.X += (float)numud_ShiftX.Value;
                    r.Y += (float)numud_ShiftY.Value;
                }
            }
        }

        private void CheckChecked()
        {
            numud_DesiredAngle.Enabled = chk_Angle.Checked;
            numud_AngleFrames.Enabled = chk_Angle.Checked;
            numud_AngleDuration.Enabled = chk_Angle.Checked;

            numud_DesiredMagnification.Enabled = chk_Magnification.Checked;
            numud_MagnificationFrames.Enabled = chk_Magnification.Checked;
            numud_MagnificationDuration.Enabled = chk_Magnification.Checked;

            numud_DesiredOpacity.Enabled = chk_Opacity.Checked;
            numud_OpacityFrames.Enabled = chk_Opacity.Checked;
            numud_OpacityDuration.Enabled = chk_Opacity.Checked;

            numud_DesiredBalance.Enabled = chk_Balance.Checked;
            numud_BalanceFrames.Enabled = chk_Balance.Checked;
            numud_BalanceDuration.Enabled = chk_Balance.Checked;

            numud_DesiredVolume.Enabled = chk_Volume.Checked;
            numud_VolumeFrames.Enabled = chk_Volume.Checked;
            numud_VolumeDuration.Enabled = chk_Volume.Checked;
        }

        private void DefaultRowValues(object sender, DataGridViewRowEventArgs e)
        {
            e.Row.Cells["xDataGridViewTextBoxColumn"].Value = 0;
            e.Row.Cells["yDataGridViewTextBoxColumn"].Value = 0;
            e.Row.Cells["TransitionTimeDataGridViewTextBoxColumn"].Value = 0;
            e.Row.Cells["persistTimeDataGridViewTextBoxColumn"].Value = 0;
            e.Row.Cells["behindImageDataGridViewCheckBoxColumn"].Value = false;
            e.Row.Cells["readMessageDataGridViewCheckBoxColumn"].Value = false;
            e.Row.HeaderCell.Value = (e.Row.Index + 1).ToString();
            dg_Waypoints.AutoResizeRowHeadersWidth(DataGridViewRowHeadersWidthSizeMode.AutoSizeToAllHeaders);
        }

        private void NumericInputsOnly(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            if (!disableUpdates)
            {
                DataGridViewCell cell = (sender as DataGridView).CurrentCell;
                if (cell.ValueType != typeof(Boolean))
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

        private void RenumberDataGrid()
        {
            foreach (DataGridViewRow row in dg_Waypoints.Rows)
            {
                row.HeaderCell.Value = (row.Index + 1).ToString();
            }
        }

        private void SaveVariableState(object sender, EventArgs e)
        {
            // Save all of the advanced waypoint information to the current Waypoint.
            if (!disableUpdates)
            {
                Waypoint currentWaypoint = waypointBindingSource.Current as Waypoint;

                if (waypointBindingSource.Position == 0)
                {
                    currentWaypoint.X = (float)numud_P0X.Value;
                    currentWaypoint.Y = (float)numud_P0Y.Value;
                }
                else
                {
                    currentWaypoint.X = (float)numud_P1X.Value;
                    currentWaypoint.Y = (float)numud_P1Y.Value;
                }

                currentWaypoint.C0 = new PointF((float)numud_C0X.Value, (float)numud_C0Y.Value);
                currentWaypoint.C1 = new PointF((float)numud_C1X.Value, (float)numud_C1Y.Value);

                currentWaypoint.IsCurve = radio_Curve.Checked;

                currentWaypoint.Angle.aroundCenter = radio_FromCenter.Checked;
                currentWaypoint.Angle.change = chk_Angle.Checked;
                currentWaypoint.Angle.duration = Convert.ToDouble(numud_AngleDuration.Value);
                currentWaypoint.Angle.frames = Convert.ToInt32(numud_AngleFrames.Value);
                currentWaypoint.Angle.target = Convert.ToDouble(numud_DesiredAngle.Value);

                currentWaypoint.Magnification.aroundCenter = radio_FromCenter.Checked;
                currentWaypoint.Magnification.change = chk_Magnification.Checked;
                currentWaypoint.Magnification.duration = Convert.ToDouble(numud_MagnificationDuration.Value);
                currentWaypoint.Magnification.frames = Convert.ToInt32(numud_MagnificationFrames.Value);
                currentWaypoint.Magnification.target = Convert.ToDouble(numud_DesiredMagnification.Value);

                currentWaypoint.Opacity.aroundCenter = radio_FromCenter.Checked;
                currentWaypoint.Opacity.change = chk_Opacity.Checked;
                currentWaypoint.Opacity.duration = Convert.ToDouble(numud_OpacityDuration.Value);
                currentWaypoint.Opacity.frames = Convert.ToInt32(numud_OpacityFrames.Value);
                currentWaypoint.Opacity.target = Convert.ToDouble(numud_DesiredOpacity.Value);

                currentWaypoint.Balance.aroundCenter = radio_FromCenter.Checked;
                currentWaypoint.Balance.change = chk_Balance.Checked;
                currentWaypoint.Balance.duration = Convert.ToDouble(numud_BalanceDuration.Value);
                currentWaypoint.Balance.frames = Convert.ToInt32(numud_BalanceFrames.Value);
                currentWaypoint.Balance.target = Convert.ToDouble(numud_DesiredBalance.Value);

                currentWaypoint.Volume.aroundCenter = radio_FromCenter.Checked;
                currentWaypoint.Volume.change = chk_Volume.Checked;
                currentWaypoint.Volume.duration = Convert.ToDouble(numud_VolumeDuration.Value);
                currentWaypoint.Volume.frames = Convert.ToInt32(numud_VolumeFrames.Value);
                currentWaypoint.Volume.target = Convert.ToDouble(numud_DesiredVolume.Value);

                CheckChecked();
            }
        }

        private void SumWaypoints()
        {
            double totalTime = 0;

            foreach (Waypoint w in waypointBindingSource)
            {
                totalTime += w.TransitionTime;
                totalTime += w.PersistTime;
            }

            lbl_WaypointDuration.Text = String.Format("Total Alert Duration: {0:F2}s", totalTime);
        }

        private void ToggleCurve(object sender, EventArgs e)
        {
            numud_C0X.Enabled = radio_Curve.Checked;
            numud_C0Y.Enabled = radio_Curve.Checked;
            numud_C1X.Enabled = radio_Curve.Checked;
            numud_C1Y.Enabled = radio_Curve.Checked;
        }

        private void UpdateAdvancedWaypoint(object sender, EventArgs e)
        {
            disableUpdates = true;

            tab_AdvancedWaypointControls.Enabled = !((sender as DataGridView).CurrentCell.OwningRow.IsNewRow);

            Waypoint w = waypointBindingSource.Current as Waypoint;

            if (waypointBindingSource.Position == 0)
            {
                // Dealing with the first Waypoint.  Set P0 and not P1.
                numud_P0X.Value = Convert.ToDecimal(w.X);
                numud_P0Y.Value = Convert.ToDecimal(w.Y);

                if (waypointBindingSource.Count > 1)
                {
                    Waypoint w2 = waypointBindingSource[1] as Waypoint;
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
                Waypoint w2 = waypointBindingSource[waypointBindingSource.Position - 1] as Waypoint;
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
                numud_C0X.Enabled = w.IsCurve;
                numud_C0Y.Enabled = w.IsCurve;
                numud_C1X.Enabled = w.IsCurve;
                numud_C1Y.Enabled = w.IsCurve;
            }

            radio_Curve.Checked = w.IsCurve;
            radio_Line.Checked = !w.IsCurve;

            chk_Angle.Checked = w.Angle.change;
            numud_DesiredAngle.Value = Convert.ToDecimal(w.Angle.target);
            numud_AngleFrames.Value = Convert.ToInt32(w.Angle.frames);
            numud_AngleDuration.Value = Convert.ToDecimal(w.Angle.duration);
            radio_FromCenter.Checked = w.Angle.aroundCenter;
            radio_FromTopLeft.Checked = !w.Angle.aroundCenter;

            chk_Magnification.Checked = w.Magnification.change;
            numud_DesiredMagnification.Value = Convert.ToDecimal(w.Magnification.target);
            numud_MagnificationFrames.Value = Convert.ToDecimal(w.Magnification.frames);
            numud_MagnificationDuration.Value = Convert.ToDecimal(w.Magnification.duration);

            chk_Opacity.Checked = w.Opacity.change;
            numud_DesiredOpacity.Value = Convert.ToDecimal(w.Opacity.target);
            numud_OpacityFrames.Value = Convert.ToDecimal(w.Opacity.frames);
            numud_OpacityDuration.Value = Convert.ToDecimal(w.Opacity.duration);

            chk_Balance.Checked = w.Balance.change;
            numud_DesiredBalance.Value = Convert.ToDecimal(w.Balance.target);
            numud_BalanceFrames.Value = Convert.ToDecimal(w.Balance.frames);
            numud_BalanceDuration.Value = Convert.ToDecimal(w.Balance.duration);

            chk_Volume.Checked = w.Volume.change;
            numud_DesiredVolume.Value = Convert.ToDecimal(w.Volume.target);
            numud_VolumeFrames.Value = Convert.ToDecimal(w.Volume.frames);
            numud_VolumeDuration.Value = Convert.ToDecimal(w.Volume.duration);

            txt_WindowTarget.Text = w.WindowTarget;
            txt_Keypresses.Text = w.Keypresses;

            SumWaypoints();
            CheckChecked();

            disableUpdates = false;
        }
    }
}
