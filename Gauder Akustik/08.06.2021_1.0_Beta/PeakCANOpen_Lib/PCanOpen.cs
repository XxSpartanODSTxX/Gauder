using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


/// <summary>
/// Inclusion of PEAK PCAN-Basic namespace
/// </summary>
using Peak.Can.Basic;
using TPCANHandle = System.UInt16;
using TPCANBitrateFD = System.String;
using TPCANTimestampFD = System.UInt64;
using System.Windows.Forms;
using System.Threading;
using System.Xml;
using System.Xml.Linq;
using System.Diagnostics;

namespace PeakCANOpen_Lib
{
    public class PCanOpen // : IController
    {
        public string PluginName { get; } = "PCanOpen";
        public string Version { get; } = "0.9.7.1";
        public string ControlName { get; set; }

        public enum NodeState
        {
            Initialisation = 0x00,
            Disconnected = 0x01,
            Connecting = 0x02,
            Preparing = 0x02,
            Stopped = 0x04,
            Operational = 0x05,
            Pre_operational = 0x7F,
            Unknown_state = 0x0F
        };


        public enum AbortCodes
        {
            OD_SUCCESSFUL = 0x00000000,
            OD_READ_NOT_ALLOWED = 0x06010001,
            OD_WRITE_NOT_ALLOWED = 0x06010002,
            OD_NO_SUCH_OBJECT = 0x06020000,
            OD_NOT_MAPPABLE = 0x06040041,
            OD_LENGTH_DATA_INVALID = 0x06070010,
            OD_NO_SUCH_SUBINDEX = 0x06090011,
            OD_VALUE_RANGE_EXCEEDED = 0x06090030, /* Value range test result */
            OD_VALUE_TOO_LOW = 0x06090031, /* Value range test result */
            OD_VALUE_TOO_HIGH = 0x06090032, /* Value range test result */

            /* Others SDO abort codes */
            SDOABT_TOGGLE_NOT_ALTERNED = 0x05030000,
            SDOABT_TIMED_OUT = 0x05040000,
            SDOABT_OUT_OF_MEMORY = 0x05040005, /* Size data exceed SDO_MAX_LENGTH_TRANSFER */
            SDOABT_GENERAL_ERROR = 0x08000000, /* Error size of SDO message */
            SDOABT_LOCAL_CTRL_ERROR = 0x08000021,
        }



        public ListBox lbxInfo;

        private NodeState State = NodeState.Unknown_state;

        public ushort controlState { get; set; }
        public ushort[] driveControlState = new ushort[16];
        public bool[]   isDriveControlValid = new bool[16];

        public String strData;

        public Dictionary<uint, CANMessageInfo> messageStack = new Dictionary<uint, CANMessageInfo>();

        

        //public bool isStateValid { get; set; }

        internal uint[] onEvent = new uint[8];  // todo drf keine feste Zahl sein

        Thread backgroundWorkerMessages;
            

          

        /// <summary>
        /// Saves the handle of a PCAN hardware
        /// </summary>
        private TPCANHandle m_PcanHandle;
        /// <summary>
        /// Saves the baudrate register for a conenction
        /// </summary>
        private TPCANBaudrate m_Baudrate;
        /// <summary>
        /// Saves the type of a non-plug-and-play hardware
        /// </summary>
        private TPCANType m_HwType;
        /// <summary>
        /// Stores the status of received messages for its display
        /// </summary>
        private System.Collections.ArrayList m_LastMsgsList;


        /// <summary>
        /// SIO Port
        /// </summary>
        uint io_port = 0x100;
        /// <summary>
        /// Interrupt
        /// </summary>
        uint cInterrupt = 3;
        /// <summary>
        /// Bei Rückmeldung CANId immer um diesen wert höher
        /// </summary>
        //   uint idOffset = 0x700;

        //   uint SDO_Send = 0x580;
        //   uint SDO_Receive = 0x600;

        public double refAnalogOffsetA1 { get; set; } = 545;
        public double refAnalogFaktorA1 { get; private set; } = -638.8797;
        public double refAnalogOffsetA2 { get; set; } = 548;
        public double refAnalogFaktorA2 { get; private set; } = -509.2397;
        public int    DefaultRefMode    { get; private set; } = 33;  // DefaultModus für Regerenzpunktfahrt (Siher Referenz Manual)
        public bool   isFatalError { get; private set; }
        public bool bDebug { get; internal set; }

        enum DriveID { drive1 = 1, drive2 = 2, drive3 = 3, drive4 = 4 };

        public PCanOpen()
        {
        m_PcanHandle = 81;
            backgroundWorkerMessages =   new Thread(new ThreadStart(backgroundWorkerMessages_DoWork));

        }

        public TPCANStatus CanInit()
        {
            //todo  Init des Peak Can Controllers
            TPCANStatus stsResult = TPCANStatus.PCAN_ERROR_UNKNOWN;

            m_Baudrate = TPCANBaudrate.PCAN_BAUD_1M;

            // Connects a selected PCAN-Basic channel
            //
            try
            {
                stsResult = PCANBasic.GetStatus(m_PcanHandle);
                if (stsResult != TPCANStatus.PCAN_ERROR_OK)
                {
                    Console.WriteLine(GetFormatedError(stsResult));
                    stsResult = PCANBasic.Reset(m_PcanHandle);
                    stsResult = PCANBasic.Uninitialize(m_PcanHandle);
                    stsResult = PCANBasic.Initialize(
                            m_PcanHandle,
                            m_Baudrate,
                            m_HwType,
                            Convert.ToUInt32(io_port),
                            Convert.ToUInt16(cInterrupt));
                }

                if (stsResult != TPCANStatus.PCAN_ERROR_OK)
                    if (stsResult != TPCANStatus.PCAN_ERROR_CAUTION)
                    {
                        // MessageBox.Show(GetFormatedError(stsResult));
                        Console.WriteLine(GetFormatedError(stsResult));
                        MessageHandler.Error(this, 2932, "Error: Initalize CAN " + GetFormatedError(stsResult));
                        isFatalError = true;
                        
                    }
                    else
                    {
                        IncludeTextMessage("******************************************************");
                        IncludeTextMessage("The bitrate being used is different than the given one");
                        IncludeTextMessage("******************************************************");
                        MessageHandler.Error(this, 2933, "Error: The CAN bitrate being used is different than the given one " + GetFormatedError(stsResult));

                        stsResult = TPCANStatus.PCAN_ERROR_OK;
                    }
                else
                {
                    // Prepares the PCAN-Basic's PCAN-Trace file
                    //
                    ConfigureTraceFile();
                    isFatalError = false;
                }

                m_LastMsgsList = new System.Collections.ArrayList();

                // Sets the connection status of the main-form
                //
                //    SetConnectionStatus(stsResult == TPCANStatus.PCAN_ERROR_OK);
                // MessageHandler.Message();

                //  backgroundWorkerMessages.WorkerSupportsCancellation = true;

                if (!backgroundWorkerMessages.IsAlive)
                    backgroundWorkerMessages.Start();

                backgroundWorkerMessages.IsBackground = true;

            }
            catch (Exception ex)
            {
               // MessageHandler.Error(this, 2934, "Kann CAN-Bus nich tinitialisieren", ex);
                MessageHandler.Error(this, 2934, "Kann CAN-Bus nich tinitialisieren");
            }
            return stsResult;
        }



