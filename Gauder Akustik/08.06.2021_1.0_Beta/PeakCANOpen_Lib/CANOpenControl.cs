using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Diagnostics;
using Peak.Can.Basic;
using System.Threading;

// using EcoTalk_Lib;

namespace PeakCANOpen_Lib
{


    public partial class CANOpenControl : UserControl
    {

        String CanController { get; set; } = "PCAN";

  //      PCanOpen can;
        PCanOpen can = new PCanOpen();
 // CANOpenDrive drive;
        public System.IO.StreamWriter fileData;
        public System.IO.StreamWriter fileCAN;

        int nData=0;
        double[,] logData;

        int operationalMode = 1;
        private bool bAnalog;
        private uint analogValue;
        private bool bPosition;
        private int pos;
        private int speed;
        private uint io;
        private uint state; 
        byte driveId;
        bool bAppend=false;
        long lastTime;

        bool isListBoxLocked;

        uint[] pdoIds = new uint[] { 0x180, 0x280, 0x380, 0x480, 0x580 }; // Bekannte SDO Nummern

        public CANOpenControl()
        {
            InitializeComponent();
            try
            {
                //todo  acs  drive = new CANOpenDrive(1);
                //todo  acs can = CANOpenDrive.CAN;
            }
            catch (Exception ex) { }

        //      updatePannelDelegateEvaluate = new UpdatePannelDelegateEvaluate(DisplayMessage);
        }

