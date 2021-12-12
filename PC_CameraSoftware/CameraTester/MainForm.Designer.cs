namespace CameraTester
{
    partial class MainForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.btnStartCam = new System.Windows.Forms.Button();
            this.gbxControl = new System.Windows.Forms.GroupBox();
            this.lbSpeed = new System.Windows.Forms.Label();
            this.cbxSpeed = new System.Windows.Forms.ComboBox();
            this.lbYValue = new System.Windows.Forms.Label();
            this.lbXValue = new System.Windows.Forms.Label();
            this.tbxYValue = new System.Windows.Forms.TextBox();
            this.tbxXValue = new System.Windows.Forms.TextBox();
            this.btnHome = new System.Windows.Forms.Button();
            this.btnHomeY = new System.Windows.Forms.Button();
            this.btnHomeX = new System.Windows.Forms.Button();
            this.btnYsub = new System.Windows.Forms.Button();
            this.btnXplus = new System.Windows.Forms.Button();
            this.btnYplus = new System.Windows.Forms.Button();
            this.btnXsub = new System.Windows.Forms.Button();
            this.gbxReview = new System.Windows.Forms.GroupBox();
            this.ibxCamReview = new Emgu.CV.UI.ImageBox();
            this.gbxCapture = new System.Windows.Forms.GroupBox();
            this.ibxCamCapture = new Emgu.CV.UI.ImageBox();
            this.gbxCOM = new System.Windows.Forms.GroupBox();
            this.btnOpenCOM = new System.Windows.Forms.Button();
            this.cbxPort = new System.Windows.Forms.ComboBox();
            this.gbxCamera = new System.Windows.Forms.GroupBox();
            this.cbxCameras = new System.Windows.Forms.ComboBox();
            this.gbxResult = new System.Windows.Forms.GroupBox();
            this.ibxCamResult = new Emgu.CV.UI.ImageBox();
            this.btnAbout = new System.Windows.Forms.Button();
            this.btnClose = new System.Windows.Forms.Button();
            this.btnStartMachine = new System.Windows.Forms.Button();
            this.btnInspection = new System.Windows.Forms.Button();
            this.btnOpenFolder = new System.Windows.Forms.Button();
            this.tbxSend = new System.Windows.Forms.TextBox();
            this.tbxReceive = new System.Windows.Forms.TextBox();
            this.btnSend = new System.Windows.Forms.Button();
            this.btnClear = new System.Windows.Forms.Button();
            this.gbxTester = new System.Windows.Forms.GroupBox();
            this.btnExcel = new System.Windows.Forms.Button();
            this.statusStrip = new System.Windows.Forms.StatusStrip();
            this.sttCom = new System.Windows.Forms.ToolStripStatusLabel();
            this.sttCamera = new System.Windows.Forms.ToolStripStatusLabel();
            this.btnManual = new System.Windows.Forms.Button();
            this.gbxOutputResult = new System.Windows.Forms.GroupBox();
            this.tbxOutput1 = new System.Windows.Forms.TextBox();
            this.tbxOutput2 = new System.Windows.Forms.TextBox();
            this.tbxOutput3 = new System.Windows.Forms.TextBox();
            this.btnManual2 = new System.Windows.Forms.Button();
            this.gbxControl.SuspendLayout();
            this.gbxReview.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ibxCamReview)).BeginInit();
            this.gbxCapture.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ibxCamCapture)).BeginInit();
            this.gbxCOM.SuspendLayout();
            this.gbxCamera.SuspendLayout();
            this.gbxResult.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ibxCamResult)).BeginInit();
            this.gbxTester.SuspendLayout();
            this.statusStrip.SuspendLayout();
            this.gbxOutputResult.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnStartCam
            // 
            this.btnStartCam.Location = new System.Drawing.Point(132, 15);
            this.btnStartCam.Name = "btnStartCam";
            this.btnStartCam.Size = new System.Drawing.Size(50, 25);
            this.btnStartCam.TabIndex = 9;
            this.btnStartCam.Text = "Start";
            this.btnStartCam.UseVisualStyleBackColor = true;
            this.btnStartCam.Click += new System.EventHandler(this.btnStartCam_Click);
            // 
            // gbxControl
            // 
            this.gbxControl.Controls.Add(this.lbSpeed);
            this.gbxControl.Controls.Add(this.cbxSpeed);
            this.gbxControl.Controls.Add(this.lbYValue);
            this.gbxControl.Controls.Add(this.lbXValue);
            this.gbxControl.Controls.Add(this.tbxYValue);
            this.gbxControl.Controls.Add(this.tbxXValue);
            this.gbxControl.Controls.Add(this.btnHome);
            this.gbxControl.Controls.Add(this.btnHomeY);
            this.gbxControl.Controls.Add(this.btnHomeX);
            this.gbxControl.Controls.Add(this.btnYsub);
            this.gbxControl.Controls.Add(this.btnXplus);
            this.gbxControl.Controls.Add(this.btnYplus);
            this.gbxControl.Controls.Add(this.btnXsub);
            this.gbxControl.Location = new System.Drawing.Point(223, 12);
            this.gbxControl.Name = "gbxControl";
            this.gbxControl.Size = new System.Drawing.Size(276, 106);
            this.gbxControl.TabIndex = 22;
            this.gbxControl.TabStop = false;
            this.gbxControl.Text = "Control  Panel";
            // 
            // lbSpeed
            // 
            this.lbSpeed.AutoSize = true;
            this.lbSpeed.Font = new System.Drawing.Font("SVN-Aptima", 10F, System.Drawing.FontStyle.Bold);
            this.lbSpeed.Location = new System.Drawing.Point(6, 17);
            this.lbSpeed.Name = "lbSpeed";
            this.lbSpeed.Size = new System.Drawing.Size(89, 20);
            this.lbSpeed.TabIndex = 35;
            this.lbSpeed.Text = "Select Speed";
            // 
            // cbxSpeed
            // 
            this.cbxSpeed.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.cbxSpeed.FormattingEnabled = true;
            this.cbxSpeed.Location = new System.Drawing.Point(101, 15);
            this.cbxSpeed.Name = "cbxSpeed";
            this.cbxSpeed.Size = new System.Drawing.Size(79, 24);
            this.cbxSpeed.TabIndex = 34;
            // 
            // lbYValue
            // 
            this.lbYValue.AutoSize = true;
            this.lbYValue.Font = new System.Drawing.Font("SVN-Aptima", 10F, System.Drawing.FontStyle.Bold);
            this.lbYValue.Location = new System.Drawing.Point(7, 71);
            this.lbYValue.Name = "lbYValue";
            this.lbYValue.Size = new System.Drawing.Size(57, 20);
            this.lbYValue.TabIndex = 33;
            this.lbYValue.Text = "Y Value";
            // 
            // lbXValue
            // 
            this.lbXValue.AutoSize = true;
            this.lbXValue.Font = new System.Drawing.Font("SVN-Aptima", 10F, System.Drawing.FontStyle.Bold);
            this.lbXValue.Location = new System.Drawing.Point(7, 44);
            this.lbXValue.Name = "lbXValue";
            this.lbXValue.Size = new System.Drawing.Size(57, 20);
            this.lbXValue.TabIndex = 26;
            this.lbXValue.Text = "X Value";
            // 
            // tbxYValue
            // 
            this.tbxYValue.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.tbxYValue.Location = new System.Drawing.Point(70, 72);
            this.tbxYValue.Name = "tbxYValue";
            this.tbxYValue.Size = new System.Drawing.Size(50, 23);
            this.tbxYValue.TabIndex = 32;
            // 
            // tbxXValue
            // 
            this.tbxXValue.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.tbxXValue.Location = new System.Drawing.Point(70, 44);
            this.tbxXValue.Name = "tbxXValue";
            this.tbxXValue.Size = new System.Drawing.Size(50, 23);
            this.tbxXValue.TabIndex = 26;
            // 
            // btnHome
            // 
            this.btnHome.Location = new System.Drawing.Point(218, 14);
            this.btnHome.Name = "btnHome";
            this.btnHome.Size = new System.Drawing.Size(50, 25);
            this.btnHome.TabIndex = 31;
            this.btnHome.Text = "Home";
            this.btnHome.UseVisualStyleBackColor = true;
            this.btnHome.Click += new System.EventHandler(this.btnHome_Click);
            // 
            // btnHomeY
            // 
            this.btnHomeY.Location = new System.Drawing.Point(218, 71);
            this.btnHomeY.Name = "btnHomeY";
            this.btnHomeY.Size = new System.Drawing.Size(50, 25);
            this.btnHomeY.TabIndex = 29;
            this.btnHomeY.Text = "HomeY";
            this.btnHomeY.UseVisualStyleBackColor = true;
            this.btnHomeY.Click += new System.EventHandler(this.btnHomeY_Click);
            // 
            // btnHomeX
            // 
            this.btnHomeX.Location = new System.Drawing.Point(218, 43);
            this.btnHomeX.Name = "btnHomeX";
            this.btnHomeX.Size = new System.Drawing.Size(50, 25);
            this.btnHomeX.TabIndex = 28;
            this.btnHomeX.Text = "HomeX";
            this.btnHomeX.UseVisualStyleBackColor = true;
            this.btnHomeX.Click += new System.EventHandler(this.btnHomeX_Click);
            // 
            // btnYsub
            // 
            this.btnYsub.Location = new System.Drawing.Point(126, 71);
            this.btnYsub.Name = "btnYsub";
            this.btnYsub.Size = new System.Drawing.Size(40, 25);
            this.btnYsub.TabIndex = 25;
            this.btnYsub.Text = "Y-";
            this.btnYsub.UseVisualStyleBackColor = true;
            this.btnYsub.Click += new System.EventHandler(this.btnYsub_Click);
            // 
            // btnXplus
            // 
            this.btnXplus.Location = new System.Drawing.Point(172, 42);
            this.btnXplus.Name = "btnXplus";
            this.btnXplus.Size = new System.Drawing.Size(40, 25);
            this.btnXplus.TabIndex = 24;
            this.btnXplus.Text = "X+";
            this.btnXplus.UseVisualStyleBackColor = true;
            this.btnXplus.Click += new System.EventHandler(this.btnXplus_Click);
            // 
            // btnYplus
            // 
            this.btnYplus.Location = new System.Drawing.Point(172, 71);
            this.btnYplus.Name = "btnYplus";
            this.btnYplus.Size = new System.Drawing.Size(40, 25);
            this.btnYplus.TabIndex = 23;
            this.btnYplus.Text = "Y+";
            this.btnYplus.UseVisualStyleBackColor = true;
            this.btnYplus.Click += new System.EventHandler(this.btnYplus_Click);
            // 
            // btnXsub
            // 
            this.btnXsub.Location = new System.Drawing.Point(126, 42);
            this.btnXsub.Name = "btnXsub";
            this.btnXsub.Size = new System.Drawing.Size(40, 25);
            this.btnXsub.TabIndex = 22;
            this.btnXsub.Text = "X-";
            this.btnXsub.UseVisualStyleBackColor = true;
            this.btnXsub.Click += new System.EventHandler(this.btnXsub_Click);
            // 
            // gbxReview
            // 
            this.gbxReview.Controls.Add(this.ibxCamReview);
            this.gbxReview.Location = new System.Drawing.Point(118, 126);
            this.gbxReview.Name = "gbxReview";
            this.gbxReview.Size = new System.Drawing.Size(260, 270);
            this.gbxReview.TabIndex = 23;
            this.gbxReview.TabStop = false;
            this.gbxReview.Text = "Camera Review";
            // 
            // ibxCamReview
            // 
            this.ibxCamReview.Location = new System.Drawing.Point(4, 14);
            this.ibxCamReview.Name = "ibxCamReview";
            this.ibxCamReview.Size = new System.Drawing.Size(250, 250);
            this.ibxCamReview.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.ibxCamReview.TabIndex = 3;
            this.ibxCamReview.TabStop = false;
            // 
            // gbxCapture
            // 
            this.gbxCapture.Controls.Add(this.ibxCamCapture);
            this.gbxCapture.Location = new System.Drawing.Point(384, 126);
            this.gbxCapture.Name = "gbxCapture";
            this.gbxCapture.Size = new System.Drawing.Size(260, 270);
            this.gbxCapture.TabIndex = 25;
            this.gbxCapture.TabStop = false;
            this.gbxCapture.Text = "Capture";
            // 
            // ibxCamCapture
            // 
            this.ibxCamCapture.Location = new System.Drawing.Point(6, 14);
            this.ibxCamCapture.Name = "ibxCamCapture";
            this.ibxCamCapture.Size = new System.Drawing.Size(250, 250);
            this.ibxCamCapture.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.ibxCamCapture.TabIndex = 13;
            this.ibxCamCapture.TabStop = false;
            // 
            // gbxCOM
            // 
            this.gbxCOM.Controls.Add(this.btnOpenCOM);
            this.gbxCOM.Controls.Add(this.cbxPort);
            this.gbxCOM.Location = new System.Drawing.Point(12, 68);
            this.gbxCOM.Name = "gbxCOM";
            this.gbxCOM.Size = new System.Drawing.Size(185, 50);
            this.gbxCOM.TabIndex = 28;
            this.gbxCOM.TabStop = false;
            this.gbxCOM.Text = "Select COM Port";
            // 
            // btnOpenCOM
            // 
            this.btnOpenCOM.Location = new System.Drawing.Point(132, 16);
            this.btnOpenCOM.Name = "btnOpenCOM";
            this.btnOpenCOM.Size = new System.Drawing.Size(50, 25);
            this.btnOpenCOM.TabIndex = 13;
            this.btnOpenCOM.Text = "Open";
            this.btnOpenCOM.UseVisualStyleBackColor = true;
            this.btnOpenCOM.Click += new System.EventHandler(this.btnOpenCOM_Click);
            // 
            // cbxPort
            // 
            this.cbxPort.FormattingEnabled = true;
            this.cbxPort.Location = new System.Drawing.Point(6, 19);
            this.cbxPort.Name = "cbxPort";
            this.cbxPort.Size = new System.Drawing.Size(120, 21);
            this.cbxPort.TabIndex = 12;
            // 
            // gbxCamera
            // 
            this.gbxCamera.Controls.Add(this.cbxCameras);
            this.gbxCamera.Controls.Add(this.btnStartCam);
            this.gbxCamera.Location = new System.Drawing.Point(12, 12);
            this.gbxCamera.Name = "gbxCamera";
            this.gbxCamera.Size = new System.Drawing.Size(185, 50);
            this.gbxCamera.TabIndex = 29;
            this.gbxCamera.TabStop = false;
            this.gbxCamera.Text = "Select Camera";
            // 
            // cbxCameras
            // 
            this.cbxCameras.FormattingEnabled = true;
            this.cbxCameras.Location = new System.Drawing.Point(6, 18);
            this.cbxCameras.Name = "cbxCameras";
            this.cbxCameras.Size = new System.Drawing.Size(120, 21);
            this.cbxCameras.TabIndex = 11;
            // 
            // gbxResult
            // 
            this.gbxResult.Controls.Add(this.ibxCamResult);
            this.gbxResult.Location = new System.Drawing.Point(650, 126);
            this.gbxResult.Name = "gbxResult";
            this.gbxResult.Size = new System.Drawing.Size(260, 270);
            this.gbxResult.TabIndex = 30;
            this.gbxResult.TabStop = false;
            this.gbxResult.Text = "Result";
            // 
            // ibxCamResult
            // 
            this.ibxCamResult.Location = new System.Drawing.Point(6, 14);
            this.ibxCamResult.Name = "ibxCamResult";
            this.ibxCamResult.Size = new System.Drawing.Size(250, 250);
            this.ibxCamResult.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.ibxCamResult.TabIndex = 13;
            this.ibxCamResult.TabStop = false;
            // 
            // btnAbout
            // 
            this.btnAbout.Location = new System.Drawing.Point(12, 286);
            this.btnAbout.Name = "btnAbout";
            this.btnAbout.Size = new System.Drawing.Size(100, 30);
            this.btnAbout.TabIndex = 31;
            this.btnAbout.Text = "About";
            this.btnAbout.UseVisualStyleBackColor = true;
            this.btnAbout.Click += new System.EventHandler(this.btnAbout_Click);
            // 
            // btnClose
            // 
            this.btnClose.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.btnClose.Location = new System.Drawing.Point(12, 322);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(100, 30);
            this.btnClose.TabIndex = 32;
            this.btnClose.Text = "Close";
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // btnStartMachine
            // 
            this.btnStartMachine.Location = new System.Drawing.Point(12, 138);
            this.btnStartMachine.Name = "btnStartMachine";
            this.btnStartMachine.Size = new System.Drawing.Size(100, 50);
            this.btnStartMachine.TabIndex = 49;
            this.btnStartMachine.Text = "START MACHINE";
            this.btnStartMachine.UseVisualStyleBackColor = true;
            this.btnStartMachine.Click += new System.EventHandler(this.btnStartMachine_Click);
            // 
            // btnInspection
            // 
            this.btnInspection.Location = new System.Drawing.Point(12, 194);
            this.btnInspection.Name = "btnInspection";
            this.btnInspection.Size = new System.Drawing.Size(100, 50);
            this.btnInspection.TabIndex = 51;
            this.btnInspection.Text = "START INSPECTION";
            this.btnInspection.UseVisualStyleBackColor = true;
            this.btnInspection.Click += new System.EventHandler(this.btnInspection_Click);
            // 
            // btnOpenFolder
            // 
            this.btnOpenFolder.Location = new System.Drawing.Point(12, 250);
            this.btnOpenFolder.Name = "btnOpenFolder";
            this.btnOpenFolder.Size = new System.Drawing.Size(100, 30);
            this.btnOpenFolder.TabIndex = 52;
            this.btnOpenFolder.Text = "Open Folder";
            this.btnOpenFolder.UseVisualStyleBackColor = true;
            this.btnOpenFolder.Click += new System.EventHandler(this.btnOpenFolder_Click);
            // 
            // tbxSend
            // 
            this.tbxSend.Location = new System.Drawing.Point(7, 20);
            this.tbxSend.Name = "tbxSend";
            this.tbxSend.Size = new System.Drawing.Size(50, 20);
            this.tbxSend.TabIndex = 0;
            // 
            // tbxReceive
            // 
            this.tbxReceive.Location = new System.Drawing.Point(7, 47);
            this.tbxReceive.Multiline = true;
            this.tbxReceive.Name = "tbxReceive";
            this.tbxReceive.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.tbxReceive.Size = new System.Drawing.Size(150, 48);
            this.tbxReceive.TabIndex = 1;
            // 
            // btnSend
            // 
            this.btnSend.Location = new System.Drawing.Point(63, 18);
            this.btnSend.Name = "btnSend";
            this.btnSend.Size = new System.Drawing.Size(40, 25);
            this.btnSend.TabIndex = 29;
            this.btnSend.Text = "Send";
            this.btnSend.UseVisualStyleBackColor = true;
            this.btnSend.Click += new System.EventHandler(this.btnSend_Click);
            // 
            // btnClear
            // 
            this.btnClear.Location = new System.Drawing.Point(109, 18);
            this.btnClear.Name = "btnClear";
            this.btnClear.Size = new System.Drawing.Size(40, 25);
            this.btnClear.TabIndex = 30;
            this.btnClear.Text = "Clear";
            this.btnClear.UseVisualStyleBackColor = true;
            this.btnClear.Click += new System.EventHandler(this.btnClear_Click);
            // 
            // gbxTester
            // 
            this.gbxTester.Controls.Add(this.btnClear);
            this.gbxTester.Controls.Add(this.btnSend);
            this.gbxTester.Controls.Add(this.tbxReceive);
            this.gbxTester.Controls.Add(this.tbxSend);
            this.gbxTester.Location = new System.Drawing.Point(505, 12);
            this.gbxTester.Name = "gbxTester";
            this.gbxTester.Size = new System.Drawing.Size(163, 106);
            this.gbxTester.TabIndex = 26;
            this.gbxTester.TabStop = false;
            this.gbxTester.Text = "Tester Panel";
            // 
            // btnExcel
            // 
            this.btnExcel.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.btnExcel.Location = new System.Drawing.Point(12, 358);
            this.btnExcel.Name = "btnExcel";
            this.btnExcel.Size = new System.Drawing.Size(100, 30);
            this.btnExcel.TabIndex = 53;
            this.btnExcel.Text = "Load Data";
            this.btnExcel.UseVisualStyleBackColor = true;
            this.btnExcel.Click += new System.EventHandler(this.btnExcel_Click);
            // 
            // statusStrip
            // 
            this.statusStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.sttCom,
            this.sttCamera});
            this.statusStrip.Location = new System.Drawing.Point(0, 435);
            this.statusStrip.Name = "statusStrip";
            this.statusStrip.Size = new System.Drawing.Size(921, 22);
            this.statusStrip.TabIndex = 54;
            this.statusStrip.Text = "sttStrip";
            // 
            // sttCom
            // 
            this.sttCom.Name = "sttCom";
            this.sttCom.Size = new System.Drawing.Size(130, 17);
            this.sttCom.Text = "COM Port is closed  ++";
            // 
            // sttCamera
            // 
            this.sttCamera.Name = "sttCamera";
            this.sttCamera.Size = new System.Drawing.Size(72, 17);
            this.sttCamera.Text = "Camera OFF";
            // 
            // btnManual
            // 
            this.btnManual.Location = new System.Drawing.Point(12, 394);
            this.btnManual.Name = "btnManual";
            this.btnManual.Size = new System.Drawing.Size(99, 32);
            this.btnManual.TabIndex = 55;
            this.btnManual.Text = "Manual";
            this.btnManual.UseVisualStyleBackColor = true;
            this.btnManual.Click += new System.EventHandler(this.btnManual_Click);
            // 
            // gbxOutputResult
            // 
            this.gbxOutputResult.Controls.Add(this.tbxOutput1);
            this.gbxOutputResult.Controls.Add(this.tbxOutput2);
            this.gbxOutputResult.Controls.Add(this.tbxOutput3);
            this.gbxOutputResult.Location = new System.Drawing.Point(674, 12);
            this.gbxOutputResult.Name = "gbxOutputResult";
            this.gbxOutputResult.Size = new System.Drawing.Size(174, 106);
            this.gbxOutputResult.TabIndex = 56;
            this.gbxOutputResult.TabStop = false;
            this.gbxOutputResult.Text = "Result";
            // 
            // tbxOutput1
            // 
            this.tbxOutput1.Location = new System.Drawing.Point(6, 19);
            this.tbxOutput1.Name = "tbxOutput1";
            this.tbxOutput1.Size = new System.Drawing.Size(157, 20);
            this.tbxOutput1.TabIndex = 49;
            // 
            // tbxOutput2
            // 
            this.tbxOutput2.Location = new System.Drawing.Point(6, 45);
            this.tbxOutput2.Name = "tbxOutput2";
            this.tbxOutput2.Size = new System.Drawing.Size(157, 20);
            this.tbxOutput2.TabIndex = 50;
            // 
            // tbxOutput3
            // 
            this.tbxOutput3.Location = new System.Drawing.Point(6, 71);
            this.tbxOutput3.Name = "tbxOutput3";
            this.tbxOutput3.Size = new System.Drawing.Size(157, 20);
            this.tbxOutput3.TabIndex = 51;
            // 
            // btnManual2
            // 
            this.btnManual2.Location = new System.Drawing.Point(118, 394);
            this.btnManual2.Name = "btnManual2";
            this.btnManual2.Size = new System.Drawing.Size(99, 32);
            this.btnManual2.TabIndex = 57;
            this.btnManual2.Text = "Manual2";
            this.btnManual2.UseVisualStyleBackColor = true;
            this.btnManual2.Click += new System.EventHandler(this.btnManual2_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(921, 457);
            this.Controls.Add(this.btnManual2);
            this.Controls.Add(this.gbxOutputResult);
            this.Controls.Add(this.btnManual);
            this.Controls.Add(this.statusStrip);
            this.Controls.Add(this.btnExcel);
            this.Controls.Add(this.btnOpenFolder);
            this.Controls.Add(this.gbxTester);
            this.Controls.Add(this.btnInspection);
            this.Controls.Add(this.btnStartMachine);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.btnAbout);
            this.Controls.Add(this.gbxResult);
            this.Controls.Add(this.gbxCamera);
            this.Controls.Add(this.gbxCOM);
            this.Controls.Add(this.gbxCapture);
            this.Controls.Add(this.gbxReview);
            this.Controls.Add(this.gbxControl);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "MainForm";
            this.Text = "Controller";
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.gbxControl.ResumeLayout(false);
            this.gbxControl.PerformLayout();
            this.gbxReview.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.ibxCamReview)).EndInit();
            this.gbxCapture.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.ibxCamCapture)).EndInit();
            this.gbxCOM.ResumeLayout(false);
            this.gbxCamera.ResumeLayout(false);
            this.gbxResult.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.ibxCamResult)).EndInit();
            this.gbxTester.ResumeLayout(false);
            this.gbxTester.PerformLayout();
            this.statusStrip.ResumeLayout(false);
            this.statusStrip.PerformLayout();
            this.gbxOutputResult.ResumeLayout(false);
            this.gbxOutputResult.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnStartCam;
        private System.Windows.Forms.GroupBox gbxControl;
        private System.Windows.Forms.Button btnHome;
        private System.Windows.Forms.Button btnHomeY;
        private System.Windows.Forms.Button btnHomeX;
        private System.Windows.Forms.Button btnYsub;
        private System.Windows.Forms.Button btnXplus;
        private System.Windows.Forms.Button btnYplus;
        private System.Windows.Forms.Button btnXsub;
        private System.Windows.Forms.GroupBox gbxReview;
        private Emgu.CV.UI.ImageBox ibxCamReview;
        private System.Windows.Forms.GroupBox gbxCapture;
        private Emgu.CV.UI.ImageBox ibxCamCapture;
        private System.Windows.Forms.Label lbYValue;
        private System.Windows.Forms.Label lbXValue;
        private System.Windows.Forms.TextBox tbxYValue;
        private System.Windows.Forms.TextBox tbxXValue;
        private System.Windows.Forms.Label lbSpeed;
        private System.Windows.Forms.ComboBox cbxSpeed;
        private System.Windows.Forms.GroupBox gbxCOM;
        private System.Windows.Forms.GroupBox gbxCamera;
        private System.Windows.Forms.Button btnOpenCOM;
        private System.Windows.Forms.ComboBox cbxPort;
        private System.Windows.Forms.ComboBox cbxCameras;
        private System.Windows.Forms.GroupBox gbxResult;
        private Emgu.CV.UI.ImageBox ibxCamResult;
        private System.Windows.Forms.Button btnAbout;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.Button btnStartMachine;
        private System.Windows.Forms.Button btnInspection;
        private System.Windows.Forms.Button btnOpenFolder;
        private System.Windows.Forms.TextBox tbxSend;
        private System.Windows.Forms.TextBox tbxReceive;
        private System.Windows.Forms.Button btnSend;
        private System.Windows.Forms.Button btnClear;
        private System.Windows.Forms.GroupBox gbxTester;
        private System.Windows.Forms.Button btnExcel;
        private System.Windows.Forms.StatusStrip statusStrip;
        private System.Windows.Forms.ToolStripStatusLabel sttCom;
        private System.Windows.Forms.ToolStripStatusLabel sttCamera;
        private System.Windows.Forms.Button btnManual;
        private System.Windows.Forms.GroupBox gbxOutputResult;
        private System.Windows.Forms.TextBox tbxOutput1;
        private System.Windows.Forms.TextBox tbxOutput2;
        private System.Windows.Forms.TextBox tbxOutput3;
        private System.Windows.Forms.Button btnManual2;
    }
}