        public TPCANStatus CheckCANErrors(TPCANStatus stsResult)
        {
            if (stsResult != TPCANStatus.PCAN_ERROR_OK)
            {
                if (stsResult == TPCANStatus.PCAN_ERROR_BUSHEAVY ||
                    stsResult == TPCANStatus.PCAN_ERROR_NODRIVER ||
                    stsResult == TPCANStatus.PCAN_ERROR_INITIALIZE ||
                    stsResult == TPCANStatus.PCAN_ERROR_BUSOFF)
                {
                    isFatalError = true;
                    MessageHandler.Error(this, 2937, "CAN Fatal Error: " + GetFormatedError(stsResult));
                }
                else
                    MessageHandler.Error(this, 2938, "CAN Error: " + GetFormatedError(stsResult));
            }
            return stsResult;
        }



        /// <summary>
        /// thread der die Message ausliest
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void backgroundWorkerMessages_DoWork()
        {
            do
            {
               ReadMessages();
               Thread.Sleep(10);
            } while (!isFatalError);
        }


        private TPCANStatus sendSDOabort(byte CliServNbr, UInt16 index, byte subIndex, AbortCodes abortCode)
        {
            byte[] data = new byte[8];
            TPCANStatus ret;

            Debug.WriteLine("Sending SDO abort {0}", abortCode.ToString(), null);
            data[0] = 0x80;
            /* Index */
            data[1] = (byte)(index & 0xFF); /* LSB */
            data[2] = (byte)((index >> 8) & 0xFF); /* MSB */
                                                   /* Subindex */
            data[3] = subIndex;
            /* Data */
            data[4] = (byte)((byte)abortCode & 0xFF);
            data[5] = (byte)(((byte)abortCode >> 8) & 0xFF);
            data[6] = (byte)(((byte)abortCode >> 16) & 0xFF);
            data[7] = (byte)(((byte)abortCode >> 24) & 0xFF);
            ret = sendSDO(CliServNbr, data);
            return ret;
        }

        private TPCANStatus sendSDO( byte CliServNbr, byte[] Data)
        {
            UInt16 offset;
            byte i;
            TPCANMsg m = new TPCANMsg();
            m.DATA = new byte[8];


            if (bDebug) Debug.WriteLine("sendSDO");
            if (!((State == NodeState.Operational) || (State == NodeState.Pre_operational)))
            {
                Debug.WriteLine("unable to send the SDO (not in op or pre-op mode) {0:X}", State);
                return TPCANStatus.PCAN_ERROR_UNKNOWN; //?  0xFF;
            }

            /*get the server->client cobid*/
            /*
            if (whoami == SDO_SERVER)
            {
                offset = device.quickIndexFirst.SDO_SVR;
                if ((offset == 0) || ((offset + CliServNbr) > device.quickIndexLast.SDO_SVR))
                {
                    System.Diagnostics.Debug.WriteLine("SendSDO : SDO server not found");
                    return 0xFF;
                }
                m.ID = (UInt16)(Convert.ToUInt32(device.ObjectDictionary[(UInt16)((offset + (UInt16)CliServNbr))].SubEntry[2].Value));
                System.Diagnostics.Debug.WriteLine("I am server Tx cobId : {0:X}", m.ID);
            }
            else
            {	//case client
                // Get the client->server cobid.
                offset = device.quickIndexFirst.SDO_CLT;
                if ((offset == 0) || ((offset + CliServNbr) > device.quickIndexLast.SDO_CLT))
                {
                    System.Diagnostics.Debug.WriteLine("SendSDO : SDO client not found");
                    return 0xFF;
                }
                m.ID = (UInt16)(Convert.ToUInt32(device.ObjectDictionary[(UInt16)(offset + (UInt16)CliServNbr)].SubEntry[1].Value));
                System.Diagnostics.Debug.WriteLine("I am client Tx cobId : {0:X}", m.ID);
            }
        */
            /* message copy for sending */
            m.MSGTYPE = TPCANMessageType.PCAN_MESSAGE_STANDARD; ;
            /* the length of SDO must be 8 */
            m.LEN = 8;
            for (i = 0; i < 8; i++)
            {
                m.DATA[i] = Data[i];
            }

            return PCANBasic.Write(m_PcanHandle, ref m);

          
        }
        public TPCANStatus sendPDO(uint id, byte[] data)
        {
            TPCANMsg CANMsg;
 
            // We create a TPCANMsg message structure 
            //
            CANMsg = new TPCANMsg();
            CANMsg.DATA = new byte[8];

            // We configurate the Message.  The ID,
            // Length of the Data, Message Type
            // and the data
            //
            CANMsg.ID = Convert.ToUInt32(id);
            CANMsg.LEN = Convert.ToByte(8);
            CANMsg.MSGTYPE = TPCANMessageType.PCAN_MESSAGE_STANDARD;
            // If a remote frame will be sent, the data bytes are not important.
            //
                 // We get so much data as the Len of the message
                //

                for(int i=0; i< data.Length; i++)
                 CANMsg.DATA[i] = data[i];



            if (bDebug) Console.WriteLine("out> #{0:x} :\t{1}", CANMsg.ID, BitConverter.ToString(CANMsg.DATA));


            // The message is sent to the configured hardware
            //
            return PCANBasic.Write(m_PcanHandle, ref CANMsg);
        }


        public TPCANStatus sendRemoteSDO(uint id, byte cmd, ushort sdoindex, byte sub)
        {
            return sendSDO( id, true, cmd, sdoindex, sub , new byte[8]);
        }

        public TPCANStatus sendSDO(uint id,  byte cmd, ushort sdo, byte sub, uint data)
        {
            if(bDebug) Debug.WriteLine(String.Format("sdo: #{0:X} sdo(#{1:X}.{2}) = {3} #{4:x2}", id, sdo, sub,(int)data,data));
            return sendSDO(id, false, cmd, sdo, sub, BitConverter.GetBytes(data));
        }

        // Befehle bei CANOpen: http://www.byteme.org.uk/canopenparent/canopen/sdo-service-data-objects-canopen/

