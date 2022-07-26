using System;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using System.IO.Ports;
using System.Threading;
using Newtonsoft.Json.Linq;
using System.IO.Pipes;
using System.IO;
using System.Globalization;
using System.Text.RegularExpressions;

namespace DarcMonitor
{
    public partial class FormDarc1000 : Form
    {
        double dir = -1;
        double[] offsetVal = { 2759, 931 }; // { 1727, 2369 };
        int index = 0;
        Kinematik Robot =new Kinematik();
        StreamWriter logFile = null;


        // Defines for State
        [Flags] enum State : ushort
        {
            DRIVEBit   =   0x0001, // Drives on off
            STOPBit   =    0x0004, // Interpolation should Stop immediately
            COLLISIONBit = 0x0008, // Collision?
            ACTIVBit  =    0x0010, // Job is Running
            ERRORBit	=  0x0020, // An Error Ocured
            CANCELBit	=  0x0040, // An Job was Canceled												 
            REFERENCEBit = 0x0100, // Drive is Refferenced
            POSITION_OutBit =  0x0200, // Is Out
            POSITION_InBit= 0x0400 //Is In
        };

        private delegate DMessage SafeCallDelegate(string text, object data);


        bool bAutoScroll = true;
        bool bRawText = true;
        bool bSendJson = true;
        bool bCanMsg = false;
        bool bDegrees = true;
        bool bInterpolationMsg = true;
        bool bLog = true;


        double DEG = 180.0 / Math.PI;
        public FormDarc1000()
        {
            InitializeComponent();
            labelVersion.Text="Version: "+   (typeof(FormDarc1000).Assembly.GetName().Version).ToString();
        }
/// <summary>
/// processes the Text and Writes teh Message
/// </summary>
/// <param name="text"></param>
/// <param name="data"></param>
        public DMessage Process_andWriteTextSafe(string text, object data)
        {
            if (rtb_Monitor.InvokeRequired)
            {
                var d = new SafeCallDelegate(Process_andWriteTextSafe);
                rtb_Monitor.Invoke(d, new object[] { text, data });
            }
            else
            {
                DMessage msg = ProcessText( text,  data);
                if(bRawText)
                    WriteTextSafe(text+"\n", Color.DarkBlue);
                else
                 WriteTextSafe(msg.Line, msg.Col);
                return msg;
            }
            return null;
            
        }


        /// <summary>
        /// write a Text in Ritch Text Control
        /// </summary>
        /// <param name="text"> Text to Write</param>
        /// <param name="data">Color of Text to Write</param>
        /// <returns>the Text</returns>
        public DMessage WriteTextSafe(string text,object data )
        {
            Color color = Color.Black;
            if (rtb_Monitor.InvokeRequired)
            {
                var d = new SafeCallDelegate(WriteTextSafe);
                rtb_Monitor.Invoke(d, new object[] { text, data });
            }
            else
            {
                try
                {
                    if (data != null)
                        color = (Color)data;
                    if (bLog)
                    {
                        if (logFile == null)
                            logFile = new StreamWriter(saveFileDialogLog.FileName);

                        logFile.WriteLine(text);
                    }
                    else if (logFile != null)
                    {
                        logFile.Close();
                        logFile = null;
                    }

                    if (!bCanMsg && text.StartsWith("$ "))
                        return new DMessage(text);
                    else if (!bInterpolationMsg && text.StartsWith("* "))
                        return new DMessage(text); ;

                    rtb_Monitor.SelectionStart = rtb_Monitor.TextLength;
                    rtb_Monitor.SelectionLength = 0;
                    rtb_Monitor.SelectionColor = color;
                    rtb_Monitor.AppendText(text.Replace('\0', ' '));
                    rtb_Monitor.SelectionColor = rtb_Monitor.ForeColor;



                    if (bAutoScroll)
                    {
                        rtb_Monitor.SelectionStart = rtb_Monitor.Text.Length;
                        rtb_Monitor.ScrollToCaret();
                    }
                }
                catch (Exception)
                {
                    //todo
                }

            }
            return new DMessage(text); 
        }


