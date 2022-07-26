
namespace DarcMonitor
{
    partial class CommunicationControl
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
            this.button9 = new System.Windows.Forms.Button();
            this.button10 = new System.Windows.Forms.Button();
            this.buttonColor = new System.Windows.Forms.Button();
            this.numericUpDownComControl = new System.Windows.Forms.NumericUpDown();
            this.radioButton3 = new System.Windows.Forms.RadioButton();
            this.radioButton2 = new System.Windows.Forms.RadioButton();
            this.radioButton1 = new System.Windows.Forms.RadioButton();
            this.button_O = new System.Windows.Forms.Button();
            this.DARC1000colorDialog = new System.Windows.Forms.ColorDialog();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownComControl)).BeginInit();
            this.SuspendLayout();
            // 
            // button9
            // 
            this.button9.Location = new System.Drawing.Point(17, 42);
            this.button9.Name = "button9";
            this.button9.Size = new System.Drawing.Size(75, 23);
            this.button9.TabIndex = 3;
            this.button9.Text = "Sound On";
            this.button9.UseVisualStyleBackColor = true;
            this.button9.Click += new System.EventHandler(this.communication_Command);
            // 
            // button10
            // 
            this.button10.Location = new System.Drawing.Point(98, 42);
            this.button10.Name = "button10";
            this.button10.Size = new System.Drawing.Size(75, 23);
            this.button10.TabIndex = 4;
            this.button10.Text = "Sound Off";
            this.button10.UseVisualStyleBackColor = true;
            this.button10.Click += new System.EventHandler(this.communication_Command);
            // 
            // buttonColor
            // 
            this.buttonColor.Location = new System.Drawing.Point(18, 13);
            this.buttonColor.Name = "buttonColor";
            this.buttonColor.Size = new System.Drawing.Size(75, 23);
            this.buttonColor.TabIndex = 6;
            this.buttonColor.Text = "Color";
            this.buttonColor.UseVisualStyleBackColor = true;
            this.buttonColor.Click += new System.EventHandler(this.communication_Command);
            // 
            // numericUpDownComControl
            // 
            this.numericUpDownComControl.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.numericUpDownComControl.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.numericUpDownComControl.Increment = new decimal(new int[] {
            5,
            0,
            0,
            0});
            this.numericUpDownComControl.Location = new System.Drawing.Point(120, 108);
            this.numericUpDownComControl.Maximum = new decimal(new int[] {
            30,
            0,
            0,
            0});
            this.numericUpDownComControl.Minimum = new decimal(new int[] {
            30,
            0,
            0,
            -2147483648});
            this.numericUpDownComControl.Name = "numericUpDownComControl";
            this.numericUpDownComControl.Size = new System.Drawing.Size(61, 32);
            this.numericUpDownComControl.TabIndex = 15;
            this.numericUpDownComControl.ValueChanged += new System.EventHandler(this.communication_Command);
            this.numericUpDownComControl.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.textBoxCmd_KeyPress);
            // 
            // radioButton3
            // 
            this.radioButton3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.radioButton3.AutoSize = true;
            this.radioButton3.Checked = true;
            this.radioButton3.Location = new System.Drawing.Point(18, 128);
            this.radioButton3.Name = "radioButton3";
            this.radioButton3.Size = new System.Drawing.Size(51, 17);
            this.radioButton3.TabIndex = 12;
            this.radioButton3.TabStop = true;
            this.radioButton3.Text = "direct";
            this.radioButton3.UseVisualStyleBackColor = true;
            // 
            // radioButton2
            // 
            this.radioButton2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.radioButton2.AutoSize = true;
            this.radioButton2.Location = new System.Drawing.Point(18, 108);
            this.radioButton2.Name = "radioButton2";
            this.radioButton2.Size = new System.Drawing.Size(80, 17);
            this.radioButton2.TabIndex = 13;
            this.radioButton2.Text = "I_command";
            this.radioButton2.UseVisualStyleBackColor = true;
            // 
            // radioButton1
            // 
            this.radioButton1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.radioButton1.AutoSize = true;
            this.radioButton1.Location = new System.Drawing.Point(18, 85);
            this.radioButton1.Name = "radioButton1";
            this.radioButton1.Size = new System.Drawing.Size(84, 17);
            this.radioButton1.TabIndex = 14;
            this.radioButton1.Text = "A_command";
            this.radioButton1.UseVisualStyleBackColor = true;
            // 
            // button_O
            // 
            this.button_O.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.button_O.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button_O.Location = new System.Drawing.Point(17, 151);
            this.button_O.Name = "button_O";
            this.button_O.Size = new System.Drawing.Size(75, 23);
            this.button_O.TabIndex = 11;
            this.button_O.Text = "O";
            this.button_O.UseVisualStyleBackColor = true;
            this.button_O.Click += new System.EventHandler(this.communication_Command);
            // 
            // CommunicationControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.numericUpDownComControl);
            this.Controls.Add(this.radioButton3);
            this.Controls.Add(this.radioButton2);
            this.Controls.Add(this.radioButton1);
            this.Controls.Add(this.button_O);
            this.Controls.Add(this.button9);
            this.Controls.Add(this.button10);
            this.Controls.Add(this.buttonColor);
            this.Name = "CommunicationControl";
            this.Size = new System.Drawing.Size(186, 201);
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownComControl)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button button9;
        private System.Windows.Forms.Button button10;
        private System.Windows.Forms.Button buttonColor;
        private System.Windows.Forms.NumericUpDown numericUpDownComControl;
        private System.Windows.Forms.RadioButton radioButton3;
        private System.Windows.Forms.RadioButton radioButton2;
        private System.Windows.Forms.RadioButton radioButton1;
        private System.Windows.Forms.Button button_O;
        private System.Windows.Forms.ColorDialog DARC1000colorDialog;
    }
}
