
namespace DarcMonitor
{
    partial class FormDarc1000
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

        #region Vom Windows Form-Designer generierter Code

        /// <summary>
        /// Erforderliche Methode für die Designerunterstützung.
        /// Der Inhalt der Methode darf nicht mit dem Code-Editor geändert werden.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.serialPortTeensy = new System.IO.Ports.SerialPort(this.components);
            this.buttonOut = new System.Windows.Forms.Button();
            this.buttonIn = new System.Windows.Forms.Button();
            this.backgroundWorkerMonitor = new System.ComponentModel.BackgroundWorker();
            this.buttonReset = new System.Windows.Forms.Button();
            this.comboBoxCOM = new System.Windows.Forms.ComboBox();
            this.splitContainer = new System.Windows.Forms.SplitContainer();
            this.rtb_Monitor = new System.Windows.Forms.RichTextBox();
            this.buttonMoveTarget = new System.Windows.Forms.Button();
            this.tabControlAction = new System.Windows.Forms.TabControl();
            this.tabPageGraph = new System.Windows.Forms.TabPage();
            this.grahControl = new DarcMonitor.GrahControl();
            this.tabPageProgram = new System.Windows.Forms.TabPage();
            this.scriptControl1 = new DarcMonitor.ScriptControl();
            this.checkBoxAutoScroll = new System.Windows.Forms.CheckBox();
            this.CheckBoxRawText = new System.Windows.Forms.CheckBox();
            this.timerUpdate = new System.Windows.Forms.Timer(this.components);
            this.checkBoxJson = new System.Windows.Forms.CheckBox();
            this.backgroundWorkerPipe = new System.ComponentModel.BackgroundWorker();
            this.buttonPowerOn = new System.Windows.Forms.Button();
            this.buttonPowwerOff = new System.Windows.Forms.Button();
            this.checkBoxCAN = new System.Windows.Forms.CheckBox();
            this.textBoxCmd = new System.Windows.Forms.TextBox();
            this.buttonCmd = new System.Windows.Forms.Button();
            this.tabControlCmd = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.labelTargetindex = new System.Windows.Forms.Label();
            this.button6 = new System.Windows.Forms.Button();
            this.button5 = new System.Windows.Forms.Button();
            this.button11 = new System.Windows.Forms.Button();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.checkBoxHexSet = new System.Windows.Forms.CheckBox();
            this.serviceControl1 = new DarcMonitor.ServiceControl();
            this.tabPage5 = new System.Windows.Forms.TabPage();
            this.targetControl1 = new DarcMonitor.TargetControl();
            this.tabPage6 = new System.Windows.Forms.TabPage();
            this.canControl = new DarcMonitor.CANControl();
            this.tabPage4 = new System.Windows.Forms.TabPage();
            this.communicationControl1 = new DarcMonitor.CommunicationControl();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.labelVersion = new System.Windows.Forms.Label();
            this.button16 = new System.Windows.Forms.Button();
            this.button17 = new System.Windows.Forms.Button();
            this.button4 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.checkBoxPowerOn = new System.Windows.Forms.CheckBox();
            this.labelTarget = new System.Windows.Forms.Label();
            this.backgroundWorkerSocket = new System.ComponentModel.BackgroundWorker();
            this.checkBox1 = new System.Windows.Forms.CheckBox();
            this.buttonClear = new System.Windows.Forms.Button();
            this.labelAngle_w1 = new System.Windows.Forms.Label();
            this.checkBoxLog = new System.Windows.Forms.CheckBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.labelError = new System.Windows.Forms.Label();
            this.radioButtonSlow = new System.Windows.Forms.RadioButton();
            this.radioButtonFast = new System.Windows.Forms.RadioButton();
            this.backgroundWorkerFile = new System.ComponentModel.BackgroundWorker();
            this.labelAngle_w2 = new System.Windows.Forms.Label();
            this.checkBoxDegres = new System.Windows.Forms.CheckBox();
            this.buttonFile = new System.Windows.Forms.Button();
            this.openFileDialogLog = new System.Windows.Forms.OpenFileDialog();
            this.buttonConnect = new System.Windows.Forms.Button();
            this.label_magnet_w1 = new System.Windows.Forms.Label();
            this.label_magnet_w2 = new System.Windows.Forms.Label();
            this.saveFileDialogLog = new System.Windows.Forms.SaveFileDialog();
            this.checkBoxInOut = new System.Windows.Forms.CheckBox();
            this.checkBoxError = new System.Windows.Forms.CheckBox();
            this.tabPage7 = new System.Windows.Forms.TabPage();
            this.canOpenControl1 = new PeakCANOpen_Lib.CANOpenControl();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer)).BeginInit();
            this.splitContainer.Panel1.SuspendLayout();
            this.splitContainer.Panel2.SuspendLayout();
            this.splitContainer.SuspendLayout();
            this.tabControlAction.SuspendLayout();
            this.tabPageGraph.SuspendLayout();
            this.tabPageProgram.SuspendLayout();
            this.tabControlCmd.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.tabPage5.SuspendLayout();
            this.tabPage6.SuspendLayout();
            this.tabPage4.SuspendLayout();
            this.tabPage3.SuspendLayout();
            this.tabPage7.SuspendLayout();
            this.SuspendLayout();
            // 
            // serialPortTeensy
            // 
            this.serialPortTeensy.PortName = "COM4";
            // 
            // buttonOut
            // 
            this.buttonOut.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonOut.Location = new System.Drawing.Point(41, 17);
            this.buttonOut.Name = "buttonOut";
            this.buttonOut.Size = new System.Drawing.Size(67, 23);
            this.buttonOut.TabIndex = 0;
            this.buttonOut.Text = "go Out";
            this.buttonOut.UseVisualStyleBackColor = true;
            this.buttonOut.Click += new System.EventHandler(this.buttonCmd_Click);
            // 
            // buttonIn
            // 
            this.buttonIn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonIn.Location = new System.Drawing.Point(172, 17);
            this.buttonIn.Name = "buttonIn";
            this.buttonIn.Size = new System.Drawing.Size(66, 23);
            this.buttonIn.TabIndex = 0;
            this.buttonIn.Text = "go In";
            this.buttonIn.UseVisualStyleBackColor = true;
            this.buttonIn.Click += new System.EventHandler(this.buttonCmd_Click);
            // 
            // backgroundWorkerMonitor
            // 
            this.backgroundWorkerMonitor.WorkerSupportsCancellation = true;
            this.backgroundWorkerMonitor.DoWork += new System.ComponentModel.DoWorkEventHandler(this.backgroundWorkerMonitor_DoWork);
            // 
            // buttonReset
            // 
            this.buttonReset.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonReset.Location = new System.Drawing.Point(797, 519);
            this.buttonReset.Name = "buttonReset";
            this.buttonReset.Size = new System.Drawing.Size(45, 23);
            this.buttonReset.TabIndex = 0;
            this.buttonReset.Text = "Reset";
            this.buttonReset.UseVisualStyleBackColor = true;
            this.buttonReset.Click += new System.EventHandler(this.buttonReset_Click);
            // 
            // comboBoxCOM
            // 
            this.comboBoxCOM.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.comboBoxCOM.FormattingEnabled = true;
            this.comboBoxCOM.Items.AddRange(new object[] {
            "DARC1000Socket",
            "DARC1000Pipe"});
            this.comboBoxCOM.Location = new System.Drawing.Point(717, 548);
            this.comboBoxCOM.Name = "comboBoxCOM";
            this.comboBoxCOM.Size = new System.Drawing.Size(256, 21);
            this.comboBoxCOM.TabIndex = 2;
            this.comboBoxCOM.Text = "offline";
            this.comboBoxCOM.DropDown += new System.EventHandler(this.comboBoxCOM_DropDown);
            // 
            // splitContainer
            // 
            this.splitContainer.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.splitContainer.Location = new System.Drawing.Point(7, 3);
            this.splitContainer.Name = "splitContainer";
            this.splitContainer.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer.Panel1
            // 
            this.splitContainer.Panel1.Controls.Add(this.rtb_Monitor);
            this.splitContainer.Panel1.Controls.Add(this.buttonMoveTarget);
            // 
            // splitContainer.Panel2
            // 
            this.splitContainer.Panel2.Controls.Add(this.tabControlAction);
            this.splitContainer.Size = new System.Drawing.Size(700, 576);
            this.splitContainer.SplitterDistance = 232;
            this.splitContainer.TabIndex = 4;
            // 
            // rtb_Monitor
            // 
            this.rtb_Monitor.Dock = System.Windows.Forms.DockStyle.Fill;
            this.rtb_Monitor.Location = new System.Drawing.Point(0, 0);
            this.rtb_Monitor.Name = "rtb_Monitor";
            this.rtb_Monitor.Size = new System.Drawing.Size(700, 232);
            this.rtb_Monitor.TabIndex = 2;
            this.rtb_Monitor.Text = "";
            // 
            // buttonMoveTarget
            // 
            this.buttonMoveTarget.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonMoveTarget.Location = new System.Drawing.Point(592, 97);
            this.buttonMoveTarget.Name = "buttonMoveTarget";
            this.buttonMoveTarget.Size = new System.Drawing.Size(76, 23);
            this.buttonMoveTarget.TabIndex = 0;
            this.buttonMoveTarget.Text = "SetTarget";
            this.buttonMoveTarget.UseVisualStyleBackColor = true;
            this.buttonMoveTarget.Click += new System.EventHandler(this.buttonCmd_Click);
            // 
            // tabControlAction
            // 
            this.tabControlAction.Controls.Add(this.tabPageGraph);
            this.tabControlAction.Controls.Add(this.tabPageProgram);
            this.tabControlAction.Controls.Add(this.tabPage7);
            this.tabControlAction.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControlAction.Location = new System.Drawing.Point(0, 0);
            this.tabControlAction.Name = "tabControlAction";
            this.tabControlAction.SelectedIndex = 0;
            this.tabControlAction.Size = new System.Drawing.Size(700, 340);
            this.tabControlAction.TabIndex = 5;
            // 
            // tabPageGraph
            // 
            this.tabPageGraph.Controls.Add(this.grahControl);
            this.tabPageGraph.Location = new System.Drawing.Point(4, 22);
            this.tabPageGraph.Name = "tabPageGraph";
            this.tabPageGraph.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageGraph.Size = new System.Drawing.Size(692, 314);
            this.tabPageGraph.TabIndex = 2;
            this.tabPageGraph.Text = "Graph";
            this.tabPageGraph.UseVisualStyleBackColor = true;
            // 
            // grahControl
            // 
            this.grahControl.bEncoder = false;
            this.grahControl.bMagnet = false;
            this.grahControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grahControl.Location = new System.Drawing.Point(3, 3);
            this.grahControl.Name = "grahControl";
            this.grahControl.Size = new System.Drawing.Size(686, 308);
            this.grahControl.TabIndex = 0;
            // 
            // tabPageProgram
            // 
            this.tabPageProgram.Controls.Add(this.scriptControl1);
            this.tabPageProgram.Location = new System.Drawing.Point(4, 22);
            this.tabPageProgram.Name = "tabPageProgram";
            this.tabPageProgram.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageProgram.Size = new System.Drawing.Size(692, 314);
            this.tabPageProgram.TabIndex = 1;
            this.tabPageProgram.Text = "Script";
            this.tabPageProgram.UseVisualStyleBackColor = true;
            // 
            // scriptControl1
            // 
            this.scriptControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.scriptControl1.Location = new System.Drawing.Point(3, 3);
            this.scriptControl1.Name = "scriptControl1";
            this.scriptControl1.Size = new System.Drawing.Size(686, 308);
            this.scriptControl1.TabIndex = 0;
            this.scriptControl1.Load += new System.EventHandler(this.scriptControl1_Load);
            // 
            // checkBoxAutoScroll
            // 
            this.checkBoxAutoScroll.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.checkBoxAutoScroll.AutoSize = true;
            this.checkBoxAutoScroll.Checked = true;
            this.checkBoxAutoScroll.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBoxAutoScroll.Location = new System.Drawing.Point(717, 457);
            this.checkBoxAutoScroll.Name = "checkBoxAutoScroll";
            this.checkBoxAutoScroll.Size = new System.Drawing.Size(74, 17);
            this.checkBoxAutoScroll.TabIndex = 6;
            this.checkBoxAutoScroll.Text = "AutoScroll";
            this.checkBoxAutoScroll.UseVisualStyleBackColor = true;
            this.checkBoxAutoScroll.CheckedChanged += new System.EventHandler(this.checkBox_CheckedChanged);
            // 
            // CheckBoxRawText
            // 
            this.CheckBoxRawText.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.CheckBoxRawText.AutoSize = true;
            this.CheckBoxRawText.Checked = true;
            this.CheckBoxRawText.CheckState = System.Windows.Forms.CheckState.Checked;
            this.CheckBoxRawText.Location = new System.Drawing.Point(717, 404);
            this.CheckBoxRawText.Name = "CheckBoxRawText";
            this.CheckBoxRawText.Size = new System.Drawing.Size(69, 17);
            this.CheckBoxRawText.TabIndex = 6;
            this.CheckBoxRawText.Text = "RawText";
            this.CheckBoxRawText.UseVisualStyleBackColor = true;
            this.CheckBoxRawText.CheckedChanged += new System.EventHandler(this.checkBox_CheckedChanged);
            // 
            // timerUpdate
            // 
            this.timerUpdate.Interval = 300;
            this.timerUpdate.Tick += new System.EventHandler(this.timerUpdate_Tick);
            // 
            // checkBoxJson
            // 
            this.checkBoxJson.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.checkBoxJson.AutoSize = true;
            this.checkBoxJson.Checked = true;
            this.checkBoxJson.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBoxJson.Location = new System.Drawing.Point(716, 386);
            this.checkBoxJson.Name = "checkBoxJson";
            this.checkBoxJson.Size = new System.Drawing.Size(90, 17);
            this.checkBoxJson.TabIndex = 6;
            this.checkBoxJson.Text = "Send as Json";
            this.checkBoxJson.UseVisualStyleBackColor = true;
            this.checkBoxJson.CheckedChanged += new System.EventHandler(this.checkBox_CheckedChanged);
            // 
            // backgroundWorkerPipe
            // 
            this.backgroundWorkerPipe.WorkerSupportsCancellation = true;
            this.backgroundWorkerPipe.DoWork += new System.ComponentModel.DoWorkEventHandler(this.backgroundWorkerPipe_DoWork);
            // 
            // buttonPowerOn
            // 
            this.buttonPowerOn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonPowerOn.Location = new System.Drawing.Point(41, 132);
            this.buttonPowerOn.Name = "buttonPowerOn";
            this.buttonPowerOn.Size = new System.Drawing.Size(66, 23);
            this.buttonPowerOn.TabIndex = 0;
            this.buttonPowerOn.Text = "Power on";
            this.buttonPowerOn.UseVisualStyleBackColor = true;
            this.buttonPowerOn.Click += new System.EventHandler(this.buttonCmd_Click);
            // 
            // buttonPowwerOff
            // 
            this.buttonPowwerOff.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonPowwerOff.Location = new System.Drawing.Point(172, 132);
            this.buttonPowwerOff.Name = "buttonPowwerOff";
            this.buttonPowwerOff.Size = new System.Drawing.Size(66, 23);
            this.buttonPowwerOff.TabIndex = 0;
            this.buttonPowwerOff.Text = "Power off";
            this.buttonPowwerOff.UseVisualStyleBackColor = true;
            this.buttonPowwerOff.Click += new System.EventHandler(this.buttonCmd_Click);
            // 
            // checkBoxCAN
            // 
            this.checkBoxCAN.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.checkBoxCAN.AutoSize = true;
            this.checkBoxCAN.Location = new System.Drawing.Point(717, 422);
            this.checkBoxCAN.Name = "checkBoxCAN";
            this.checkBoxCAN.Size = new System.Drawing.Size(67, 17);
            this.checkBoxCAN.TabIndex = 6;
            this.checkBoxCAN.Text = "CANmsg";
            this.checkBoxCAN.UseVisualStyleBackColor = true;
            this.checkBoxCAN.CheckedChanged += new System.EventHandler(this.checkBox_CheckedChanged);
            // 
            // textBoxCmd
            // 
            this.textBoxCmd.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxCmd.Location = new System.Drawing.Point(713, 246);
            this.textBoxCmd.Name = "textBoxCmd";
            this.textBoxCmd.Size = new System.Drawing.Size(204, 20);
            this.textBoxCmd.TabIndex = 10;
            this.textBoxCmd.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.textBoxCmd_KeyPress);
            // 
            // buttonCmd
            // 
            this.buttonCmd.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonCmd.Location = new System.Drawing.Point(938, 245);
            this.buttonCmd.Name = "buttonCmd";
            this.buttonCmd.Size = new System.Drawing.Size(41, 23);
            this.buttonCmd.TabIndex = 0;
            this.buttonCmd.Text = "cmd";
            this.buttonCmd.UseVisualStyleBackColor = true;
            this.buttonCmd.Click += new System.EventHandler(this.buttonCmd_Click);
            // 
            // tabControlCmd
            // 
            this.tabControlCmd.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.tabControlCmd.Controls.Add(this.tabPage1);
            this.tabControlCmd.Controls.Add(this.tabPage2);
            this.tabControlCmd.Controls.Add(this.tabPage5);
            this.tabControlCmd.Controls.Add(this.tabPage6);
            this.tabControlCmd.Controls.Add(this.tabPage4);
            this.tabControlCmd.Controls.Add(this.tabPage3);
            this.tabControlCmd.Location = new System.Drawing.Point(709, 3);
            this.tabControlCmd.Name = "tabControlCmd";
            this.tabControlCmd.SelectedIndex = 0;
            this.tabControlCmd.Size = new System.Drawing.Size(274, 241);
            this.tabControlCmd.TabIndex = 11;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.labelTargetindex);
            this.tabPage1.Controls.Add(this.buttonOut);
            this.tabPage1.Controls.Add(this.buttonIn);
            this.tabPage1.Controls.Add(this.button6);
            this.tabPage1.Controls.Add(this.button5);
            this.tabPage1.Controls.Add(this.buttonPowerOn);
            this.tabPage1.Controls.Add(this.buttonPowwerOff);
            this.tabPage1.Controls.Add(this.button11);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(266, 215);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Active";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // labelTargetindex
            // 
            this.labelTargetindex.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.labelTargetindex.AutoSize = true;
            this.labelTargetindex.Location = new System.Drawing.Point(175, 62);
            this.labelTargetindex.Name = "labelTargetindex";
            this.labelTargetindex.Size = new System.Drawing.Size(38, 13);
            this.labelTargetindex.TabIndex = 13;
            this.labelTargetindex.Text = "Target";
            // 
            // button6
            // 
            this.button6.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.button6.Location = new System.Drawing.Point(41, 103);
            this.button6.Name = "button6";
            this.button6.Size = new System.Drawing.Size(66, 23);
            this.button6.TabIndex = 0;
            this.button6.Text = "Stop";
            this.button6.UseVisualStyleBackColor = true;
            this.button6.Click += new System.EventHandler(this.buttonCmd_Click);
            // 
            // button5
            // 
            this.button5.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.button5.Location = new System.Drawing.Point(172, 103);
            this.button5.Name = "button5";
            this.button5.Size = new System.Drawing.Size(66, 23);
            this.button5.TabIndex = 0;
            this.button5.Text = "Continue";
            this.button5.UseVisualStyleBackColor = true;
            this.button5.Click += new System.EventHandler(this.buttonCmd_Click);
            // 
            // button11
            // 
            this.button11.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.button11.Location = new System.Drawing.Point(41, 57);
            this.button11.Name = "button11";
            this.button11.Size = new System.Drawing.Size(76, 23);
            this.button11.TabIndex = 0;
            this.button11.Text = "MoveTarget";
            this.button11.UseVisualStyleBackColor = true;
            this.button11.Click += new System.EventHandler(this.buttonCmd_Click);
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.checkBoxHexSet);
            this.tabPage2.Controls.Add(this.serviceControl1);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(266, 215);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Service";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // checkBoxHexSet
            // 
            this.checkBoxHexSet.AutoSize = true;
            this.checkBoxHexSet.Location = new System.Drawing.Point(139, 9);
            this.checkBoxHexSet.Name = "checkBoxHexSet";
            this.checkBoxHexSet.Size = new System.Drawing.Size(43, 17);
            this.checkBoxHexSet.TabIndex = 21;
            this.checkBoxHexSet.Text = "hex";
            this.checkBoxHexSet.UseVisualStyleBackColor = true;
            // 
            // serviceControl1
            // 
            this.serviceControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.serviceControl1.Location = new System.Drawing.Point(3, 3);
            this.serviceControl1.Name = "serviceControl1";
            this.serviceControl1.Size = new System.Drawing.Size(260, 209);
            this.serviceControl1.TabIndex = 0;
            this.serviceControl1.Load += new System.EventHandler(this.serviceControl1_Load);
            // 
            // tabPage5
            // 
            this.tabPage5.Controls.Add(this.targetControl1);
            this.tabPage5.Location = new System.Drawing.Point(4, 22);
            this.tabPage5.Name = "tabPage5";
            this.tabPage5.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage5.Size = new System.Drawing.Size(266, 215);
            this.tabPage5.TabIndex = 4;
            this.tabPage5.Text = "Target";
            this.tabPage5.UseVisualStyleBackColor = true;
            // 
            // targetControl1
            // 
            this.targetControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.targetControl1.Location = new System.Drawing.Point(3, 3);
            this.targetControl1.Name = "targetControl1";
            this.targetControl1.Size = new System.Drawing.Size(260, 209);
            this.targetControl1.TabIndex = 0;
            this.targetControl1.Load += new System.EventHandler(this.targetControl1_Load);
            // 
            // tabPage6
            // 
            this.tabPage6.Controls.Add(this.canControl);
            this.tabPage6.Location = new System.Drawing.Point(4, 22);
            this.tabPage6.Name = "tabPage6";
            this.tabPage6.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage6.Size = new System.Drawing.Size(266, 215);
            this.tabPage6.TabIndex = 5;
            this.tabPage6.Text = "CAN";
            this.tabPage6.UseVisualStyleBackColor = true;
            // 
            // canControl
            // 
            this.canControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.canControl.Location = new System.Drawing.Point(3, 3);
            this.canControl.Name = "canControl";
            this.canControl.Size = new System.Drawing.Size(260, 209);
            this.canControl.TabIndex = 0;
            this.canControl.Load += new System.EventHandler(this.canControl1_Load);
            // 
            // tabPage4
            // 
            this.tabPage4.Controls.Add(this.communicationControl1);
            this.tabPage4.Location = new System.Drawing.Point(4, 22);
            this.tabPage4.Name = "tabPage4";
            this.tabPage4.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage4.Size = new System.Drawing.Size(266, 215);
            this.tabPage4.TabIndex = 3;
            this.tabPage4.Text = "Com";
            this.tabPage4.UseVisualStyleBackColor = true;
            // 
            // communicationControl1
            // 
            this.communicationControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.communicationControl1.Location = new System.Drawing.Point(3, 3);
            this.communicationControl1.Name = "communicationControl1";
            this.communicationControl1.Size = new System.Drawing.Size(260, 209);
            this.communicationControl1.TabIndex = 0;
            this.communicationControl1.Load += new System.EventHandler(this.communicationControl1_Load);
            // 
            // tabPage3
            // 
            this.tabPage3.Controls.Add(this.labelVersion);
            this.tabPage3.Controls.Add(this.button16);
            this.tabPage3.Controls.Add(this.button17);
            this.tabPage3.Controls.Add(this.button4);
            this.tabPage3.Controls.Add(this.button2);
            this.tabPage3.Location = new System.Drawing.Point(4, 22);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage3.Size = new System.Drawing.Size(266, 215);
            this.tabPage3.TabIndex = 2;
            this.tabPage3.Text = "Admin";
            this.tabPage3.UseVisualStyleBackColor = true;
            // 
            // labelVersion
            // 
            this.labelVersion.AutoSize = true;
            this.labelVersion.ForeColor = System.Drawing.SystemColors.AppWorkspace;
            this.labelVersion.Location = new System.Drawing.Point(27, 172);
            this.labelVersion.Name = "labelVersion";
            this.labelVersion.Size = new System.Drawing.Size(69, 13);
            this.labelVersion.TabIndex = 3;
            this.labelVersion.Text = "Version 0.9.9";
            // 
            // button16
            // 
            this.button16.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.button16.Location = new System.Drawing.Point(147, 16);
            this.button16.Name = "button16";
            this.button16.Size = new System.Drawing.Size(82, 23);
            this.button16.TabIndex = 2;
            this.button16.Text = "Reset System";
            this.button16.UseVisualStyleBackColor = true;
            this.button16.Click += new System.EventHandler(this.buttonCmd_Click);
            // 
            // button17
            // 
            this.button17.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.button17.Location = new System.Drawing.Point(147, 50);
            this.button17.Name = "button17";
            this.button17.Size = new System.Drawing.Size(82, 23);
            this.button17.TabIndex = 2;
            this.button17.Text = "Reset Magnet";
            this.button17.UseVisualStyleBackColor = true;
            this.button17.Click += new System.EventHandler(this.buttonCmd_Click);
            // 
            // button4
            // 
            this.button4.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.button4.Location = new System.Drawing.Point(42, 50);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(73, 23);
            this.button4.TabIndex = 2;
            this.button4.Text = "Reset Zero";
            this.button4.UseVisualStyleBackColor = true;
            this.button4.Click += new System.EventHandler(this.buttonCmd_Click);
            // 
            // button2
            // 
            this.button2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.button2.Location = new System.Drawing.Point(42, 16);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(73, 23);
            this.button2.TabIndex = 2;
            this.button2.Text = "Save EE";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.buttonCmd_Click);
            // 
            // checkBoxPowerOn
            // 
            this.checkBoxPowerOn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.checkBoxPowerOn.AutoCheck = false;
            this.checkBoxPowerOn.AutoSize = true;
            this.checkBoxPowerOn.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.checkBoxPowerOn.Location = new System.Drawing.Point(716, 272);
            this.checkBoxPowerOn.Name = "checkBoxPowerOn";
            this.checkBoxPowerOn.Size = new System.Drawing.Size(70, 17);
            this.checkBoxPowerOn.TabIndex = 12;
            this.checkBoxPowerOn.Text = "PowerOn";
            this.checkBoxPowerOn.ThreeState = true;
            this.checkBoxPowerOn.UseMnemonic = false;
            this.checkBoxPowerOn.UseVisualStyleBackColor = false;
            // 
            // labelTarget
            // 
            this.labelTarget.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.labelTarget.AutoSize = true;
            this.labelTarget.Location = new System.Drawing.Point(836, 328);
            this.labelTarget.Name = "labelTarget";
            this.labelTarget.Size = new System.Drawing.Size(38, 13);
            this.labelTarget.TabIndex = 13;
            this.labelTarget.Text = "Target";
            // 
            // backgroundWorkerSocket
            // 
            this.backgroundWorkerSocket.WorkerReportsProgress = true;
            this.backgroundWorkerSocket.WorkerSupportsCancellation = true;
            this.backgroundWorkerSocket.DoWork += new System.ComponentModel.DoWorkEventHandler(this.backgroundWorkerSocket_DoWork);
            // 
            // checkBox1
            // 
            this.checkBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.checkBox1.AutoSize = true;
            this.checkBox1.Checked = true;
            this.checkBox1.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBox1.Location = new System.Drawing.Point(717, 440);
            this.checkBox1.Name = "checkBox1";
            this.checkBox1.Size = new System.Drawing.Size(103, 17);
            this.checkBox1.TabIndex = 6;
            this.checkBox1.Text = "Interpolationmsg";
            this.checkBox1.UseVisualStyleBackColor = true;
            this.checkBox1.CheckedChanged += new System.EventHandler(this.checkBox_CheckedChanged);
            // 
            // buttonClear
            // 
            this.buttonClear.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonClear.Location = new System.Drawing.Point(716, 481);
            this.buttonClear.Name = "buttonClear";
            this.buttonClear.Size = new System.Drawing.Size(57, 23);
            this.buttonClear.TabIndex = 0;
            this.buttonClear.Text = "Clear";
            this.buttonClear.UseVisualStyleBackColor = true;
            this.buttonClear.Click += new System.EventHandler(this.buttonClear_Click);
            // 
            // labelAngle_w1
            // 
            this.labelAngle_w1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.labelAngle_w1.AutoSize = true;
            this.labelAngle_w1.Location = new System.Drawing.Point(794, 273);
            this.labelAngle_w1.Name = "labelAngle_w1";
            this.labelAngle_w1.Size = new System.Drawing.Size(21, 13);
            this.labelAngle_w1.TabIndex = 13;
            this.labelAngle_w1.Text = "w1";
            // 
            // checkBoxLog
            // 
            this.checkBoxLog.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.checkBoxLog.AutoSize = true;
            this.checkBoxLog.Location = new System.Drawing.Point(877, 487);
            this.checkBoxLog.Name = "checkBoxLog";
            this.checkBoxLog.Size = new System.Drawing.Size(67, 17);
            this.checkBoxLog.TabIndex = 6;
            this.checkBoxLog.Text = "LogData";
            this.checkBoxLog.UseVisualStyleBackColor = true;
            this.checkBoxLog.CheckedChanged += new System.EventHandler(this.checkBox_CheckedChanged);
            // 
            // label2
            // 
            this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(836, 346);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(50, 13);
            this.label2.TabIndex = 13;
            this.label2.Text = "Override:";
            // 
            // label3
            // 
            this.label3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(836, 405);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(31, 13);
            this.label3.TabIndex = 13;
            this.label3.Text = "TCP:";
            // 
            // label4
            // 
            this.label4.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(836, 423);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(58, 13);
            this.label4.TabIndex = 13;
            this.label4.Text = "OutTarget:";
            // 
            // label6
            // 
            this.label6.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(836, 365);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(77, 13);
            this.label6.TabIndex = 13;
            this.label6.Text = "Collision mode:";
            // 
            // label7
            // 
            this.label7.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(836, 384);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(97, 13);
            this.label7.TabIndex = 13;
            this.label7.Text = "Interpolation mode:";
            // 
            // labelError
            // 
            this.labelError.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.labelError.AutoSize = true;
            this.labelError.Location = new System.Drawing.Point(770, 309);
            this.labelError.Name = "labelError";
            this.labelError.Size = new System.Drawing.Size(16, 13);
            this.labelError.TabIndex = 13;
            this.labelError.Text = "...";
            // 
            // radioButtonSlow
            // 
            this.radioButtonSlow.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.radioButtonSlow.AutoSize = true;
            this.radioButtonSlow.Location = new System.Drawing.Point(877, 520);
            this.radioButtonSlow.Name = "radioButtonSlow";
            this.radioButtonSlow.Size = new System.Drawing.Size(46, 17);
            this.radioButtonSlow.TabIndex = 14;
            this.radioButtonSlow.Text = "slow";
            this.radioButtonSlow.UseVisualStyleBackColor = true;
            // 
            // radioButtonFast
            // 
            this.radioButtonFast.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.radioButtonFast.AutoSize = true;
            this.radioButtonFast.Checked = true;
            this.radioButtonFast.Location = new System.Drawing.Point(929, 520);
            this.radioButtonFast.Name = "radioButtonFast";
            this.radioButtonFast.Size = new System.Drawing.Size(42, 17);
            this.radioButtonFast.TabIndex = 14;
            this.radioButtonFast.TabStop = true;
            this.radioButtonFast.Text = "fast";
            this.radioButtonFast.UseVisualStyleBackColor = true;
            // 
            // backgroundWorkerFile
            // 
            this.backgroundWorkerFile.WorkerReportsProgress = true;
            this.backgroundWorkerFile.WorkerSupportsCancellation = true;
            this.backgroundWorkerFile.DoWork += new System.ComponentModel.DoWorkEventHandler(this.backgroundWorkerFile_DoWork);
            // 
            // labelAngle_w2
            // 
            this.labelAngle_w2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.labelAngle_w2.AutoSize = true;
            this.labelAngle_w2.Location = new System.Drawing.Point(874, 274);
            this.labelAngle_w2.Name = "labelAngle_w2";
            this.labelAngle_w2.Size = new System.Drawing.Size(21, 13);
            this.labelAngle_w2.TabIndex = 13;
            this.labelAngle_w2.Text = "w2";
            // 
            // checkBoxDegres
            // 
            this.checkBoxDegres.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.checkBoxDegres.AutoSize = true;
            this.checkBoxDegres.Checked = true;
            this.checkBoxDegres.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBoxDegres.Location = new System.Drawing.Point(716, 369);
            this.checkBoxDegres.Name = "checkBoxDegres";
            this.checkBoxDegres.Size = new System.Drawing.Size(66, 17);
            this.checkBoxDegres.TabIndex = 6;
            this.checkBoxDegres.Text = "Degrees";
            this.checkBoxDegres.UseVisualStyleBackColor = true;
            this.checkBoxDegres.CheckedChanged += new System.EventHandler(this.checkBox_CheckedChanged);
            // 
            // buttonFile
            // 
            this.buttonFile.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonFile.Location = new System.Drawing.Point(797, 481);
            this.buttonFile.Name = "buttonFile";
            this.buttonFile.Size = new System.Drawing.Size(45, 23);
            this.buttonFile.TabIndex = 0;
            this.buttonFile.Text = "File";
            this.buttonFile.UseVisualStyleBackColor = true;
            this.buttonFile.Click += new System.EventHandler(this.buttonFile_Click);
            // 
            // openFileDialogLog
            // 
            this.openFileDialogLog.FileName = "logFile.txt";
            this.openFileDialogLog.InitialDirectory = "%USERPROFILE%\\My Documents";
            // 
            // buttonConnect
            // 
            this.buttonConnect.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonConnect.Location = new System.Drawing.Point(717, 518);
            this.buttonConnect.Name = "buttonConnect";
            this.buttonConnect.Size = new System.Drawing.Size(56, 23);
            this.buttonConnect.TabIndex = 0;
            this.buttonConnect.Text = "Connect";
            this.buttonConnect.UseVisualStyleBackColor = true;
            this.buttonConnect.Click += new System.EventHandler(this.buttonReset_Click);
            // 
            // label_magnet_w1
            // 
            this.label_magnet_w1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label_magnet_w1.AutoSize = true;
            this.label_magnet_w1.Location = new System.Drawing.Point(794, 291);
            this.label_magnet_w1.Name = "label_magnet_w1";
            this.label_magnet_w1.Size = new System.Drawing.Size(21, 13);
            this.label_magnet_w1.TabIndex = 13;
            this.label_magnet_w1.Text = "w1";
            // 
            // label_magnet_w2
            // 
            this.label_magnet_w2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label_magnet_w2.AutoSize = true;
            this.label_magnet_w2.Location = new System.Drawing.Point(874, 290);
            this.label_magnet_w2.Name = "label_magnet_w2";
            this.label_magnet_w2.Size = new System.Drawing.Size(21, 13);
            this.label_magnet_w2.TabIndex = 13;
            this.label_magnet_w2.Text = "w2";
            // 
            // saveFileDialogLog
            // 
            this.saveFileDialogLog.DefaultExt = "txt";
            this.saveFileDialogLog.FileName = "logFile.txt";
            this.saveFileDialogLog.Filter = "LogFiles|*.txt|AlleDateien |*.*";
            this.saveFileDialogLog.InitialDirectory = "%USERPROFILE%/My Documents";
            // 
            // checkBoxInOut
            // 
            this.checkBoxInOut.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.checkBoxInOut.AutoCheck = false;
            this.checkBoxInOut.AutoSize = true;
            this.checkBoxInOut.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.checkBoxInOut.Checked = true;
            this.checkBoxInOut.CheckState = System.Windows.Forms.CheckState.Indeterminate;
            this.checkBoxInOut.Location = new System.Drawing.Point(716, 289);
            this.checkBoxInOut.Name = "checkBoxInOut";
            this.checkBoxInOut.Size = new System.Drawing.Size(70, 17);
            this.checkBoxInOut.TabIndex = 12;
            this.checkBoxInOut.Text = "in <-> out";
            this.checkBoxInOut.ThreeState = true;
            this.checkBoxInOut.UseMnemonic = false;
            this.checkBoxInOut.UseVisualStyleBackColor = false;
            // 
            // checkBoxError
            // 
            this.checkBoxError.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.checkBoxError.AutoCheck = false;
            this.checkBoxError.AutoSize = true;
            this.checkBoxError.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.checkBoxError.Checked = true;
            this.checkBoxError.CheckState = System.Windows.Forms.CheckState.Indeterminate;
            this.checkBoxError.Location = new System.Drawing.Point(716, 306);
            this.checkBoxError.Name = "checkBoxError";
            this.checkBoxError.Size = new System.Drawing.Size(48, 17);
            this.checkBoxError.TabIndex = 12;
            this.checkBoxError.Text = "Error";
            this.checkBoxError.ThreeState = true;
            this.checkBoxError.UseMnemonic = false;
            this.checkBoxError.UseVisualStyleBackColor = false;
            // 
            // tabPage7
            // 
            this.tabPage7.Controls.Add(this.canOpenControl1);
            this.tabPage7.Location = new System.Drawing.Point(4, 22);
            this.tabPage7.Name = "tabPage7";
            this.tabPage7.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage7.Size = new System.Drawing.Size(692, 314);
            this.tabPage7.TabIndex = 3;
            this.tabPage7.Text = "CANDrive";
            this.tabPage7.UseVisualStyleBackColor = true;
            // 
            // canOpenControl1
            // 
            this.canOpenControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.canOpenControl1.Location = new System.Drawing.Point(3, 3);
            this.canOpenControl1.Name = "canOpenControl1";
            this.canOpenControl1.Size = new System.Drawing.Size(686, 308);
            this.canOpenControl1.TabIndex = 0;
            // 
            // FormDarc1000
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(986, 583);
            this.Controls.Add(this.radioButtonFast);
            this.Controls.Add(this.radioButtonSlow);
            this.Controls.Add(this.label_magnet_w2);
            this.Controls.Add(this.labelAngle_w2);
            this.Controls.Add(this.label_magnet_w1);
            this.Controls.Add(this.labelAngle_w1);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.labelError);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.labelTarget);
            this.Controls.Add(this.checkBoxError);
            this.Controls.Add(this.checkBoxInOut);
            this.Controls.Add(this.checkBoxPowerOn);
            this.Controls.Add(this.tabControlCmd);
            this.Controls.Add(this.textBoxCmd);
            this.Controls.Add(this.checkBoxDegres);
            this.Controls.Add(this.checkBoxJson);
            this.Controls.Add(this.CheckBoxRawText);
            this.Controls.Add(this.checkBox1);
            this.Controls.Add(this.checkBoxLog);
            this.Controls.Add(this.checkBoxCAN);
            this.Controls.Add(this.checkBoxAutoScroll);
            this.Controls.Add(this.splitContainer);
            this.Controls.Add(this.comboBoxCOM);
            this.Controls.Add(this.buttonFile);
            this.Controls.Add(this.buttonClear);
            this.Controls.Add(this.buttonConnect);
            this.Controls.Add(this.buttonReset);
            this.Controls.Add(this.buttonCmd);
            this.Name = "FormDarc1000";
            this.Text = "DARC1000 Monitor";
            this.Load += new System.EventHandler(this.FormDarc1000_Load);
            this.splitContainer.Panel1.ResumeLayout(false);
            this.splitContainer.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer)).EndInit();
            this.splitContainer.ResumeLayout(false);
            this.tabControlAction.ResumeLayout(false);
            this.tabPageGraph.ResumeLayout(false);
            this.tabPageProgram.ResumeLayout(false);
            this.tabControlCmd.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            this.tabPage2.ResumeLayout(false);
            this.tabPage2.PerformLayout();
            this.tabPage5.ResumeLayout(false);
            this.tabPage6.ResumeLayout(false);
            this.tabPage4.ResumeLayout(false);
            this.tabPage3.ResumeLayout(false);
            this.tabPage3.PerformLayout();
            this.tabPage7.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Button buttonOut;
        private System.Windows.Forms.Button buttonIn;
        private System.ComponentModel.BackgroundWorker backgroundWorkerMonitor;
        public System.IO.Ports.SerialPort serialPortTeensy;
        private System.Windows.Forms.Button buttonReset;
        private System.Windows.Forms.ComboBox comboBoxCOM;
        private System.Windows.Forms.SplitContainer splitContainer;
        private System.Windows.Forms.RichTextBox rtb_Monitor;
        private System.Windows.Forms.CheckBox checkBoxAutoScroll;
        private System.Windows.Forms.CheckBox CheckBoxRawText;
        private System.Windows.Forms.Timer timerUpdate;
        private System.Windows.Forms.CheckBox checkBoxJson;
        private System.ComponentModel.BackgroundWorker backgroundWorkerPipe;
        private System.Windows.Forms.Button buttonPowerOn;
        private System.Windows.Forms.Button buttonPowwerOff;
        private System.Windows.Forms.CheckBox checkBoxCAN;
        private System.Windows.Forms.Button buttonMoveTarget;
        private System.Windows.Forms.TextBox textBoxCmd;
        private System.Windows.Forms.Button buttonCmd;
        private System.Windows.Forms.TabControl tabControlCmd;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.Button button5;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.TabPage tabPage3;
        private System.Windows.Forms.Button button4;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button6;
        private System.Windows.Forms.CheckBox checkBoxPowerOn;
        private System.Windows.Forms.Label labelTarget;
        private System.Windows.Forms.TabPage tabPage4;
        private System.Windows.Forms.Button button11;
        private System.Windows.Forms.TabPage tabPage5;
        private System.Windows.Forms.Label labelTargetindex;
        private System.ComponentModel.BackgroundWorker backgroundWorkerSocket;
        private System.Windows.Forms.CheckBox checkBox1;
        private System.Windows.Forms.Button buttonClear;
        private System.Windows.Forms.Label labelAngle_w1;
        private System.Windows.Forms.CheckBox checkBoxLog;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label labelError;
        private System.Windows.Forms.Button button16;
        private System.Windows.Forms.Button button17;
        private System.Windows.Forms.Label labelVersion;
        private System.Windows.Forms.RadioButton radioButtonSlow;
        private System.Windows.Forms.RadioButton radioButtonFast;
        private System.ComponentModel.BackgroundWorker backgroundWorkerFile;
        private System.Windows.Forms.Label labelAngle_w2;
        private System.Windows.Forms.CheckBox checkBoxDegres;
        private System.Windows.Forms.TabControl tabControlAction;
        private System.Windows.Forms.TabPage tabPageProgram;
        private ScriptControl scriptControl1;
        private System.Windows.Forms.Button buttonFile;
        private System.Windows.Forms.OpenFileDialog openFileDialogLog;
        private System.Windows.Forms.Button buttonConnect;
        private System.Windows.Forms.Label label_magnet_w1;
        private System.Windows.Forms.Label label_magnet_w2;
        private System.Windows.Forms.SaveFileDialog saveFileDialogLog;
        private System.Windows.Forms.CheckBox checkBoxInOut;
        private System.Windows.Forms.TabPage tabPageGraph;
        private GrahControl grahControl;
        private System.Windows.Forms.TabPage tabPage6;
        private System.Windows.Forms.CheckBox checkBoxError;
        protected internal ServiceControl serviceControl1;
        private TargetControl targetControl1;
        private CommunicationControl communicationControl1;
        private System.Windows.Forms.CheckBox checkBoxHexSet;
        private CANControl canControl;
        private System.Windows.Forms.TabPage tabPage7;
        private PeakCANOpen_Lib.CANOpenControl canOpenControl1;
    }
}

