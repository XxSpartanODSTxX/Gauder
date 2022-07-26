using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using Peak.Can.Basic;
using System.Diagnostics;
using System.Windows.Forms;
using System.Xml;

namespace PeakCANOpen_Lib
{
    public class CANOpenDrive //: CDrive, IInput, IOutput, IRobotPlugin, IUserSettings
    {
        public string PluginName { get; } = "CANOpenDrive";
        public string Version { get; } = "0.9.7.1";

      

        // static PCanOpen can = new PCanOpen();
        static PCanOpen can;

        uint driveId=1;
        bool ErrorFlag;
        int incPos;

       UInt16 control = 0x0000;   //0x6040

        UInt16 analog = 0x0000;   //0x3320:1
        bool bAnalogInput;
        bool bPosition = true;
        bool bDigitalInput;

        bool isStateValid = false;

        UInt32 _state = 0x0000;
        public   uint State { get { return _state; } }

        TPCANStatus err; 


        public uint input { get; set; }
        public uint output { get; set; }


        // Enthält den aktuellen Betriebsmodus.
        public enum MODES_OF_OPERATION
        { //(0x6060)
            CLTestshort=-2, // Kurzer Closed-Loop Testlauf (Alignment)
            CLTest=-1,      // Closed-Loop Testlauf
            NoMode=0,       // No mode change/no mode assigned
            Profile_Position_Mode= +1, // Profile Position Mode
            Velocity_Mode = +2, // Velocity Mode
            Profile_Velocity_Mode= +3,  // Profile Velocity Mode
            Torque_Profile_Mode = +4, // Torque Profile Mode
            Reserved= +5, //  Reserved
            Homing_Mode = +6, //  Homing Mode
            Interpolated_Position_Mode = +7  //Interpolated Position Mode
        }




        public static PCanOpen CAN { get
        {
              //  if (can == null)
              //      can = CSystemRessources.GetController("PCAN") as PCanOpen;
                if (can == null)
                {
                    can = new PCanOpen();
              //      CSystemRessources.AddController("PCAN", can);
                }
                return can; }
        }


        public CANOpenDrive()
        {
        }


        public CANOpenDrive(uint id)
        {
            driveId = id;
            if (can == null)
            {
              //  can = CSystemRessources.GetController("PCAN") as PCanOpen;
                can.CanInit();
                //todo  acs   Init();
            }
        }


        public  string GetVersion()
        {
            can.GetVersions();
            Thread.Sleep(1);
            can.ReadMessages();
            return "";  // Todo Messages auslesen
        }

        /*
                public uint Init() {
                    TPCANStatus erg = 0;
                    if ((erg = can.CanInit()) == TPCANStatus.PCAN_ERROR_OK)
                    {
                        MessageHandler.Message(this, 4101, 2, "CanInit ok");
                        can.GetVersions();
                    }
                    else
                        MessageHandler.Error(this, 4101, "Error CanInit: "+erg);
                    return (uint)erg;
                }
                */
        /// <summary>
        /// Aktiviert einen Antrieb
        /// </summary>

        public  int InitDrive(uint mode)
        {
            ErrorFlag = false;  // Fehler rücksetzen
            switch (mode) {
                case 1: can.InitDrive((byte)driveId, 0x01); break;
        }
            err = can.getStatus((byte)driveId);
            can.CheckCANErrors(err);

            if(can.onEvent[driveId - 1]>0)
             can.OnMessage -= CCANOpen_OnMessage;

            can.OnMessage += new PCanOpen.CMessageEvent(CCANOpen_OnMessage);  // ToDo nur einmal Aktivieren 
            can.onEvent[driveId-1]++;


            return (int)err;

        }

        public void GetManufacturerSoftwareVersion()
        {
            can.sendSDO(0x600 + driveId, 0x2b, 0x100A, 0, 0);
            can.sendSDO(0x600 + driveId, 0x2b, 0x100A, 1, 0);
            err= can.sendSDO(0x600 + driveId, 0x2b, 0x100A, 2, 0);
            can.CheckCANErrors(err);



        }

