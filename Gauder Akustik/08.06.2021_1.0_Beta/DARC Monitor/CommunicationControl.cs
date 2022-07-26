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
    public partial class CommunicationControl : UserControl
    {
        public event EventHandler eventHandler;

        public CommunicationControl()
        {
            InitializeComponent();
        }

        private void communication_Command(object sender, EventArgs e)
        {
            String sCmd = "O";
            String jsonCmd = "{}";
            Button button = sender as Button;
            if (button != null)
            {
                switch(button.Text)
                {
                    case "o": sCmd = "O"; break;
                    case "Color":
                        DARC1000colorDialog.ShowDialog();
                        Color color = DARC1000colorDialog.Color;
                        sCmd = "C " + color.R + " " + color.G + " " + color.B + "\r\n";
                        jsonCmd = @"{'cmd':'C', 'sub':'0', 'data':['" + +color.R + "', '" + color.G + "', '" + color.B + "'] }";
                        break;
                    case "Sound On":
                        sCmd = "N p\r\n";
                        jsonCmd = @"{'cmd':'N', 'sub':'p'}";
                        break;
                    case "Sound Off":
                        sCmd = "L n\r\n";
                        jsonCmd = @"{'cmd':'L', 'sub':'m'}";
                        break;


                }

                //                if (serialPortTeensy.IsOpen)
                //                    serialPortTeensy.Write(cmd + '\0');
            }
            else
            {
                NumericUpDown nud = sender as NumericUpDown;
                if (nud == null)
                    return;
                int num = (int)nud.Value;
                int numabs = (num > 0) ? num : -num;

                if (radioButton1.Checked)
                {
                    if (num >= 0)
                        sCmd = "A" + "+" + numabs.ToString("0#");
                    else
                        sCmd = "A" + "-" + numabs.ToString("0#");
                }
                else if (radioButton2.Checked)
                {
                    if (num >= 0)
                        sCmd = "I" + "+" + numabs.ToString("0#");
                    else
                        sCmd = "I" + "-" + numabs.ToString("0#");
                }
                else if (radioButton3.Checked)
                {
                    if (num >= 0)
                        sCmd = "+" + numabs.ToString("0#");
                    else
                        sCmd = "-" + numabs.ToString("0#");
                }
    //            if (serialPortTeensy.IsOpen)
    //                serialPortTeensy.Write(cmd + '\0');

            }

            eventHandler?.Invoke(sender, new CommandEventArgs(sCmd, jsonCmd));

            //      if (serialPortTeensy.IsOpen)
            //                serialPortTeensy.Write(cmd + '\0');
            //          WriteTextSafe(cmd + "\r\n", Color.SandyBrown);
        }



        private void textBoxCmd_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == '\r')
           //     if (sender == textBoxCmd)
           //         buttonCmd_Click(buttonCmd, e);
                 if (sender == numericUpDownComControl)
                    communication_Command(sender, e);
            //            WriteTextSafe(textBoxCmd.Text, Color.DarkBlue);
        }

    }
}