        /// <summary>
        /// thread der die Message ausliest
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void backgroundWorkerMessages_DoWork(object sender, DoWorkEventArgs e)
        {
            do {
                can.ReadMessages();
                Thread.Sleep(100);
            } while (true);
        }

      
        /// <summary>
        /// Zeigt die Messages in der Listbox
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="messageInfo"></param>
        public void DisplayMessage(object sender, CANMessageInfo messageInfo)
        {
            int index;
             driveId = (byte)numericUpDownDrive.Value;
            // Setzt den Status einer Message
            if (messageInfo.msg.ID == 0x580 + driveId)
            {
                if (messageInfo.msg.DATA[1] == 0x41 && messageInfo.msg.DATA[2] == 0x60)
                {
                    this.label6041.Text = messageInfo.sCommand;
                    checkStatusBits(messageInfo);
                }
                else if (messageInfo.msg.DATA[1] == 0x20 && messageInfo.msg.DATA[2] == 0x33) // Analog Input
                {
                    //  analogValue = BitConverter.ToUInt16(messageInfo.msg.DATA, 4);
                    //  bAnalog = true;
                    this.labelAnalog.Text = "a" + driveId + "=" + analogValue;

                }
                else if (messageInfo.msg.DATA[1] == 0x64 && messageInfo.msg.DATA[2] == 0x60) // Position actual Value
                {
                    //  this.pos = BitConverter.ToInt32(messageInfo.msg.DATA, 4);
                    this.labelPosition.Text = "p" + driveId + "=" + pos;

                }

                else if (messageInfo.msg.DATA[1] == 0xfd && messageInfo.msg.DATA[2] == 0x60) // Digital Input
                {
                    // Digital inputs 
                    io = messageInfo.msg.DATA[4];
                    checkBoxIn1.Checked = ((messageInfo.msg.DATA[4] & 0x01) == 0x01);
                    checkBoxIn2.Checked = ((messageInfo.msg.DATA[4] & 0x02) == 0x02);
                    checkBoxIn3.Checked = ((messageInfo.msg.DATA[4] & 0x04) == 0x04);
                    checkBoxIn4.Checked = ((messageInfo.msg.DATA[4] & 0x08) == 0x08);
                    //this.labelAnalog.Text = "a" + DriveId + "=" + BitConverter.ToUInt16(messageInfo.msg.DATA, 4);
                }

                else
                    this.label581.Text = messageInfo.sCommand + " = " + BitConverter.ToInt32(messageInfo.msg.DATA, 4);
            }

            else if (messageInfo.msg.ID == 0x180 + driveId)
            {
                this.label6041.Text = messageInfo.sCommand;
                checkStatusBits(messageInfo);
            }
            else if (messageInfo.msg.ID == 0x280 + driveId)
            {
                // pos= BitConverter.ToInt32(messageInfo.msg.DATA, 0);
                this.labelPosition.Text = "p" + driveId + "=" + pos;
            }
            else if (messageInfo.msg.ID == 0x380 + driveId)
            {
                speed = BitConverter.ToInt32(messageInfo.msg.DATA, 0);
                // this.labelSpeed.Text = "v" + DriveId + "=" + BitConverter.ToInt32(messageInfo.msg.DATA, 0);
            }

            else if (messageInfo.msg.ID == 0x480 + driveId)
            {
                io = messageInfo.msg.DATA[2];
                checkBoxIn1.Checked = ((messageInfo.msg.DATA[2] & 0x01) == 0x01);
                checkBoxIn2.Checked = ((messageInfo.msg.DATA[2] & 0x02) == 0x02);
                checkBoxIn3.Checked = ((messageInfo.msg.DATA[2] & 0x04) == 0x04);
                checkBoxIn4.Checked = ((messageInfo.msg.DATA[2] & 0x08) == 0x08);
            }
            else if (messageInfo.msg.ID == 0x48a)
            {
                uint crtl= messageInfo.msg.DATA[0];
                uint psi = messageInfo.msg.DATA[4];
                labelRec.Text = String.Format("ACS: {0:x}  Psi= {1}",crtl,psi);
            }






            string str;
            if (!isListBoxLocked)
            {
      //          listBox1.BeginUpdate();
                isListBoxLocked = true;
            }
            str = string.Format("{0:x} :", messageInfo.msg.ID);
            index = listBox1.FindString(str);
            //index = 0;
            if (index >= 0)
            {
               // if (!this.listBox1.Items[index].ToString().Equals(messageInfo.sCommand))
               // this.listBox1.Items[index] = messageInfo.sCommand;
            }
            else
            {
               // listBox1.BeginUpdate();
               // this.listBox1.Items.Add(messageInfo.sCommand);
                this.listBox1.Items.Add(can.messageStack[messageInfo.msg.ID].sCommand);
            }

           // listBox1.EndUpdate();

            
            if (DateTime.Now.Ticks - lastTime > 300*10000)
            {
          //      if(isListBoxLocked)
          //       listBox1.EndUpdate();
                lastTime = DateTime.Now.Ticks;
                isListBoxLocked = false;

                /*
               
                foreach (KeyValuePair<uint, CANMessageInfo> msg in can.messageStack)
                {
                    str = string.Format("{0:x} :", msg.Value.msg.ID);
                    index = listBox1.FindString(str);
                    if (index >= 0)
                    {
                        if (!this.listBox1.Items[index].ToString().Equals(messageInfo.sCommand))
                            this.listBox1.Items[index] = messageInfo.sCommand;
                    }
                    else
                        this.listBox1.Items.Add(messageInfo.sCommand);
                   
                }
                */
            }

           

            //  listBox1.SelectedIndex = listBox1.Items.Count - 1;
           
            if (checkBoxLog.Checked)
                fileCAN.WriteLine("{0:x}\t{1}", messageInfo.msg.ID, BitConverter.ToString(messageInfo.msg.DATA));


        }

        private void checkStatusBits(CANMessageInfo messageInfo)
        {
            if ((messageInfo.msg.ID & 0x580) == 0x580)
                checkStatusBits(messageInfo.msg.DATA[4], messageInfo.msg.DATA[5]);
            else
                checkStatusBits(messageInfo.msg.DATA[0], messageInfo.msg.DATA[1]);
        }