        public TPCANStatus sendSDO(uint id, bool bRemote, byte cmd, ushort sdoindex, byte subindex, byte[] data)
        {
            TPCANMsg CANMsg;
            //   TextBox txtbCurrentTextBox;

            // We create a TPCANMsg message structure 
            //
            CANMsg = new TPCANMsg();
            CANMsg.DATA = new byte[8];

            // We configurate the Message.  The ID,
            // Length of the Data, Message Type
            // and the data
            //
            CANMsg.ID = Convert.ToUInt32(id);
            CANMsg.LEN = Convert.ToByte(8);
            CANMsg.MSGTYPE = TPCANMessageType.PCAN_MESSAGE_STANDARD;
            // If a remote frame will be sent, the data bytes are not important.
            //
            if (bRemote)
                CANMsg.MSGTYPE |= TPCANMessageType.PCAN_MESSAGE_RTR;
            else
            {
                // We get so much data as the Len of the message
                //
                //  Console.WriteLine(BitConverter.IsLittleEndian);

                //  Kann Header     rtr len Byte 0 Byte 1  Byte 2   Byte 3    Byte 4  Byte 5  Byte 6  Byte 7
                //  0x600 + Knoten  0   8   Befehl Index            Subindex  Daten

                // Befehl 23 schreiben 4 Byte
                // Befehl 2B schreiben 2 Byte
                // Befehl 2F schreiben 1 Byte


                // Abfrage 40 daten   0 Bytes

                // Antwort 40 antwort 0 Bytes Abfrage
                // Antwort 60 antwort 4 Bytes auf Befehl

                // Antwort 43 antwort 4 Bytes auf Frage
                // Antwort 4B antwort 2 Bytes auf Frage
                // Antwort 4F antwort 1 Bytes auf Frage

                // SDO-Transfer abbrechen  80

                // subindex 0 bei einem Wert
                // subindex 1,2, .. bei mehreren Werten

                // CCD Meaning Valid for
                // 0x23 Write 4 bytes SDO request
                // 0x27 Write 3 bytes SDO request
                // 0x2B Write 2 bytes SDO request
                // 0x2F Write 1 byte SDO request
                // 0x60 Write successful SDO response
                // 0x80 Error SDO response
                // 0x40 Read request SDO request
                // 0x43 4 bytes of data SDO response to read request
                // 0x47 3 bytes of data SDO response to read request
                // 0x4B 2 bytes of data SDO response to read request
                // 0x40 1 byte of data SDO response to read request
                // Table 4: Command codes for SDO

                // http://www.canopensolutions.com/english/about_canopen/device_configuration_canopen.shtml

                CANMsg.DATA[0] = cmd; 

                byte[] sdo = BitConverter.GetBytes(sdoindex);
                CANMsg.DATA[1] = sdo[0]; 
                CANMsg.DATA[2] = sdo[1]; 

                CANMsg.DATA[3] = subindex;

                CANMsg.DATA[4] = data[0];
                CANMsg.DATA[5] = data[1];
                CANMsg.DATA[6] = data[2];
                CANMsg.DATA[7] = data[3];

            }


       //     Console.WriteLine("{0:x} :\t{1}", CANMsg.ID, BitConverter.ToString(CANMsg.DATA));


            // The message is sent to the configured hardware
            //
            return PCANBasic.Write(m_PcanHandle, ref CANMsg);

        }


        public CANMessageInfo CheckOutMessage(uint id)
        {
            if (messageStack.ContainsKey(id))
            {
                return messageStack[id];
            }
            else return new CANMessageInfo();
        }

        public TPCANStatus InitDrive(byte driveID, byte command)
        {
            // CANopen-Node hochfahren
            /*  2.2 CANopen - Node hochfahren
              Status „Operational“
            Um die Funktionalität der Steuerung nutzen zu können, muss nach jedem Einschalten der Steuerung diese in den Operational-Status versetzt werden.
            Dies geschieht durch das Senden einer Network - Management - Nachricht mit der COB - ID 0x0 und dem 2 Byte langem Inhalt: < Kommando > und < Node - ID >.
                  Eingabe in IXXAT MiniMon
            Die komplette Eingabe in IXXAT MiniMon lautet: „0 1 22“.
            • 0 : COB - ID für NMT Nachricht
            • 1: Starte Node
            • 22 : CANopen Node-ID(hier 0x22 bzw. 34)
            Kommandos
            Die Kommandos sind:
            • 0x01: Start Node(Wechsel zu Operational, Status 0x05)
            • 0x02: Stop Node(Wechsel zu Stopped, Status 0x04)
            • 0x80: Wechsel zu Pre - Operational(Status 0x7F, Zustand nach Anlegen der Betriebsspannung)
            • 0x81: Neustart der Firmware, Rücksetzen aller CANopen - Einstellungen auf zuletzt um EEPROM abgelegte Werte
            • 0x82: Neustart der Firmware, Rücksetzen aller CANopen - Einstellungen auf zuletzt um EEPROM abgelegte Werte */

            // CANopen-Node-Status abfragen
            /* 2.3 CANopen - Node - Status abfragen
                Statusabfrage
            Der Status kann mit einem Remote Transmission Request(RTR) auf COB-ID 0x700 + Node - ID abgefragt werden.
            Ein Motor mit der Node - ID 34(dec) sendet seinen Netzwerkstatus auf der COB-ID 0x700 + 34 = 0x722.
            Um diese Nachricht zu Empfangen, muss ein Remote Transmission Request(RTR) für diese COB - ID gesendet werden.
            Es ist auch möglich, den Motor diese Nachricht zyklisch senden zu lassen(siehe SDO 0x1017: Dynamic Heartbeat Time).
            Mögliche Status
            Es gibt folgende Status:
            • Status Pre-operational(Zustand nach Anlegen der Betriebsspannung, nach Neustart und Reset): 0x7F In diesem Zustand können SDOs abgefragt und geschrieben werden aber keine PDOs gelesen oder geschrieben werden.
            • Status Stopped: 0x04 In diesem Modus können weder SDOs noch PDOs abgefragt werden.
            • Status Operational: 0x05 In diesem Modus können sowohl SDOs als auch PDOs gelesen und geschrieben werden */

            // Leistungsteil einschalten
            /* Controlword
             Das Einschalten des Leistungsteils geschieht über das Controlword.Dieses ist unter dem Service - Daten - Objekt(SDO) 0x6040 erreichbar.
             Abfrage des Statusword
             Nach Senden jedes Kommandos wird empfohlen, durch Abfrage des Statusword zu überprüfen, ob der beabsichtigte Status erreicht wurde, 
             da Statusübergänge verhindert werden(z.B.durch einen Unterspannungsfehler) oder sich verzögern können
             (z.B.durch die Verzögerungszeit der mechanischen Bremse oder durch die Ausführungszeit internen Übergänge). */

            // Operationsmodus auswählen
            TPCANMsg CANMsg = new TPCANMsg();
            CANMsg.DATA = new byte[8];
            CANMsg.ID = Convert.ToUInt32(0x00);
            CANMsg.LEN = Convert.ToByte(2);
            CANMsg.MSGTYPE = TPCANMessageType.PCAN_MESSAGE_STANDARD;
            CANMsg.DATA[0] = command;  // Kommando: Start Node
            CANMsg.DATA[1] = driveID;  // Node ID
            return PCANBasic.Write(m_PcanHandle, ref CANMsg);
        }