        /// <summary>
        /// processes teh State and set the flags
        /// </summary>
        /// <param name="text"></param>
        /// <param name="scal"></param>
       String  ProcessControlState(String text, double scal)
        {
            ushort value;
            double d;
            String sMsg=text;
            try
            {
                text = Regex.Replace(text, "\\s+", " ");
                string[] hexValuesSplit = text.Split(' ');
                value = (ushort)Convert.ToInt16(hexValuesSplit[0], 16);
                checkBoxPowerOn.Checked = ((value & (ushort)State.DRIVEBit) > 0);
                checkBoxInOut.CheckState = CheckState.Indeterminate;
                if ((value & (ushort)State.POSITION_OutBit) > 0)
                    checkBoxInOut.CheckState = CheckState.Checked;
                if ((value & (ushort)State.POSITION_InBit) > 0)
                    checkBoxInOut.CheckState = CheckState.Unchecked;
                sMsg = hexValuesSplit[0] + " ";
                checkBoxError.Checked = ((value & (ushort)State.ERRORBit) > 0);
                    if (Double.TryParse(hexValuesSplit[1], NumberStyles.Number, CultureInfo.InvariantCulture, out d)) {
                        labelAngle_w1.Text = "w1: " + (scal * d).ToString("N4");
                    sMsg += (scal * d).ToString("N4")+  " ";
                    // targetControl1.setnumericUpDown(1, d);
                }
                    if (Double.TryParse(hexValuesSplit[2], NumberStyles.Number, CultureInfo.InvariantCulture, out d))
                    {
                        labelAngle_w2.Text = "w2: " + (scal * d).ToString("N4");
                        sMsg += (scal * d).ToString("N4") + "    ";
                    // targetControl1.setnumericUpDown(2, d);
                }
                if (Double.TryParse(hexValuesSplit[3], NumberStyles.Number, CultureInfo.InvariantCulture, out d))
                {
                    labelAngle_w1.Text = "w1: " + (scal * d).ToString("N4");
                    sMsg += (scal * d).ToString("N4") + " ";
                    // targetControl1.setnumericUpDown(1, d);
                }
                if (Double.TryParse(hexValuesSplit[4], NumberStyles.Number, CultureInfo.InvariantCulture, out d))
                {
                    labelAngle_w1.Text = "w1: " + (scal * d).ToString("N4");
                    sMsg += (scal * d).ToString("N4");
                    // targetControl1.setnumericUpDown(1, d);
                }



            }
            catch (Exception ex)
            {
                WriteTextSafe(ex.Message, Color.Red);
            }
            return sMsg;
        }

/// <summary>
/// Process Text 
/// reead line and interpretates the Context
/// </summary>
/// <param name="text">Text to process</param>
/// <param name="data">aditional Information of null</param>
/// <returns></returns>
private DMessage ProcessText(string text, object data)
        {
            //string processedText = text;
            DMessage msg = new DMessage(text+"\n");
            double d;
            int k = 0;
            int j = 0;
            uint value;
           // double dvalue;
            double scal = dir;
            if (bDegrees)
                scal *= DEG;
            String Jtext = "";
            if (rtb_Monitor.InvokeRequired)
            {
                var di = new SafeCallDelegate(ProcessText);
                rtb_Monitor.Invoke(di, new object[] { text, data });
            }
            else
            {
                
                if (text.StartsWith("#"))
                {
                    if (text.StartsWith("#State="))
                    {
                        labelError.Text = "...";
                        ProcessControlState(text.Substring(text.IndexOf("=") + 2), scal);
                    }
                    else if (text.StartsWith("#Error"))
                    {
                        labelError.Text = text.Substring(text.IndexOf(":") + 2);
                        msg.Col = Color.Red;
                    }
                    else if (text.StartsWith("#Warning"))
                    {
                        labelError.Text = text.Substring(text.IndexOf(":") + 2);
                        msg.Col = Color.DarkRed;
                    }

                    else if (text.StartsWith("#DrivePosition="))
                    {
                        String[] words = text.Substring(text.IndexOf("=") + 2).Split(' ');
                        Decimal deci = 0;
                        if (Decimal.TryParse(words[0], NumberStyles.Number, CultureInfo.InvariantCulture, out deci))
                            targetControl1.setnumericUpDown(2, (double)deci);
                        if (Decimal.TryParse(words[1], NumberStyles.Number, CultureInfo.InvariantCulture, out deci))
                            targetControl1.setnumericUpDown(2, (double)deci);
                    }
                    else if (text.StartsWith("#Target="))
                    {
                        labelTarget.Text = "Target: " + text.Substring(text.IndexOf("=") + 1);
                    }
                    else if (text.StartsWith("#MagnetEncoder="))
                    {
                        try
                        {
                            String[] words = text.Substring(text.IndexOf("=") + 2).Split(' ');
                            Decimal deci = 0;
                        //    if (Decimal.TryParse(words[0], NumberStyles.Number, CultureInfo.InvariantCulture, out deci))
                        //    targetControl1.setnumericUpDown(1, (double)deci);
                        //    if (Decimal.TryParse(words[1], NumberStyles.Number, CultureInfo.InvariantCulture, out deci))
                        //    targetControl1.setnumericUpDown(2, (double)deci);
                        }
                        catch (Exception ex)
                        {
                            WriteTextSafe(ex.Message, Color.Red);



                        }
                    }

                    else if (text.StartsWith("#Override="))
                    {
                        label2.Text = "Override: " + text.Substring(text.IndexOf("=") + 1);
                    }
                    else if (text.StartsWith("#TCP="))
                    {
                        label3.Text = "Tcp: " + text.Substring(text.IndexOf("=") + 1);
                    }
                    else if (text.StartsWith("#OutTarget="))
                    {
                        label4.Text = "OutTarget: " + text.Substring(text.IndexOf("=") + 1);
                    }
                    else if (text.StartsWith("#CollisionMode="))
                    {
                        label6.Text = "CollisionMode: " + text.Substring(text.IndexOf("=") + 1);
                    }
                    else if (text.StartsWith("#InterpolationMode="))
                    {
                        label7.Text = "InterpolationMode: " + text.Substring(text.IndexOf("=") + 1);
                    }
                    else if (text.StartsWith("#Target="))
                    {
                        labelTargetindex.Text = "Target: " + text.Substring(text.IndexOf("=") + 1);
                        labelTarget.Text = "Target: " + text.Substring(text.IndexOf("=") + 1);
                    }
                    else if (text.StartsWith("#TargetPos"))
                    {
                        String[] str = text.Substring(text.IndexOf("=")+1).Trim().Split(' ');
                        if (double.TryParse(str[0], NumberStyles.Number, CultureInfo.InvariantCulture,  out d))
                           targetControl1.setnumericUpDown(1, d);
                        if (double.TryParse(str[1], NumberStyles.Number, CultureInfo.InvariantCulture,  out d))
                            targetControl1.setnumericUpDown(2, d);

                    }

                }
                else if (text.StartsWith("0x"))
                {
                        msg.Line = ProcessControlState(text, scal) +"\n";
                }
                else if (text.StartsWith("{"))
                {
                    try
                    {
                        int i = 0, br = 0;
                        for (i = 0; i < text.Length; i++)
                        {
                            if (text[i] == '{') br++;
                            else if (text[i] == '}') br--;
                            if (br == 0)
                            {
                                Jtext = text.Substring(0, i + 1); break;
                            }
                        }

                        var cmd = JObject.Parse(Jtext);
                    }
                    catch (Exception ex)
                    {

                    }
                }
                else
                {
                    if (text.StartsWith("$ "))
                    {

                    }
                    else if (text.StartsWith("* "))
                    {
                     //   processedText = processMotion(text);
                          msg.Line = processMotion(text);
                    }
                }
            }
            return msg; // processedText+ "\n";
        }
        String processMotion(String text)
        {
            int k = 0;
            double d, dvalue;
            double scal = dir;
            if (bDegrees)
                scal *= DEG;

            String processedText = "* ";
            string[] sData = text.Split(' ');
            Int32 val;
            foreach (String str in sData)
            {
                if (str.StartsWith("0x"))
                {
                    if (Int32.TryParse(str.Replace("0x", ""), NumberStyles.HexNumber, CultureInfo.InvariantCulture, out val))
                    {
                        processedText += str + " ";
                        checkBoxPowerOn.Checked = ((val & 0x01) > 0);
                    }
                }
                else if (Double.TryParse(str, NumberStyles.Number, CultureInfo.InvariantCulture, out d))
                {
                    switch (++k)
                    {
                        case 1:
                        case 2:
                        case 3:
                        case 4:
                            if (d < -10 || d > 10)
                                dvalue = 0;
                            else
                                dvalue = d * scal;
                            grahControl.setValue(k - 1, index, dvalue);
                            if (k == 1)
                                labelAngle_w1.Text = "w1: " + dvalue.ToString("N4");
                            else if (k == 2)
                                labelAngle_w2.Text = "w2: " + dvalue.ToString("N4");

                            processedText += (d * scal).ToString("N4") + " ";
                            break;
                        case 5:
                            if (grahControl.bEncoder)
                            {
                                dvalue = d * scal;
                                labelAngle_w1.Text = "w1: " + dvalue.ToString("N4");
                                grahControl.setValue(k - 1, index, dvalue);
                            }
                            processedText += (d * scal).ToString("N4") + " ";
                            break;

                        case 6:
                            if (grahControl.bEncoder)
                            {
                                dvalue = d * scal;
                                labelAngle_w2.Text = "w2: " + dvalue.ToString("N4");
                                grahControl.setValue(5, index, dvalue);
                            }
                            processedText += (d * scal).ToString("N4") + " ";

                            break;
                        case 7:
                            if (grahControl.bMagnet)
                            {
                                dvalue = d * scal;
                                label_magnet_w1.Text = "w1: " + dvalue.ToString("N4");
                                grahControl.setValue(6, index, dvalue);
                            }
                            processedText += (d * scal).ToString("N4") + " ";

                            break;
                        case 8:
                            if (grahControl.bMagnet)
                            {
                                dvalue = d * scal;
                                label_magnet_w2.Text = "w2: " + dvalue.ToString("N4");
                                grahControl.setValue(7, index, dvalue);

                            }
                            processedText += (d * scal).ToString("N4") + " ";

                            break;
                    }



                    if (k >= 16)
                        break;

                }
            }
            if (k >= 7)
            {
                index++;
                if (index >= 1024)
                    index = 0;

            }
            return processedText + "\n";
        }