        /// <summary>
        /// setzt das Contreollwort des Antriebs
        /// </summary>
        /// <param name="control"></param>
        public  void SetState(uint control) {
            err= can.SetState(driveId, control);
            can.CheckCANErrors(err);

        }



        /// <summary>
        /// liefert den aktuellen Status
        /// wartet dabei bis der Status aktualisiert wird
        /// </summary>
        /// <returns></returns>
        public  uint GetState() {
            isStateValid = false;
            err =can.getStatus(driveId);  // Fordert Status des Antriebes an
            Thread.Sleep(2);
            if (!can.ReadMessages()) throw new Exception("Fatal CAN Error");
            int n=0;
            // Warten bis die Status Nachricht kommt
            while (!isStateValid)
            {
                err = can.getStatus(driveId); // nochmal anfordern
                Thread.Sleep(3);
                can.ReadMessages();
                if (n++ > 10)
                {
                   MessageHandler.Error(this,23423,"Error Timeout CAN getStatus Drive: "+driveId);
                   ErrorFlag = true;
                   break;
                }
            }
            // state = can.driveControlState[driveId];
            return _state;
        }

        /*
        TPCANStatus CheckCANErrors(TPCANStatus stsResult)
        {
            if (stsResult != TPCANStatus.PCAN_ERROR_OK)
            {
                MessageHandler.Error(this, 232, "CAN Error: " + can.GetFormatedError(stsResult));
                can.isError = true;
            }
            return stsResult;
        }
        */

        /// <summary>
        /// Startet eine Bewegung
        /// der Status mus bei Erreichen der Zielposition auf 0x0f gesetzt werden
        /// dann kan mit Togel die Bewegugn gestartet werden
        /// </summary>
        /// <returns></returns>
        public  int StartTravelProfile() {
            control |= 0x1f;
            err= can.SetState((byte)driveId, control);
        //    can.OnMessage += new PCanOpen.CMessageEvent(CCANOpen_OnMessage);
            Thread.Sleep(3);  //todo sollte nicht erforderlich sein!  ( könnte auch später zurüchgesetzt werden)
            control &= 0xef;  // 0x10 zurücksetzen
            control =  0x0f;
            err = can.SetState((byte)driveId, control);
            can.CheckCANErrors(err);
            //can.OnMessage += new PCanOpen.CMessageEvent(CCANOpen_OnMessage);  // ToDo nur einmal Aktivieren 
            //   can.OnMessage -= CCANOpen_OnMessage;
            _state= GetState();
//            isMove = true;
            return 0;  
        }

        public  int StopTravelProfile()
        {
            control &= 0xfb;  // Bit 2 auf 0 Setzen
            err= can.SetState((byte)driveId, control);
            can.CheckCANErrors(err);
            //   can.OnMessage -= CCANOpen_OnMessage;
            return 0;
        }


        public  void FinishMove()
        {
           // if(isMove)
           //  can.OnMessage -= CCANOpen_OnMessage;
        }

        public  uint SetDriveState(int mode) {
            switch (mode) {
                case 0:  // Off
                    err= can.SetState(driveId, 0x03); // Todo normale Off Routinr verwenden
                    can.CheckCANErrors(err);

                    break;
 
                case 1: // On
                    err= can.SetState(driveId, 0x0d);
            Thread.Sleep(20);
                    _state = GetState();
                    can.SetState(driveId, 0x0e);
            Thread.Sleep(20);
                    _state = GetState();
                    err = can.SetState(driveId, 0x0f);
            Thread.Sleep(20);
                    can.CheckCANErrors(err);

                    break;
           }
            _state= GetState();
            return _state;
        }