        //CANopen-Node-Status abfragen
        public TPCANStatus getStatus(uint driveID)
        {

            // sendRemoteSDO(0x700 + id, 0, 0, 0);
            /* Statusabfrage
            Der Status kann mit einem Remote Transmission Request(RTR) auf COB-ID 0x700 + Node - ID abgefragt werden.
            Ein Motor mit der Node - ID 34(dec) sendet seinen Netzwerkstatus auf der COB-ID 0x700 + 34 = 0x722.
            Um diese Nachricht zu Empfangen, muss ein Remote Transmission Request(RTR) für diese COB - ID gesendet werden.
            Es ist auch möglich, den Motor diese Nachricht zyklisch senden zu lassen(siehe SDO 0x1017: Dynamic Heartbeat Time).
            Mögliche Status
            Es gibt folgende Status:
            • Status Pre-operational(Zustand nach Anlegen der Betriebsspannung, nach Neustart und Reset): 0x7F In diesem Zustand können SDOs abgefragt und geschrieben werden aber keine PDOs gelesen oder geschrieben werden.
            • Status Stopped: 0x04 In diesem Modus können weder SDOs noch PDOs abgefragt werden.
            • Status Operational: 0x05 In diesem Modus können sowohl SDOs als auch PDOs gelesen und geschrieben werden
            */
            //isStateValid = false;
            return sendSDO(0x600 + driveID, 0x40, 0x6041, 0, 0);

            
        }

        enum PositionMode{ Switch_On=0 , Enable_Voltage=1, Quick_Stop=2, Enable_Operation=3, Relative_Position=6, Fault_Reset = 7, Halt = 8 }

        /// <summary>
        /// setzt den Status der Powerstate Machine
        /// </summary>
        /// <param name="id"></param>
        /// <param name="state"></param>
        /// <returns></returns>
        public TPCANStatus SetState(uint driveID, uint state)
        {
            /*
                Bit 0: Switch On
                Bit 1: Enable Voltage
                Bit 3: Enable Operation
                Mit Bit 0, 1 und 3 wird der Motor vom Zustand Ausgeschaltet bis Betriebsbereit kommandiert(siehe auch Abschnitt 2).
                Die Zustände sind:
                • Ausgeschaltet(Switch On Disabled)
                • Bereit zum Einschalten(Ready to Switch On)
                • Eingeschaltet(Switch On)
                • Betriebsbereit(Operation Enabled)
                Ab dem Zustand Eingeschaltet(Switch On) ist die Haltebremse gelöst und das elektrische Feld des Motors aktiv.Eine Bewegung des Motors ist nur im Zustand Betriebsbereit(Operation Enabled) möglich.
                Zusätzliche Zustände sind:
                • Ausführung der Schnellbremsung(Quick Stop Active)
                • Reagieren auf Fehler(Fault Reaction Active)
                • Fehler(Fault)
                Die Zustände müssen von Ausgeschaltet bis Betriebsbereit in der angegebenen Reihenfolge durchlaufen werden.Das geschieht durch das aufeinander folgende
                Setzen der Bits 0, 1 und 3.Am Ende des Einschaltvorganges sind alle drei Bits gesetzt.
                                Bit 2
                Quick Stop(invertiert: 0 bedeutet Quick Stop aktivieren)
                Bit 2 muss immer auf „1“ gesetzt sein, außer es ist eine Schnellbremsung(Quick Stop) gefordert.Wird dieses Bit auf „0" gesetzt, führt der Motor eine Schnellbremsung aus. Während der Schnellbremsung befindet sich der Motor im Zustand „Quick Stop Active“. Nach der Schnellbremsung geht der Motor automatisch in den Zustand „Switch On Disabled“.
                Bit 4 bis 15
                Bit 4 bis 6: Modusspezifisch.  Bit6:(Absolute Position <=> Relative Position)
                Bit 7: Fault Reset.              0x80
                Ist ein Fehler aufgetreten, befindet sich die Firmware nach der Fehlerreaktion im Zustand Fault.Um die Firmware in „Switch On Disabled“ zurück zu versetzen, muss dieses Bit einen Übergang von „0“ nach „1“ ausführen(eine Dauer -„1“ reicht hier nicht).
                Bit 8: Halt(Modusspezifisch).  0x0100
                Bit 9: Modusspezifisch.
                Bit 10: Reserviert.
                Bit 11 bis 15: Herstellerspezifisch.
                */
           // isStateValid = false;
            return sendSDO(0x600 + driveID, 0x2b, 0x6040, 0, state);
            
            //todo  sendPDO(0x200 + driveID, BitConverter.GetBytes(state));

           
        }

        // Operationsmodus auswählen
        /* Voraussetzung
            Änderungen des Modus können im Status „Operation Enabled“ erfolgen.
            Es sollte dabei sichergestellt werden, dass sich der Motor beim Kommandieren eines Moduswechsels nicht bewegt.
            Beispiel
            Am Beispiel des PP-Modus (Profile Position bzw. Positionier-Modus) wird die Auswahl eines Modus aufgezeigt:
            COB-ID
            Datenbytes
            Beschreibung
            622            2F 60 60 00 01            Modus: Profile Position(PP)
            5A2            60 60 60 00 00 00 00 00   Antwort: OK */
        public TPCANStatus SetOperationalMode(uint driveID, byte mode)
        {
            return sendSDO(0x600 + driveID, 0x2f, 0x6060, 0, mode);
            

        }

        public TPCANStatus GetOperationalMode(uint driveID)
        {
            return sendSDO(0x600 + driveID, 0x40, 0x6060, 0, 0);
        }



        /// <summary>
        /// setzen der Zielposition
        /// </summary>
        /// <param name="driveID"></param>
        /// <param name="pos"></param>
        /// <returns></returns>
        public TPCANStatus SetTargetPosition(uint driveID, int pos)
        {
            return sendSDO(0x600 + driveID, 0x23, 0x607a, 0, (uint)pos);

        }

        /// <summary>
        /// setzen der Zielgeschwindigkeit
        /// </summary>
        /// <param name="driveID"></param>
        /// <param name="pos"></param>
        /// <returns></returns>
        public TPCANStatus SetTargetVelocity(uint driveID, int vel)
        {
            return sendSDO(0x600 + driveID, 0x23, 0x6081, 0, (uint)vel);

        }


        public TPCANStatus SetAcceleration(uint driveID, int acc)
        {
                   sendSDO(0x600 + driveID, 0x23, 0x6083, 0, (uint)acc);
            return sendSDO(0x600 + driveID, 0x23, 0x6084, 0, (uint)acc);
                        
        }

        public TPCANStatus SetRampType(uint driveID, int rampType)
        {
            return sendSDO(0x600 + driveID, 0x2b, 0x6086, 0, (uint)rampType);
        }


        public TPCANStatus SetHomingMethod(uint driveID, int homingMethod)
        {
            return sendSDO(0x600 + driveID, 0x2f, 0x6098, 0, (uint)homingMethod);
        }

        public TPCANStatus SetHomeOffset(uint driveID, int homeOffset)
        {
            return sendSDO(0x600 + driveID, 0x23, 0x607C, 0, (uint)homeOffset);
        }
        


        public TPCANStatus GetAnalogInput(uint driveID)
        {
              //   sendSDO(0x600 + driveID, 0x40, 0x6401, 1, 0);
            return sendSDO(0x600 + driveID, 0x40, 0x3320, 1, 0);
        }

        public TPCANStatus SetOutput(uint driveID, uint value)
        {
            return sendSDO(0x600 + driveID, 0x23, 0x60FE, 1, value);
        }

        public TPCANStatus GetInput(uint driveID)
        {
            return sendSDO(0x600 + driveID, 0x40, 0x60FD, 0, 0);
        }

