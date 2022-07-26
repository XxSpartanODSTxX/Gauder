using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DarcMonitor
{
    public partial class ScriptControl : UserControl
    {
        int actLineNr=0;
        int maxLineNuber = 1;
        public event EventHandler eventHandler;

        private delegate void  SafeCallDelegate(int  index, Color color);

        public ScriptControl()
        {
            InitializeComponent();
        }

        private void buttonDoScript_Click(object sender, EventArgs e)
        {
            openFileDialogScript.ShowDialog();
            string text = System.IO.File.ReadAllText(openFileDialogScript.FileName);
            richTextBoxScript.Text = text;




        }

        private void buttonStep_Click(object sender, EventArgs e)
        {
            HighlightLine(actLineNr++, Color.Cyan);
        }

        private void richTextBoxScript_MouseDown(object sender, MouseEventArgs e)
        {
            try
            {
                int firstcharindex = richTextBoxScript.GetFirstCharIndexOfCurrentLine();
                int currentline = richTextBoxScript.GetLineFromCharIndex(firstcharindex);
                string currentlinetext = richTextBoxScript.Lines[currentline];
                richTextBoxScript.Select(firstcharindex, currentlinetext.Length);
                actLineNr = currentline;
                //HighlightLine(actLineNr, Color.Cyan);
            }
            catch (Exception ex) { }

        }
        public void  HighlightLine(int index, Color color)
        {
            if (richTextBoxScript.InvokeRequired)
            {
                var d = new SafeCallDelegate(HighlightLine);
                richTextBoxScript.Invoke(d, new object[] { index, color });
            }
            else
            {
                richTextBoxScript.SelectAll();
                richTextBoxScript.SelectionBackColor = richTextBoxScript.BackColor;
                var lines = richTextBoxScript.Lines;
                maxLineNuber = lines.Length;
                if (index < 0 || index >= lines.Length)
                {
                    actLineNr = 0;
                    return ;
                }

                var start = richTextBoxScript.GetFirstCharIndexFromLine(index);  // Get the 1st char index of the appended text
                var length = lines[index].Length;
                richTextBoxScript.Select(start, length);                 // Select from there to the end
                richTextBoxScript.SelectionBackColor = color;
                labelStep.Text = richTextBoxScript.SelectedText;
            }
            
        }

        private void ScriptControl_Click(object sender, EventArgs e)
        {
     
        }

        private void buttonSave_Click(object sender, EventArgs e)
        {
            String text = richTextBoxScript.Text;
            
            if (openFileDialogScript.FileName == null || ((Control.ModifierKeys & Keys.Shift) == Keys.Shift ))
                saveFileDialogScript.ShowDialog();
            else
                System.IO.File.WriteAllText(openFileDialogScript.FileName, text);
        }
   
    public void recordCommand(String cmd)
    {
            try
            {
                if (checkBoxRecord.Checked)
                    if (cmd.EndsWith("\r\n"))
                        richTextBoxScript.AppendText(cmd);
                    else
                        richTextBoxScript.AppendText(cmd + "\r\n\0");
            }
            catch (Exception ex) { }
        }

        private void buttonRunScript_Click(object sender, EventArgs e)
        {
            backgroundWorkerScript.RunWorkerAsync();
        }

        private void backgroundWorkerScript_DoWork(object sender, DoWorkEventArgs e)
        {
            actLineNr = 0;
            while (!backgroundWorkerScript.CancellationPending)
            {
                HighlightLine(actLineNr++, Color.Cyan);
                eventHandler?.Invoke(buttonRunScript, new EventArgs());
                if (actLineNr < 0 || actLineNr >= maxLineNuber)
                {
                    actLineNr = 0;
                    break;
                }
                Thread.Sleep(500);
            }

        }
    }
}
