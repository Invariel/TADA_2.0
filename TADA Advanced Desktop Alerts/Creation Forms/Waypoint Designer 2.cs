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
        public Waypoint_Designer_2()
        {
            InitializeComponent();

            pb_Preview.MouseClick += AddWaypointFromImage;

            numud_InitialX.ValueChanged += UpdateInitialWaypoint;
            numud_InitialY.ValueChanged += UpdateInitialWaypoint;
            numud_InitialAngle.ValueChanged += UpdateInitialWaypoint;
            numud_InitialMagnification.ValueChanged += UpdateInitialWaypoint;
            numud_InitialOpacity.ValueChanged += UpdateInitialWaypoint;
            numud_InitialDelay.ValueChanged += UpdateInitialWaypoint;
        }

        private void UpdateInitialWaypoint(object sender, EventArgs e)
        {
            if (((dg_Waypoints.DataSource) as BindingSource).Count == 0)
            {
                PointF firstPoint = new PointF((float)numud_InitialX.Value, (float)numud_InitialY.Value);
                Waypoint firstWaypoint = new Waypoint(firstPoint, 0, Convert.ToDouble(numud_InitialDelay.Value));

                // HUGE TODO:  Include initial state in the three classes, so that the initial angle, magnification, and opacity can be saved and used.
            }
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
                    ((dg_Waypoints.DataSource) as BindingSource).Add(newWaypoint);
                }
                else if (System.Windows.Input.Keyboard.Modifiers.HasFlag(System.Windows.Input.ModifierKeys.Control))
                {
                    if (dg_Waypoints.SelectedRows.Count == 1)
                    {
                        ((dg_Waypoints.DataSource) as BindingSource).Insert(dg_Waypoints.SelectedRows[0].Index + 1, newWaypoint);
                    }
                }
            }
        }
    }
}