        public TPCANStatus GetPosition(uint driveID)
        {
            return sendSDO(0x600 + driveID, 0x40, 0x6064, 0, 0);
        }


        public void MoveDrive()
        {
            // Fahrt starten
            // Neue Endposition vorgeben
           

        }


        

        /// <summary>
        /// Function for reading CAN messages on normal CAN devices
        /// </summary>
        /// <returns>A TPCANStatus error code</returns>
        private TPCANStatus ReadMessage()
        {
            TPCANMsg CANMsg;
            TPCANTimestamp CANTimeStamp;
            TPCANStatus stsResult;

            // We execute the "Read" function of the PCANBasic                
            //
            lock (this)
            {
                stsResult = PCANBasic.Read(m_PcanHandle, out CANMsg, out CANTimeStamp);
                if (stsResult != TPCANStatus.PCAN_ERROR_QRCVEMPTY)
                    if (stsResult == TPCANStatus.PCAN_ERROR_OK)
                        ProcessMessage(CANMsg, CANTimeStamp);// We process the received message
                    else
                    {
                        Debug.WriteLine("CAN Error" + GetFormatedError(stsResult)); // todo Message auswerten
//                        MessageInfo info = new MessageInfo(this, 2935, MessageTyp.Error,"Error CanBus read: ");
//                        info.sCommand = GetFormatedError(stsResult);
 //                       info.data = stsResult;
  //                      MessageHandler.Message(info);
                        MessageHandler.Message(this, 2935, MessageTyp.Error, "Error CanBus read: ");
                    }

            }

            return stsResult;
        }

        /// <summary>
        /// Function for reading PCAN-Basic messages
        /// </summary>
        public  bool ReadMessages()
        {
            TPCANStatus stsResult;
            if (isFatalError) return false;
            // We read at least one time the queue looking for messages.
            // If a message is found, we look again trying to find more.
            // If the queue is empty or an error occurr, we get out from
            // the dowhile statement.
            //			
            do
            {
                stsResult = ReadMessage();
                if (stsResult == TPCANStatus.PCAN_ERROR_ILLOPERATION
                    || stsResult == TPCANStatus.PCAN_ERROR_INITIALIZE)
                    break;
            } while ( !Convert.ToBoolean(stsResult & TPCANStatus.PCAN_ERROR_QRCVEMPTY));
            return true;
        }


        /// <summary>
        /// Inserts a new entry for a new message in the Message-ListView
        /// </summary>
        /// <param name="newMsg">The messasge to be inserted</param>
        /// <param name="timeStamp">The Timesamp of the new message</param>
        private void InsertMsgEntry(TPCANMsgFD newMsg, TPCANTimestampFD timeStamp)
        {
            MessageStatus msgStsCurrentMsg;
            //ListViewItem lviCurrentItem;

            lock (m_LastMsgsList.SyncRoot)
            {
                // We add this status in the last message list
                //
                msgStsCurrentMsg = new MessageStatus(newMsg, timeStamp, 1 /*, lstMessages.Items.Count*/);
                msgStsCurrentMsg.ShowingPeriod = true; // chbShowPeriod.Checked;
                m_LastMsgsList.Add(msgStsCurrentMsg);

                // Add the new ListView Item with the Type of the message
                //	
              //  lviCurrentItem = lstMessages.Items.Add(msgStsCurrentMsg.TypeString);
                // We set the ID of the message
                //
               // lviCurrentItem.SubItems.Add(msgStsCurrentMsg.IdString);
                // We set the length of the Message
                //
              //  lviCurrentItem.SubItems.Add(GetLengthFromDLC(newMsg.DLC, (newMsg.MSGTYPE & TPCANMessageType.PCAN_MESSAGE_FD) == 0).ToString());
                // we set the message count message (this is the First, so count is 1)            
                //
               // lviCurrentItem.SubItems.Add(msgStsCurrentMsg.Count.ToString());
                // Add time stamp information if needed
                //
               // lviCurrentItem.SubItems.Add(msgStsCurrentMsg.TimeString);
                // We set the data of the message. 	
                //
               // lviCurrentItem.SubItems.Add(msgStsCurrentMsg.DataString);
            }
        }

        /// <summary>
        /// Processes a received message, in order to show it in the Message-ListView
        /// </summary>
        /// <param name="theMsg">The received PCAN-Basic message</param>
        /// <returns>True if the message must be created, false if it must be modified</returns>
        private void ProcessMessage(TPCANMsgFD theMsg, TPCANTimestampFD itsTimeStamp)
        {
            // We search if a message (Same ID and Type) is 
            // already received or if this is a new message
            //
            lock (m_LastMsgsList.SyncRoot)
            {
                foreach (MessageStatus msg in m_LastMsgsList)
                {
                    if ((msg.CANMsg.ID == theMsg.ID) && (msg.CANMsg.MSGTYPE == theMsg.MSGTYPE))
                    {
                        // Modify the message and exit
                        //
                        msg.Update(theMsg, itsTimeStamp);
                        return;
                    }
                }
                // Message not found. It will created
                //
                InsertMsgEntry(theMsg, itsTimeStamp);
                


            }
            
        }