    private void backgroundWorkerMonitor_DoWork(object sender, DoWorkEventArgs e)
        {
            try
            {
                using (StreamWriter writer = File.CreateText(@"LogDARC.txt"))
                {
                    for (; ; )
                    {

                        if (serialPortTeensy.IsOpen)
                        {
                            String line = serialPortTeensy.ReadLine();
                            Process_andWriteTextSafe(line.Trim(), null);
                            /*
                            if (bRawText)
                            {
                                DMessage msg = ProcessText(line.Trim(), null);
                                WriteTextSafe(line, Color.Black );
                            }
                            else
                            {
                                DMessage msg = ProcessText(line, null);
                                WriteTextSafe(msg.Line, msg.Col);
                            }*/
                            writer.Write(line);

                        }
                    }
                }
            }
            catch (Exception ex) {
                    WriteTextSafe(ex.Message, Color.DarkRed);
                }

        }
       
        private void buttonReset_Click(object sender, EventArgs e)
        {
            Button btn = sender as Button;
            // initData();
            this.Text = "DARC 1000 Monitor";
            grahControl.initData();
            if (btn.Text == "Reset")
            {
                rtb_Monitor.AppendText("Reset\n");
                comboBoxCOM.Text = "offline";
            }
            else
                rtb_Monitor.AppendText("Connect " + comboBoxCOM.Text + "\n");

            if (comboBoxCOM.Text == "DARC1000Socket")
            {
                if (backgroundWorkerSocket.IsBusy)
                {
                    if (listener.Connected)
                    {
                        backgroundWorkerSocket.CancelAsync();
                        Thread.Sleep(100);
                    }
                    else
                    {
                        listener.Close();
                    }
                    
                }
                else
                {
                    backgroundWorkerSocket.RunWorkerAsync();
                    this.Text = "DARC 1000 Monitor (Socket)";
                }
            }
            else if (comboBoxCOM.Text.StartsWith("file"))
            {
                if (backgroundWorkerFile.IsBusy)
                {

                         backgroundWorkerFile.CancelAsync();
                        Thread.Sleep(100);
                    }
                    
                
                else
                    backgroundWorkerFile.RunWorkerAsync();

            }
            else if (comboBoxCOM.Text == "offline")
            {
                rtb_Monitor.Clear();
                rtb_Monitor.AppendText("Reset\n");


                if (serialPortTeensy.IsOpen) 
                { 
                serialPortTeensy.Close();
                }
                if (backgroundWorkerPipe.IsBusy)
                {
                    backgroundWorkerPipe.CancelAsync();
                }
                if (backgroundWorkerSocket.IsBusy)
                {
                    if (listener.Connected)
                    {
                        backgroundWorkerSocket.CancelAsync();
                        Thread.Sleep(100);
                    }
                    else
                    {
                        listener.Close();
                    }
                }

            }

            else if (comboBoxCOM.Text == "DARC1000Pipe")
            {
                if (backgroundWorkerPipe.IsBusy)
                {

                    backgroundWorkerPipe.CancelAsync();
                    Thread.Sleep(100);
                }
                else
                {
                    backgroundWorkerPipe.RunWorkerAsync();
                    this.Text = "DARC 1000 Monitor (Pipe)";

                }
            }
            else if (backgroundWorkerMonitor.IsBusy)
            {
                backgroundWorkerMonitor.CancelAsync();
                Thread.Sleep(100);
            }
            else
            {
                backgroundWorkerMonitor.RunWorkerAsync();
            }

           grahControl.updateChart();
            timerUpdate.Start();

            try
            {
                if (serialPortTeensy.IsOpen)
                    serialPortTeensy.Close();
                String com = comboBoxCOM.Text;
                if (com.ToLower().StartsWith("com")){
                    serialPortTeensy.PortName = com;
                    if (radioButtonSlow.Checked)
                        serialPortTeensy.BaudRate = 9600;
                    else
                        serialPortTeensy.BaudRate = 115000;
                    serialPortTeensy.Open();
                    this.Text = "DARC 1000 Monitor (" + com + ")";
                }
                //this.Text = "DARC 1000 Monitor";


            }
            catch (Exception ex)
            {
                WriteTextSafe("Could not open Com port "+ ex.Message, Color.Red);
            }
        }

       

private void comboBoxCOM_DropDown(object sender, EventArgs e)
        {
            string[] comports = SerialPort.GetPortNames();
            comboBoxCOM.Items.Clear();
            comboBoxCOM.Items.Add("DARC1000Socket");
            comboBoxCOM.Items.Add("DARC1000Pipe");
            comboBoxCOM.Items.AddRange(comports);
            comboBoxCOM.Items.Add("file");
            comboBoxCOM.Items.Add("offline");
            /*
            using (var searcher = new ManagementObjectSearcher
                 ("SELECT * FROM WIN32_SerialPort"))
            {
                string[] portnames = SerialPort.GetPortNames();
                var ports = searcher.Get().Cast<ManagementBaseObject>().ToList();
                var tList = (from n in portnames
                             join p in ports on n equals p["DeviceID"].ToString()
                             select n + " - " + p["Caption"]).ToList();
               
                foreach(String com in tList)
                    comboBoxCOM.Items.Add(com); 
           
            }
             */
        }