        public  void MoveRef(int mode) {
            int n = 0;
            _state= this.SetDriveState(0); //Drive Off

            SetOperationalMode((byte)MODES_OF_OPERATION.Homing_Mode);  // Homing
 //           isReferencing = true;
            if (mode == 33 || mode == 0)
                err= can.sendSDO(0x600 + driveId, 0x2f, 0x6098, 0, 33);
            else if (mode == 34 || mode == 1)
                err= can.sendSDO(0x600 + driveId, 0x2f, 0x6098, 0, 34);

//            ResetPositionError(true, 0);
            
            _state= this.SetDriveState(1); //Drive On
            StartTravelProfile();
            Thread.Sleep(100);
            _state = GetState();
            while (!isTargetReached())
            {
                if (n++ > 100)
                {
                    MessageHandler.Error(this, 2983, String.Format("Error Timeout Position Reached during Referencing! ({0:x)", _state));
                    break;
                }
                Thread.Sleep(50);
                _state=GetState();
            }
            
            this.analog = (ushort)GetAnalog(this.driveId);

            //int analogOffset1 = 545;
            //int analogOffset2 = 448;
            int analogOffset1 = (int)can.refAnalogOffsetA1;
            int analogOffset2 = (int)can.refAnalogOffsetA2;

            double analogFaktor1 = can.refAnalogFaktorA1;
            double analogFaktor2 = can.refAnalogFaktorA2;


            int hoOffset = this.analog;
            int theta1 = 0;


            switch (driveId) {
                case 1: hoOffset -= analogOffset1;
                    hoOffset = (int)(-2000.0 * (60.0 / 12.0) * 25.0 * hoOffset / 360.0);
                    break;
                case 2: hoOffset -= analogOffset2;
                    hoOffset = (int)(+2000.0 * (44.0 / 15.0) * 25.0 * (hoOffset + theta1 * 15.0 / 44.0) / 360.0);
                    break;

            }
            //  hoOffset = 0;  // todo HomOffset richtig berechnen



            // Set Homing Offset  Home Offset (SDO 0x607C)   Typ s32 rw
            err= can.sendSDO(0x600 + this.driveId, 0x23, 0x607C, 0, (uint)hoOffset);
            Thread.Sleep(10);
            _state = GetState();




            //  Set HomeOffset to Aktual Position
            /*
            Modus 35: Positionsreset
                    • Setzt die aktuelle Position auf Home-Offset, ohne dass die Welle bewegt
                    */

            if (mode == 35 || mode == 0 || mode == 1) {
                _state = this.SetDriveState(0); //Drive Off
            err= can.sendSDO(0x600 + this.driveId, 0x2f, 0x6098, 0, 35);
            StartTravelProfile();
            Thread.Sleep(20);
            }

            SetOperationalMode((byte)MODES_OF_OPERATION.Profile_Position_Mode);  // Homing ende
//            isReferencing = false;

            decimal x = GetPosition(0);
            if (Math.Abs(hoOffset - x) < 100) {
                MessageHandler.Message(this, 2983, MessageTyp.Message,"Referencing completed for a" + (driveId + 1));
   //             isReferenced = true;
            }else
                MessageHandler.Error(this, 2983, "Error Referencing for a" + driveId );

        }



        /// <summary>
        /// Setzt Absolute oder Relative Bewegung
        /// Controlword (SDO 0x6040)  Bit 6: Bei „0“ ist die Zielposition (SDO 0x607A) absolut und bei „1“ ist die Zielposition relativ zur aktuellen Position.
        /// </summary>
        /// <param name="mode"></param>
        /// <returns></returns>
        public  int SetPositionType(int mode) {
            
            if (mode == 1) // MoveRel
              control |=   0x40; // Bit 6 setzen
            else if (mode == 2) // MoveAbs
              control &= 0xbf; // Bit 6 rücksetzen
  //todo  acs          can.setState((byte)driveId, control);  //Todo Muss mit richtigem State verwaltet werden
            return 0;
        }