            private void checkStatusBits(byte b0, byte b1)
        {

            state = (uint)b1 << 8 | b0;
            // Bit 0: ready to switch on
            this.checkBoxBit0.Checked = ((b0 & 0x01) == 0x01);
            //Bit 1: switched on
            this.checkBoxBit1.Checked = ((b0 & 0x02) == 0x02);
            //Bit 2: operation enabled: Der eingestellt Operationsmodus ist aktiv und nimmt Befehle entgegen(z.B.Profile Position Mode)
            this.checkBoxEnabled.Checked = ((b0 & 0x04) == 0x04);
            //Bit 3: fault: Wird im Fehlerfall gesetzt
            this.checkBoxBit3.Checked = ((b0 & 0x08) == 0x08);
            //Bit 4: voltage enabled: Bit ist gesetzt, wenn Motor bestromt wird
            this.checkBoxPower.Checked = ((b0 & 0x10) == 0x10);
            //Bit 5: quick stop
            this.checkBoxBit5.Checked = ((b0 & 0x20) == 0x20);
            //Bit 6: switch on disabled
            this.checkBoxBit6.Checked = ((b0 & 0x40) == 0x40);
            //Bit 7: warning
            this.checkBoxBit7.Checked = ((b0 & 0x80) == 0x80);
            //Bit 8: PLL sync complete: Wird gesetzt, sobald die Synchronisation mit dem SYNC - Objekt abgeschlossen ist.
            this.checkBoxBit8.Checked = ((b1 & 0x01) == 0x01);
            //Bit 9: remote
            this.checkBoxBit9.Checked = ((b1 & 0x02) == 0x02);
            //Bit 10: target reached: Wird gesetzt, wenn der Motor sein Ziel erreicht hat(Profile Position Mode)
            this.checkBoxBit10.Checked = ((b1 & 0x04) == 0x04);
            //Bit 11: internal limit active: Wird gesetzt, wenn die Sollwerte die Maximalgrenzen überschreiten.
            this.checkBoxBit11.Checked = ((b1 & 0x08) == 0x08);

            this.checkBoxBit12.Checked = ((b1 & 0x10) == 0x10);
            this.checkBoxBit13.Checked = ((b1 & 0x20) == 0x20);
            this.checkBoxBit14.Checked = ((b1 & 0x40) == 0x40);
            this.checkBoxBit15.Checked = ((b1 & 0x80) == 0x80);
        }

        private void ButtonInit_Click(object sender, EventArgs e)
        {
            TPCANStatus stsResult;
            Debug.Write("Init CAN ");
            try
            {
//                can = CSystemRessources.GetController(CanController) as PCanOpen;
                TPCANStatus err = can.CanInit();
                CheckCANErrors(err);
                // if (drive ==null)
                //   drive = new CANOpenDrive(1);
                this.listBox1.Items.Clear();
                can.lbxInfo = this.listBox1;
                //  drive.Init();

                can.OnMessage -= CCANOpen_OnDisplayMessage;
                can.OnMessage += new PCanOpen.CMessageEvent(CCANOpen_OnDisplayMessage);  // ToDo nur einmal Aktivieren 
                byte driveId = (byte)numericUpDownDrive.Value;
                can.InitDrive(driveId, (byte)0x01);
                can.GetVersions();

                stsResult = can.getStatus(driveId);
                CheckCANErrors(stsResult);
                can.SetOperationalMode(driveId, (byte)CANOpenDrive.MODES_OF_OPERATION.Profile_Position_Mode);
                can.ReadMessages();

                // MessageHandler.Message();
                backgroundWorkerMessages.WorkerSupportsCancellation = true;
            }catch(Exception ex)
            {
                MessageHandler.Error(this, 342324, "Can Init not possible", ex);
            }


         //   if (!backgroundWorkerMessages.IsBusy)
         //       backgroundWorkerMessages.RunWorkerAsync();
        }