        private void checkBox_CheckedChanged(object sender, EventArgs e)
        {
            CheckBox box = sender as CheckBox;
            switch (box.Text) {
                case "CANmsg":
                    bCanMsg = box.Checked;
                    break;
                case "Interpolationmsg":
                    bInterpolationMsg = box.Checked;
                    break;

                case "AutoScroll":
                    bAutoScroll = box.Checked;
                    break;
                case "RawText":
                    bRawText = box.Checked;
                    break;
                case "Send as Json":
                    bSendJson = box.Checked;
                    break;
                case "Degrees":
                    bDegrees = box.Checked;
                    break;
                case "LogData":
                    bLog = box.Checked;
                    break;



            }
        }

        
        private void backgroundWorkerPipe_DoWork(object sender, DoWorkEventArgs e)
        {
            StreamReader streamReader;
            StreamWriter writer;
            NamedPipeServerStream namedPipeServer;

            try
            {
                namedPipeServer = new NamedPipeServerStream("DARC1000Pipe", PipeDirection.InOut, 1, PipeTransmissionMode.Byte);
                namedPipeServer.WaitForConnection();
                streamReader = new StreamReader(namedPipeServer);
                writer = new StreamWriter(namedPipeServer);
            }
            catch (Exception ex)
            {

                throw;
            }       

            while (!backgroundWorkerPipe.CancellationPending)
            {
                try
                {
                    String line = streamReader.ReadLine();
                    if (line == null)
                        break;
                    Process_andWriteTextSafe(line.Replace('\0', ' ').Trim(), null);
                    /*
                    if (bRawText)
                    {
                        DMessage msg = ProcessText(line.Replace('\0', ' ').Trim(), null);
                        WriteTextSafe(line + "\n", Color.Blue );
                    }
                    else
                    {
                        DMessage msg = ProcessText(line.Replace('\0', ' ').Trim(), null);
                        WriteTextSafe(msg.Line + "\n", msg.Col);

                    }

                    */
                }
                catch (Exception ex)
                {

                    WriteTextSafe("Error Broken Pipe", Color.Red);
                    namedPipeServer.Close();
                    break;
                }

                //   writer.WriteLineAsync("Hello again from c#");

                // writer.Write("c#\r\n\0");
                // writer.Write((char)0);
                // writer.Flush();
                // Thread.Sleep(10);

            }
            namedPipeServer.Dispose();
           
            e.Cancel = true;

        }