        /*
        /// <summary>
        /// Setzt 
        /// </summary>
        /// <param name="joint"></param>
        /// <returns></returns>
        public override int SetDriveParam(CPose.SJoint joint) {
            if(joint.isPos_Valid)
                can.CheckCANErrors(can.SetTargetPosition((byte)driveId, (int)joint.axis)); //todo prüfen ob das immer funktioniert
            if (joint.isVel_Valid)
                can.CheckCANErrors(can.SetTargetVelocity((byte)driveId, (int)joint.velocity));
            if (joint.isAcc_Valid)
                can.CheckCANErrors(can.SetAcceleration((byte)driveId, (int)joint.acceleration));
            if (joint.isRamp_Valid)
                can.CheckCANErrors(can.SetRampType((byte)driveId, (int)joint.rampTyp));
            return 0;
        }
        */
        /// <summary>
        /// setzt den aktuellen Betriebsmodus des Motors
        /// Modes aus MODES_OF_OPERATION
        /// </summary>
        /// <param name="mode"></param>
        public  void  SetOperationalMode(byte mode)
        {
            err= can.SetOperationalMode((byte)driveId, mode);
            can.CheckCANErrors(err);

        }

        public void ResetFault()
        {
            err= can.SetState(driveId, 0x80);
            can.CheckCANErrors(err);

        }

        /// <summary>
        /// prüft ob der Motor bereit ist
        /// </summary>
        /// <returns>True wenn Motor Bereit</returns>
        public  bool IsMotorReady() {
             uint c= can.driveControlState[driveId];
    //todo bisher        return (state & 0x67) == 0x027;  // Active
            return (_state & 0x77) == 0x037;  // Active
        }






        enum ControlWord { Target_Reached = 10 };

        /// <summary>
        /// prüft ob Ziel für die Achse erreicht
        /// </summary>
        /// <returns></returns>
        public  bool isTargetReached()
        {
            uint c = can.driveControlState[driveId];

            //    return CDrive.CheckBit(can.driveControlState[driveId], (int)ControlWord.Target_Reached);
          //ru  return CDrive.CheckBit(_state, (int)ControlWord.Target_Reached);
            return true;
        }





