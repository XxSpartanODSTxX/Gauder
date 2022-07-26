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
    public partial class ServiceControl : UserControl
    {

        public event EventHandler eventHandler;

        public ServiceControl()
        {
            InitializeComponent();
        }
        private void comboBoxSet_SelectedIndexChanged(object sender, EventArgs e)
        {
            ComboBox box = sender as ComboBox;

            try
            {
                switch (comboBoxSetValue.Text)
                {
                    case "Override":
                        numericSetValue.Value = Decimal.Parse(box.SelectedItem.ToString());
                        break;
                    case "OutTarget":
                        numericSetValue.Value = 6 + box.SelectedIndex;
                        break;
                    case "InterpolationMode":
                    case "CollisionMode":
                    case "OperationalMode":
                    case "TCP":
                        numericSetValue.Value = box.SelectedIndex;
                        break;


                }
            }
            catch (Exception ex)
            {
                ;
           //     WriteTextSafe(ex.Message, Color.DarkRed);

            }

        }


        private void comboBoxSetValue_SelectedIndexChanged(object sender, EventArgs e)
        {
            string[] overrides = new string[] { "10", "20", "50", "70", "90", "100", "120" };
            string[] targets = new string[] { "back", "middel", "front", "left", "rigt", "last", "teached" };
            string[] interpolationModes = new string[] { "PTP", "SplineA", "SplineB", "SplineC" };
            string[] collisionModes = new string[] { "none", "Limit", "RLE", "QuadTree", "Geometric" };
            string[] tcps = new string[] { "left", "right", "middle", "back", "center", "none" };
            string[] opModes = new string[] { "PreOperational", "Operational" };
            ComboBox box = sender as ComboBox;
            comboBoxSet.Items.Clear();
            comboBoxSet.Text = "(>select)";
            labelId.Text = "...";
            numericUpDownId.Value = 0;
            switch (box.Text)
            {
                case "Override":
                    comboBoxSet.Items.AddRange(overrides);
                    break;
                case "OutTarget":
                    comboBoxSet.Items.AddRange(targets);
                    break;
                case "InterpolationMode":
                    comboBoxSet.Items.AddRange(interpolationModes);
                    break;
                case "CollisionMode":
                    comboBoxSet.Items.AddRange(collisionModes);
                    break;
                case "TCP":
                    comboBoxSet.Items.AddRange(tcps);
                    break;
                case "OperationalMode":
                    comboBoxSet.Items.AddRange(opModes);
                    break;
                    
                case "LowerJointLimit":
                    numericUpDownId.Value = 1;
                    labelId.Text = "axis:";
                    break;
                case "UpperJointLimit":
                    numericUpDownId.Value = 1;
                    labelId.Text = "axis:";
                    break;
                case "MagOffset":
                    numericUpDownId.Value = 1;
                    labelId.Text = "axis:";
                    break;
                case "MagScale":
                    numericUpDownId.Value = 1;
                    labelId.Text = "axis:";
                    break;





            }
        }

        private void button_Click(object sender, EventArgs e)
        {
                eventHandler?.Invoke(sender, new EventArgs());
        }
    }
}
