namespace PeakCANOpen_Lib
{
    partial class SettingsPanel
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
            this.label13 = new System.Windows.Forms.Label();
            this.numericUpDownA1id = new System.Windows.Forms.NumericUpDown();
            this.checkBoxRefA1 = new System.Windows.Forms.CheckBox();
            this.numericUpDownSpeedA1 = new System.Windows.Forms.NumericUpDown();
            this.numericA1 = new System.Windows.Forms.NumericUpDown();
            this.label4 = new System.Windows.Forms.Label();
            this.numericUpDownA1min = new System.Windows.Forms.NumericUpDown();
            this.numericUpDownA1max = new System.Windows.Forms.NumericUpDown();
            this.label29 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label32 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.numericUpDownAcc = new System.Windows.Forms.NumericUpDown();
            this.labelError = new System.Windows.Forms.Label();
            this.checkBoxIgnorValidMove = new System.Windows.Forms.CheckBox();
            this.checkBoxIgnoreLimits = new System.Windows.Forms.CheckBox();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownA1id)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownSpeedA1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericA1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownA1min)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownA1max)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownAcc)).BeginInit();
            this.SuspendLayout();
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(17, 62);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(55, 13);
            this.label13.TabIndex = 32;
            this.label13.Text = "MotorA1id";
            // 
            // numericUpDownA1id
            // 
            this.numericUpDownA1id.Location = new System.Drawing.Point(94, 60);
            this.numericUpDownA1id.Name = "numericUpDownA1id";
            this.numericUpDownA1id.Size = new System.Drawing.Size(37, 20);
            this.numericUpDownA1id.TabIndex = 31;
            this.numericUpDownA1id.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // checkBoxRefA1
            // 
            this.checkBoxRefA1.Location = new System.Drawing.Point(375, 56);
            this.checkBoxRefA1.Name = "checkBoxRefA1";
            this.checkBoxRefA1.Size = new System.Drawing.Size(41, 24);
            this.checkBoxRefA1.TabIndex = 33;
            this.checkBoxRefA1.Text = "-";
            // 
            // numericUpDownSpeedA1
            // 
            this.numericUpDownSpeedA1.Increment = new decimal(new int[] {
            50,
            0,
            0,
            0});
            this.numericUpDownSpeedA1.Location = new System.Drawing.Point(227, 60);
            this.numericUpDownSpeedA1.Maximum = new decimal(new int[] {
            1200,
            0,
            0,
            0});
            this.numericUpDownSpeedA1.Minimum = new decimal(new int[] {
            100000,
            0,
            0,
            -2147483648});
            this.numericUpDownSpeedA1.Name = "numericUpDownSpeedA1";
            this.numericUpDownSpeedA1.Size = new System.Drawing.Size(62, 20);
            this.numericUpDownSpeedA1.TabIndex = 30;
            this.numericUpDownSpeedA1.Value = new decimal(new int[] {
            600,
            0,
            0,
            0});
            // 
            // numericA1
            // 
            this.numericA1.Location = new System.Drawing.Point(155, 60);
            this.numericA1.Maximum = new decimal(new int[] {
            10000,
            0,
            0,
            0});
            this.numericA1.Minimum = new decimal(new int[] {
            10000,
            0,
            0,
            -2147483648});
            this.numericA1.Name = "numericA1";
            this.numericA1.Size = new System.Drawing.Size(64, 20);
            this.numericA1.TabIndex = 27;
            this.numericA1.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // label4
            // 
            this.label4.Location = new System.Drawing.Point(17, 35);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(20, 16);
            this.label4.TabIndex = 25;
            this.label4.Text = "A1";
            // 
            // numericUpDownA1min
            // 
            this.numericUpDownA1min.Increment = new decimal(new int[] {
            50,
            0,
            0,
            0});
            this.numericUpDownA1min.Location = new System.Drawing.Point(468, 60);
            this.numericUpDownA1min.Maximum = new decimal(new int[] {
            1500,
            0,
            0,
            0});
            this.numericUpDownA1min.Minimum = new decimal(new int[] {
            100,
            0,
            0,
            -2147483648});
            this.numericUpDownA1min.Name = "numericUpDownA1min";
            this.numericUpDownA1min.Size = new System.Drawing.Size(62, 20);
            this.numericUpDownA1min.TabIndex = 28;
            // 
            // numericUpDownA1max
            // 
            this.numericUpDownA1max.Increment = new decimal(new int[] {
            50,
            0,
            0,
            0});
            this.numericUpDownA1max.Location = new System.Drawing.Point(536, 60);
            this.numericUpDownA1max.Maximum = new decimal(new int[] {
            10000,
            0,
            0,
            0});
            this.numericUpDownA1max.Name = "numericUpDownA1max";
            this.numericUpDownA1max.Size = new System.Drawing.Size(69, 20);
            this.numericUpDownA1max.TabIndex = 26;
            this.numericUpDownA1max.Value = new decimal(new int[] {
            1500,
            0,
            0,
            0});
            // 
            // label29
            // 
            this.label29.Location = new System.Drawing.Point(375, 34);
            this.label29.Name = "label29";
            this.label29.Size = new System.Drawing.Size(54, 16);
            this.label29.TabIndex = 38;
            this.label29.Text = "RefDir:";
            // 
            // label6
            // 
            this.label6.Location = new System.Drawing.Point(227, 34);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(83, 23);
            this.label6.TabIndex = 35;
            this.label6.Text = "Speed [Grad/s]";
            // 
            // label5
            // 
            this.label5.Location = new System.Drawing.Point(155, 34);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(59, 23);
            this.label5.TabIndex = 34;
            this.label5.Text = "inc/Grad";
            // 
            // label32
            // 
            this.label32.Location = new System.Drawing.Point(307, 34);
            this.label32.Name = "label32";
            this.label32.Size = new System.Drawing.Size(48, 23);
            this.label32.TabIndex = 37;
            this.label32.Text = "acc";
            // 
            // label8
            // 
            this.label8.Location = new System.Drawing.Point(468, 34);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(48, 23);
            this.label8.TabIndex = 36;
            this.label8.Text = "min";
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(536, 34);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(48, 23);
            this.label1.TabIndex = 36;
            this.label1.Text = "max";
            // 
            // numericUpDownAcc
            // 
            this.numericUpDownAcc.Increment = new decimal(new int[] {
            50,
            0,
            0,
            0});
            this.numericUpDownAcc.Location = new System.Drawing.Point(307, 60);
            this.numericUpDownAcc.Maximum = new decimal(new int[] {
            1200,
            0,
            0,
            0});
            this.numericUpDownAcc.Minimum = new decimal(new int[] {
            100000,
            0,
            0,
            -2147483648});
            this.numericUpDownAcc.Name = "numericUpDownAcc";
            this.numericUpDownAcc.Size = new System.Drawing.Size(62, 20);
            this.numericUpDownAcc.TabIndex = 39;
            this.numericUpDownAcc.Value = new decimal(new int[] {
            600,
            0,
            0,
            0});
            // 
            // labelError
            // 
            this.labelError.AutoSize = true;
            this.labelError.Location = new System.Drawing.Point(16, 96);
            this.labelError.Name = "labelError";
            this.labelError.Size = new System.Drawing.Size(32, 13);
            this.labelError.TabIndex = 40;
            this.labelError.Text = "Error:";
            // 
            // checkBoxIgnorValidMove
            // 
            this.checkBoxIgnorValidMove.AutoSize = true;
            this.checkBoxIgnorValidMove.Location = new System.Drawing.Point(499, 92);
            this.checkBoxIgnorValidMove.Name = "checkBoxIgnorValidMove";
            this.checkBoxIgnorValidMove.Size = new System.Drawing.Size(109, 17);
            this.checkBoxIgnorValidMove.TabIndex = 42;
            this.checkBoxIgnorValidMove.Text = "Ignore ValidMove";
            // 
            // checkBoxIgnoreLimits
            // 
            this.checkBoxIgnoreLimits.AutoSize = true;
            this.checkBoxIgnoreLimits.Location = new System.Drawing.Point(394, 92);
            this.checkBoxIgnoreLimits.Name = "checkBoxIgnoreLimits";
            this.checkBoxIgnoreLimits.Size = new System.Drawing.Size(85, 17);
            this.checkBoxIgnoreLimits.TabIndex = 41;
            this.checkBoxIgnoreLimits.Text = "Ignore Limits";
            // 
            // SettingsPanel
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.checkBoxIgnorValidMove);
            this.Controls.Add(this.checkBoxIgnoreLimits);
            this.Controls.Add(this.labelError);
            this.Controls.Add(this.numericUpDownAcc);
            this.Controls.Add(this.label29);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label32);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.label13);
            this.Controls.Add(this.numericUpDownA1id);
            this.Controls.Add(this.checkBoxRefA1);
            this.Controls.Add(this.numericUpDownSpeedA1);
            this.Controls.Add(this.numericA1);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.numericUpDownA1min);
            this.Controls.Add(this.numericUpDownA1max);
            this.Name = "SettingsPanel";
            this.Size = new System.Drawing.Size(662, 131);
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownA1id)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownSpeedA1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericA1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownA1min)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownA1max)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownAcc)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.NumericUpDown numericUpDownA1id;
        private System.Windows.Forms.CheckBox checkBoxRefA1;
        private System.Windows.Forms.NumericUpDown numericUpDownSpeedA1;
        private System.Windows.Forms.NumericUpDown numericA1;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.NumericUpDown numericUpDownA1min;
        private System.Windows.Forms.NumericUpDown numericUpDownA1max;
        private System.Windows.Forms.Label label29;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label32;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.NumericUpDown numericUpDownAcc;
        private System.Windows.Forms.Label labelError;
        private System.Windows.Forms.CheckBox checkBoxIgnorValidMove;
        private System.Windows.Forms.CheckBox checkBoxIgnoreLimits;
    }
}