        /// <summary>
        /// Processes a received message, in order to show it in the Message-ListView
        /// </summary>
        /// <param name="theMsg">The received PCAN-Basic message</param>
        /// <returns>True if the message must be created, false if it must be modified</returns>
        private void ProcessMessage(TPCANMsg theMsg, TPCANTimestamp itsTimeStamp)
        {
            
          //  lock (m_LastMsgsList.SyncRoot)
            {
                /*
                foreach (MessageStatus msg in m_LastMsgsList)
                {
                    if ((msg.CANMsg.ID == theMsg.ID) && (msg.CANMsg.MSGTYPE == theMsg.MSGTYPE))
                    {
                        // Modify the message and exit
                        //
                        // msg.Update(theMsg, itsTimeStamp);
                        return;
                    }
                }
                // Message not found. It will created
                //
                //todo    InsertMsgEntry(theMsg, itsTimeStamp);
                */

            }
            //string str = "";
            /*
            
            byte d1 = 0;
            byte d2 = 0;
            
            if (((theMsg.ID & 0x0580) == 0x580 && theMsg.DATA[1]==0x41 &&  theMsg.DATA[2] == 0x60)|| (theMsg.ID & 0x0280) == 0x180)
            {
                lock (m_LastMsgsList.SyncRoot)
                {
                    if ((theMsg.ID & 0x0280) == 0x180)
                    {
                        d1 = theMsg.DATA[0];
                        d2 = theMsg.DATA[1];
                    }
                    else
                    {
                        d1 = theMsg.DATA[4];
                        d2 = theMsg.DATA[5];
                    }

                    controlState = (ushort)(d2 << 8 | d1); // Setzt die Statusveriable
                    isStateValid = true;

                    driveControlState[theMsg.ID & 0x3f] = controlState;
                    isDriveControlValid[theMsg.ID & 0x3f] = true;
                   
                }


                if ((d1 & 0x01) == 0x01) str += " ready_to_switch_on"; //Bit 0: ready to switch on
                if ((d1 & 0x02) == 0x02) str += " switched_on"; // Bit 1: switched on
                if ((d1 & 0x04) == 0x04) str += " operation_enabled"; // Bit 2: operation enabled: Der eingestellt Operationsmodus ist aktiv und nimmt Befehle entgegen(z.B.Profile Position Mode)
                if ((d1 & 0x08) == 0x08) str += " fault"; //Bit 3: fault: Wird im Fehlerfall gesetzt
                                
                if ((d1 & 0x10) == 0x10) str += " activ"; else string.Concat(" inactiv");   //Bit 4: voltage enabled: Bit ist gesetzt, wenn Motor bestromt wird
                if ((d1 & 0x20) == 0x20) str += " QuikStop";   //Bit 5: quick stop
                if ((d1 & 0x40) == 0x40) str += " switch_on_disabled";   //Bit 6: switch on disabled
                if ((d1 & 0x80) == 0x80) str += " warning";    //Bit 7: warning

           
                if ((d2 & 0x01) == 0x01) str += " Bit8";  //Bit 8: PLL sync complete: Wird gesetzt, sobald die Synchronisation mit dem SYNC - Objekt abgeschlossen ist. 
                if ((d2 & 0x02) == 0x02) str += " Bit9";  //Bit 9: remote
                if ((d2 & 0x04) == 0x04) str += " target_reached";     //Bit 10: target reached: Wird gesetzt, wenn der Motor sein Ziel erreicht hat(Profile Positi-on Mode) 
                if ((d2 & 0x08) == 0x80) str += " Bit11";  //Bit 11: internal limit active: Wird gesetzt, wenn die Sollwerte die Maximalgrenzen über-schreiten.
                if ((d2 & 0x10) == 0x10) str += " Bit12";  //Bit 12: Modusspezifisch
                if ((d2 & 0x20) == 0x20) str += " Bit13";  //Bit 13: Modusspezifisch
                if ((d2 & 0x40) == 0x40) str += " Bit14";  //Bit 14: Herstellerspezifisch(nicht genutzt)
                if ((d2 & 0x80) == 0x80) str += " Bit15";  //Bit 15: Herstellerspezifisch(nicht genutzt)


            }
            else if ((theMsg.ID & 0x0580)  == 0x580) // SDO
            {
                str = string.Format(" =  {0}", BitConverter.ToInt32(theMsg.DATA, 4));
            }
            if ((theMsg.ID & 0x0600) == 0x0600) // Set SDO Befehl
            {
                str = string.Format("{0}", BitConverter.ToInt32(theMsg.DATA, 4));
            }

            if ((theMsg.ID & 0x0280) == 0x0280) // Position
            {
                str = string.Format("{0}",BitConverter.ToInt32(theMsg.DATA,0));
            }
            if ((theMsg.ID & 0x0380) == 0x380) // Velocity
            {
                str = string.Format("{0}", BitConverter.ToInt16(theMsg.DATA, 0));
            }
            */

            CANMessageInfo messageInfo = new CANMessageInfo(theMsg, itsTimeStamp, string.Format("{0:x} :\t{1}", theMsg.ID, BitConverter.ToString(theMsg.DATA)));

            if (messageStack.ContainsKey(theMsg.ID))
                messageStack[theMsg.ID] = messageInfo;
            else
                messageStack.Add(theMsg.ID, messageInfo);





           if (bDebug) Debug.WriteLine("in> "+ messageInfo.sCommand);
            //NotifyMessage(theMsg, itsTimeStamp,string.Format("{0:x} :\t{1}\t{2}", theMsg.ID, BitConverter.ToString(theMsg.DATA), str));
            // NotifyMessage(theMsg, itsTimeStamp,"");
            NotifyMessage(messageInfo);


        }

        /// <summary>
        /// Help Function used to get an error as text
        /// </summary>
        /// <param name="error">Error code to be translated</param>
        /// <returns>A text with the translated error</returns>
        public string GetFormatedError(TPCANStatus error)
        {
            StringBuilder strTemp;

            // Creates a buffer big enough for a error-text
            //
            strTemp = new StringBuilder(256);
            // Gets the text using the GetErrorText API function
            // If the function success, the translated error is returned. If it fails,
            // a text describing the current error is returned.
            //
            if (PCANBasic.GetErrorText(error, 0, strTemp) != TPCANStatus.PCAN_ERROR_OK)
                return string.Format("An error occurred. Error-code's text ({0:X}) couldn't be retrieved", error);
            else
                return strTemp.ToString();
        }

        /// <summary>
        /// Includes a new line of text into the information Listview
        /// </summary>
        /// <param name="strMsg">Text to be included</param>
        private void IncludeTextMessage(string strMsg)
        {
            Console.WriteLine(strMsg);

            if (lbxInfo != null) { 
              lbxInfo.Items.Add(strMsg);
              lbxInfo.SelectedIndex = lbxInfo.Items.Count - 1;
            }
        }
        

        /// <summary>
        /// Configures the PCAN-Trace file for a PCAN-Basic Channel
        /// </summary>
        private void ConfigureTraceFile()
        {
            UInt32 iBuffer;
            TPCANStatus stsResult;

            // Configure the maximum size of a trace file to 5 megabytes
            //
            iBuffer = 5;
            stsResult = PCANBasic.SetValue(m_PcanHandle, TPCANParameter.PCAN_TRACE_SIZE, ref iBuffer, sizeof(UInt32));
            if (stsResult != TPCANStatus.PCAN_ERROR_OK)
                IncludeTextMessage(GetFormatedError(stsResult));
            // MessageHandler.Error(this,92342,GetFormatedError(stsResult));

            // Configure the way how trace files are created: 
            // * Standard name is used
            // * Existing file is ovewritten, 
            // * Only one file is created.
            // * Recording stopts when the file size reaches 5 megabytes.
            //
            iBuffer = PCANBasic.TRACE_FILE_SINGLE | PCANBasic.TRACE_FILE_OVERWRITE;
            stsResult = PCANBasic.SetValue(m_PcanHandle, TPCANParameter.PCAN_TRACE_CONFIGURE, ref iBuffer, sizeof(UInt32));
            if (stsResult != TPCANStatus.PCAN_ERROR_OK)
                IncludeTextMessage(GetFormatedError(stsResult));
            // MessageHandler.Error(this,92342,GetFormatedError(stsResult));
        }

        /// <summary>
        /// Convert a CAN DLC value into the actual data length of the CAN/CAN-FD frame.
        /// </summary>
        /// <param name="dlc">A value between 0 and 15 (CAN and FD DLC range)</param>
        /// <param name="isSTD">A value indicating if the msg is a standard CAN (FD Flag not checked)</param>
        /// <returns>The length represented by the DLC</returns>
        public static int GetLengthFromDLC(int dlc, bool isSTD)
        {
            if (dlc <= 8)
                return dlc;

            if (isSTD)
                return 8;

            switch (dlc)
            {
                case 9: return 12;
                case 10: return 16;
                case 11: return 20;
                case 12: return 24;
                case 13: return 32;
                case 14: return 48;
                case 15: return 64;
                default: return dlc;
            }
        }

