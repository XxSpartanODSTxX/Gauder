using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;


namespace DarcMonitor
{

    public class DMessage
    {
     public   String Line { get; set; }
     public   Color Col { get; set; }

        public DMessage()
        {
            this.Line = "";
            Col = Color.DarkBlue;
        }
        public DMessage(String line)
        {
            this.Line = line;
            Col = Color.DarkBlue;
        }
        public  DMessage(String line, Color color)
        {
            this.Line = line;
            Col = color;
        }

    }

    public class CommandEventArgs : EventArgs
    {
        public String Cmd { get; set; }
        public String JSONCmd { get; set; }

        char ccmd;
        char scmd;
        int id;
        String Value;

        public CommandEventArgs(String cmd,  String jsonCmd)
        {
            Cmd = cmd;
            JSONCmd = jsonCmd;
        }
    }

}