        /// <summary>
        /// sends a comman to Serial Port
        /// used for Simulating the Communication controller
        /// </summary>
        /// <param name="sender">Sender who sends the Command</param>
        /// <param name="e">Evendtargs or CommandEventArgs</param>
        private void send_Command(object sender, EventArgs e)
        {
            CommandEventArgs ec =  e as CommandEventArgs;
            if (ec != null)
            {
                if (serialPortTeensy.IsOpen)
                    serialPortTeensy.Write(ec.Cmd + '\0');
                WriteTextSafe(ec.Cmd + "\r\n", Color.SandyBrown);
            }
        }

        /// <summary>
        /// comand soll abgesetzt werden
        /// </summary>
        /// <param name="sender">Button der das Commando abgesetzt hat</param>
        /// <param name="e"></param>
        private void buttonCmd_Click(object sender, EventArgs e)
        {
            Button button = sender as Button;
            CommandEventArgs ec= e as CommandEventArgs;
            decimal nd = targetControl1.getTarget();
            String jsonCmd = "{}";
            String sCmd = " ";
            String subcmd = "";
            String[] strc=null;
            int id;
            switch (button.Text)
            {
                case "cmd":
                    String sTcmd = textBoxCmd.Text.Trim();
                    labelError.Text = "...";
                    if (sTcmd.StartsWith("{"))
                        jsonCmd = sTcmd + "\r\n";
                    else
                    {
                        if (sTcmd.IndexOf('%') > 0)
                        {
                            strc = sTcmd.Split('%');
                            sTcmd = strc[0].Trim();

                        }
                        sTcmd = Regex.Replace(sTcmd, "\\s+", " ");
                        sTcmd = Regex.Replace(sTcmd, "\\s*[=]\\s*", "=");
                        sCmd = sTcmd + "\r\n";

                        String[] str = sTcmd.Split(' ');
                        if (str.Length == 1) {
                            if (str[0].Length < 1) {
                                jsonCmd = @"{'cmd':'R', 'sub':'0'";
                                sCmd = "R0\r\n";
                                    }
                            else
                                jsonCmd = @"{'cmd':'" + str[0] + "', 'sub':'0'";
                        }
                        else if (str.Length == 2)
                            jsonCmd = @"{'cmd':'" + str[0] + "', 'sub':'" + str[1] + "'";
                        else if (str.Length == 3)
                            if (Int32.TryParse(str[2], out id))
                                jsonCmd = @"{'cmd':'" + str[0] + "', 'sub':'" + str[1] + "', 'id':'" + str[2] + "'";
                            else
                            {
                                String[] stre = str[2].Split('=');
                                if (stre.Length > 1)
                                    jsonCmd = @"{'cmd':'" + str[0] + "', 'sub':'" + str[1] + "', 'value':'" + stre[0] + "', 'id':'" + stre[1] + "'";
                                else
                                    jsonCmd = @"{'cmd':'" + str[0] + "', 'sub':'" + str[1] + "', 'value':'" + str[2] + "'";
                            }

                        else
                        {
                            jsonCmd = @"{'cmd':'" + str[0] + "'";
                            int i = 1;
                            if (str[1].Any(char.IsLower))
                                jsonCmd += @",'sub':'" + str[i++] + "'";
                            jsonCmd += ", 'data':[";
                            for (; i < str.Length - 1; i++)
                            {
                                jsonCmd += "'" + str[i] + "', ";
                            }
                            jsonCmd += "'" + str[i] + "'";
                            jsonCmd += "]";

                        }
                        if (strc != null)
                            jsonCmd += ", 'comment':'" + strc[1] + "'";
                        jsonCmd += "}";


                    }

                    break;
                case "go In":
                    sCmd = "I\r\n";
                    sCmd = "H\r\n";
                    jsonCmd = @"{'cmd':'H', 'sub':'0' }";
                    break;
                case "go Out":
                    sCmd = "O\r\n";
                    sCmd = "B\r\n";
                    jsonCmd = @"{'cmd':'B', 'sub':'0' }";
                    break;
                case "Power on":
                    sCmd = "P\r\n";
                    jsonCmd = @"{'cmd':'P', 'sub':'0' }";
                    break;
                case "Power off":
                    sCmd = "S\r\n";
                    jsonCmd = @"{'cmd':'S', 'sub':'0' }";
                    break;
                case "Save EE":
                    sCmd = "E s\r\n";
                    jsonCmd = @"{'cmd':'E', 'sub':'s' }";
                    break;
                case "Reset Zero":
                    sCmd = "R z\r\n";
                    jsonCmd = @"{'cmd':'R', 'sub':'z' }";
                    break;
                case "Reset System":
                    sCmd = "R s " + nd + "\r\n";
                    jsonCmd = @"{'cmd':'R', 'sub':'m', 'id':'" + nd + "' }";
                    break;
                case "Reset Magnet":
                    sCmd = "R m " + nd + "\r\n";
                    jsonCmd = @"{'cmd':'R', 'sub':'m', 'id':'" + nd + "' }";
                    break;


                case "Set CAN":
                    if (ec != null)
                    {
                        sCmd = ec.Cmd;
                        jsonCmd = ec.JSONCmd;
                    }
                    ;
/*
                    String stc = comboBoxSetCAN.Text;  // CANID ist immer Hex
                    sTcmd = comboBoxData.Text.Trim().ToLower();
                    sTcmd = Regex.Replace(sTcmd, "\\s+", " ");
                    comboBoxData.Items.Add(sTcmd);
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
*/
                    break;

                case "Set Debug":
                    String std = serviceControl1.comboBoxSetDebug.Text;
                    sCmd = "W d " + std + "=" + serviceControl1.textBoxSetDebug.Text + "\r\n";
                    jsonCmd = @"{'cmd':'W', 'sub':'d' , 'value':'" + std + "', 'data':'" + serviceControl1.textBoxSetDebug.Text + "'}";

                    break;
                case "Set Value":
                    String stv = serviceControl1.comboBoxSetValue.Text;
                    decimal n = serviceControl1.numericUpDownId.Value;
                    // u v w   0, 1 2 
                    switch (n) {
                        case 0:
                            subcmd = "u"; break;
                        case 1:
                            subcmd = "v"; break;
                        case 2: 
                            subcmd = "w"; break;
                    }
                    sCmd = "W "+subcmd+" "+ stv + "=" + serviceControl1.numericSetValue.Value + "\r\n";
                    if(checkBoxHexSet.Checked)
                   jsonCmd = @"{'cmd':'W', 'sub':'" + subcmd + "' , 'value':'" + stv + "', 'id':'0x" + serviceControl1.numericSetValue.Value + "'}";
                    else
                    jsonCmd = @"{'cmd':'W', 'sub':'" + subcmd + "' , 'value':'" + stv + "', 'id':'" + serviceControl1.numericSetValue.Value + "'}";
                    break;
                case "Get Value":
                    String stg = serviceControl1.comboBoxGetValue.Text;
                    sCmd = "G v " + stg + "\r\n";
                    jsonCmd = @"{'cmd':'G', 'sub':'v' , 'value':'" + stg + "'}";
                    break;
                case "Stop":
                    sCmd = "S\r\n";
                    jsonCmd = @"{'cmd':'S', 'sub':'0' }";
                    break;
                case "Continue":
                    sCmd = "R c\r\n";
                    jsonCmd = @"{'cmd':'R', 'sub':'c' }";
                    break;
                case "Teach Target":
                    sCmd = "K s " + nd + "\r\n";
                    jsonCmd = @"{'cmd':'K', 'sub':'s', 'id':'" + nd + "' }";
                    break;
                case "Goto Target":
                    sCmd = "K g " + nd + "\r\n";
                    jsonCmd = @"{'cmd':'K', 'sub':'g', 'id':'" + nd + "' }";
                    break;
                case "Get Target":
                    sCmd = "K r " + nd + "\r\n";
                    jsonCmd = @"{'cmd':'K', 'sub':'r', 'id':'" + nd + "' }";
                    break;

                case "Turn Target":
                case "Move Target":
                case "Set Target":
                    if (targetControl1.checkBoxWorld.Checked)
                        targetControl1.checkBoxWorld.Checked = false;

                    double w1 = (double)targetControl1.getnumericUpDown(1);
                    double w2 = (double)targetControl1.getnumericUpDown(2);
                    double dw = (double)targetControl1.getnumericUpDown(3);
                    double vel = (double)targetControl1.getnumericUpDown(4);
                  //  double vel = (double)numericUpDown3.Value;
                    //                    checkBoxWorld_CheckedChanged(checkBoxWorld, null);
                    if (targetControl1.checkBoxDeg.Checked)
                    {
                        w1 *= Math.PI / 180;
                        w2 *= Math.PI / 180;
                        dw *= Math.PI / 180;
                    }
                    if (button.Text == "Move Target")
                    {
                        if (targetControl1.checkBoxWorld.Checked)
                            subcmd = "k";
                        else
                            subcmd = "a";
                        sCmd = "K " + subcmd + " " + nd + " " + w1.ToString("0.00", CultureInfo.InvariantCulture) + " " + w2.ToString("0.00", CultureInfo.InvariantCulture) + " " + vel.ToString("0.00", CultureInfo.InvariantCulture) + "\r\n";
                        jsonCmd = @"{'cmd':'K', 'sub':'" + subcmd + "', 'id':'" + nd + "', 't1':'" + w1.ToString("0.00", CultureInfo.InvariantCulture) + "', 't2':'" + w2.ToString("0.00", CultureInfo.InvariantCulture) + "', 'v':'" + vel.ToString("0.00", CultureInfo.InvariantCulture) + "' }";

                    }
                    else if (button.Text == "Set Target") {
                        if (targetControl1.checkBoxWorld.Checked)
                            subcmd = "w";
                        else
                            subcmd = "j";
                        sCmd = "K " + subcmd + " " + nd + " " + w1.ToString("0.00", CultureInfo.InvariantCulture) + " " + w2.ToString("0.00", CultureInfo.InvariantCulture) + " " + vel.ToString("0.00", CultureInfo.InvariantCulture) + "\r\n";
                        jsonCmd = @"{'cmd':'K', 'sub':'" + subcmd + "', 'id':'" + nd + "', 't1':'" + w1.ToString("0.00", CultureInfo.InvariantCulture) + "', 't2':'" + w2.ToString("0.00", CultureInfo.InvariantCulture) + "', 'v':'" + vel.ToString("0.00", CultureInfo.InvariantCulture) + "' }";

                    }
                    else
                    {
                        if (targetControl1.checkBoxAbsolut.Checked)
                        {
                            subcmd = "g";
                            int icmd = (int)Math.Round(dw * 180.0 / Math.PI);
                            sCmd = "K " + subcmd + " " + icmd + "\r\n";
                            jsonCmd = @"{'cmd':'K', 'sub':'" + subcmd + "',  'id1':'" + icmd+"' }";

                        }
                        else
                        {
                            subcmd = "t";
                            sCmd = "K " + subcmd + " " + nd + " " + dw.ToString("0.00", CultureInfo.InvariantCulture) + "\r\n";
                            jsonCmd = @"{'cmd':'K', 'sub':'" + subcmd + "', 'id':'" + nd + "', 't1':'" + dw.ToString("0.00", CultureInfo.InvariantCulture) + "' }";

                        }

                    }



                    break;

                case "MoveTarget":
                    //sCmd = "T " + nd + "\r\n";
                    //jsonCmd = @"{'cmd':'T', 'sub':'" + nd + "' }";
                    sCmd = "K m " + nd + "\r\n";
                    jsonCmd = @"{'cmd':'K', 'sub':'m', 'id':'" + nd + "' }";
                    break;

                case "Color":
                  //  Color color = DARC1000colorDialog.Color;
                  //  sCmd = "C " + color.R + " " + color.G + " " + color.B + "\r\n";
                  //  jsonCmd = @"{'cmd':'C', 'sub':'0', 'data':['" + +color.R + "', '" + color.G + "', '" + color.B + "'] }";
                    break;
                case "Sound On":
                    sCmd = "N p\r\n";
                    jsonCmd = @"{'cmd':'N', 'sub':'p'}";
                    break;
                case "Sound Off":
                    sCmd = "L n\r\n";
                    jsonCmd = @"{'cmd':'L', 'sub':'m'}";
                    break;
                case "Run Script":
                case "Step":
                    String jc = "", js = "", ji = "", buf1 = "", buf2 = "", buf3 = "";
                    id = 1;
                    if (scriptControl1.labelStep.Text.Trim().StartsWith("{")) { 
                        jsonCmd = scriptControl1.labelStep.Text + "\r\n";
                    var cmd = JObject.Parse(jsonCmd);
                    JToken scm = cmd["cmd"];
                    if (scm != null) jc = cmd.Value<String>("cmd");
                    JToken ssu = cmd["sub"];
                    if (ssu != null) js = cmd.Value<String>("sub");

                    JToken sid = cmd["id"];
                    if (sid != null) ji = cmd.Value<String>("id");
                    sCmd = jc + " " + js + " " + ji + "\r\n";
                     }
                     else {
                        sCmd = scriptControl1.labelStep.Text.Trim();
                        if (sCmd.IndexOf('%') > 0)
                        {
                            strc = sCmd.Split('%');
                            sCmd = strc[0].Trim();

                        }
                        if (sCmd.Length < 1)
                            return;

                        foreach (char ch in sCmd)
                        {
                            if (id ==1 && Char.IsLower(ch))
                            {
                                js = ch.ToString();
                                id = 2;
                            }
                            else if ((id == 2 || id == 3) && Char.IsDigit(ch))
                            {
                                buf1 += ch;
                                id = 3;
                            }
                            else if ((id == 2 || id == 3) && Char.IsLetter(ch))
                            {
                                buf2 += ch;
                                id = 3;
                            }

                            else if (id == 3 &&  Char.IsWhiteSpace(ch))
                            {
                                id = 4;
                                
                            }
                            else if (id==4 && (Char.IsNumber(ch) || Char.IsPunctuation(ch)  || Char.IsWhiteSpace(ch)))
                                buf3 += ch;


                        }
                        sCmd += "\r\n";
                        jsonCmd = @"{'cmd':'" + sCmd[0] + "', 'sub':'" + js + "'";
                        if (buf1.Length > 0)
                            jsonCmd += @", 'id':'" + buf1 + "'";
                        if (buf2.Length>0)
                        jsonCmd += @", 'value':'" + buf2 + "'";
                        if (buf3.Length > 0)
                            jsonCmd += @", 'data':[" + buf3 + "]";
                        if (strc != null)
                            jsonCmd += ", 'comment':'" + strc[1] + "'";
                        jsonCmd += "}";
                    }

                    break;
            }

            //rtb_Monitor.AppendText(button.Text+"\n");
            WriteTextSafe(button.Text + " ==> " + jsonCmd + "  " + sCmd, Color.DarkBlue);
            if (bSendJson)
                SendText = jsonCmd;
            else
                SendText = sCmd;
            
            if(button.Text != "Step" && button.Text != "Run Script")
             scriptControl1.recordCommand(SendText);

            try
            {
                if (serialPortTeensy.IsOpen)
                {
                    if (bSendJson)
                    {
                        var cmd = JObject.Parse(jsonCmd);
                        serialPortTeensy.WriteLine(jsonCmd);
                    }
                    else
                        serialPortTeensy.WriteLine(sCmd);
                }
            }catch (Exception ex)
            {
                WriteTextSafe(ex.Message, Color.DarkRed);
            }
        }


      
        private void buttonClear_Click(object sender, EventArgs e)
        {
           rtb_Monitor.Clear();
           grahControl.updateChart();
        }
      