        private void buttonMove_Click(object sender, EventArgs e)
        {

            try
            {
                int pos = (int)numericUpDownPos1.Value;
                driveId = (byte)numericUpDownDrive.Value;
                can.SetOperationalMode(driveId, (byte)CANOpenDrive.MODES_OF_OPERATION.Profile_Position_Mode);

                can.SetState(driveId, 0x0f);
                can.SetTargetPosition(driveId, pos);
                Thread.Sleep(10);
                Debug.WriteLine("Move: " + pos);
                MessageHandler.Message(this, 7352, MessageTyp.Info,  "Move: " + pos);

                TPCANStatus err = can.SetState(driveId, 0x1f);
                Thread.Sleep(20);
                can.SetState(driveId, 0x0f);
                CheckCANErrors(err);
                

            }
            catch (Exception ex)
            {
                Debug.WriteLine("Error: ", ex);
                MessageHandler.Error(this, 7352,"Error: CAN ",ex);
            }

        }

        private void buttonSetSDO_Click(object sender, EventArgs e)
        {
            string command;
            String pre;
            TPCANStatus err = TPCANStatus.PCAN_ERROR_UNKNOWN;
            try
            {

                driveId = (byte)numericUpDownDrive.Value;
                if(textBoxSDOsub.Text.Length<1) textBoxSDOsub.Text ="0";
                if (labelcmd.Text == "0")
                    pre = "";
                else
                    pre = "0x";

                if (textBoxSDOValue.Text.Length<1)
             command = pre+textBoxSDOId.Text + ":" + textBoxSDOsub.Text;
            else
                command = pre+textBoxSDOId.Text + ":" + textBoxSDOsub.Text + "=" + textBoxSDOValue.Text;
                Debug.WriteLine("Set SDO: " + command);
                MessageHandler.Message(this, 7353, MessageTyp.Info, "Set SDO: " + command);

                err = can.setSDOParameter(driveId, command);
                Thread.Sleep(10);
                labelError.Text= can.DecodeErros(0x580 + driveId);
                CheckCANErrors(err);
                labelData.Text = can.strData;
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Error: ", ex);

            }
            
        }

        private void buttonDriveOn_Click(object sender, EventArgs e)
        {
            driveId = (byte)numericUpDownDrive.Value;
            can.SetState(driveId, 0x0d);
            Thread.Sleep(10);
            can.SetState(driveId, 0x0e);
            Thread.Sleep(10);
            can.SetState(driveId, 0x0f);
            Thread.Sleep(10);
            MessageHandler.Message(this, 7353, MessageTyp.Info, "Drive On!");

            TPCANStatus err = can.getStatus(driveId);
            CheckCANErrors(err);

        }

        private void buttonDriveOff_Click(object sender, EventArgs e)
        {
            driveId = (byte)numericUpDownDrive.Value;

            TPCANStatus err = can.SetState(driveId, 0x03);
            MessageHandler.Message(this, 7354, MessageTyp.Info, "Drive Off!");

            CheckCANErrors(err); 


        }


        private void buttonSetControl_Click(object sender, EventArgs e)
            {
                uint state = 0x00;
            try
            {
                driveId = (byte)numericUpDownDrive.Value;

                if (textBoxControl.Text.Length > 0)
                {
                    state = PCanOpen.Convert2UInt32(textBoxControl.Text);
                    Debug.WriteLine(string.Format("Control = {0:x}", state));
                    MessageHandler.Message(this, 7354, MessageTyp.Info, string.Format("Control = {0:x}", state));

                    can.SetState(driveId, state);
                }
                TPCANStatus err = can.getStatus(driveId);
                CheckCANErrors(err);
                Thread.Sleep(5);
                labelError.Text = can.DecodeErros(0x580 + driveId);

            }
            catch (Exception ex)
            {
                MessageHandler.Error(this,2329,"CAN Error in setControl: ", ex);

            }
        }

        private void buttonSpeed_Click(object sender, EventArgs e)
        {
            driveId = (byte)numericUpDownDrive.Value;

            Int32 vel = (UInt16)numericUpDownSpeed.Value;
            Debug.WriteLine(string.Format("Vel = {0}", vel));
            MessageHandler.Message(this, 7354, MessageTyp.Info, string.Format("Vel = {0}", vel));

            //todo  acs      drive.CAN.sendSDO((uint)(0x600+DriveId), 0x23, 0x6081,0,(UInt16)vel);
            TPCANStatus err = can.SetTargetVelocity(driveId, vel);
            CheckCANErrors(err);
            Thread.Sleep(5);
            labelError.Text = can.DecodeErros(0x580 + driveId);
        }

