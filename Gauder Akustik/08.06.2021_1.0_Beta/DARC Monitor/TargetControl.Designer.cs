
namespace DarcMonitor
{
    partial class TargetControl
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
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.checkBoxDeg = new System.Windows.Forms.CheckBox();
            this.checkBoxAbsolut = new System.Windows.Forms.CheckBox();
            this.checkBoxWorld = new System.Windows.Forms.CheckBox();
            this.label_4 = new System.Windows.Forms.Label();
            this.label_3 = new System.Windows.Forms.Label();
            this.numericUpDown3 = new System.Windows.Forms.NumericUpDown();
            this.buttonTurnTarget = new System.Windows.Forms.Button();
            this.button15 = new System.Windows.Forms.Button();
            this.button13 = new System.Windows.Forms.Button();
            this.label_5 = new System.Windows.Forms.Label();
            this.label_2 = new System.Windows.Forms.Label();
            this.numericUpDownTurnAngle = new System.Windows.Forms.NumericUpDown();
            this.numericUpDown2 = new System.Windows.Forms.NumericUpDown();
            this.numericUpDown1 = new System.Windows.Forms.NumericUpDown();
            this.label_1 = new System.Windows.Forms.Label();
            this.button14 = new System.Windows.Forms.Button();
            this.label_t2 = new System.Windows.Forms.Label();
            this.button12 = new System.Windows.Forms.Button();
            this.numericUpDownTarget = new System.Windows.Forms.NumericUpDown();
            this.buttonTarget = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownTurnAngle)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownTarget)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.checkBoxDeg);
            this.groupBox1.Controls.Add(this.checkBoxAbsolut);
            this.groupBox1.Controls.Add(this.checkBoxWorld);
            this.groupBox1.Controls.Add(this.label_4);
            this.groupBox1.Controls.Add(this.label_3);
            this.groupBox1.Controls.Add(this.numericUpDown3);
            this.groupBox1.Controls.Add(this.buttonTurnTarget);
            this.groupBox1.Controls.Add(this.button15);
            this.groupBox1.Controls.Add(this.button13);
            this.groupBox1.Controls.Add(this.label_5);
            this.groupBox1.Controls.Add(this.label_2);
            this.groupBox1.Controls.Add(this.numericUpDownTurnAngle);
            this.groupBox1.Controls.Add(this.numericUpDown2);
            this.groupBox1.Controls.Add(this.numericUpDown1);
            this.groupBox1.Controls.Add(this.label_1);
            this.groupBox1.Controls.Add(this.button14);
            this.groupBox1.Location = new System.Drawing.Point(11, 61);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(248, 147);
            this.groupBox1.TabIndex = 21;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Move";
            // 
            // checkBoxDeg
            // 
            this.checkBoxDeg.AutoSize = true;
            this.checkBoxDeg.Location = new System.Drawing.Point(7, 126);
            this.checkBoxDeg.Name = "checkBoxDeg";
            this.checkBoxDeg.Size = new System.Drawing.Size(46, 17);
            this.checkBoxDeg.TabIndex = 21;
            this.checkBoxDeg.Text = "Deg";
            this.checkBoxDeg.UseVisualStyleBackColor = true;
            this.checkBoxDeg.Click += new System.EventHandler(this.checkBoxWorld_CheckedChanged);
            // 
            // checkBoxAbsolut
            // 
            this.checkBoxAbsolut.AutoSize = true;
            this.checkBoxAbsolut.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.checkBoxAbsolut.Checked = true;
            this.checkBoxAbsolut.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBoxAbsolut.Location = new System.Drawing.Point(136, 126);
            this.checkBoxAbsolut.Name = "checkBoxAbsolut";
            this.checkBoxAbsolut.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.checkBoxAbsolut.Size = new System.Drawing.Size(60, 17);
            this.checkBoxAbsolut.TabIndex = 21;
            this.checkBoxAbsolut.Text = "absolut";
            this.checkBoxAbsolut.UseVisualStyleBackColor = true;
            this.checkBoxAbsolut.Click += new System.EventHandler(this.checkBoxWorld_CheckedChanged);
            // 
            // checkBoxWorld
            // 
            this.checkBoxWorld.AutoSize = true;
            this.checkBoxWorld.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.checkBoxWorld.Location = new System.Drawing.Point(60, 126);
            this.checkBoxWorld.Name = "checkBoxWorld";
            this.checkBoxWorld.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.checkBoxWorld.Size = new System.Drawing.Size(70, 17);
            this.checkBoxWorld.TabIndex = 21;
            this.checkBoxWorld.Text = "Cartesian";
            this.checkBoxWorld.UseVisualStyleBackColor = true;
            this.checkBoxWorld.Click += new System.EventHandler(this.checkBoxWorld_CheckedChanged);
            // 
            // label_4
            // 
            this.label_4.AutoSize = true;
            this.label_4.Location = new System.Drawing.Point(144, 105);
            this.label_4.Name = "label_4";
            this.label_4.Size = new System.Drawing.Size(36, 13);
            this.label_4.TabIndex = 20;
            this.label_4.Text = "speed";
            // 
            // label_3
            // 
            this.label_3.AutoSize = true;
            this.label_3.Location = new System.Drawing.Point(109, 33);
            this.label_3.Name = "label_3";
            this.label_3.Size = new System.Drawing.Size(16, 13);
            this.label_3.TabIndex = 20;
            this.label_3.Text = "...";
            // 
            // numericUpDown3
            // 
            this.numericUpDown3.DecimalPlaces = 2;
            this.numericUpDown3.Increment = new decimal(new int[] {
            1,
            0,
            0,
            65536});
            this.numericUpDown3.Location = new System.Drawing.Point(186, 102);
            this.numericUpDown3.Maximum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numericUpDown3.Name = "numericUpDown3";
            this.numericUpDown3.Size = new System.Drawing.Size(57, 20);
            this.numericUpDown3.TabIndex = 19;
            this.numericUpDown3.Value = new decimal(new int[] {
            3,
            0,
            0,
            65536});
            // 
            // buttonTurnTarget
            // 
            this.buttonTurnTarget.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonTurnTarget.Location = new System.Drawing.Point(6, 96);
            this.buttonTurnTarget.Name = "buttonTurnTarget";
            this.buttonTurnTarget.Size = new System.Drawing.Size(80, 23);
            this.buttonTurnTarget.TabIndex = 18;
            this.buttonTurnTarget.Text = "Turn Target";
            this.buttonTurnTarget.UseVisualStyleBackColor = true;
            this.buttonTurnTarget.Click += new System.EventHandler(this.buttonTarget_Click);
            // 
            // button15
            // 
            this.button15.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.button15.Location = new System.Drawing.Point(6, 15);
            this.button15.Name = "button15";
            this.button15.Size = new System.Drawing.Size(80, 23);
            this.button15.TabIndex = 18;
            this.button15.Text = "Get Target";
            this.button15.UseVisualStyleBackColor = true;
            this.button15.Click += new System.EventHandler(this.buttonTarget_Click);
            // 
            // button13
            // 
            this.button13.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.button13.Location = new System.Drawing.Point(7, 41);
            this.button13.Name = "button13";
            this.button13.Size = new System.Drawing.Size(80, 23);
            this.button13.TabIndex = 18;
            this.button13.Text = "Set Target";
            this.button13.UseVisualStyleBackColor = true;
            this.button13.Click += new System.EventHandler(this.buttonTarget_Click);
            // 
            // label_5
            // 
            this.label_5.AutoSize = true;
            this.label_5.Location = new System.Drawing.Point(104, 80);
            this.label_5.Name = "label_5";
            this.label_5.Size = new System.Drawing.Size(81, 13);
            this.label_5.TabIndex = 17;
            this.label_5.Text = "turn angle [rad]:";
            // 
            // label_2
            // 
            this.label_2.AutoSize = true;
            this.label_2.Location = new System.Drawing.Point(104, 52);
            this.label_2.Name = "label_2";
            this.label_2.Size = new System.Drawing.Size(64, 13);
            this.label_2.TabIndex = 17;
            this.label_2.Text = "theta2 [rad]:";
            // 
            // numericUpDownTurnAngle
            // 
            this.numericUpDownTurnAngle.DecimalPlaces = 2;
            this.numericUpDownTurnAngle.Increment = new decimal(new int[] {
            1,
            0,
            0,
            65536});
            this.numericUpDownTurnAngle.Location = new System.Drawing.Point(186, 77);
            this.numericUpDownTurnAngle.Maximum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numericUpDownTurnAngle.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            -2147483648});
            this.numericUpDownTurnAngle.Name = "numericUpDownTurnAngle";
            this.numericUpDownTurnAngle.Size = new System.Drawing.Size(57, 20);
            this.numericUpDownTurnAngle.TabIndex = 16;
            // 
            // numericUpDown2
            // 
            this.numericUpDown2.DecimalPlaces = 3;
            this.numericUpDown2.Increment = new decimal(new int[] {
            1,
            0,
            0,
            65536});
            this.numericUpDown2.Location = new System.Drawing.Point(174, 50);
            this.numericUpDown2.Maximum = new decimal(new int[] {
            3,
            0,
            0,
            0});
            this.numericUpDown2.Name = "numericUpDown2";
            this.numericUpDown2.Size = new System.Drawing.Size(70, 20);
            this.numericUpDown2.TabIndex = 16;
            // 
            // numericUpDown1
            // 
            this.numericUpDown1.DecimalPlaces = 3;
            this.numericUpDown1.Increment = new decimal(new int[] {
            1,
            0,
            0,
            65536});
            this.numericUpDown1.Location = new System.Drawing.Point(174, 10);
            this.numericUpDown1.Maximum = new decimal(new int[] {
            2,
            0,
            0,
            0});
            this.numericUpDown1.Name = "numericUpDown1";
            this.numericUpDown1.Size = new System.Drawing.Size(70, 20);
            this.numericUpDown1.TabIndex = 15;
            // 
            // label_1
            // 
            this.label_1.AutoSize = true;
            this.label_1.Location = new System.Drawing.Point(104, 13);
            this.label_1.Name = "label_1";
            this.label_1.Size = new System.Drawing.Size(64, 13);
            this.label_1.TabIndex = 14;
            this.label_1.Text = "theta1 [rad]:";
            // 
            // button14
            // 
            this.button14.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.button14.Location = new System.Drawing.Point(7, 67);
            this.button14.Name = "button14";
            this.button14.Size = new System.Drawing.Size(81, 23);
            this.button14.TabIndex = 12;
            this.button14.Text = "Move Target";
            this.button14.UseVisualStyleBackColor = true;
            this.button14.Click += new System.EventHandler(this.buttonTarget_Click);
            // 
            // label_t2
            // 
            this.label_t2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label_t2.AutoSize = true;
            this.label_t2.Location = new System.Drawing.Point(170, 42);
            this.label_t2.Name = "label_t2";
            this.label_t2.Size = new System.Drawing.Size(64, 13);
            this.label_t2.TabIndex = 20;
            this.label_t2.Text = "TargetIndex";
            // 
            // button12
            // 
            this.button12.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.button12.Location = new System.Drawing.Point(44, 32);
            this.button12.Name = "button12";
            this.button12.Size = new System.Drawing.Size(88, 23);
            this.button12.TabIndex = 19;
            this.button12.Text = "Goto Target";
            this.button12.UseVisualStyleBackColor = true;
            this.button12.Click += new System.EventHandler(this.buttonTarget_Click);
            // 
            // numericUpDownTarget
            // 
            this.numericUpDownTarget.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.numericUpDownTarget.Location = new System.Drawing.Point(173, 19);
            this.numericUpDownTarget.Maximum = new decimal(new int[] {
            32,
            0,
            0,
            0});
            this.numericUpDownTarget.Name = "numericUpDownTarget";
            this.numericUpDownTarget.Size = new System.Drawing.Size(67, 20);
            this.numericUpDownTarget.TabIndex = 18;
            this.numericUpDownTarget.Value = new decimal(new int[] {
            16,
            0,
            0,
            0});
            // 
            // buttonTarget
            // 
            this.buttonTarget.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonTarget.Location = new System.Drawing.Point(44, 3);
            this.buttonTarget.Name = "buttonTarget";
            this.buttonTarget.Size = new System.Drawing.Size(88, 23);
            this.buttonTarget.TabIndex = 17;
            this.buttonTarget.Text = "Teach Target";
            this.buttonTarget.UseVisualStyleBackColor = true;
            this.buttonTarget.Click += new System.EventHandler(this.buttonTarget_Click);
            // 
            // TargetControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.label_t2);
            this.Controls.Add(this.button12);
            this.Controls.Add(this.numericUpDownTarget);
            this.Controls.Add(this.buttonTarget);
            this.Name = "TargetControl";
            this.Size = new System.Drawing.Size(269, 212);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownTurnAngle)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownTarget)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label_4;
        private System.Windows.Forms.Label label_3;
        private System.Windows.Forms.NumericUpDown numericUpDown3;
        private System.Windows.Forms.Button buttonTurnTarget;
        private System.Windows.Forms.Button button15;
        private System.Windows.Forms.Button button13;
        private System.Windows.Forms.Label label_5;
        private System.Windows.Forms.Label label_2;
        private System.Windows.Forms.NumericUpDown numericUpDownTurnAngle;
        private System.Windows.Forms.NumericUpDown numericUpDown2;
        private System.Windows.Forms.NumericUpDown numericUpDown1;
        private System.Windows.Forms.Label label_1;
        private System.Windows.Forms.Button button14;
        private System.Windows.Forms.Label label_t2;
        private System.Windows.Forms.Button button12;
        private System.Windows.Forms.NumericUpDown numericUpDownTarget;
        private System.Windows.Forms.Button buttonTarget;
        protected internal System.Windows.Forms.CheckBox checkBoxDeg;
        protected internal System.Windows.Forms.CheckBox checkBoxAbsolut;
        protected internal System.Windows.Forms.CheckBox checkBoxWorld;
    }
}
