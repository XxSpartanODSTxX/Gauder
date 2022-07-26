using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;
using System.Drawing;
using System.Threading;
using System.Windows.Forms;
using System.ComponentModel;

namespace DarcMonitor
{
    public partial class FormDarc1000 : Form
    {

        static String _sendText;
        static bool _bValid = false;

        Socket handler = null;
        Socket listener = null;


        public static String SendText
        {
            get { /*_bValid = false; */  return _sendText;}
            set { _sendText = value; _bValid = true; }
        }



        private void backgroundWorkerSocket_DoWork(object sender, DoWorkEventArgs e)
        {
        
            // Get Host IP Address that is used to establish a connection  
            // In this case, we get one IP address of localhost that is IP : 127.0.0.1  
            // If a host has multiple addresses, you will get a list of addresses  
            IPHostEntry host = Dns.GetHostEntry("localhost");
            IPAddress ipAddress = host.AddressList[0];
            IPEndPoint localEndPoint = new IPEndPoint(ipAddress, 27015);

            try
            {

                // Create a Socket that will use Tcp protocol      
                 listener = new Socket(ipAddress.AddressFamily, SocketType.Stream, ProtocolType.Tcp);
                // A Socket must be associated with an endpoint using the Bind method  
                listener.Bind(localEndPoint);
                // Specify how many requests a Socket can listen before it gives Server busy response.  
                // We will listen 10 requests at a time  
                listener.Listen(20);

                //Console.WriteLine("Waiting for a connection...");
                WriteTextSafe("Waiting for a connection...\n", Color.Green);

                handler = listener.Accept();

                // Incoming data from the client.    
                string data = "<#>";
                string line = null;
                byte[] bytes = null;
                int bytesToRead =0;
                string resetLine = "";
                WriteTextSafe("SocketServer connected!\n", Color.Green);
                while (!backgroundWorkerSocket.CancellationPending)
                {
                 //   while (true)
                    {
                        bytes = new byte[1024];
                        byte[] outValue = BitConverter.GetBytes(0);

                        bytesToRead = handler.Available;
                        handler.IOControl(0x4004667F, null, outValue);
                        uint bytesAvailable = BitConverter.ToUInt32(outValue, 0);
                        if (bytesToRead > 0 && bytesToRead != 65536)
                        {
                            int bytesRec = handler.Receive(bytes);

                            line = Encoding.ASCII.GetString(bytes, 0, bytesRec);
                            try
                            {
                                int n = 1;
                                string[] words = (resetLine + line).Split('\n');
                                foreach (var word in words)
                                {
                                    if (n++ < words.Length)
                                    {
                                        Process_andWriteTextSafe(word, null);
                                        /*
                                        if (bRawText)
                                        {
                                            ProcessText(word, null);
                                            WriteTextSafe(word + "\n", Color.DarkBlue);
                                        }
                                        else
                                        {
                                            DMessage msg = ProcessText(word, null);
                                            WriteTextSafe(msg.Line, msg.Col);
                                        }
                                        */
                                    }

                                    else
                                        resetLine = word;
                                }
                            }
                            catch (Exception ex)
                            { 
                                WriteTextSafe(ex.Message, Color.Red);
                                WriteTextSafe(ex.StackTrace, Color.DarkRed);
                            }

                            data += line;
                        }
                        if (data.IndexOf("<EOF>") > -1)
                          {
                                break;
                        }
                    }

                    // parent.WriteTextSafe(line + "\n", Color.Cyan);
                    //     Console.WriteLine("Text received : {0}", data);


                    if (_bValid)
                    {
                        byte[] msg = Encoding.ASCII.GetBytes(SendText);
                        handler.Send(msg);
                        _bValid = false; 
                    }
                    else
                    {
                        
                        byte[] msg = Encoding.ASCII.GetBytes("#");
                       // handler.Send(msg);
                        if (bytesToRead == 0)
                            Thread.Sleep(50);
                    }
                }
                    handler.Shutdown(SocketShutdown.Both);
                    handler.Close();
                    WriteTextSafe("SocketServer Shutdown!", Color.Green);

            }
            catch (Exception ex)
            {
                WriteTextSafe(ex.Message + "\n", Color.Red);
                // Console.WriteLine(e.ToString());
            }
            finally{
                if (handler != null)
                {
                    handler.Shutdown(SocketShutdown.Both);
                    handler.Close();
                    WriteTextSafe("SocketServer Shutdown!", Color.Green);
                }

                if (listener != null)
                    listener.Shutdown(SocketShutdown.Both);
                    listener.Dispose();

            }

            //            Console.WriteLine("\n Press any key to continue...");
            //           Console.ReadKey();
            
        }

    }
}
