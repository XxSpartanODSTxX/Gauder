namespace Robot_Lib
{
    partial class Configuration
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
            this.propertyGrid1 = new System.Windows.Forms.PropertyGrid();
            this.comboBoxControls = new System.Windows.Forms.ComboBox();
            this.comboBoxPluggins = new System.Windows.Forms.ComboBox();
            this.labelPluggins = new System.Windows.Forms.Label();
            this.buttonRes = new System.Windows.Forms.Button();
            this.buttonPlugin = new System.Windows.Forms.Button();
            this.labelControlls = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // propertyGrid1
            // 
            this.propertyGrid1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.propertyGrid1.Location = new System.Drawing.Point(2, 48);
            this.propertyGrid1.Name = "propertyGrid1";
            this.propertyGrid1.Size = new System.Drawing.Size(359, 179);
            this.propertyGrid1.TabIndex = 0;
            // 
            // comboBoxControls
            // 
            this.comboBoxControls.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.comboBoxControls.FormattingEnabled = true;
            this.comboBoxControls.Location = new System.Drawing.Point(6, 2);
            this.comboBoxControls.Name = "comboBoxControls";
            this.comboBoxControls.Size = new System.Drawing.Size(410, 21);
            this.comboBoxControls.TabIndex = 1;
            this.comboBoxControls.SelectedIndexChanged += new System.EventHandler(this.comboBoxPropertyselect_SelectedIndexChanged);
            // 
            // comboBoxPluggins
            // 
            this.comboBoxPluggins.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.comboBoxPluggins.FormattingEnabled = true;
            this.comboBoxPluggins.Location = new System.Drawing.Point(5, 26);
            this.comboBoxPluggins.Name = "comboBoxPluggins";
            this.comboBoxPluggins.Size = new System.Drawing.Size(411, 21);
            this.comboBoxPluggins.TabIndex = 1;
            this.comboBoxPluggins.SelectedIndexChanged += new System.EventHandler(this.comboBoxPropertyselect_SelectedIndexChanged);
            // 
            // labelPluggins
            // 
            this.labelPluggins.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.labelPluggins.AutoSize = true;
            this.labelPluggins.Location = new System.Drawing.Point(367, 89);
            this.labelPluggins.Name = "labelPluggins";
            this.labelPluggins.Size = new System.Drawing.Size(16, 13);
            this.labelPluggins.TabIndex = 5;
            this.labelPluggins.Text = "...";
            // 
            // buttonRes
            // 
            this.buttonRes.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonRes.Location = new System.Drawing.Point(422, 2);
            this.buttonRes.Name = "buttonRes";
            this.buttonRes.Size = new System.Drawing.Size(98, 23);
            this.buttonRes.TabIndex = 2;
            this.buttonRes.Text = "LoadRessources";
            this.buttonRes.UseVisualStyleBackColor = true;
            this.buttonRes.Click += new System.EventHandler(this.buttonRes_Click);
            // 
            // buttonPlugin
            // 
            this.buttonPlugin.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonPlugin.Location = new System.Drawing.Point(422, 26);
            this.buttonPlugin.Name = "buttonPlugin";
            this.buttonPlugin.Size = new System.Drawing.Size(98, 23);
            this.buttonPlugin.TabIndex = 2;
            this.buttonPlugin.Text = "LoadPlugins";
            this.buttonPlugin.UseVisualStyleBackColor = true;
            this.buttonPlugin.Click += new System.EventHandler(this.buttonPlugin_Click);
            // 
            // labelControlls
            // 
            this.labelControlls.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.labelControlls.AutoSize = true;
            this.labelControlls.Location = new System.Drawing.Point(367, 61);
            this.labelControlls.Name = "labelControlls";
            this.labelControlls.Size = new System.Drawing.Size(16, 13);
            this.labelControlls.TabIndex = 6;
            this.labelControlls.Text = "...";
            // 
            // Configuration
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.labelControlls);
            this.Controls.Add(this.labelPluggins);
            this.Controls.Add(this.buttonRes);
            this.Controls.Add(this.buttonPlugin);
            this.Controls.Add(this.comboBoxPluggins);
            this.Controls.Add(this.comboBoxControls);
            this.Controls.Add(this.propertyGrid1);
            this.Name = "Configuration";
            this.Size = new System.Drawing.Size(523, 233);
            this.Enter += new System.EventHandler(this.Configuration_Enter);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.ComboBox comboBoxControls;
        private System.Windows.Forms.ComboBox comboBoxPluggins;
        private System.Windows.Forms.Button buttonRes;
        private System.Windows.Forms.Button buttonPlugin;
        public System.Windows.Forms.PropertyGrid propertyGrid1;
        public System.Windows.Forms.Label labelPluggins;
        public System.Windows.Forms.Label labelControlls;
    }
}
