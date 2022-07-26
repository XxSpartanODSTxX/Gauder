namespace PeakCANOpen_Lib
{
    partial class CANOpenControl
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
            this.components = new System.ComponentModel.Container();
            this.backgroundWorkerMessages = new System.ComponentModel.BackgroundWorker();
            this.radioButtonBit0 = new System.Windows.Forms.RadioButton();
            this.numericUpDownPos1 = new System.Windows.Forms.NumericUpDown();
            this.label6041 = new System.Windows.Forms.Label();
            this.buttonMove = new System.Windows.Forms.Button();
            this.listBox1 = new System.Windows.Forms.ListBox();
            this.buttonStopMove = new System.Windows.Forms.Button();
            this.label581 = new System.Windows.Forms.Label();
            this.textBoxControl = new System.Windows.Forms.TextBox();
            this.button1 = new System.Windows.Forms.Button();
            this.buttonSetSDO = new System.Windows.Forms.Button();
            this.textBoxSDOId = new System.Windows.Forms.TextBox();
            this.textBoxSDOsub = new System.Windows.Forms.TextBox();
            this.textBoxSDOValue = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.numericUpDownDrive = new System.Windows.Forms.NumericUpDown();
            this.buttonCANInit = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.buttonSpeed = new System.Windows.Forms.Button();
            this.checkBoxEnabled = new System.Windows.Forms.CheckBox();
            this.checkBoxPower = new System.Windows.Forms.CheckBox();
            this.checkBoxBit1 = new System.Windows.Forms.CheckBox();
            this.checkBoxBit3 = new System.Windows.Forms.CheckBox();
            this.checkBoxBit5 = new System.Windows.Forms.CheckBox();
            this.checkBoxBit6 = new System.Windows.Forms.CheckBox();
            this.checkBoxBit7 = new System.Windows.Forms.CheckBox();
            this.checkBoxBit8 = new System.Windows.Forms.CheckBox();
            this.checkBoxBit9 = new System.Windows.Forms.CheckBox();
            this.checkBoxBit10 = new System.Windows.Forms.CheckBox();
            this.checkBoxBit11 = new System.Windows.Forms.CheckBox();
            this.checkBoxBit0 = new System.Windows.Forms.CheckBox();
            this.checkBoxBit12 = new System.Windows.Forms.CheckBox();
            this.checkBoxBit13 = new System.Windows.Forms.CheckBox();
            this.checkBoxBit14 = new System.Windows.Forms.CheckBox();
            this.checkBoxBit15 = new System.Windows.Forms.CheckBox();
            this.button3 = new System.Windows.Forms.Button();
            this.numericUpDownSpeed = new System.Windows.Forms.NumericUpDown();
            this.checkBoxLog = new System.Windows.Forms.CheckBox();
            this.buttonUpdate = new System.Windows.Forms.Button();
            this.checkBoxOut1 = new System.Windows.Forms.CheckBox();
            this.checkBoxOut2 = new System.Windows.Forms.CheckBox();
            this.checkBoxOut3 = new System.Windows.Forms.CheckBox();
            this.checkBoxIn1 = new System.Windows.Forms.CheckBox();
            this.checkBoxIn2 = new System.Windows.Forms.CheckBox();
            this.checkBoxIn3 = new System.Windows.Forms.CheckBox();
            this.checkBoxIn4 = new System.Windows.Forms.CheckBox();
            this.checkBoxAutoUpdate = new System.Windows.Forms.CheckBox();
            this.timerAutoUpdate = new System.Windows.Forms.Timer(this.components);
            this.buttonDriveInit = new System.Windows.Forms.Button();
            this.buttonStopCAN = new System.Windows.Forms.Button();
            this.labelAnalog = new System.Windows.Forms.Label();
            this.buttonDriveOff = new System.Windows.Forms.Button();
            this.labelPosition = new System.Windows.Forms.Label();
            this.labelcmd = new System.Windows.Forms.Label();
            this.tabControlHand = new System.Windows.Forms.TabControl();
            this.tabPageOperation = new System.Windows.Forms.TabPage();
            this.labelData = new System.Windows.Forms.Label();
            this.tabPageProfile = new System.Windows.Forms.TabPage();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.numericUpDownAccel = new System.Windows.Forms.NumericUpDown();
            this.buttonSetProfile = new System.Windows.Forms.Button();
            this.tabPageHoming = new System.Windows.Forms.TabPage();
            this.buttonSetOffset = new System.Windows.Forms.Button();
            this.labelAnalogOffset = new System.Windows.Forms.Label();
            this.buttonCalcHomeOffset = new System.Windows.Forms.Button();
            this.numericUpDownHomeOffset = new System.Windows.Forms.NumericUpDown();
            this.label4 = new System.Windows.Forms.Label();
            this.buttonHoming = new System.Windows.Forms.Button();
            this.comboBox2 = new System.Windows.Forms.ComboBox();
            this.tabPageInterpolated = new System.Windows.Forms.TabPage();
            this.button2 = new System.Windows.Forms.Button();
            this.numericUpDown1 = new System.Windows.Forms.NumericUpDown();
            this.tabPageHand = new System.Windows.Forms.TabPage();
            this.labelRec = new System.Windows.Forms.Label();
            this.labelHandControl = new System.Windows.Forms.Label();
            this.checkBox4 = new System.Windows.Forms.CheckBox();
            this.Control = new System.Windows.Forms.CheckBox();
            this.checkBoxPumpe = new System.Windows.Forms.CheckBox();
            this.checkBoxVentil = new System.Windows.Forms.CheckBox();
            this.labelError = new System.Windows.Forms.Label();
            this.checkBoxDebug = new System.Windows.Forms.CheckBox();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownPos1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownDrive)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownSpeed)).BeginInit();
            this.tabControlHand.SuspendLayout();
            this.tabPageOperation.SuspendLayout();
            this.tabPageProfile.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownAccel)).BeginInit();
            this.tabPageHoming.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownHomeOffset)).BeginInit();
            this.tabPageInterpolated.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown1)).BeginInit();
            this.tabPageHand.SuspendLayout();
            this.SuspendLayout();
            // 
            // backgroundWorkerMessages
            // 
            this.backgroundWorkerMessages.WorkerSupportsCancellation = true;
            this.backgroundWorkerMessages.DoWork += new System.ComponentModel.DoWorkEventHandler(this.backgroundWorkerMessages_DoWork);
            // 
            // radioButtonBit0
            // 
            this.radioButtonBit0.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.radioButtonBit0.AutoSize = true;
            this.radioButtonBit0.Location = new System.Drawing.Point(13, 313);
            this.radioButtonBit0.Name = "radioButtonBit0";
            this.radioButtonBit0.Size = new System.Drawing.Size(51, 17);
            this.radioButtonBit0.TabIndex = 1;
            this.radioButtonBit0.TabStop = true;
            this.radioButtonBit0.Text = "ready";
            this.radioButtonBit0.UseVisualStyleBackColor = true;
            // 
            // numericUpDownPos1
            // 
            this.numericUpDownPos1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.numericUpDownPos1.Increment = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.numericUpDownPos1.Location = new System.Drawing.Point(131, 72);
            this.numericUpDownPos1.Maximum = new decimal(new int[] {
            500000,
            0,
            0,
            0});
            this.numericUpDownPos1.Minimum = new decimal(new int[] {
            500000,
            0,
            0,
            -2147483648});
            this.numericUpDownPos1.Name = "numericUpDownPos1";
            this.numericUpDownPos1.Size = new System.Drawing.Size(84, 20);
            this.numericUpDownPos1.TabIndex = 2;
            // 
            // label6041
            // 
            this.label6041.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label6041.AutoSize = true;
            this.label6041.Location = new System.Drawing.Point(3, 283);
            this.label6041.Name = "label6041";
            this.label6041.Size = new System.Drawing.Size(30, 13);
            this.label6041.TabIndex = 3;
            this.label6041.Text = "0x00";
            // 
            // buttonMove
            // 
            this.buttonMove.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonMove.Location = new System.Drawing.Point(226, 72);
            this.buttonMove.Name = "buttonMove";
            this.buttonMove.Size = new System.Drawing.Size(75, 23);
            this.buttonMove.TabIndex = 4;
            this.buttonMove.Text = "move";
            this.buttonMove.UseVisualStyleBackColor = true;
            this.buttonMove.Click += new System.EventHandler(this.buttonMove_Click);
            // 
            // listBox1
            // 
            this.listBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.listBox1.FormattingEnabled = true;
            this.listBox1.Location = new System.Drawing.Point(3, 3);
            this.listBox1.Name = "listBox1";
            this.listBox1.Size = new System.Drawing.Size(392, 251);
            this.listBox1.Sorted = true;
            this.listBox1.TabIndex = 5;
            // 
            // buttonStopMove
            // 
            this.buttonStopMove.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonStopMove.Location = new System.Drawing.Point(653, 207);
            this.buttonStopMove.Name = "buttonStopMove";
            this.buttonStopMove.Size = new System.Drawing.Size(75, 23);
            this.buttonStopMove.TabIndex = 6;
            this.buttonStopMove.Text = "Stop Move";
            this.buttonStopMove.UseVisualStyleBackColor = true;
            this.buttonStopMove.Click += new System.EventHandler(this.buttonStopMove_Click);
            // 
            // label581
            // 
            this.label581.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label581.AutoSize = true;
            this.label581.Location = new System.Drawing.Point(3, 303);
            this.label581.Name = "label581";
            this.label581.Size = new System.Drawing.Size(30, 13);
            this.label581.TabIndex = 3;
            this.label581.Text = "0x00";
            // 
            // textBoxControl
            // 
            this.textBoxControl.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxControl.Location = new System.Drawing.Point(149, 43);
            this.textBoxControl.Name = "textBoxControl";
            this.textBoxControl.Size = new System.Drawing.Size(76, 20);
            this.textBoxControl.TabIndex = 7;
            // 
            // button1
            // 
            this.button1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.button1.Location = new System.Drawing.Point(233, 43);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 4;
            this.button1.Text = "set Control";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.buttonSetControl_Click);
            // 
            // buttonSetSDO
            // 
            this.buttonSetSDO.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonSetSDO.Location = new System.Drawing.Point(233, 13);
            this.buttonSetSDO.Name = "buttonSetSDO";
            this.buttonSetSDO.Size = new System.Drawing.Size(75, 23);
            this.buttonSetSDO.TabIndex = 4;
            this.buttonSetSDO.Text = "set SDO";
            this.buttonSetSDO.UseVisualStyleBackColor = true;
            this.buttonSetSDO.Click += new System.EventHandler(this.buttonSetSDO_Click);
            // 
            // textBoxSDOId
            // 
            this.textBoxSDOId.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxSDOId.Location = new System.Drawing.Point(33, 16);
            this.textBoxSDOId.Name = "textBoxSDOId";
            this.textBoxSDOId.Size = new System.Drawing.Size(60, 20);
            this.textBoxSDOId.TabIndex = 7;
            // 
            // textBoxSDOsub
            // 
            this.textBoxSDOsub.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxSDOsub.Location = new System.Drawing.Point(107, 16);
            this.textBoxSDOsub.Name = "textBoxSDOsub";
            this.textBoxSDOsub.Size = new System.Drawing.Size(20, 20);
            this.textBoxSDOsub.TabIndex = 7;
            // 
            // textBoxSDOValue
            // 
            this.textBoxSDOValue.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxSDOValue.Location = new System.Drawing.Point(148, 16);
            this.textBoxSDOValue.Name = "textBoxSDOValue";
            this.textBoxSDOValue.Size = new System.Drawing.Size(77, 20);
            this.textBoxSDOValue.TabIndex = 7;
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(133, 19);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(13, 13);
            this.label1.TabIndex = 3;
            this.label1.Text = "=";
            // 
            // label2
            // 
            this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(94, 18);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(10, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = ":";
            // 
            // numericUpDownDrive
            // 
            this.numericUpDownDrive.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.numericUpDownDrive.Location = new System.Drawing.Point(473, 19);
            this.numericUpDownDrive.Maximum = new decimal(new int[] {
            4,
            0,
            0,
            0});
            this.numericUpDownDrive.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numericUpDownDrive.Name = "numericUpDownDrive";
            this.numericUpDownDrive.Size = new System.Drawing.Size(39, 20);
            this.numericUpDownDrive.TabIndex = 2;
            this.numericUpDownDrive.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numericUpDownDrive.ValueChanged += new System.EventHandler(this.numericUpDownDrive_ValueChanged);
            // 
            // buttonCANInit
            // 
            this.buttonCANInit.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonCANInit.Location = new System.Drawing.Point(653, 16);
            this.buttonCANInit.Name = "buttonCANInit";
            this.buttonCANInit.Size = new System.Drawing.Size(75, 23);
            this.buttonCANInit.TabIndex = 4;
            this.buttonCANInit.Text = "CANinit";
            this.buttonCANInit.UseVisualStyleBackColor = true;
            this.buttonCANInit.Click += new System.EventHandler(this.ButtonInit_Click);
            // 
            // label3
            // 
            this.label3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(402, 21);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(68, 13);
            this.label3.TabIndex = 3;
            this.label3.Text = "Select Drive:";
            // 
            // buttonSpeed
            // 
            this.buttonSpeed.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonSpeed.Location = new System.Drawing.Point(226, 43);
            this.buttonSpeed.Name = "buttonSpeed";
            this.buttonSpeed.Size = new System.Drawing.Size(74, 23);
            this.buttonSpeed.TabIndex = 4;
            this.buttonSpeed.Text = "setSpeed";
            this.buttonSpeed.UseVisualStyleBackColor = true;
            this.buttonSpeed.Click += new System.EventHandler(this.buttonSpeed_Click);
            // 
            // checkBoxEnabled
            // 
            this.checkBoxEnabled.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.checkBoxEnabled.AutoCheck = false;
            this.checkBoxEnabled.AutoSize = true;
            this.checkBoxEnabled.Location = new System.Drawing.Point(129, 322);
            this.checkBoxEnabled.Name = "checkBoxEnabled";
            this.checkBoxEnabled.Size = new System.Drawing.Size(65, 17);
            this.checkBoxEnabled.TabIndex = 1;
            this.checkBoxEnabled.Text = "Enabled";
            this.checkBoxEnabled.UseVisualStyleBackColor = true;
            // 
            // checkBoxPower
            // 
            this.checkBoxPower.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.checkBoxPower.AutoCheck = false;
            this.checkBoxPower.AutoSize = true;
            this.checkBoxPower.Location = new System.Drawing.Point(296, 322);
            this.checkBoxPower.Name = "checkBoxPower";
            this.checkBoxPower.Size = new System.Drawing.Size(56, 17);
            this.checkBoxPower.TabIndex = 1;
            this.checkBoxPower.Text = "Power";
            this.checkBoxPower.UseVisualStyleBackColor = true;
            // 
            // checkBoxBit1
            // 
            this.checkBoxBit1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.checkBoxBit1.AutoCheck = false;
            this.checkBoxBit1.AutoSize = true;
            this.checkBoxBit1.Location = new System.Drawing.Point(66, 322);
            this.checkBoxBit1.Name = "checkBoxBit1";
            this.checkBoxBit1.Size = new System.Drawing.Size(38, 17);
            this.checkBoxBit1.TabIndex = 1;
            this.checkBoxBit1.Text = "on";
            this.checkBoxBit1.UseVisualStyleBackColor = true;
            // 
            // checkBoxBit3
            // 
            this.checkBoxBit3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.checkBoxBit3.AutoCheck = false;
            this.checkBoxBit3.AutoSize = true;
            this.checkBoxBit3.Location = new System.Drawing.Point(216, 322);
            this.checkBoxBit3.Name = "checkBoxBit3";
            this.checkBoxBit3.Size = new System.Drawing.Size(46, 17);
            this.checkBoxBit3.TabIndex = 1;
            this.checkBoxBit3.Text = "fault";
            this.checkBoxBit3.UseVisualStyleBackColor = true;
            // 
            // checkBoxBit5
            // 
            this.checkBoxBit5.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.checkBoxBit5.AutoCheck = false;
            this.checkBoxBit5.AutoSize = true;
            this.checkBoxBit5.Location = new System.Drawing.Point(361, 322);
            this.checkBoxBit5.Name = "checkBoxBit5";
            this.checkBoxBit5.Size = new System.Drawing.Size(91, 17);
            this.checkBoxBit5.TabIndex = 1;
            this.checkBoxBit5.Text = "no QuickStop";
            this.checkBoxBit5.UseVisualStyleBackColor = true;
            // 
            // checkBoxBit6
            // 
            this.checkBoxBit6.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.checkBoxBit6.AutoCheck = false;
            this.checkBoxBit6.AutoSize = true;
            this.checkBoxBit6.Location = new System.Drawing.Point(448, 322);
            this.checkBoxBit6.Name = "checkBoxBit6";
            this.checkBoxBit6.Size = new System.Drawing.Size(65, 17);
            this.checkBoxBit6.TabIndex = 1;
            this.checkBoxBit6.Text = "disabled";
            this.checkBoxBit6.UseVisualStyleBackColor = true;
            // 
            // checkBoxBit7
            // 
            this.checkBoxBit7.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.checkBoxBit7.AutoCheck = false;
            this.checkBoxBit7.AutoSize = true;
            this.checkBoxBit7.Location = new System.Drawing.Point(517, 322);
            this.checkBoxBit7.Name = "checkBoxBit7";
            this.checkBoxBit7.Size = new System.Drawing.Size(63, 17);
            this.checkBoxBit7.TabIndex = 1;
            this.checkBoxBit7.Text = "warning";
            this.checkBoxBit7.UseVisualStyleBackColor = true;
            // 
            // checkBoxBit8
            // 
            this.checkBoxBit8.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.checkBoxBit8.AutoCheck = false;
            this.checkBoxBit8.AutoSize = true;
            this.checkBoxBit8.Location = new System.Drawing.Point(3, 343);
            this.checkBoxBit8.Name = "checkBoxBit8";
            this.checkBoxBit8.Size = new System.Drawing.Size(48, 17);
            this.checkBoxBit8.TabIndex = 1;
            this.checkBoxBit8.Text = "sync";
            this.checkBoxBit8.UseVisualStyleBackColor = true;
            // 
            // checkBoxBit9
            // 
            this.checkBoxBit9.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.checkBoxBit9.AutoCheck = false;
            this.checkBoxBit9.AutoSize = true;
            this.checkBoxBit9.Location = new System.Drawing.Point(66, 343);
            this.checkBoxBit9.Name = "checkBoxBit9";
            this.checkBoxBit9.Size = new System.Drawing.Size(58, 17);
            this.checkBoxBit9.TabIndex = 1;
            this.checkBoxBit9.Text = "remote";
            this.checkBoxBit9.UseVisualStyleBackColor = true;
            // 
            // checkBoxBit10
            // 
            this.checkBoxBit10.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.checkBoxBit10.AutoCheck = false;
            this.checkBoxBit10.AutoSize = true;
            this.checkBoxBit10.Location = new System.Drawing.Point(129, 343);
            this.checkBoxBit10.Name = "checkBoxBit10";
            this.checkBoxBit10.Size = new System.Drawing.Size(53, 17);
            this.checkBoxBit10.TabIndex = 1;
            this.checkBoxBit10.Text = "target";
            this.checkBoxBit10.UseVisualStyleBackColor = true;
            // 
            // checkBoxBit11
            // 
            this.checkBoxBit11.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.checkBoxBit11.AutoCheck = false;
            this.checkBoxBit11.AutoSize = true;
            this.checkBoxBit11.Location = new System.Drawing.Point(216, 343);
            this.checkBoxBit11.Name = "checkBoxBit11";
            this.checkBoxBit11.Size = new System.Drawing.Size(43, 17);
            this.checkBoxBit11.TabIndex = 1;
            this.checkBoxBit11.Text = "limit";
            this.checkBoxBit11.UseVisualStyleBackColor = true;
            // 
            // checkBoxBit0
            // 
            this.checkBoxBit0.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.checkBoxBit0.AutoCheck = false;
            this.checkBoxBit0.AutoSize = true;
            this.checkBoxBit0.Location = new System.Drawing.Point(3, 322);
            this.checkBoxBit0.Name = "checkBoxBit0";
            this.checkBoxBit0.Size = new System.Drawing.Size(52, 17);
            this.checkBoxBit0.TabIndex = 1;
            this.checkBoxBit0.Text = "ready";
            this.checkBoxBit0.UseVisualStyleBackColor = true;
            // 
            // checkBoxBit12
            // 
            this.checkBoxBit12.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.checkBoxBit12.AutoCheck = false;
            this.checkBoxBit12.AutoSize = true;
            this.checkBoxBit12.Location = new System.Drawing.Point(296, 343);
            this.checkBoxBit12.Name = "checkBoxBit12";
            this.checkBoxBit12.Size = new System.Drawing.Size(44, 17);
            this.checkBoxBit12.TabIndex = 1;
            this.checkBoxBit12.Text = "ack";
            this.checkBoxBit12.UseVisualStyleBackColor = true;
            // 
            // checkBoxBit13
            // 
            this.checkBoxBit13.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.checkBoxBit13.AutoCheck = false;
            this.checkBoxBit13.AutoSize = true;
            this.checkBoxBit13.Location = new System.Drawing.Point(361, 343);
            this.checkBoxBit13.Name = "checkBoxBit13";
            this.checkBoxBit13.Size = new System.Drawing.Size(53, 17);
            this.checkBoxBit13.TabIndex = 1;
            this.checkBoxBit13.Text = "f-error";
            this.checkBoxBit13.UseVisualStyleBackColor = true;
            // 
            // checkBoxBit14
            // 
            this.checkBoxBit14.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.checkBoxBit14.AutoCheck = false;
            this.checkBoxBit14.AutoSize = true;
            this.checkBoxBit14.Location = new System.Drawing.Point(448, 343);
            this.checkBoxBit14.Name = "checkBoxBit14";
            this.checkBoxBit14.Size = new System.Drawing.Size(49, 17);
            this.checkBoxBit14.TabIndex = 1;
            this.checkBoxBit14.Text = "bit14";
            this.checkBoxBit14.UseVisualStyleBackColor = true;
            // 
            // checkBoxBit15
            // 
            this.checkBoxBit15.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.checkBoxBit15.AutoCheck = false;
            this.checkBoxBit15.AutoSize = true;
            this.checkBoxBit15.Location = new System.Drawing.Point(517, 343);
            this.checkBoxBit15.Name = "checkBoxBit15";
            this.checkBoxBit15.Size = new System.Drawing.Size(49, 17);
            this.checkBoxBit15.TabIndex = 1;
            this.checkBoxBit15.Text = "bit15";
            this.checkBoxBit15.UseVisualStyleBackColor = true;
            // 
            // button3
            // 
            this.button3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.button3.Location = new System.Drawing.Point(558, 180);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(66, 23);
            this.button3.TabIndex = 4;
            this.button3.Text = "Drive On";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.buttonDriveOn_Click);
            // 
            // numericUpDownSpeed
            // 
            this.numericUpDownSpeed.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.numericUpDownSpeed.Increment = new decimal(new int[] {
            100,
            0,
            0,
            0});
            this.numericUpDownSpeed.Location = new System.Drawing.Point(131, 45);
            this.numericUpDownSpeed.Maximum = new decimal(new int[] {
            100000,
            0,
            0,
            0});
            this.numericUpDownSpeed.Minimum = new decimal(new int[] {
            100000,
            0,
            0,
            -2147483648});
            this.numericUpDownSpeed.Name = "numericUpDownSpeed";
            this.numericUpDownSpeed.Size = new System.Drawing.Size(84, 20);
            this.numericUpDownSpeed.TabIndex = 9;
            this.numericUpDownSpeed.Value = new decimal(new int[] {
            500,
            0,
            0,
            0});
            // 
            // checkBoxLog
            // 
            this.checkBoxLog.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.checkBoxLog.AutoSize = true;
            this.checkBoxLog.Location = new System.Drawing.Point(473, 235);
            this.checkBoxLog.Name = "checkBoxLog";
            this.checkBoxLog.Size = new System.Drawing.Size(44, 17);
            this.checkBoxLog.TabIndex = 10;
            this.checkBoxLog.Text = "Log";
            this.checkBoxLog.UseVisualStyleBackColor = true;
            this.checkBoxLog.CheckedChanged += new System.EventHandler(this.checkBoxLog_CheckedChanged);
            // 
            // buttonUpdate
            // 
            this.buttonUpdate.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonUpdate.Location = new System.Drawing.Point(401, 231);
            this.buttonUpdate.Name = "buttonUpdate";
            this.buttonUpdate.Size = new System.Drawing.Size(66, 23);
            this.buttonUpdate.TabIndex = 6;
            this.buttonUpdate.Text = "Update";
            this.buttonUpdate.UseVisualStyleBackColor = true;
            this.buttonUpdate.Click += new System.EventHandler(this.buttonUpdate_Click);
            // 
            // checkBoxOut1
            // 
            this.checkBoxOut1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.checkBoxOut1.AutoSize = true;
            this.checkBoxOut1.Location = new System.Drawing.Point(402, 180);
            this.checkBoxOut1.Name = "checkBoxOut1";
            this.checkBoxOut1.Size = new System.Drawing.Size(40, 17);
            this.checkBoxOut1.TabIndex = 1;
            this.checkBoxOut1.Text = "O1";
            this.checkBoxOut1.UseVisualStyleBackColor = true;
            this.checkBoxOut1.CheckedChanged += new System.EventHandler(this.checkBoxOut_CheckedChanged);
            // 
            // checkBoxOut2
            // 
            this.checkBoxOut2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.checkBoxOut2.AutoSize = true;
            this.checkBoxOut2.Location = new System.Drawing.Point(446, 180);
            this.checkBoxOut2.Name = "checkBoxOut2";
            this.checkBoxOut2.Size = new System.Drawing.Size(40, 17);
            this.checkBoxOut2.TabIndex = 1;
            this.checkBoxOut2.Text = "O2";
            this.checkBoxOut2.UseVisualStyleBackColor = true;
            this.checkBoxOut2.CheckedChanged += new System.EventHandler(this.checkBoxOut_CheckedChanged);
            // 
            // checkBoxOut3
            // 
            this.checkBoxOut3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.checkBoxOut3.AutoSize = true;
            this.checkBoxOut3.Location = new System.Drawing.Point(490, 180);
            this.checkBoxOut3.Name = "checkBoxOut3";
            this.checkBoxOut3.Size = new System.Drawing.Size(40, 17);
            this.checkBoxOut3.TabIndex = 1;
            this.checkBoxOut3.Text = "O3";
            this.checkBoxOut3.UseVisualStyleBackColor = true;
            this.checkBoxOut3.CheckedChanged += new System.EventHandler(this.checkBoxOut_CheckedChanged);
            // 
            // checkBoxIn1
            // 
            this.checkBoxIn1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.checkBoxIn1.AutoSize = true;
            this.checkBoxIn1.Location = new System.Drawing.Point(401, 206);
            this.checkBoxIn1.Name = "checkBoxIn1";
            this.checkBoxIn1.Size = new System.Drawing.Size(35, 17);
            this.checkBoxIn1.TabIndex = 1;
            this.checkBoxIn1.Text = "I1";
            this.checkBoxIn1.UseVisualStyleBackColor = true;
            // 
            // checkBoxIn2
            // 
            this.checkBoxIn2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.checkBoxIn2.AutoSize = true;
            this.checkBoxIn2.Location = new System.Drawing.Point(435, 206);
            this.checkBoxIn2.Name = "checkBoxIn2";
            this.checkBoxIn2.Size = new System.Drawing.Size(35, 17);
            this.checkBoxIn2.TabIndex = 1;
            this.checkBoxIn2.Text = "I2";
            this.checkBoxIn2.UseVisualStyleBackColor = true;
            // 
            // checkBoxIn3
            // 
            this.checkBoxIn3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.checkBoxIn3.AutoSize = true;
            this.checkBoxIn3.Location = new System.Drawing.Point(468, 206);
            this.checkBoxIn3.Name = "checkBoxIn3";
            this.checkBoxIn3.Size = new System.Drawing.Size(35, 17);
            this.checkBoxIn3.TabIndex = 1;
            this.checkBoxIn3.Text = "I3";
            this.checkBoxIn3.UseVisualStyleBackColor = true;
            // 
            // checkBoxIn4
            // 
            this.checkBoxIn4.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.checkBoxIn4.AutoSize = true;
            this.checkBoxIn4.Location = new System.Drawing.Point(504, 206);
            this.checkBoxIn4.Name = "checkBoxIn4";
            this.checkBoxIn4.Size = new System.Drawing.Size(35, 17);
            this.checkBoxIn4.TabIndex = 1;
            this.checkBoxIn4.Text = "I4";
            this.checkBoxIn4.UseVisualStyleBackColor = true;
            // 
            // checkBoxAutoUpdate
            // 
            this.checkBoxAutoUpdate.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.checkBoxAutoUpdate.AutoSize = true;
            this.checkBoxAutoUpdate.Location = new System.Drawing.Point(564, 211);
            this.checkBoxAutoUpdate.Name = "checkBoxAutoUpdate";
            this.checkBoxAutoUpdate.Size = new System.Drawing.Size(83, 17);
            this.checkBoxAutoUpdate.TabIndex = 10;
            this.checkBoxAutoUpdate.Text = "AutoUpdate";
            this.checkBoxAutoUpdate.UseVisualStyleBackColor = true;
            this.checkBoxAutoUpdate.CheckedChanged += new System.EventHandler(this.checkBoxAutoUpdate_CheckedChanged);
            // 
            // timerAutoUpdate
            // 
            this.timerAutoUpdate.Interval = 800;
            this.timerAutoUpdate.Tick += new System.EventHandler(this.timerAutoUpdate_Tick);
            // 
            // buttonDriveInit
            // 
            this.buttonDriveInit.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonDriveInit.Location = new System.Drawing.Point(579, 16);
            this.buttonDriveInit.Name = "buttonDriveInit";
            this.buttonDriveInit.Size = new System.Drawing.Size(63, 23);
            this.buttonDriveInit.TabIndex = 4;
            this.buttonDriveInit.Text = "DriveInit";
            this.buttonDriveInit.UseVisualStyleBackColor = true;
            this.buttonDriveInit.Click += new System.EventHandler(this.ButtonDriveInit_Click);
            // 
            // buttonStopCAN
            // 
            this.buttonStopCAN.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonStopCAN.Location = new System.Drawing.Point(653, 235);
            this.buttonStopCAN.Name = "buttonStopCAN";
            this.buttonStopCAN.Size = new System.Drawing.Size(75, 23);
            this.buttonStopCAN.TabIndex = 6;
            this.buttonStopCAN.Text = "Stop CAN";
            this.buttonStopCAN.UseVisualStyleBackColor = true;
            this.buttonStopCAN.Click += new System.EventHandler(this.buttonStop_Click);
            // 
            // labelAnalog
            // 
            this.labelAnalog.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.labelAnalog.AutoSize = true;
            this.labelAnalog.Location = new System.Drawing.Point(596, 323);
            this.labelAnalog.Name = "labelAnalog";
            this.labelAnalog.Size = new System.Drawing.Size(30, 13);
            this.labelAnalog.TabIndex = 3;
            this.labelAnalog.Text = "0x00";
            // 
            // buttonDriveOff
            // 
            this.buttonDriveOff.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonDriveOff.Location = new System.Drawing.Point(696, 178);
            this.buttonDriveOff.Name = "buttonDriveOff";
            this.buttonDriveOff.Size = new System.Drawing.Size(32, 23);
            this.buttonDriveOff.TabIndex = 4;
            this.buttonDriveOff.Text = "Off";
            this.buttonDriveOff.UseVisualStyleBackColor = true;
            this.buttonDriveOff.Click += new System.EventHandler(this.buttonDriveOff_Click);
            // 
            // labelPosition
            // 
            this.labelPosition.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.labelPosition.AutoSize = true;
            this.labelPosition.Location = new System.Drawing.Point(596, 343);
            this.labelPosition.Name = "labelPosition";
            this.labelPosition.Size = new System.Drawing.Size(30, 13);
            this.labelPosition.TabIndex = 3;
            this.labelPosition.Text = "0x00";
            // 
            // labelcmd
            // 
            this.labelcmd.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.labelcmd.AutoSize = true;
            this.labelcmd.Location = new System.Drawing.Point(15, 19);
            this.labelcmd.Name = "labelcmd";
            this.labelcmd.Size = new System.Drawing.Size(14, 13);
            this.labelcmd.TabIndex = 3;
            this.labelcmd.Text = "#";
            this.labelcmd.Click += new System.EventHandler(this.labelcmd_Click);
            // 
            // tabControlHand
            // 
            this.tabControlHand.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.tabControlHand.Controls.Add(this.tabPageOperation);
            this.tabControlHand.Controls.Add(this.tabPageProfile);
            this.tabControlHand.Controls.Add(this.tabPageHoming);
            this.tabControlHand.Controls.Add(this.tabPageInterpolated);
            this.tabControlHand.Controls.Add(this.tabPageHand);
            this.tabControlHand.Location = new System.Drawing.Point(405, 45);
            this.tabControlHand.Name = "tabControlHand";
            this.tabControlHand.SelectedIndex = 0;
            this.tabControlHand.Size = new System.Drawing.Size(323, 127);
            this.tabControlHand.TabIndex = 12;
            // 
            // tabPageOperation
            // 
            this.tabPageOperation.Controls.Add(this.labelData);
            this.tabPageOperation.Controls.Add(this.buttonSetSDO);
            this.tabPageOperation.Controls.Add(this.label1);
            this.tabPageOperation.Controls.Add(this.label2);
            this.tabPageOperation.Controls.Add(this.labelcmd);
            this.tabPageOperation.Controls.Add(this.textBoxSDOId);
            this.tabPageOperation.Controls.Add(this.textBoxSDOsub);
            this.tabPageOperation.Controls.Add(this.textBoxControl);
            this.tabPageOperation.Controls.Add(this.textBoxSDOValue);
            this.tabPageOperation.Controls.Add(this.button1);
            this.tabPageOperation.Location = new System.Drawing.Point(4, 22);
            this.tabPageOperation.Name = "tabPageOperation";
            this.tabPageOperation.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageOperation.Size = new System.Drawing.Size(315, 101);
            this.tabPageOperation.TabIndex = 2;
            this.tabPageOperation.Text = "Operation";
            this.tabPageOperation.UseVisualStyleBackColor = true;
            // 
            // labelData
            // 
            this.labelData.AutoSize = true;
            this.labelData.Location = new System.Drawing.Point(18, 78);
            this.labelData.Name = "labelData";
            this.labelData.Size = new System.Drawing.Size(16, 13);
            this.labelData.TabIndex = 8;
            this.labelData.Text = "...";
            // 
            // tabPageProfile
            // 
            this.tabPageProfile.Controls.Add(this.comboBox1);
            this.tabPageProfile.Controls.Add(this.numericUpDownAccel);
            this.tabPageProfile.Controls.Add(this.numericUpDownSpeed);
            this.tabPageProfile.Controls.Add(this.numericUpDownPos1);
            this.tabPageProfile.Controls.Add(this.buttonMove);
            this.tabPageProfile.Controls.Add(this.buttonSetProfile);
            this.tabPageProfile.Controls.Add(this.buttonSpeed);
            this.tabPageProfile.Location = new System.Drawing.Point(4, 22);
            this.tabPageProfile.Name = "tabPageProfile";
            this.tabPageProfile.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageProfile.Size = new System.Drawing.Size(315, 101);
            this.tabPageProfile.TabIndex = 0;
            this.tabPageProfile.Text = "Profile";
            this.tabPageProfile.UseVisualStyleBackColor = true;
            // 
            // comboBox1
            // 
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Items.AddRange(new object[] {
            "trapez",
            "Sin2 ",
            "Jerkfree"});
            this.comboBox1.Location = new System.Drawing.Point(6, 16);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(98, 21);
            this.comboBox1.TabIndex = 10;
            // 
            // numericUpDownAccel
            // 
            this.numericUpDownAccel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.numericUpDownAccel.Increment = new decimal(new int[] {
            100,
            0,
            0,
            0});
            this.numericUpDownAccel.Location = new System.Drawing.Point(131, 14);
            this.numericUpDownAccel.Maximum = new decimal(new int[] {
            100000,
            0,
            0,
            0});
            this.numericUpDownAccel.Minimum = new decimal(new int[] {
            100000,
            0,
            0,
            -2147483648});
            this.numericUpDownAccel.Name = "numericUpDownAccel";
            this.numericUpDownAccel.Size = new System.Drawing.Size(84, 20);
            this.numericUpDownAccel.TabIndex = 9;
            this.numericUpDownAccel.Value = new decimal(new int[] {
            500,
            0,
            0,
            0});
            // 
            // buttonSetProfile
            // 
            this.buttonSetProfile.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonSetProfile.Location = new System.Drawing.Point(227, 14);
            this.buttonSetProfile.Name = "buttonSetProfile";
            this.buttonSetProfile.Size = new System.Drawing.Size(74, 23);
            this.buttonSetProfile.TabIndex = 4;
            this.buttonSetProfile.Text = "setProfile";
            this.buttonSetProfile.UseVisualStyleBackColor = true;
            this.buttonSetProfile.Click += new System.EventHandler(this.buttonSetProfile_Click);
            // 
            // tabPageHoming
            // 
            this.tabPageHoming.Controls.Add(this.buttonSetOffset);
            this.tabPageHoming.Controls.Add(this.labelAnalogOffset);
            this.tabPageHoming.Controls.Add(this.buttonCalcHomeOffset);
            this.tabPageHoming.Controls.Add(this.numericUpDownHomeOffset);
            this.tabPageHoming.Controls.Add(this.label4);
            this.tabPageHoming.Controls.Add(this.buttonHoming);
            this.tabPageHoming.Controls.Add(this.comboBox2);
            this.tabPageHoming.Location = new System.Drawing.Point(4, 22);
            this.tabPageHoming.Name = "tabPageHoming";
            this.tabPageHoming.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageHoming.Size = new System.Drawing.Size(315, 101);
            this.tabPageHoming.TabIndex = 1;
            this.tabPageHoming.Text = "Homing";
            this.tabPageHoming.UseVisualStyleBackColor = true;
            // 
            // buttonSetOffset
            // 
            this.buttonSetOffset.Location = new System.Drawing.Point(185, 55);
            this.buttonSetOffset.Name = "buttonSetOffset";
            this.buttonSetOffset.Size = new System.Drawing.Size(62, 23);
            this.buttonSetOffset.TabIndex = 17;
            this.buttonSetOffset.Text = "SetOffset";
            this.buttonSetOffset.UseVisualStyleBackColor = true;
            this.buttonSetOffset.Click += new System.EventHandler(this.buttonSetOffset_Click);
            // 
            // labelAnalogOffset
            // 
            this.labelAnalogOffset.AutoSize = true;
            this.labelAnalogOffset.Location = new System.Drawing.Point(11, 81);
            this.labelAnalogOffset.Name = "labelAnalogOffset";
            this.labelAnalogOffset.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.labelAnalogOffset.Size = new System.Drawing.Size(13, 13);
            this.labelAnalogOffset.TabIndex = 16;
            this.labelAnalogOffset.Text = "0";
            // 
            // buttonCalcHomeOffset
            // 
            this.buttonCalcHomeOffset.Location = new System.Drawing.Point(120, 54);
            this.buttonCalcHomeOffset.Name = "buttonCalcHomeOffset";
            this.buttonCalcHomeOffset.Size = new System.Drawing.Size(47, 23);
            this.buttonCalcHomeOffset.TabIndex = 15;
            this.buttonCalcHomeOffset.Text = "Calc";
            this.buttonCalcHomeOffset.UseVisualStyleBackColor = true;
            this.buttonCalcHomeOffset.Click += new System.EventHandler(this.buttonCalcHomeOffset_Click);
            // 
            // numericUpDownHomeOffset
            // 
            this.numericUpDownHomeOffset.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.numericUpDownHomeOffset.Increment = new decimal(new int[] {
            100,
            0,
            0,
            0});
            this.numericUpDownHomeOffset.Location = new System.Drawing.Point(10, 55);
            this.numericUpDownHomeOffset.Maximum = new decimal(new int[] {
            500000,
            0,
            0,
            0});
            this.numericUpDownHomeOffset.Minimum = new decimal(new int[] {
            500000,
            0,
            0,
            -2147483648});
            this.numericUpDownHomeOffset.Name = "numericUpDownHomeOffset";
            this.numericUpDownHomeOffset.Size = new System.Drawing.Size(84, 20);
            this.numericUpDownHomeOffset.TabIndex = 14;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(6, 39);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(66, 13);
            this.label4.TabIndex = 13;
            this.label4.Text = "Home Offset";
            // 
            // buttonHoming
            // 
            this.buttonHoming.Location = new System.Drawing.Point(244, 15);
            this.buttonHoming.Name = "buttonHoming";
            this.buttonHoming.Size = new System.Drawing.Size(56, 23);
            this.buttonHoming.TabIndex = 12;
            this.buttonHoming.Text = "Homing";
            this.buttonHoming.UseVisualStyleBackColor = true;
            this.buttonHoming.Click += new System.EventHandler(this.buttonHoming_Click);
            // 
            // comboBox2
            // 
            this.comboBox2.FormattingEnabled = true;
            this.comboBox2.Items.AddRange(new object[] {
            "19 : Externe Referenzfahrt – Schalter als Öffner",
            "20 : Externe Referenzfahrt – Schalter als Schließer",
            "21 : Externe Referenzfahrt – Schalter als Öffner",
            "22 : Externe Referenzfahrt – Schalter als Schließer",
            "33 : Interne Referenzfahrt",
            "34 : Interne Referenzfahrt",
            "35 : Positionsreset",
            "-2 : Referenzfahrt auf Blockierung CW",
            "-3 : Referenzfahrt auf Blockierung CCW",
            "-4 : Referenzfahrt auf externes IO-Node",
            "-5 : Referenzfahrt auf externes IO-Node",
            "-6 : Referenzfahrt auf externes IO-Node",
            "-7 : Referenzfahrt auf externes IO-Node"});
            this.comboBox2.Location = new System.Drawing.Point(6, 15);
            this.comboBox2.Name = "comboBox2";
            this.comboBox2.Size = new System.Drawing.Size(232, 21);
            this.comboBox2.TabIndex = 11;
            // 
            // tabPageInterpolated
            // 
            this.tabPageInterpolated.Controls.Add(this.button2);
            this.tabPageInterpolated.Controls.Add(this.numericUpDown1);
            this.tabPageInterpolated.Location = new System.Drawing.Point(4, 22);
            this.tabPageInterpolated.Name = "tabPageInterpolated";
            this.tabPageInterpolated.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageInterpolated.Size = new System.Drawing.Size(315, 101);
            this.tabPageInterpolated.TabIndex = 3;
            this.tabPageInterpolated.Text = "Interpolated";
            this.tabPageInterpolated.UseVisualStyleBackColor = true;
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(190, 17);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(75, 23);
            this.button2.TabIndex = 1;
            this.button2.Text = "Move";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // numericUpDown1
            // 
            this.numericUpDown1.Location = new System.Drawing.Point(51, 17);
            this.numericUpDown1.Name = "numericUpDown1";
            this.numericUpDown1.Size = new System.Drawing.Size(120, 20);
            this.numericUpDown1.TabIndex = 0;
            // 
            // tabPageHand
            // 
            this.tabPageHand.Controls.Add(this.labelRec);
            this.tabPageHand.Controls.Add(this.labelHandControl);
            this.tabPageHand.Controls.Add(this.checkBox4);
            this.tabPageHand.Controls.Add(this.Control);
            this.tabPageHand.Controls.Add(this.checkBoxPumpe);
            this.tabPageHand.Controls.Add(this.checkBoxVentil);
            this.tabPageHand.Location = new System.Drawing.Point(4, 22);
            this.tabPageHand.Name = "tabPageHand";
            this.tabPageHand.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageHand.Size = new System.Drawing.Size(315, 101);
            this.tabPageHand.TabIndex = 4;
            this.tabPageHand.Text = "Hand";
            this.tabPageHand.UseVisualStyleBackColor = true;
            // 
            // labelRec
            // 
            this.labelRec.AutoSize = true;
            this.labelRec.Location = new System.Drawing.Point(155, 36);
            this.labelRec.Name = "labelRec";
            this.labelRec.Size = new System.Drawing.Size(30, 13);
            this.labelRec.TabIndex = 1;
            this.labelRec.Text = "0x00";
            // 
            // labelHandControl
            // 
            this.labelHandControl.AutoSize = true;
            this.labelHandControl.Location = new System.Drawing.Point(155, 9);
            this.labelHandControl.Name = "labelHandControl";
            this.labelHandControl.Size = new System.Drawing.Size(30, 13);
            this.labelHandControl.TabIndex = 1;
            this.labelHandControl.Text = "0x00";
            // 
            // checkBox4
            // 
            this.checkBox4.AutoSize = true;
            this.checkBox4.Location = new System.Drawing.Point(22, 79);
            this.checkBox4.Name = "checkBox4";
            this.checkBox4.Size = new System.Drawing.Size(58, 17);
            this.checkBox4.TabIndex = 0;
            this.checkBox4.Text = "remote";
            this.checkBox4.UseVisualStyleBackColor = true;
            this.checkBox4.CheckedChanged += new System.EventHandler(this.checkBox4_CheckedChanged);
            // 
            // Control
            // 
            this.Control.AutoSize = true;
            this.Control.Location = new System.Drawing.Point(23, 57);
            this.Control.Name = "Control";
            this.Control.Size = new System.Drawing.Size(57, 17);
            this.Control.TabIndex = 0;
            this.Control.Text = "Regler";
            this.Control.UseVisualStyleBackColor = true;
            this.Control.CheckedChanged += new System.EventHandler(this.Control_CheckedChanged);
            // 
            // checkBoxPumpe
            // 
            this.checkBoxPumpe.AutoSize = true;
            this.checkBoxPumpe.Location = new System.Drawing.Point(23, 32);
            this.checkBoxPumpe.Name = "checkBoxPumpe";
            this.checkBoxPumpe.Size = new System.Drawing.Size(59, 17);
            this.checkBoxPumpe.TabIndex = 0;
            this.checkBoxPumpe.Text = "Pumpe";
            this.checkBoxPumpe.UseVisualStyleBackColor = true;
            this.checkBoxPumpe.CheckedChanged += new System.EventHandler(this.checkBoxPumpe_CheckedChanged);
            // 
            // checkBoxVentil
            // 
            this.checkBoxVentil.AutoSize = true;
            this.checkBoxVentil.Location = new System.Drawing.Point(24, 9);
            this.checkBoxVentil.Name = "checkBoxVentil";
            this.checkBoxVentil.Size = new System.Drawing.Size(52, 17);
            this.checkBoxVentil.TabIndex = 0;
            this.checkBoxVentil.Text = "Ventil";
            this.checkBoxVentil.UseVisualStyleBackColor = true;
            this.checkBoxVentil.CheckedChanged += new System.EventHandler(this.checkBoxVentil_CheckedChanged);
            // 
            // labelError
            // 
            this.labelError.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.labelError.AutoSize = true;
            this.labelError.Location = new System.Drawing.Point(3, 261);
            this.labelError.Name = "labelError";
            this.labelError.Size = new System.Drawing.Size(16, 13);
            this.labelError.TabIndex = 13;
            this.labelError.Text = "...";
            // 
            // checkBoxDebug
            // 
            this.checkBoxDebug.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.checkBoxDebug.AutoSize = true;
            this.checkBoxDebug.Location = new System.Drawing.Point(522, 236);
            this.checkBoxDebug.Name = "checkBoxDebug";
            this.checkBoxDebug.Size = new System.Drawing.Size(58, 17);
            this.checkBoxDebug.TabIndex = 10;
            this.checkBoxDebug.Text = "Debug";
            this.checkBoxDebug.UseVisualStyleBackColor = true;
            this.checkBoxDebug.CheckedChanged += new System.EventHandler(this.checkBoxDebug_CheckedChanged);
            // 
            // CANOpenControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.labelError);
            this.Controls.Add(this.tabControlHand);
            this.Controls.Add(this.checkBoxAutoUpdate);
            this.Controls.Add(this.checkBoxDebug);
            this.Controls.Add(this.checkBoxLog);
            this.Controls.Add(this.buttonUpdate);
            this.Controls.Add(this.buttonStopCAN);
            this.Controls.Add(this.buttonStopMove);
            this.Controls.Add(this.listBox1);
            this.Controls.Add(this.buttonDriveInit);
            this.Controls.Add(this.buttonCANInit);
            this.Controls.Add(this.buttonDriveOff);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.label581);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.labelPosition);
            this.Controls.Add(this.labelAnalog);
            this.Controls.Add(this.label6041);
            this.Controls.Add(this.numericUpDownDrive);
            this.Controls.Add(this.checkBoxEnabled);
            this.Controls.Add(this.checkBoxPower);
            this.Controls.Add(this.checkBoxOut3);
            this.Controls.Add(this.checkBoxOut2);
            this.Controls.Add(this.checkBoxIn4);
            this.Controls.Add(this.checkBoxIn3);
            this.Controls.Add(this.checkBoxIn2);
            this.Controls.Add(this.checkBoxIn1);
            this.Controls.Add(this.checkBoxOut1);
            this.Controls.Add(this.checkBoxBit15);
            this.Controls.Add(this.checkBoxBit14);
            this.Controls.Add(this.checkBoxBit13);
            this.Controls.Add(this.checkBoxBit12);
            this.Controls.Add(this.checkBoxBit11);
            this.Controls.Add(this.checkBoxBit10);
            this.Controls.Add(this.checkBoxBit9);
            this.Controls.Add(this.checkBoxBit8);
            this.Controls.Add(this.checkBoxBit7);
            this.Controls.Add(this.checkBoxBit6);
            this.Controls.Add(this.checkBoxBit5);
            this.Controls.Add(this.checkBoxBit3);
            this.Controls.Add(this.checkBoxBit0);
            this.Controls.Add(this.checkBoxBit1);
            this.Name = "CANOpenControl";
            this.Size = new System.Drawing.Size(744, 367);
            this.Load += new System.EventHandler(this.CANOpenControl_Load);
            this.Click += new System.EventHandler(this.buttonSetControl_Click);
            this.Enter += new System.EventHandler(this.CANOpenControl_Enter);
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownPos1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownDrive)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownSpeed)).EndInit();
            this.tabControlHand.ResumeLayout(false);
            this.tabPageOperation.ResumeLayout(false);
            this.tabPageOperation.PerformLayout();
            this.tabPageProfile.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownAccel)).EndInit();
            this.tabPageHoming.ResumeLayout(false);
            this.tabPageHoming.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownHomeOffset)).EndInit();
            this.tabPageInterpolated.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown1)).EndInit();
            this.tabPageHand.ResumeLayout(false);
            this.tabPageHand.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.ComponentModel.BackgroundWorker backgroundWorkerMessages;
        private System.Windows.Forms.RadioButton radioButtonBit0;
        private System.Windows.Forms.NumericUpDown numericUpDownPos1;
        private System.Windows.Forms.Label label6041;
        private System.Windows.Forms.Button buttonMove;
        private System.Windows.Forms.ListBox listBox1;
        private System.Windows.Forms.Button buttonStopMove;
        private System.Windows.Forms.Label label581;
        private System.Windows.Forms.TextBox textBoxControl;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button buttonSetSDO;
        private System.Windows.Forms.TextBox textBoxSDOId;
        private System.Windows.Forms.TextBox textBoxSDOsub;
        private System.Windows.Forms.TextBox textBoxSDOValue;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.NumericUpDown numericUpDownDrive;
        private System.Windows.Forms.Button buttonCANInit;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button buttonSpeed;
        private System.Windows.Forms.CheckBox checkBoxEnabled;
        private System.Windows.Forms.CheckBox checkBoxPower;
        private System.Windows.Forms.CheckBox checkBoxBit1;
        private System.Windows.Forms.CheckBox checkBoxBit3;
        private System.Windows.Forms.CheckBox checkBoxBit5;
        private System.Windows.Forms.CheckBox checkBoxBit6;
        private System.Windows.Forms.CheckBox checkBoxBit7;
        private System.Windows.Forms.CheckBox checkBoxBit8;
        private System.Windows.Forms.CheckBox checkBoxBit9;
        private System.Windows.Forms.CheckBox checkBoxBit10;
        private System.Windows.Forms.CheckBox checkBoxBit11;
        private System.Windows.Forms.CheckBox checkBoxBit0;
        private System.Windows.Forms.CheckBox checkBoxBit12;
        private System.Windows.Forms.CheckBox checkBoxBit13;
        private System.Windows.Forms.CheckBox checkBoxBit14;
        private System.Windows.Forms.CheckBox checkBoxBit15;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.NumericUpDown numericUpDownSpeed;
        private System.Windows.Forms.CheckBox checkBoxLog;
        private System.Windows.Forms.Button buttonUpdate;
        private System.Windows.Forms.CheckBox checkBoxOut1;
        private System.Windows.Forms.CheckBox checkBoxOut2;
        private System.Windows.Forms.CheckBox checkBoxOut3;
        private System.Windows.Forms.CheckBox checkBoxIn1;
        private System.Windows.Forms.CheckBox checkBoxIn2;
        private System.Windows.Forms.CheckBox checkBoxIn3;
        private System.Windows.Forms.CheckBox checkBoxIn4;
        private System.Windows.Forms.CheckBox checkBoxAutoUpdate;
        private System.Windows.Forms.Timer timerAutoUpdate;
        private System.Windows.Forms.Button buttonDriveInit;
        private System.Windows.Forms.Button buttonStopCAN;
        private System.Windows.Forms.Label labelAnalog;
        private System.Windows.Forms.Button buttonDriveOff;
        private System.Windows.Forms.Label labelPosition;
        private System.Windows.Forms.Label labelcmd;
        private System.Windows.Forms.TabControl tabControlHand;
        private System.Windows.Forms.TabPage tabPageOperation;
        private System.Windows.Forms.TabPage tabPageProfile;
        private System.Windows.Forms.TabPage tabPageHoming;
        private System.Windows.Forms.ComboBox comboBox1;
        private System.Windows.Forms.NumericUpDown numericUpDownAccel;
        private System.Windows.Forms.Button buttonSetProfile;
        private System.Windows.Forms.NumericUpDown numericUpDownHomeOffset;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button buttonHoming;
        private System.Windows.Forms.ComboBox comboBox2;
        private System.Windows.Forms.TabPage tabPageInterpolated;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.NumericUpDown numericUpDown1;
        private System.Windows.Forms.Button buttonCalcHomeOffset;
        private System.Windows.Forms.Label labelAnalogOffset;
        private System.Windows.Forms.Button buttonSetOffset;
        private System.Windows.Forms.Label labelData;
        private System.Windows.Forms.Label labelError;
        private System.Windows.Forms.TabPage tabPageHand;
        private System.Windows.Forms.Label labelHandControl;
        private System.Windows.Forms.CheckBox checkBox4;
        private System.Windows.Forms.CheckBox Control;
        private System.Windows.Forms.CheckBox checkBoxPumpe;
        private System.Windows.Forms.CheckBox checkBoxVentil;
        private System.Windows.Forms.Label labelRec;
        private System.Windows.Forms.CheckBox checkBoxDebug;
    }
}
