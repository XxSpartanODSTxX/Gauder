
namespace DarcMonitor
{
    partial class ServiceControl
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
            this.numericUpDownId = new System.Windows.Forms.NumericUpDown();
            this.numericSetValue = new System.Windows.Forms.NumericUpDown();
            this.label9 = new System.Windows.Forms.Label();
            this.labelId = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.labelShowTarget = new System.Windows.Forms.Label();
            this.comboBoxSet = new System.Windows.Forms.ComboBox();
            this.comboBoxGetValue = new System.Windows.Forms.ComboBox();
            this.textBoxSetDebug = new System.Windows.Forms.TextBox();
            this.comboBoxSetValue = new System.Windows.Forms.ComboBox();
            this.comboBoxSetDebug = new System.Windows.Forms.ComboBox();
            this.button7 = new System.Windows.Forms.Button();
            this.buttonGetValue = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownId)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericSetValue)).BeginInit();
            this.SuspendLayout();
            // 
            // numericUpDownId
            // 
            this.numericUpDownId.Location = new System.Drawing.Point(60, 30);
            this.numericUpDownId.Maximum = new decimal(new int[] {
            150,
            0,
            0,
            0});
            this.numericUpDownId.Name = "numericUpDownId";
            this.numericUpDownId.Size = new System.Drawing.Size(38, 20);
            this.numericUpDownId.TabIndex = 28;
            // 
            // numericSetValue
            // 
            this.numericSetValue.Location = new System.Drawing.Point(118, 32);
            this.numericSetValue.Maximum = new decimal(new int[] {
            150,
            0,
            0,
            0});
            this.numericSetValue.Name = "numericSetValue";
            this.numericSetValue.Size = new System.Drawing.Size(66, 20);
            this.numericSetValue.TabIndex = 29;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(104, 34);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(13, 13);
            this.label9.TabIndex = 24;
            this.label9.Text = "=";
            // 
            // labelId
            // 
            this.labelId.AutoSize = true;
            this.labelId.Location = new System.Drawing.Point(5, 32);
            this.labelId.Name = "labelId";
            this.labelId.Size = new System.Drawing.Size(16, 13);
            this.labelId.TabIndex = 25;
            this.labelId.Text = "...";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(5, 116);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(16, 13);
            this.label1.TabIndex = 26;
            this.label1.Text = "...";
            // 
            // labelShowTarget
            // 
            this.labelShowTarget.AutoSize = true;
            this.labelShowTarget.Location = new System.Drawing.Point(5, 175);
            this.labelShowTarget.Name = "labelShowTarget";
            this.labelShowTarget.Size = new System.Drawing.Size(16, 13);
            this.labelShowTarget.TabIndex = 27;
            this.labelShowTarget.Text = "...";
            // 
            // comboBoxSet
            // 
            this.comboBoxSet.FormattingEnabled = true;
            this.comboBoxSet.Items.AddRange(new object[] {
            "10",
            "20",
            "50",
            "70",
            "100"});
            this.comboBoxSet.Location = new System.Drawing.Point(60, 60);
            this.comboBoxSet.Name = "comboBoxSet";
            this.comboBoxSet.Size = new System.Drawing.Size(195, 21);
            this.comboBoxSet.TabIndex = 19;
            this.comboBoxSet.SelectedIndexChanged += new System.EventHandler(this.comboBoxSet_SelectedIndexChanged);
            // 
            // comboBoxGetValue
            // 
            this.comboBoxGetValue.FormattingEnabled = true;
            this.comboBoxGetValue.Items.AddRange(new object[] {
            "State",
            "DrivePosition",
            "MagnetEncoder",
            "Interpolation",
            "Target",
            "Override",
            "OutTarget",
            "MoveOutIndex",
            "TCP",
            "Direction",
            "InterpolationMode",
            "CollisionMode",
            "GearRatio",
            "Debug",
            "ListTargets",
            "Program",
            "MagneticOffset",
            "MagneticCalibaration"});
            this.comboBoxGetValue.Location = new System.Drawing.Point(3, 140);
            this.comboBoxGetValue.Name = "comboBoxGetValue";
            this.comboBoxGetValue.Size = new System.Drawing.Size(175, 21);
            this.comboBoxGetValue.TabIndex = 20;
            this.comboBoxGetValue.Text = "State";
            // 
            // textBoxSetDebug
            // 
            this.textBoxSetDebug.Location = new System.Drawing.Point(116, 92);
            this.textBoxSetDebug.Name = "textBoxSetDebug";
            this.textBoxSetDebug.Size = new System.Drawing.Size(66, 20);
            this.textBoxSetDebug.TabIndex = 23;
            this.textBoxSetDebug.Text = "0";
            // 
            // comboBoxSetValue
            // 
            this.comboBoxSetValue.FormattingEnabled = true;
            this.comboBoxSetValue.Items.AddRange(new object[] {
            "Override",
            "OutTarget",
            "MoveOutIndex",
            "TCP",
            "InterpolationMode",
            "CollisionMode",
            "OperationalMode",
            "LowerJointLimit",
            "UpperJointLimit",
            "MagOffset",
            "MagScale",
            "MagTollerance"});
            this.comboBoxSetValue.Location = new System.Drawing.Point(3, 3);
            this.comboBoxSetValue.Name = "comboBoxSetValue";
            this.comboBoxSetValue.Size = new System.Drawing.Size(117, 21);
            this.comboBoxSetValue.TabIndex = 21;
            this.comboBoxSetValue.Text = "Override";
            this.comboBoxSetValue.SelectedIndexChanged += new System.EventHandler(this.comboBoxSetValue_SelectedIndexChanged);
            // 
            // comboBoxSetDebug
            // 
            this.comboBoxSetDebug.FormattingEnabled = true;
            this.comboBoxSetDebug.Items.AddRange(new object[] {
            "cmd",
            "motion",
            "canmsg",
            "info",
            "warning",
            "error",
            "debug",
            "state"});
            this.comboBoxSetDebug.Location = new System.Drawing.Point(5, 92);
            this.comboBoxSetDebug.Name = "comboBoxSetDebug";
            this.comboBoxSetDebug.Size = new System.Drawing.Size(101, 21);
            this.comboBoxSetDebug.TabIndex = 22;
            // 
            // button7
            // 
            this.button7.Location = new System.Drawing.Point(187, 92);
            this.button7.Name = "button7";
            this.button7.Size = new System.Drawing.Size(71, 23);
            this.button7.TabIndex = 16;
            this.button7.Text = "Set Debug";
            this.button7.UseVisualStyleBackColor = true;
            this.button7.Click += new System.EventHandler(this.button_Click);
            // 
            // buttonGetValue
            // 
            this.buttonGetValue.Location = new System.Drawing.Point(190, 140);
            this.buttonGetValue.Name = "buttonGetValue";
            this.buttonGetValue.Size = new System.Drawing.Size(68, 23);
            this.buttonGetValue.TabIndex = 17;
            this.buttonGetValue.Text = "Get Value";
            this.buttonGetValue.UseVisualStyleBackColor = true;
            this.buttonGetValue.Click += new System.EventHandler(this.button_Click);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(187, 31);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(71, 23);
            this.button1.TabIndex = 18;
            this.button1.Text = "Set Value";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button_Click);
            // 
            // ServiceControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.numericUpDownId);
            this.Controls.Add(this.numericSetValue);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.labelId);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.labelShowTarget);
            this.Controls.Add(this.comboBoxSet);
            this.Controls.Add(this.comboBoxGetValue);
            this.Controls.Add(this.textBoxSetDebug);
            this.Controls.Add(this.comboBoxSetValue);
            this.Controls.Add(this.comboBoxSetDebug);
            this.Controls.Add(this.button7);
            this.Controls.Add(this.buttonGetValue);
            this.Controls.Add(this.button1);
            this.Name = "ServiceControl";
            this.Size = new System.Drawing.Size(270, 213);
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownId)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericSetValue)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label labelId;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label labelShowTarget;
        private System.Windows.Forms.ComboBox comboBoxSet;
        private System.Windows.Forms.Button button7;
        private System.Windows.Forms.Button buttonGetValue;
        private System.Windows.Forms.Button button1;
        protected internal System.Windows.Forms.ComboBox comboBoxGetValue;
        protected internal System.Windows.Forms.TextBox textBoxSetDebug;
        protected internal System.Windows.Forms.ComboBox comboBoxSetValue;
        protected internal System.Windows.Forms.ComboBox comboBoxSetDebug;
        protected internal System.Windows.Forms.NumericUpDown numericUpDownId;
        protected internal System.Windows.Forms.NumericUpDown numericSetValue;
    }
}
