using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DarcMonitor
{
    public partial class CANControl : UserControl
    {
        public event EventHandler eventHandler;


        String jsonCmd = "{}";
        String sCmd;

        public CANControl()
        {
            InitializeComponent();
        }


        void scanCAN_SDO_Data(String stc, String sTcmd)
        {
            String[] sdata = sTcmd.Split('=');
            String[] msg = { "00", "00", "00", "00", "00", "00", "00", "00" };
           
            if (sdata.Length > 0)
            {
                msg[0] = "08";
                String[] iddata = sdata[0].Split(':');
                if (iddata.Length > 0)
                {
                    msg[2] = iddata[0].Substring(0, 2);
                    msg[1] = iddata[0].Substring(2, 2);
                    msg[3] = iddata[1];
                    if (sdata.Length > 1)
                    {
                        int data;

                        if (Int32.TryParse(sdata[1], out data))
                        {
                            byte[] bData = BitConverter.GetBytes(data);
                            msg[4] = bData[0].ToString("x2");
                            msg[5] = bData[1].ToString("x2");
                            msg[6] = bData[2].ToString("x2");
                            msg[7] = bData[3].ToString("x2");
                        }
                        sCmd = "W c " + stc + "  ";
                        jsonCmd = @"{'cmd':'W', 'sub':'c' ,'id':'" + stc + "','value':'" + stc + "', 'data': [";

                    }
                    else
                    {
                        sCmd = "W g " + stc + "  ";
                        jsonCmd = @"{'cmd':'W', 'sub':'g' ,'id':'" + stc + "','value':'" + stc + "', 'data': [";

                    }
                    foreach (String s in msg)
                        {
                            sCmd += "0x" + s + " ";
                        }

                        sCmd += "\r\n";


                       // jsonCmd = @"{'cmd':'W', 'sub':'c' ,'id':'" + stc + "','value':'" + stc + "', 'data': [";
                        foreach (String s in msg)
                        {
                            jsonCmd += ", '0x" + s + "'";
                        }
                        jsonCmd += "]}";
                        jsonCmd = jsonCmd.Replace("[,", "[");
                    }
              
                

            }
            

        }

        void  scanCANData(String stc, String sTcmd)
        {

            String jsonCmd ="{}";
            sCmd = "W c " + stc + " : " + sTcmd + "\r\n";
            String[] sdata = sTcmd.Split(' ');
            if (sdata.Length > 1)
            {
                jsonCmd = @"{'cmd':'W', 'sub':'c' ,'id':'" + stc + "','value':'" + stc + "', 'data': [";
                if (checkBoxHex.Checked)
                    foreach (String s in sdata)
                    {
                        if (s.StartsWith("0x"))
                            jsonCmd += ", '" + s + "'";
                        else
                            jsonCmd += ", '0x" + s + "'";
                    }
                else
                {
                    foreach (String s in sdata)
                        jsonCmd += ", '" + s + "'";
                }
                jsonCmd += "]}";
                jsonCmd = jsonCmd.Replace("[,", "[");
            }
            else
            {
                sTcmd = Regex.Replace(sTcmd, "\\s*[:]\\s*", ":");
                sTcmd = Regex.Replace(sTcmd, "\\s*[=]\\s*", "=");
                jsonCmd = @"{'cmd':'W', 'sub':'c' ,'id':" + stc + ", 'value':'" + sTcmd + "'}";
            }

        }

private void buttonSetCAN_Click(object sender, EventArgs e)
        {
            String stc = comboBoxSetCAN.Text;  // CANID ist immer Hex
            String sTcmd = comboBoxData.Text.Trim().ToLower();
            sTcmd = Regex.Replace(sTcmd, "\\s+", " ");
            comboBoxData.Items.Add(sTcmd);
            if (sTcmd.IndexOf('=') > 0)
            {
                scanCAN_SDO_Data(stc, sTcmd);
            }
            else
            {
                if (sTcmd.IndexOf(':') > 0)
                    scanCAN_SDO_Data(stc, sTcmd);
                else
                    scanCANData(stc, sTcmd);
            }
            eventHandler?.Invoke(sender, new CommandEventArgs(sCmd, jsonCmd));
        }
    }
}