        private void buttonStop_Click(object sender, EventArgs e)
        {
            driveId = (byte)numericUpDownDrive.Value;
            can.InitDrive(driveId, 0x02);
            MessageHandler.Message(this, 7354, MessageTyp.Info, "Stop!");

            TPCANStatus err = can.getStatus(driveId);
            CheckCANErrors(err);

            backgroundWorkerMessages.CancelAsync();
        }

        private void buttonReset_Click(object sender, EventArgs e)
        {
            driveId = (byte)numericUpDownDrive.Value;

            listBox1.Items.Clear();
        //    if (!backgroundWorkerMessages.IsBusy)
        //        backgroundWorkerMessages.RunWorkerAsync();

            MessageHandler.Message(this, 7354, MessageTyp.Info, "Reset CAN!");

            //can.GetVersions();
            can.SetState(driveId, 0x80);
            Thread.Sleep(5);
            TPCANStatus err = can.getStatus(driveId);
            CheckCANErrors(err);

        }


          delegate void UpdatePannelDelegateEvaluate(object sender, CANMessageInfo messageInfo);
         //UpdatePannelDelegateEvaluate updatePannelDelegateEvaluate;

        
         private void CCANOpen_OnDisplayMessage(object sender, CANMessageInfo messageInfo)
          {
              try
              {

                // Setzt den Status einer Message
                if (messageInfo.msg.ID == 0x580 + driveId)
                {
                    if (messageInfo.msg.DATA[1] == 0x20 && messageInfo.msg.DATA[2] == 0x33) // Analog Input
                    {
                        analogValue = BitConverter.ToUInt16(messageInfo.msg.DATA, 4);
                        bAnalog = true;
                    }

                    else if (messageInfo.msg.DATA[1] == 0x64 && messageInfo.msg.DATA[2] == 0x60) // Position actual Value
                    {
                        this.pos = BitConverter.ToInt32(messageInfo.msg.DATA, 4);
                        bPosition = true;

                    }
                }
                else if (messageInfo.msg.ID == 0x280 + driveId)
                {
                    pos = BitConverter.ToInt32(messageInfo.msg.DATA, 0);
                    bPosition = true;
                    
                }
                else if (messageInfo.msg.ID == 0x380 + driveId)
                {
                    speed = BitConverter.ToInt32(messageInfo.msg.DATA, 0);

                }


                this.BeginInvoke(new UpdatePannelDelegateEvaluate(DisplayMessage), new object[] { sender, messageInfo });
              }
              catch { }
          }

