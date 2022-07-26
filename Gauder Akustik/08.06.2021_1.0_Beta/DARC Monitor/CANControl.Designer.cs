
namespace DarcMonitor
{
    partial class CANControl
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
            this.checkBoxHex = new System.Windows.Forms.CheckBox();
            this.comboBoxData = new System.Windows.Forms.ComboBox();
            this.comboBoxSetCAN = new System.Windows.Forms.ComboBox();
            this.buttonSetCAN = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // checkBoxHex
            // 
            this.checkBoxHex.AutoSize = true;
            this.checkBoxHex.Checked = true;
            this.checkBoxHex.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBoxHex.Location = new System.Drawing.Point(9, 17);
            this.checkBoxHex.Name = "checkBoxHex";
            this.checkBoxHex.Size = new System.Drawing.Size(43, 17);
            this.checkBoxHex.TabIndex = 24;
            this.checkBoxHex.Text = "hex";
            this.checkBoxHex.UseVisualStyleBackColor = true;
            // 
            // comboBoxData
            // 
            this.comboBoxData.FormattingEnabled = true;
            this.comboBoxData.Location = new System.Drawing.Point(9, 40);
            this.comboBoxData.Name = "comboBoxData";
            this.comboBoxData.Size = new System.Drawing.Size(249, 21);
            this.comboBoxData.TabIndex = 22;
            // 
            // comboBoxSetCAN
            // 
            this.comboBoxSetCAN.FormattingEnabled = true;
            this.comboBoxSetCAN.Items.AddRange(new object[] {
            "0x601",
            "0x602"});
            this.comboBoxSetCAN.Location = new System.Drawing.Point(56, 14);
            this.comboBoxSetCAN.Name = "comboBoxSetCAN";
            this.comboBoxSetCAN.Size = new System.Drawing.Size(128, 21);
            this.comboBoxSetCAN.TabIndex = 23;
            // 
            // buttonSetCAN
            // 
            this.buttonSetCAN.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonSetCAN.Location = new System.Drawing.Point(187, 13);
            this.buttonSetCAN.Name = "buttonSetCAN";
            this.buttonSetCAN.Size = new System.Drawing.Size(72, 23);
            this.buttonSetCAN.TabIndex = 21;
            this.buttonSetCAN.Text = "Set CAN";
            this.buttonSetCAN.UseVisualStyleBackColor = true;
            this.buttonSetCAN.Click += new System.EventHandler(this.buttonSetCAN_Click);
            // 
            // CANControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.checkBoxHex);
            this.Controls.Add(this.comboBoxData);
            this.Controls.Add(this.comboBoxSetCAN);
            this.Controls.Add(this.buttonSetCAN);
            this.Name = "CANControl";
            this.Size = new System.Drawing.Size(262, 85);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.CheckBox checkBoxHex;
        private System.Windows.Forms.ComboBox comboBoxData;
        private System.Windows.Forms.ComboBox comboBoxSetCAN;
        private System.Windows.Forms.Button buttonSetCAN;
    }
}