        /// <summary>
        /// Rückmeldung der CanMessages auf Antrieb 
        /// PDOs werden ausgewetet
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="messageInfo"></param>
        private  void CCANOpen_OnMessage(object sender, CANMessageInfo messageInfo)
        {
            if((messageInfo.msg.ID & 0x03f) == driveId)   // 7bits ausblendentry
            try{
                // this.BeginInvoke(updatePannelDelegateEvaluate, new object[] { sender, messageInfo });
                
                switch (messageInfo.msg.ID & 0xff80)
                {
                 case 0x180:
                        _state = BitConverter.ToUInt16(messageInfo.msg.DATA, 0);
                        Debug.WriteLine(String.Format("s(a" + driveId + ")=#{0:x}", _state));
                        break;
                 case 0x280:
  //                  this.incPos = BitConverter.ToInt32(messageInfo.msg.DATA, 0); ;
                    bPosition = true;
 //                   Debug.WriteLine("x(a" + driveId + ")=" + drivePos);
                 break;
                 case 0x380:
                        int v = BitConverter.ToInt16(messageInfo.msg.DATA, 0);
  //                      this.driveSpeed = v;
 //                       Debug.WriteLine("v(a"+driveId+")=" + driveSpeed);
                 break;
                 case 0x480:  // PDO auf Inputs gemaped
                        this.input = BitConverter.ToUInt32(messageInfo.msg.DATA, 0);
                        bDigitalInput = true;
                        Debug.WriteLine("io(a" + driveId + ")=" + this.input);
                        break;
                 case 0x580:  // SDO 
                            uint sdo = BitConverter.ToUInt16(messageInfo.msg.DATA, 1);
                            uint value = BitConverter.ToUInt32(messageInfo.msg.DATA, 4);
                            switch (sdo)
                            {
                                case 0x6041:
                                    _state = (ushort)value;
                                    isStateValid = true;
                                    break;
                                case 0x6040:
                                    // Byte 0 = 0x60 => quitirung mit Daten 0
                                    this.control = (ushort)value;
                                    break;
                                case 0x6060:
                                    // Modes of Operation quit
                                    break;
                                case 0x3320:
                                    this.analog = (ushort)value;
                                    bAnalogInput = true;
                                    break;
                                case 0x6062:
//                                    this.drivePos = (int)value;
                                    bPosition = true;
                                    break;
                                case 0x6064:
                                    this.incPos = (int)value;
                                    bPosition = true;
                                    break;
                            }
                            /*
                            if (messageInfo.msg.DATA[1] == 0x41 && messageInfo.msg.DATA[2] == 0x60)
                            {
                                this.state = (ushort)value;
                                isStateValid = true;
                            }
                            else if (messageInfo.msg.DATA[1] == 0x40 && messageInfo.msg.DATA[2] == 0x60)
                            {
                                // Byte 0 = 0x60 => quitirung mit Daten 0
                                this.control = (ushort)value;
                            }
                            else if (messageInfo.msg.DATA[1] == 0x60 && messageInfo.msg.DATA[2] == 0x60)
                            {
                                // Modes of Operation quit
                            }

                            else if (messageInfo.msg.DATA[1] == 0x20 && messageInfo.msg.DATA[2] == 0x33)
                            {
                                this.analog = (ushort)value;
                                bAnalogInput = true;
                            }
                            else if (messageInfo.msg.DATA[1] == 0x62 && messageInfo.msg.DATA[2] == 0x60)// Position Demand Value (SDO 0x6062)
                            {
                                this.drivePos = (int)value;
                                bPosition = true;
                            }
                            else if (messageInfo.msg.DATA[1] == 0x64 && messageInfo.msg.DATA[2] == 0x60)// Position Position actual Value (SDO 0x6064)
                            {
                                this.incPos = (int)value;
                                bPosition = true;
                            }
                            */

                            Debug.WriteLine(String.Format("a{0} sdo(#{1:X}) = {2} #{3:x2}", driveId,sdo, (int)value,value));
                 break;



                    }

            }
            catch { }
        }


        public  decimal GetPosition(int mode) {
            bPosition = false;
            int n=0;
            can.sendSDO(0x600 + driveId, 0x40, 0x6064, 0, 0); // Position actual Value
      //todo mit mode      can.sendSDO(0x600 + driveId, 0x40, 0x6062, 0, 0); // Last Target Position
            can.ReadMessages();
            while (!bPosition)
            {
                if (n++ > 100)
                {
                    MessageHandler.Error(this, 2314, "Timeout Waiting for Position");
                    break;
                }
                Thread.Sleep(10);
                can.ReadMessages();


            }
            return incPos;
          //  return drivePos;
        }

        public uint GetInput()
        {
            bDigitalInput = false;
            int n = 0;
            can.sendSDO(0x600+driveId, 0x2b, 0x60fd, 1, 0);
            while (!bDigitalInput)
            {
                Thread.Sleep(10);
                if (n++ > 100)
                {
                    MessageHandler.Error(this, 2314, "Timeout Waiting for Input");
                    break;
                }
            }
            return input;
        }


        public bool CheckInput(uint id)
        {
            int bitmask = (0x01 << (int)id);
            return ((input & bitmask) == bitmask);
        }


        public void SetOutput(uint value)
        {
            err=can.SetOutput(driveId, value<<16);
            can.CheckCANErrors(err);

        }


        public void SetOutput(uint id, bool bValue) {
            uint new_output = output;
            if (bValue)
                new_output |=  (uint)(0x01 << (int)id); // setzen 
            else
                new_output &= ~(uint)(0x01 << (int)id); // rücksetzen

            err=can.SetOutput(driveId, new_output);

            
        }



