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
    public partial class Waypoint_Designer_2 : Form
    {
        private Boolean disableUpdates = false;

        // Section of variables for drawing the preview image.
        private Bitmap previewBitmap;

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

                if (System.Windows.Input.Keyboard.Modifiers.HasFlag(System.Windows.Input.ModifierKeys.Shift))
                {
                    waypointBindingSource.Add(newWaypoint);
                }
                else if (System.Windows.Input.Keyboard.Modifiers.HasFlag(System.Windows.Input.ModifierKeys.Control))
                {
                    if (dg_Waypoints.SelectedRows.Count == 1)
                    {
                        waypointBindingSource.Insert(dg_Waypoints.SelectedRows[0].Index + 1, newWaypoint);
                    }
                }
            }
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

//                UpdatePreview();
//                CheckChecked();


                // Move this entire block to the "ok done" section.
/*
                Boolean alerted = false;

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
*/
            }
        }
    }
}