        public void GetVersions()
        {
            TPCANStatus stsResult;
            StringBuilder strTemp;
            string[] strArrayVersion;

            strTemp = new StringBuilder(256);

            // We get the vesion of the PCAN-Basic API
            //
            stsResult = PCANBasic.GetValue(PCANBasic.PCAN_NONEBUS, TPCANParameter.PCAN_API_VERSION, strTemp, 256);
            if (stsResult == TPCANStatus.PCAN_ERROR_OK)
            {
                IncludeTextMessage("API Version: " + strTemp.ToString());
                // We get the driver version of the channel being used
                //
                stsResult = PCANBasic.GetValue(m_PcanHandle, TPCANParameter.PCAN_CHANNEL_VERSION, strTemp, 256);
                if (stsResult == TPCANStatus.PCAN_ERROR_OK)
                {
                    // Because this information contains line control characters (several lines)
                    // we split this also in several entries in the Information List-Box
                    //
                    strArrayVersion = strTemp.ToString().Split(new char[] { '\n' });
                    IncludeTextMessage("Channel/Driver Version: ");
                    for (int i = 0; i < strArrayVersion.Length; i++)
                        IncludeTextMessage("     * " + strArrayVersion[i]);
                }
            }

            // If an error ccurred, a message is shown
            //
            if (stsResult != TPCANStatus.PCAN_ERROR_OK)
                // MessageBox.Show(GetFormatedError(stsResult));
                Console.WriteLine(GetFormatedError(stsResult));
        }
        /// <summary>
        /// setzt ein SDO Parameter im Drive
        /// ohne = value wird der Wert gelesen
        /// </summary>
        /// <param name="id">Nummer des Drives</param>
        /// <param name="command">sdo:sub=value</param>
        /// <returns></returns>
         public TPCANStatus setSDOParameter(uint driveID, string command)
        {
            byte cCmd = 0x2b; // 2Byte Schreiben
            String[] cmd = System.Text.RegularExpressions.Regex.Split(command, @"(=)|(:)");

            if (cmd.Length > 4 && cmd[1] == ":" && cmd[3] == "=")
            {
                TPCANHandle key = Convert2Int16(cmd[0]);
                //   CanOpen.CanOpenDev.ODEntry cid = ObjectDictionary[key];
                switch (key) {
                   
                    case 0x6060:
                    case 0x6098:
                    case 0x60C2:
                        cCmd = 0x2f; // 1Byte schreiben
                        break;
                    case 0x607C:  // Home Offset
                    case 0x6081:  // Profile Velocity
                    case 0x6099:  // Homing Speed
                    case 0x60FE:  // Outputs
                    case 0x60A4:  // Jerk
                        cCmd = 0x23; // 4 Byte schreiben
                        break;
            }
            return sendSDO(0x600 + driveID, cCmd, Convert2Int16(cmd[0]), Convert2Int8(cmd[2]), Convert2UInt32(cmd[4]));
            
           
            }
            else if (cmd[1] == ":")
                if(cmd[2] != "x")
                return sendSDO(0x600 + driveID, 0x40, Convert2Int16(cmd[0]), Convert2Int8(cmd[2]), 0x0000);
                else
                {
                    ushort n = Convert2Int16(cmd[0]);
                    sendSDO(0x600 + driveID, 0x40, n, 0x0000, 0x0000);
                    Thread.Sleep(10);
                    String s= this.messageStack[0x580 + driveID].sCommand;
                    ushort l = this.messageStack[0x580 + driveID].msg.DATA[4];
                    char[] chars = new char[l];
                    for (byte i = 1; i <= l; i++)
                    {
                        sendSDO(0x600 + driveID, 0x40, n, i, 0x0000);
                        Thread.Sleep(10);
                        s = this.messageStack[0x581].sCommand;
                        DecodeErros(messageStack[0x580 + driveID].msg);
                        if (this.messageStack[0x580 + driveID].msg.DATA[0] == 0x80)
                        {
                            // break;   // Fehlercode
                            ;
                        }
                        chars[i-1] = (char)this.messageStack[0x580 + driveID].msg.DATA[4];
                        //Console.WriteLine("String:>c"+s);
                    }
                    strData = new String(chars);
                    Console.WriteLine("String:> " + strData);


                }
            return 0;
        }


        public String DecodeErros(int id) {
            if (messageStack.Keys.Contains((uint)id))
                return DecodeErros(messageStack[(uint)id].msg);
            else return "";
            }

        public String DecodeErros(TPCANMsg msg)
        {
            uint errCode = BitConverter.ToUInt32(msg.DATA, 4);
            String errStr= "";
            if (msg.DATA[0] == 0x80)
            {
                switch (errCode)
                {
                    case 0x05030000 : errStr = "Toggle - Bit nicht geändert"; break;
                    case 0x05040001 : errStr = "SDO - Command - Specifier ungültig oder unbekannt"; break;
                    case 0x06010000 : errStr = "Zugriff auf dieses Objekt wird nicht unterstützt"; break;
                    case 0x06010001 : errStr = "Versuch, einen Write-Only - Parameter zu lesen"; break;
                    case 0x06010002 : errStr = "Versuch, auf einen Read - Only - Parameter zu schreiben"; break;
                    case 0x06020000 : errStr = "Objekt nicht im Objektverzeichnis vorhanden"; break;
                    case 0x06040041 : errStr = "Objekt kann nicht in PDO gemappt werden"; break;
                    case 0x06040042 : errStr = "Anzahl und/ oder Länge der gemappten Objekte würde PDO-Länge überschreiten"; break;
                    case 0x06040043 : errStr = "Allgemeine Parameter-Inkompatibilität"; break;
                    case 0x06040047 : errStr = "Allgemeiner interner Inkompatibilitätfehler im Gerät"; break;
                    case 0x06070010 : errStr = "Datentyp oder Parameterlänge stimmen nicht überein oder sind unbekannt"; break;
                    case 0x06070012 : errStr = "Datentypen stimmen nicht überein, Parameterlänge zu groß"; break;
                    case 0x06070013 : errStr = "Datentypen stimmen nicht überein, Parameterlänge zu klein"; break;
                    case 0x06090011: errStr = "Subindex nicht vorhanden"; break;
                    case 0x06090030: errStr = "Allgemeiner Wertebereichfehler"; break;
                    case 0x06090031 : errStr = "Wertebereichfehler: Parameterwert zu groß"; break;
                    case 0x06090032 : errStr = "Wertebereichfehler: Parameterwert zu klein"; break;
                    case 0x06090036 : errStr = "Wertebereichfehler: Maximumwert kleiner als Minimumwert"; break;
                    case 0x08000000 : errStr = "Allgemeiner SDO-Fehler"; break;
                    case 0x08000020 : errStr = "Zugriff nicht möglich"; break;
                    case 0x08000022 : errStr = "Zugriff bei aktuellem Gerätestatus nicht möglich"; break;
                }
            }
            if (errStr.Length > 1)
                MessageHandler.Warning(this, 2937, String.Format("Error CAN id=#{0:x} #{1:x}.{2}> {3}", msg.ID, BitConverter.ToUInt16(msg.DATA, 1), msg.DATA[3], errStr));
            return errStr;
                
        }
        /*
         * https://www.faulhaber.com/fileadmin/Import/Media/DE_7000_00050.pdf
         * 0x580 (1536d) +Node ID  0x80 Index LB Index HB Subindex ERROR 0 ERROR 1 ERROR 2 ERROR 3
         * [7]Fehlerklasse  [6]Fehlercode [5] Zusatzcode  [4]Beschreibung
            0x05 0x03 0x0000 Toggle-Bit nicht geändert
            0x05 0x04 0x0001 SDO-Command-Specifier ungültig oder unbekannt
            0x06 0x01 0x0000 Zugriff auf dieses Objekt wird nicht unterstützt
            0x06 0x01 0x0001 Versuch, einen Write-Only-Parameter zu lesen
            0x06 0x01 0x0002 Versuch, auf einen Read-Only-Parameter zu schreiben
            0x06 0x02 0x0000 Objekt nicht im Objektverzeichnis vorhanden
            0x06 0x04 0x0041 Objekt kann nicht in PDO gemappt werden
            0x06 0x04 0x0042 Anzahl und/oder Länge der gemappten Objekte würde PDO-Länge überschreiten
            0x06 0x04 0x0043 Allgemeine Parameter-Inkompatibilität
            0x06 0x04 0x0047 Allgemeiner interner Inkompatibilitätfehler im Gerät
            0x06 0x07 0x0010 Datentyp oder Parameterlänge stimmen nicht überein oder sind unbekannt
            0x06 0x07 0x0012 Datentypen stimmen nicht überein, Parameterlänge zu groß
            0x06 0x07 0x0013 Datentypen stimmen nicht überein, Parameterlänge zu klein
            0x06 0x09 0x0011 Subindex nicht vorhanden
            0x06 0x09 0x0030 Allgemeiner Wertebereichfehler
            0x06 0x09 0x0031 Wertebereichfehler: Parameterwert zu groß
            0x06 0x09 0x0032 Wertebereichfehler: Parameterwert zu klein
            0x06 0x09 0x0036 Wertebereichfehler: Maximumwert kleiner als Minimumwert
            0x08 0x00 0x0000 Allgemeiner SDO-Fehler
            0x08 0x00 0x0020 Zugriff nicht möglich
            0x08 0x00 0x0022 Zugriff bei aktuellem Gerätestatus nicht möglich
*/