        public uint GetAnalog(uint chanalNr)
        {
            int n = 0;
      //      can.OnMessage += new PCanOpen.CMessageEvent(CCANOpen_OnMessage);

            bAnalogInput = false;
            can.GetAnalogInput(this.driveId);
            while (!bAnalogInput)
            {
                Thread.Sleep(10);
                if (n++ > 100)
                {
                    MessageHandler.Error(this, 2314, "Timeout Waiting for Analog");
                    break;
                }
            }


         //   can.OnMessage -= CCANOpen_OnMessage;
            return analog;
        }


        



        public void Homing(uint id, bool bValue)
        {

            can.SetOperationalMode(id, 6); // Homing
            //bValue //cw or ccw
            //Homing Method(0x6098)
            if(bValue)
            can.sendSDO(0x600 + id, 0x2f, 0x6098, 0, 33);
            else
            can.sendSDO(0x600 + id, 0x2f, 0x6098, 0, 34);


            //    Statusword(0x6041)      Bit 12: Homing attained: Auf „1“ gesetzt, wenn Referenzposition erreicht.

            /*    Modus 33: Interne Referenzfahrt
                    • Suche des Index-Strichs des internen Drehgebers
                    • Motor dreht im Uhrzeigersinn
                    • Geschwindigkeit aus Objekt 0x6099_2 (Search for zero)
                    • Bis Indexstrich erreicht
                    • Bei Erreichen des Index-Strichs wird die Richtung umgekehrt
                    • Motor dreht gegen den Uhrzeigersinn
                    • Motor fährt vom Index-Strich herunter
                    • Motor hält an
               Modus 34: Interne Referenzfahrt
                    • Suche des Index-Strichs des internen Drehgebers
                    • Motor dreht gegen den Uhrzeigersinn
                    • Geschwindigkeit aus Objekt 0x6099_2 (Search for zero)
                    • Bis Index-Strich erreicht
                    • Bei Erreichen des Index-Striches wird die Richtung umgekehrt
                    • Motor dreht im Uhrzeigersinn
                    • Motor fährt vom Index-Strich herunter
                    • Motor hält an
                    */

            // Get Analog Value     Analog Input (0x6401)
            can.sendSDO(0x600 + id, 0x40, 0x3320, 1,0 );
            // Verarbeiten der Rückgabe
            Thread.Sleep(10);
            //todo    can.getSDOValue(id, 0x6401);  <== liest SDO Value aus Buffer

            // HomeOffset  Gibt die Differenz zwischen Null - Position der Applikation und dem Referenzpunkt der Maschine an.
            //todo Calculate HomeOffsetValue
            int hoValue = 0;

            // Set Homing Offset  Home Offset (SDO 0x607C)   Typ s32 rw
            can.sendSDO(0x600 + id, 0x23, 0x607C, 0, (uint)hoValue);


            //  Set HomeOffset to Aktual Position
            /*
            Modus 35: Positionsreset
                    • Setzt die aktuelle Position auf Home-Offset, ohne dass die Welle bewegt
                    */
            can.sendSDO(0x600 + id, 0x2f, 0x6098, 0, 35);

        }

        public uint GetInput(uint id, uint sub)
        {
            throw new NotImplementedException();
        }


        public void SetOutput(uint id, uint sub, bool value)
        {
            throw new NotImplementedException();
        }

        public void ExecuteCommand(object sender, string cmd)
        {
            MessageBox.Show("Plugin" + PluginName + " with Command: " + cmd);
        }


        /*
        public override XmlElement SaveSettings(CAppSettings xNode)
        {
            XmlElement vNode = base.SaveSettings(xNode);
        //todo:    xNode.AddData(vNode, "CanController", CanController);
            xNode.AddData(vNode, "num", 5);

            return vNode;
        }
        */
    }
    }
