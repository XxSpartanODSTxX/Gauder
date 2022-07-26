
namespace DarcMonitor
{
    partial class ScriptControl
    {
        /// <summary> 
        /// Erforderliche Designervariable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Verwendete Ressourcen bereinigen.
        /// </summary>
        /// <param name="disposing">True, wenn verwaltete Ressourcen gelöscht werden sollen; andernfalls False.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Vom Komponenten-Designer generierter Code

        /// <summary> 
        /// Erforderliche Methode für die Designerunterstützung. 
        /// Der Inhalt der Methode darf nicht mit dem Code-Editor geändert werden.
        /// </summary>
        private void InitializeComponent()
        {
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.richTextBoxScript = new System.Windows.Forms.RichTextBox();
            this.checkBoxRecord = new System.Windows.Forms.CheckBox();
            this.labelStep = new System.Windows.Forms.Label();
            this.buttonRunScript = new System.Windows.Forms.Button();
            this.buttonDoScript = new System.Windows.Forms.Button();
            this.buttonStep = new System.Windows.Forms.Button();
            this.buttonSave = new System.Windows.Forms.Button();
            this.openFileDialogScript = new System.Windows.Forms.OpenFileDialog();
            this.backgroundWorkerScript = new System.ComponentModel.BackgroundWorker();
            this.saveFileDialogScript = new System.Windows.Forms.SaveFileDialog();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.SuspendLayout();
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.richTextBoxScript);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.checkBoxRecord);
            this.splitContainer1.Panel2.Controls.Add(this.labelStep);
            this.splitContainer1.Panel2.Controls.Add(this.buttonRunScript);
            this.splitContainer1.Panel2.Controls.Add(this.buttonDoScript);
            this.splitContainer1.Panel2.Controls.Add(this.buttonStep);
            this.splitContainer1.Panel2.Controls.Add(this.buttonSave);
            this.splitContainer1.Size = new System.Drawing.Size(577, 261);
            this.splitContainer1.SplitterDistance = 292;
            this.splitContainer1.TabIndex = 0;
            // 
            // richTextBoxScript
            // 
            this.richTextBoxScript.Dock = System.Windows.Forms.DockStyle.Fill;
            this.richTextBoxScript.Location = new System.Drawing.Point(0, 0);
            this.richTextBoxScript.Name = "richTextBoxScript";
            this.richTextBoxScript.Size = new System.Drawing.Size(292, 261);
            this.richTextBoxScript.TabIndex = 0;
            this.richTextBoxScript.Text = "";
            this.richTextBoxScript.MouseDown += new System.Windows.Forms.MouseEventHandler(this.richTextBoxScript_MouseDown);
            // 
            // checkBoxRecord
            // 
            this.checkBoxRecord.AutoSize = true;
            this.checkBoxRecord.Location = new System.Drawing.Point(143, 20);
            this.checkBoxRecord.Name = "checkBoxRecord";
            this.checkBoxRecord.Size = new System.Drawing.Size(84, 17);
            this.checkBoxRecord.TabIndex = 6;
            this.checkBoxRecord.Text = "Record cmd";
            this.checkBoxRecord.UseVisualStyleBackColor = true;
            // 
            // labelStep
            // 
            this.labelStep.AutoSize = true;
            this.labelStep.Location = new System.Drawing.Point(35, 116);
            this.labelStep.Name = "labelStep";
            this.labelStep.Size = new System.Drawing.Size(29, 13);
            this.labelStep.TabIndex = 5;
            this.labelStep.Text = "Step";
            // 
            // buttonRunScript
            // 
            this.buttonRunScript.Location = new System.Drawing.Point(38, 45);
            this.buttonRunScript.Name = "buttonRunScript";
            this.buttonRunScript.Size = new System.Drawing.Size(73, 23);
            this.buttonRunScript.TabIndex = 3;
            this.buttonRunScript.Text = "Run Script";
            this.buttonRunScript.UseVisualStyleBackColor = true;
            this.buttonRunScript.Click += new System.EventHandler(this.buttonRunScript_Click);
            // 
            // buttonDoScript
            // 
            this.buttonDoScript.Location = new System.Drawing.Point(38, 16);
            this.buttonDoScript.Name = "buttonDoScript";
            this.buttonDoScript.Size = new System.Drawing.Size(73, 23);
            this.buttonDoScript.TabIndex = 4;
            this.buttonDoScript.Text = "Load Script";
            this.buttonDoScript.UseVisualStyleBackColor = true;
            this.buttonDoScript.Click += new System.EventHandler(this.buttonDoScript_Click);
            // 
            // buttonStep
            // 
            this.buttonStep.Location = new System.Drawing.Point(36, 83);
            this.buttonStep.Name = "buttonStep";
            this.buttonStep.Size = new System.Drawing.Size(75, 23);
            this.buttonStep.TabIndex = 0;
            this.buttonStep.Text = "Step";
            this.buttonStep.UseVisualStyleBackColor = true;
            this.buttonStep.Click += new System.EventHandler(this.buttonStep_Click);
            // 
            // buttonSave
            // 
            this.buttonSave.Location = new System.Drawing.Point(36, 226);
            this.buttonSave.Name = "buttonSave";
            this.buttonSave.Size = new System.Drawing.Size(75, 23);
            this.buttonSave.TabIndex = 0;
            this.buttonSave.Text = "Save";
            this.buttonSave.UseVisualStyleBackColor = true;
            this.buttonSave.Click += new System.EventHandler(this.buttonSave_Click);
            // 
            // openFileDialogScript
            // 
            this.openFileDialogScript.FileName = "DARC1000_setup.txt";
            this.openFileDialogScript.Filter = "DARC1000 Script|*.json|Alle Dateien|*.*";
            this.openFileDialogScript.InitialDirectory = "\"../\"";
            // 
            // backgroundWorkerScript
            // 
            this.backgroundWorkerScript.DoWork += new System.ComponentModel.DoWorkEventHandler(this.backgroundWorkerScript_DoWork);
            // 
            // ScriptControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.splitContainer1);
            this.Name = "ScriptControl";
            this.Size = new System.Drawing.Size(577, 261);
            this.Click += new System.EventHandler(this.ScriptControl_Click);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.RichTextBox richTextBoxScript;
        private System.Windows.Forms.Button buttonRunScript;
        private System.Windows.Forms.Button buttonDoScript;
        private System.Windows.Forms.OpenFileDialog openFileDialogScript;
        public System.Windows.Forms.Label labelStep;
        private System.Windows.Forms.Button buttonSave;
        public System.Windows.Forms.Button buttonStep;
        private System.Windows.Forms.CheckBox checkBoxRecord;
        private System.ComponentModel.BackgroundWorker backgroundWorkerScript;
        private System.Windows.Forms.SaveFileDialog saveFileDialogScript;
    }
}
