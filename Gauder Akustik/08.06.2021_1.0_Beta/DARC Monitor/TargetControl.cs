using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DarcMonitor
{
    public partial class TargetControl : UserControl
    {

        Kinematik Robot = new Kinematik();

        public event EventHandler eventHandler;

        public TargetControl()
        {
            InitializeComponent();
        }

        public void  setTarget(int num)
        {
            numericUpDownTarget.Value= num;
        }
        public decimal getTarget()
        {
            return numericUpDownTarget.Value;
        }

            private double gettnumericUpDown(int num)
        {
            double d = 0;
            if (checkBoxDeg.Checked)
                d = d * Math.PI / 180.0;
            switch (num)
            {
                case 1: d = (double)numericUpDown1.Value; break;
                case 2: d = (double)numericUpDown2.Value; break;
            }
            return d;
        }
        public decimal getnumericUpDown(int num)
        {
            switch (num)
            {
                case 1:
                    return numericUpDown1.Value;
                case 2:
                    return numericUpDown2.Value;
                case 3:
                    return numericUpDownTurnAngle.Value;
                case 4:
                    return numericUpDown3.Value;
            }
            return 0;
        }
        public void setnumericUpDown(int num, double d)
        {
            try
            {
                Decimal dd = (Decimal)d;
                if (checkBoxWorld.Checked)
                {

                }
                if (checkBoxDeg.Checked)
                    dd = (Decimal)(d * 180.0 / Math.PI);

                switch (num)
                {
                    case 1:
                        if (numericUpDown1.Minimum > dd)
                            numericUpDown1.Minimum = dd * 1.5m;
                        if (numericUpDown1.Maximum < dd)
                            numericUpDown1.Maximum = dd * 1.5m;

                        numericUpDown1.Value = dd; break;
                    case 2:
                        if (numericUpDown2.Minimum > dd)
                            numericUpDown2.Minimum = dd * 1.5m;
                        if (numericUpDown2.Maximum < dd)
                            numericUpDown2.Maximum = dd * 1.5m;

                        numericUpDown2.Value = dd; break;

                    case 3:
                        if (numericUpDownTurnAngle.Minimum > dd)
                            numericUpDownTurnAngle.Minimum = dd * 1.5m;
                        if (numericUpDownTurnAngle.Maximum < dd)
                            numericUpDownTurnAngle.Maximum = dd * 1.5m;

                        numericUpDownTurnAngle.Value = dd; break;
                }
          

            }
            catch (Exception)
            {
            }
        }

        private void checkBoxWorld_CheckedChanged(object sender, EventArgs e)
        {

            try
            {
                if (sender == checkBoxDeg)
                {
                    if (checkBoxWorld.Checked)
                    {
                        double w2 = (double)numericUpDown2.Value;
                        double wt = (double)numericUpDownTurnAngle.Value;

                        if (checkBoxDeg.Checked)
                        {
                            w2 *= 180 / Math.PI;
                            wt *= 180 / Math.PI;
                            label_2.Text = "phi [°]:";
                            label_5.Text = "turn  [°]:";
                            numericUpDown2.Maximum = 500;
                            numericUpDownTurnAngle.Minimum = -500;
                            numericUpDownTurnAngle.Maximum = 500;

                        }
                        else
                        {
                            w2 *= Math.PI / 180;
                            wt *= Math.PI / 180;
                            label_2.Text = "phi [rad]:";
                            label_5.Text = "turn [rad]:";

                            numericUpDown1.Maximum = 500;
                            numericUpDown2.Maximum = 500;
                            numericUpDownTurnAngle.Minimum = -500;
                            numericUpDownTurnAngle.Maximum = 500;
                        }
                        numericUpDown2.Value = (decimal)w2;
                        numericUpDownTurnAngle.Value = (decimal)wt;

                    }

                    else
                    {
                        double w1 = (double)numericUpDown1.Value;
                        double w2 = (double)numericUpDown2.Value;
                        double wt = (double)numericUpDownTurnAngle.Value;
                        if (checkBoxDeg.Checked)
                        {
                            w1 *= 180 / Math.PI;
                            w2 *= 180 / Math.PI;
                            wt *= 180 / Math.PI;
                            label_1.Text = "theta1 [°]:";
                            label_2.Text = "theta2 [°]:";
                            label_5.Text = "turrn  [°]:";
                            numericUpDown1.Maximum = 500;
                            numericUpDown2.Maximum = 500;
                            numericUpDownTurnAngle.Minimum = -500;
                            numericUpDownTurnAngle.Maximum = 500;

                        }
                        else
                        {
                            w1 *= Math.PI / 180;
                            w2 *= Math.PI / 180;
                            wt *= Math.PI / 180;
                            label_1.Text = "theta1 [rad]:";
                            label_2.Text = "theta2 [rad]:";
                            label_5.Text = "turn   [rad]:";

                            numericUpDown1.Maximum = 500;
                            numericUpDown2.Maximum = 500;
                            numericUpDownTurnAngle.Minimum = -500;
                            numericUpDownTurnAngle.Maximum = 500;
                        }
                        numericUpDown1.Value = (decimal)w1;
                        numericUpDown2.Value = (decimal)w2;
                        numericUpDownTurnAngle.Value = (decimal)wt;

                    }
                }

                else if (sender == checkBoxWorld)
                {
                    if (checkBoxWorld.Checked)
                    {
                        label_1.Text = "x [mm]:";
                        label_2.Text = "phi [rad]:";
                        double w1 = (double)numericUpDown1.Value;
                        double w2 = (double)numericUpDown2.Value;
                        if (checkBoxDeg.Checked)
                        {
                            w1 *= Math.PI / 180;
                            w2 *= Math.PI / 180;
                            label_2.Text = "phi [°]:";

                        }
                        double[] xd = Robot.jointWorld(w1, w2);
                        numericUpDown1.Minimum = -500;
                        numericUpDown1.Maximum = 500;
                        numericUpDown1.Value = (decimal)xd[0];
                        label_3.Text = "y [mm]:  " + xd[1].ToString("N2");
                        numericUpDown2.Minimum = -500;
                        numericUpDown2.Maximum = 500;
                        if (checkBoxDeg.Checked)
                        {
                            xd[2] *= 180 / Math.PI;
                        }
                        numericUpDown2.Value = (decimal)xd[2];
                    }
                    else
                    {
                        double[] xJ = Robot.worldJoint((double)numericUpDown1.Value, (double)numericUpDown2.Value);
                        if (checkBoxDeg.Checked)
                        {
                            xJ[0] *= 180 / Math.PI;
                            xJ[1] *= 180 / Math.PI;
                            label_1.Text = "theta1 [°]:";
                            label_2.Text = "theta2 [°]:";
                            label_5.Text = "turn [°]:";


                            numericUpDown1.Minimum = -2;
                            numericUpDown1.Maximum = 100;
                            numericUpDown2.Minimum = -30;
                            numericUpDown2.Maximum = 180;
                        }
                        else
                        {
                            numericUpDown1.Minimum = -2;
                            numericUpDown1.Maximum = 2;
                            numericUpDown2.Minimum = -3;
                            numericUpDown2.Maximum = 3;
                            label_1.Text = "theta1 [rad]:";
                            label_2.Text = "theta2 [rad]:";
                            label_5.Text = "turn [rad]:";


                        }
                        numericUpDown1.Value = (decimal)xJ[0];
                        numericUpDown2.Value = (decimal)xJ[1];
                        label_3.Text = "... ";
                    }
                }
            }
            catch (Exception ex)
            {
                ;
            }
        }

        private void buttonTarget_Click(object sender, EventArgs e)
        {
            eventHandler?.Invoke(sender, new EventArgs());
        }
    }
}