        /* todo wieder Einfügen
        public uint getSDOParameter(uint driveID, string command, Dictionary<UInt16, CanOpen.CanOpenDev.ODEntry> ObjectDictionary)
        {
            String[] cmd = System.Text.RegularExpressions.Regex.Split(command, @"(=)|(:)");
            if (cmd[1] == ":")
            {
                TPCANHandle key = Convert2Int16(cmd[0]);
                CanOpen.CanOpenDev.ODEntry cid = ObjectDictionary[key];
                Console.WriteLine("{0:x} :\t{1} \t{2}", key, cid.Name, cid.Type);
                foreach (CanOpen.CanOpenDev.ODSubEntry sub in cid.SubEntry)
                {
                    Console.WriteLine("\t\t{1} \t{2}", key, sub.Name, sub.Type);
                }
                sendSDO(0x600 + driveID, 0x40, Convert2Int16(cmd[0]), Convert2Int8(cmd[2]), 0x0000);
            }
            return 0;
        }
        */




        static public uint Convert2UInt32(String cmd)
        {
            cmd = cmd.Trim();
            if (cmd.StartsWith("0x"))
                return Convert.ToUInt32(cmd, 16);
            else
                return Convert.ToUInt32(cmd, 10);
        }

        static public int Convert2Int32(String cmd)
        {
            cmd = cmd.Trim();
            if (cmd.StartsWith("0x"))
                return Convert.ToInt32(cmd, 16);
            else
                return Convert.ToInt32(cmd, 10);
        }


        static public ushort Convert2Int16(String cmd)
        {
            cmd = cmd.Trim();
            if (cmd.StartsWith("0x"))
                return Convert.ToUInt16(cmd, 16);
            else
                return Convert.ToUInt16(cmd, 10);
        }

        static public byte Convert2Int8(String cmd)
        {
            cmd = cmd.Trim();
            if (cmd.StartsWith("0x"))
                return Convert.ToByte(cmd, 16);
            else
                return Convert.ToByte(cmd, 10);
        }



        static public byte[] Convert2Bytes(String cmd)
        {
            byte[] b = new byte[4];
            cmd = cmd.Trim();

            if (cmd.StartsWith("0x"))
            {
                b[0] = Convert.ToByte(cmd.Substring(2, 2), 16);
                b[1] = Convert.ToByte(cmd.Substring(4, 2), 16);
                b[2] = Convert.ToByte(cmd.Substring(6, 2), 16);
                b[3] = Convert.ToByte(cmd.Substring(7, 2), 16);
            }
            return b;
        }

       

        /// <summary>
        /// setzt ein einzelnes Bit zurück
        /// </summary>
        /// <param name="value"></param>
        /// <param name="bitNum"></param>
        /// <returns></returns>
        static public uint ResetBit(uint value, int bitNum)
        {
            uint bit = ~(0x01u << bitNum);
            return (value & bit);
        }





        /// <summary>
        /// Deklerationen
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="moveInf"></param>
        public delegate void CMessageEvent(object sender, CANMessageInfo messageInfo);
        public event CMessageEvent OnMessage;

        public void NotifyMessage(CANMessageInfo messageInfo)
        {
            OnMessage?.Invoke(this, messageInfo);
        }


        public void NotifyMessage(TPCANMsg msg, TPCANTimestamp itsTimeStamp, string cmd)
        {
            if (OnMessage != null)
            {
                CANMessageInfo messageInfo = new CANMessageInfo(msg, itsTimeStamp, cmd);

                OnMessage(this, messageInfo);
            }
        }



        public int Connect()
        {
            return (int)this.CanInit();
        }

        public string GetVersion()
        {
           // GetVersions();
            return "Version";
        }

        public void ExecuteCommand(object sender, string cmd)
        {
            byte[] data = { 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00 };
           
            switch (cmd.ToLower()) {
                case "reset?": if (isFatalError) CanInit(); break;
                case "open":  data[0] = 0x01; sendPDO(0x40a, data); break;
                case "close": data[0] = 0x08; sendPDO(0x40a, data); break; // schliesen mit Regler
                case "hold":  data[0] = 0x0a; sendPDO(0x40a, data); break;
                default:  MessageBox.Show("Plugin" + PluginName + " with Command: " + cmd);break;
        }
           
        }


    }
    /// <summary>
    /// Sampleinfo
    /// </summary>
    public class CANMessageInfo :  EventArgs
    {
        //public String sCommand;
        public TPCANMsg msg;
        public TPCANTimestamp itsTimeStamp;
        public String sCommand { get; set; }

        public CANMessageInfo(TPCANMsg msg, TPCANTimestamp itsTimeStamp, String cmd)
        {
            this.msg = msg;
            this.itsTimeStamp = itsTimeStamp;
            this.sCommand = cmd;
        }

        public CANMessageInfo()
        {
           // this.itsTimeStamp = new TPCANTimestamp {  millis=0, millis_overflow=0,micros=0}; 
            this.sCommand = "Empty";
        }
    }
}
