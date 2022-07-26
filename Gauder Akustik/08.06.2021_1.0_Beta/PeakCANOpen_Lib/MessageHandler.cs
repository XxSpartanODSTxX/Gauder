using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PeakCANOpen_Lib
{
    enum MessageTyp {Info, Message, Warning, Error };

    class MessageHandler
    {
        

        public static void Message(object sender, int errnum, MessageTyp mtyp, String msg)
        {
            ;

        }

        public static void Warning(object sender, int errnum, String msg)
        {
            ;

        }
        public static void Error(object sender, int errnum, String msg)
        {
            ;

        }
        public static void Error(object sender, int errnum, String msg, Exception ex)
        {
            ;

        }

        

    }
}