        private void checkBoxLog_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBoxLog.Checked)
            {
                fileData = new System.IO.StreamWriter(@".\ACSLogData.txt", bAppend);
                fileCAN =  new System.IO.StreamWriter(@".\ACSLogCAN.txt", bAppend);
                bAppend = true;
            }
            else
            {
                fileData.Close();
                fileData = null;
                fileCAN.Close();
                fileCAN = null;

            }
        }

        private void buttonUpdate_Click(object sender, EventArgs e)
        {
            driveId = (byte)numericUpDownDrive.Value;
            can.GetInput(driveId);
            can.GetAnalogInput(driveId);
            can.GetPosition(driveId);
            TPCANStatus err = can.getStatus(driveId);

            CheckCANErrors(err);
            Thread.Sleep(5);
            labelError.Text = can.DecodeErros(0x580 + driveId);
            if (checkBoxLog.Checked) {
                fileData.WriteLine(String.Format("data\t{0}\t{1:x}\t{2}\t{3}\t{4}\t{5:x}", this.driveId, this.state, this.analogValue, this.pos, this.speed, this.io));
                if (logData != null && nData < 1000)
                {
                    logData[nData, 0] = this.analogValue;
                    logData[nData, 1] = this.pos;
                    logData[nData, 1] = this.speed;
                    nData++;
                }
            }

        }

        private void buttonOutput_Click(object sender, EventArgs e)
        {
            driveId = (byte)numericUpDownDrive.Value;

            uint value = 0;
            if (checkBoxOut1.Checked)
                value |= 0x01 << 16;
            if (checkBoxOut2.Checked)
                value |= 0x02 << 16;
            if (checkBoxOut3.Checked)
                value |= 0x04 << 16;
            TPCANStatus err = can.SetOutput(driveId,value);
            CheckCANErrors(err);
            err = can.getStatus(driveId);
            CheckCANErrors(err);
            Thread.Sleep(5);
            labelError.Text = can.DecodeErros(0x580 + driveId);
        }


        private void buttonStopMove_Click(object sender, EventArgs e)
        {
            driveId = (byte)numericUpDownDrive.Value;
            MessageHandler.Message(this, 7354, MessageTyp.Info, "Stop Move!");

            TPCANStatus err = can.SetState(driveId, 0x03); // QuickStop auf 0 setzen wird damit Aktiv
            CheckCANErrors(err);
            err = can.getStatus(driveId);
            CheckCANErrors(err);
            Thread.Sleep(5);
            labelError.Text = can.DecodeErros(0x580 + driveId);
        }

        private void buttonResetDrive_Click(object sender, EventArgs e)
        {
            driveId = (byte)numericUpDownDrive.Value;
            MessageHandler.Message(this, 7354, MessageTyp.Info, "Reset Drive! " + driveId);

            TPCANStatus err = can.InitDrive(driveId, 0x01);
            CheckCANErrors(err);
            can.SetState(driveId, 0x80);  // ResetFault
            CheckCANErrors(err);
            err = can.getStatus(driveId);
            CheckCANErrors(err);
            Thread.Sleep(5);
            labelError.Text = can.DecodeErros(0x580 + driveId);

        }

        private void ButtonDriveInit_Click(object sender, EventArgs e)
        {
            driveId = (byte)numericUpDownDrive.Value;
            MessageHandler.Message(this, 7354, MessageTyp.Info, "Drive Init! " + driveId);

            TPCANStatus err = can.InitDrive(driveId, (byte)0x01);
            CheckCANErrors(err);
            err = can.getStatus(driveId);
            CheckCANErrors(err);
            Thread.Sleep(5);
            labelError.Text = can.DecodeErros(0x580 + driveId);


        }


        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            driveId = (byte)numericUpDownDrive.Value;
          //  operationalMode = comboBox1.SelectedIndex - 2;
            TPCANStatus err = can.SetOperationalMode(driveId, (byte)operationalMode);
                     CheckCANErrors(err);
            err = can.getStatus(driveId);
            CheckCANErrors(err);
            Thread.Sleep(5);
            labelError.Text = can.DecodeErros(0x580 + driveId);
        }


        private void numericUpDownDrive_ValueChanged(object sender, EventArgs e)
        {
            driveId = (byte)numericUpDownDrive.Value;
       
            numericUpDownPos1.Value = 0;
            this.labelPosition.Text = "p" + driveId + "=";
            this.labelAnalog.Text = "a" + driveId + "=";
            checkStatusBits(0, 0);
            TPCANStatus err = can.SetOperationalMode(driveId, (byte)operationalMode);
            CheckCANErrors(err);
            err = can.getStatus(driveId);
            CheckCANErrors(err);
            Thread.Sleep(5);
            labelError.Text = can.DecodeErros(0x580 + driveId);
            buttonUpdate_Click(sender, e);






        }
        private void timerAutoUpdate_Tick(object sender, EventArgs e)
        {
            buttonUpdate_Click(sender, e);
        }



        void CheckCANErrors(TPCANStatus stsResult)
        {
            if (stsResult != TPCANStatus.PCAN_ERROR_OK)
              MessageHandler.Error(this,232,"CAN Error: "+ can.GetFormatedError(stsResult));
        }

        private void checkBoxOut_CheckedChanged(object sender, EventArgs e)
        {
            buttonOutput_Click(sender, e);
        }

        private void checkBoxAutoUpdate_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBoxAutoUpdate.Checked)
                timerAutoUpdate.Start();
            else
                timerAutoUpdate.Stop();

        }

        private void CANOpenControl_Enter(object sender, EventArgs e)
        {
            if (can == null)
            {
            //    can = CSystemRessources.GetController(CanController) as PCanOpen;
                if (can != null)
                {
                    can.OnMessage -= CCANOpen_OnDisplayMessage;
                    can.OnMessage += new PCanOpen.CMessageEvent(CCANOpen_OnDisplayMessage);  // ToDo nur einmal Aktivieren 
                }
                else
                    MessageHandler.Error(this, 7354, "No Can Controller avalable!" );
            }

        }

        private void buttonSetProfile_Click(object sender, EventArgs e)
        {
            driveId = (byte)numericUpDownDrive.Value;

            Int32 acc = (UInt16)numericUpDownAccel.Value;
            Debug.WriteLine(string.Format("Acc = {0}", acc));
            MessageHandler.Message(this, 7354, MessageTyp.Info, string.Format("Acc = {0}", acc));

            //todo  acs      drive.CAN.sendSDO((uint)(0x600+DriveId), 0x23, 0x6081,0,(UInt16)vel);
            TPCANStatus err = can.SetAcceleration(driveId, acc);
            Thread.Sleep(5);
            labelError.Text = can.DecodeErros(0x580 + driveId);
            if (comboBox1.SelectedIndex >= 0)
            {
                err = can.SetRampType(driveId, comboBox1.SelectedIndex);
                Thread.Sleep(5);
                labelError.Text = can.DecodeErros(0x580 + driveId);
            }

            CheckCANErrors(err);

        }

        private void buttonHoming_Click(object sender, EventArgs e)
        {

            int homeOffset = (int)numericUpDownHomeOffset.Value;
            driveId = (byte)numericUpDownDrive.Value;

            can.SetState(driveId, 0x0f);

            can.SetOperationalMode(driveId, (byte)CANOpenDrive.MODES_OF_OPERATION.Homing_Mode);
            can.SetHomeOffset(driveId, homeOffset);
            if(comboBox2.SelectedItem != null){
                string value = comboBox2.SelectedItem.ToString();
                int index = int.Parse(value.Substring(0, 3));
                can.SetHomingMethod(driveId, index);
                Thread.Sleep(5);
                labelError.Text = can.DecodeErros(0x580 + driveId);
                Thread.Sleep(5);
                can.SetState(driveId, 0x1f); // Starte Bewegung
                logData = new double[2000, 3];
                nData = 0;
            }
            //Thread.Sleep(5);
            //can.SetState(driveId, 0x0f); // Bewegung Flag Rücksetzen nicht bei Homing


        }

        private void button2_Click(object sender, EventArgs e)
        {

        }

        private void buttonCalcHomeOffset_Click(object sender, EventArgs e)
        {
            driveId = (byte)numericUpDownDrive.Value;
            //  int analogOffset1 = 545;
            //  int analogOffset2 = 548;
            int analogOffset1 = (int)can.refAnalogOffsetA1;
            int analogOffset2 = (int)can.refAnalogOffsetA2;

            double analogFaktor1 = can.refAnalogFaktorA1;
            double analogFaktor2 = can.refAnalogFaktorA2;

            decimal ao =0;

            int n = 0;
            bAnalog = false;
            can.GetAnalogInput(driveId);
            Thread.Sleep(2);
            while (!bAnalog) {
                Thread.Sleep(10);
                if (n++ > 20)
                {
                    MessageHandler.Error(this,7324,"Error Timeout GetAnalogInput");
                    can.ReadMessages();

                    break;
                }
            }
            switch (driveId) {
              //  case 1: ao = (decimal)(-638.8797 * (analogValue - analogOffset1)); break;
              //  case 2: ao = (decimal)(-509.2397 * (analogValue - analogOffset2)); break;
                case 1: ao = (decimal)(analogFaktor1 * (analogValue - analogOffset1)); break;
                case 2: ao = (decimal)(analogFaktor2 * (analogValue - analogOffset2)); break;
            }

            int nao =(int)Math.Round(ao / 2000.0M);
            numericUpDownHomeOffset.Value = (decimal)(nao * 2000);
            labelAnalogOffset.Text = "inc: " + ao + "    turn: " + nao;

       //     GeneralMatrix matA;
       //     GeneralMatrix matB;
            if (logData != null && nData>2)
            {
         //       matA = new GeneralMatrix(nData, 2);
         //       matB = new GeneralMatrix(nData, 1);
                for (int i = 0; i < nData; i++)
                {
           //         matA.SetElement(i, 0, logData[i, 0]);
           //         matA.SetElement(i, 1, 1.0);
            //        matB.SetElement(i, 0, logData[i, 1]);
                }
                
              //  GeneralMatrix matD = matA.Transpose().Multiply(matA).Inverse().Multiply(matA.Transpose()).Multiply(matB);
 //               labelAnalogOffset.Text = "inc: " + ao + "    turn: " + nao + "    faktor : " + matD.GetElement(0, 0);
//                Console.WriteLine("k= " + matD.GetElement(0,0) +", "+ matD.GetElement(1, 0));
            }

        }

        private void CANOpenControl_Load(object sender, EventArgs e)
        {
            MessageHandler.Message(this,2354,MessageTyp.Info, "CAN Open Form Loaded");
        }

        private void buttonSetOffset_Click(object sender, EventArgs e)
        {
            bAnalog = false;
            can.GetAnalogInput(driveId);
            Thread.Sleep(2);
            int n = 0;
            while (!bAnalog)
            {
                Thread.Sleep(10);
                if (n++ > 20)
                {
                    MessageHandler.Error(this, 7324, "Error Timeout GetAnalogInput");
                    can.ReadMessages();

                    break;
                }
            }
            if(analogValue>1)
            switch (driveId)
            {
            case 1:  can.refAnalogOffsetA1 = (double)analogValue; break;
            case 2:  can.refAnalogOffsetA2 = (double)analogValue; break;
            }
        }

        private void labelcmd_Click(object sender, EventArgs e)
        {
            if(labelcmd.Text == "0")
            labelcmd.Text = "#";
            else
            labelcmd.Text = "0";
        }

        byte[] data = { 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00 };

        private void checkBoxVentil_CheckedChanged(object sender, EventArgs e)
        {
            if((sender as CheckBox).Checked)
             data[0] |= 0x01;
            else
             data[0] &= 0xfe;
            can.sendPDO(0x40a, data);
            labelHandControl.Text = String.Format("c={0:x}" , data[0]);


        }

        private void checkBoxPumpe_CheckedChanged(object sender, EventArgs e)
        {
            if ((sender as CheckBox).Checked)
                data[0] |= 0x02;
            else
                data[0] &= 0xfd;
            can.sendPDO(0x40a, data);
            labelHandControl.Text = String.Format("c={0:x}", data[0]);

        }

        private void Control_CheckedChanged(object sender, EventArgs e)
        {
            if ((sender as CheckBox).Checked)
                data[0] |= 0x08;
            else
                data[0] &= 0xf7;
            can.sendPDO(0x40a, data);
            labelHandControl.Text = String.Format("c={0:x}" ,data[0]);
        }

        private void checkBox4_CheckedChanged(object sender, EventArgs e)
        {
            if ((sender as CheckBox).Checked)
                data[0] |= 0x10;
            else
                data[0] &= 0x0f;
            can.sendPDO(0x40a, data);
            labelHandControl.Text = String.Format("c={0:x}" , data[0]);

        }

        private void checkBoxDebug_CheckedChanged(object sender, EventArgs e)
        {
            can.bDebug = (sender as CheckBox).Checked;
        }
    }
}