        private void backgroundWorkerFile_DoWork(object sender, DoWorkEventArgs e)
        {
            try
            {
                String filename = @"LogDARC.txt";
                if(openFileDialogLog.CheckFileExists)
                  filename = openFileDialogLog.FileName;
               using (StreamReader reader = File.OpenText(filename))
                {
                    while (!backgroundWorkerFile.CancellationPending)
                    {
                        if (!reader.EndOfStream)
                        {
                            String line = reader.ReadLine();
                            if (line != null)
                            {
                                ProcessText(line, null);
                                if (line.Length > 0) line += "\r\n\0";
                                WriteTextSafe(line, Color.DarkGreen);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                WriteTextSafe(ex.Message, Color.DarkRed);
            }
        }

        
        private void buttonFile_Click(object sender, EventArgs e)
        {
            if (comboBoxCOM.Text == "file")
            {
                openFileDialogLog.ShowDialog();
                comboBoxCOM.Text = "file > " + openFileDialogLog.FileName;
            }
            else if (checkBoxLog.Checked)
            {
                saveFileDialogLog.ShowDialog();
            }
            else
            {
                saveFileDialogLog.ShowDialog();
            }
        }

        private void textBoxCmd_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == '\r')
                if (sender == textBoxCmd)
                    buttonCmd_Click(buttonCmd, e);
        }

        private void timerUpdate_Tick(object sender, EventArgs e)
        {
            grahControl.timerUpdate_Tick(sender, e);
        }

     #region FormLoad
        private void FormDarc1000_Load(object sender, EventArgs e)
        {

        }

        private void scriptControl1_Load(object sender, EventArgs e)
        {
            scriptControl1.buttonStep.Click += new System.EventHandler(this.buttonCmd_Click);
            scriptControl1.eventHandler += new System.EventHandler(this.buttonCmd_Click);
        }


        private void targetControl1_Load(object sender, EventArgs e)
        {
            targetControl1.eventHandler += new System.EventHandler(this.buttonCmd_Click);

        }

        private void serviceControl1_Load(object sender, EventArgs e)
        {
            serviceControl1.eventHandler += new System.EventHandler(this.buttonCmd_Click);
        }

        private void communicationControl1_Load(object sender, EventArgs e)
        {
            communicationControl1.eventHandler += new System.EventHandler(this.send_Command);

        }

        #endregion

        private void canControl1_Load(object sender, EventArgs e)
        {
            canControl.eventHandler += new System.EventHandler(this.buttonCmd_Click);

        }
    }
}