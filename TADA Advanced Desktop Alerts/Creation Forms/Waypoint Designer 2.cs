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
    public partial class Waypoint_Designer_2 : Form
    {
        private Boolean disableUpdates = false;

        // Section of variables for drawing the preview image.
        private Bitmap previewBitmap;

        protected SizeF itemSize = new SizeF(200, 100);

        private int drawWaypoint = 0;

        private float drawAngle = 0,
                      drawMagnification = 1,
                      drawOpacity = 1;

        private double drawTime;

        private PointF drawPosition = new PointF(0, 0);

        protected double totalTime = 0;

        public Waypoint_Designer_2()
        {
            InitializeComponent();

            dg_Waypoints.SelectionChanged += UpdateAdvancedWaypoint;
            dg_Waypoints.DefaultValuesNeeded += DefaultRowValues;
            dg_Waypoints.EditingControlShowing += NumericInputsOnly;

            pb_Preview.MouseClick += AddWaypointFromImage;

            radio_Line.CheckedChanged += SaveVariableState;
            radio_Curve.CheckedChanged += SaveVariableState;
            radio_FromCenter.CheckedChanged += SaveVariableState;
            radio_FromTopRight.CheckedChanged += SaveVariableState;

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

            previewBitmap = new Bitmap(pb_Preview.Width, pb_Preview.Height);
        }

        private void AddWaypointFromImage (object sender, MouseEventArgs e)
        {
            if (!System.Windows.Input.Keyboard.Modifiers.HasFlag(System.Windows.Input.ModifierKeys.Shift) &&
                !System.Windows.Input.Keyboard.Modifiers.HasFlag(System.Windows.Input.ModifierKeys.Control))
            {
                // Then do nothing and bow out gracefully.
            }
            else
            {
                // Calculate position of e.Location (or of Control.PointToClient(Cursor.Position) multiplied by the X/Y ratios to get the point.
                float ratioX = MainTADAForm.mainTADAForm.AnimationWindow.Width / (float)pb_Preview.Width;
                float ratioY = MainTADAForm.mainTADAForm.AnimationWindow.Height / (float)pb_Preview.Height;

                PointF modifiedLocation = new PointF((float)(Math.Round(e.Location.X * ratioX, 2)), (float)(Math.Round(e.Location.Y * ratioY, 2)));

                Waypoint newWaypoint = new Waypoint(modifiedLocation, 0, 0, false, false, false);

                // Shift + Click = Append
                if (System.Windows.Input.Keyboard.Modifiers.HasFlag(System.Windows.Input.ModifierKeys.Shift))
                {
                    waypointBindingSource.Add(newWaypoint);
                }
                // Ctrl + Click = Insert Before
                else if (System.Windows.Input.Keyboard.Modifiers.HasFlag(System.Windows.Input.ModifierKeys.Control))
                {
                    if (dg_Waypoints.SelectedRows.Count == 1)
                    {
                        waypointBindingSource.Insert(dg_Waypoints.SelectedRows[0].Index, newWaypoint);
                    }
                }
            }
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
            radio_FromTopRight.Checked = !w.Angle.aroundCenter;

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

            UpdatePreview();

            CheckChecked();

            disableUpdates = false;
        }

        private void SumWaypoints()
        {
            totalTime = 0;

            foreach (Waypoint w in waypointBindingSource)
            {
                totalTime += w.TransitionTime;
                totalTime += w.PersistTime;
            }

            lbl_Time.Text = (trk_Time.Value / 1000.0 * totalTime).ToString();
            lbl_WaypointDuration.Text = String.Format("Total Alert Duration: {0:F2}s", totalTime);
        }

        private void SaveVariableState (object sender, EventArgs e)
        {
            // Save all of the advanced waypoint information to the current Waypoint.
            if (!disableUpdates)
            {
                Waypoint currentWaypoint = waypointBindingSource.Current as Waypoint;

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

                UpdatePreview();
                CheckChecked();
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

        protected void UpdatePreview()
        {
            if (waypointBindingSource.Count <= 0)
            {
                return;
            }

            float widthRatio = pb_Preview.Width / MainTADAForm.mainTADAForm.AnimationWindow.Width;
            float heightRatio = pb_Preview.Height / MainTADAForm.mainTADAForm.AnimationWindow.Height;

            PointF P0 = (waypointBindingSource[0] as Waypoint).P1;
            P0.X *= widthRatio;
            P0.Y *= heightRatio;

            Graphics graphics = Graphics.FromImage(previewBitmap);
            graphics.FillRectangle(Brushes.Black, new Rectangle(new Point(0, 0), new Size(pb_Preview.Width, pb_Preview.Height)));

            for (int i = 0; i < waypointBindingSource.Count; ++i)
            {
                Waypoint w = waypointBindingSource[i] as Waypoint;
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
                    graphics.DrawLines(new Pen(c), rectanglePoints);
                }
                P0 = P1;
            }

            for (int i = 0; i < dg_Waypoints.Rows.Count; ++i)
            {
                if (!dg_Waypoints.Rows[i].IsNewRow)
                {
                    foreach (DataGridViewCell c in dg_Waypoints.SelectedCells)
                    {
                        if (Math.Abs(i - c.RowIndex) <= numud_SurroundingWaypoints.Value)
                        {
                            Waypoint waypoint = waypointBindingSource[i] as Waypoint;
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

            pb_Preview.Image = previewBitmap;
            pb_Preview.Update();
        }

        protected internal Boolean CheckEffectFPS ()
        {
            Boolean alerted = false;
/*
            foreach (Waypoint w in waypointBindingSource)
            {
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
                    if (!alerted && tuple.Item1.change && time < MIN_SAFE_RESOLUTION && time > 0)
                    {
                        // Only make this happen when focus is left.
                        MessageBox.Show(null,
                                        String.Format("Your {0} effect may take longer than desired because {1:F3} is lower than the safe {2} ms resolution.",
                                                        tuple.Item2,
                                                        time,
                                                        MIN_SAFE_RESOLUTION),
                                        "May Take Longer Than Desired",
                                        MessageBoxButtons.OK,
                                        MessageBoxIcon.Warning);

                        alerted = true;
                    }
                }
            }
*/
            return alerted;
        }
    }
}
