namespace PCA_Application
{
    partial class Form1
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.btnLoadTraning = new System.Windows.Forms.Button();
            this.btnTraining = new System.Windows.Forms.Button();
            this.gbxCamera = new System.Windows.Forms.GroupBox();
            this.cbxCameras = new System.Windows.Forms.ComboBox();
            this.btnStartCam = new System.Windows.Forms.Button();
            this.btnInsCam = new System.Windows.Forms.Button();
            this.btnInsFile = new System.Windows.Forms.Button();
            this.gbxOutputResult = new System.Windows.Forms.GroupBox();
            this.tbxOutput1 = new System.Windows.Forms.TextBox();
            this.tbxOutput2 = new System.Windows.Forms.TextBox();
            this.tbxOutput3 = new System.Windows.Forms.TextBox();
            this.gbxCapture = new System.Windows.Forms.GroupBox();
            this.ibxCamCapture = new Emgu.CV.UI.ImageBox();
            this.gbxCamera.SuspendLayout();
            this.gbxOutputResult.SuspendLayout();
            this.gbxCapture.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ibxCamCapture)).BeginInit();
            this.SuspendLayout();
            // 
            // btnLoadTraning
            // 
            this.btnLoadTraning.Location = new System.Drawing.Point(12, 68);
            this.btnLoadTraning.Name = "btnLoadTraning";
            this.btnLoadTraning.Size = new System.Drawing.Size(87, 50);
            this.btnLoadTraning.TabIndex = 53;
            this.btnLoadTraning.Text = "Load Training Data";
            this.btnLoadTraning.UseVisualStyleBackColor = true;
            this.btnLoadTraning.Click += new System.EventHandler(this.btnLoadTraning_Click);
            // 
            // btnTraining
            // 
            this.btnTraining.Location = new System.Drawing.Point(105, 68);
            this.btnTraining.Name = "btnTraining";
            this.btnTraining.Size = new System.Drawing.Size(87, 50);
            this.btnTraining.TabIndex = 52;
            this.btnTraining.Text = "Traning New Data";
            this.btnTraining.UseVisualStyleBackColor = true;
            this.btnTraining.Click += new System.EventHandler(this.btnTraining_Click);
            // 
            // gbxCamera
            // 
            this.gbxCamera.Controls.Add(this.cbxCameras);
            this.gbxCamera.Controls.Add(this.btnStartCam);
            this.gbxCamera.Location = new System.Drawing.Point(12, 12);
            this.gbxCamera.Name = "gbxCamera";
            this.gbxCamera.Size = new System.Drawing.Size(189, 50);
            this.gbxCamera.TabIndex = 54;
            this.gbxCamera.TabStop = false;
            this.gbxCamera.Text = "Select Camera";
            // 
            // cbxCameras
            // 
            this.cbxCameras.FormattingEnabled = true;
            this.cbxCameras.Location = new System.Drawing.Point(6, 18);
            this.cbxCameras.Name = "cbxCameras";
            this.cbxCameras.Size = new System.Drawing.Size(118, 21);
            this.cbxCameras.TabIndex = 11;
            // 
            // btnStartCam
            // 
            this.btnStartCam.Location = new System.Drawing.Point(130, 15);
            this.btnStartCam.Name = "btnStartCam";
            this.btnStartCam.Size = new System.Drawing.Size(50, 25);
            this.btnStartCam.TabIndex = 9;
            this.btnStartCam.Text = "Start";
            this.btnStartCam.UseVisualStyleBackColor = true;
            this.btnStartCam.Click += new System.EventHandler(this.btnStartCam_Click);
            // 
            // btnInsCam
            // 
            this.btnInsCam.Location = new System.Drawing.Point(105, 124);
            this.btnInsCam.Name = "btnInsCam";
            this.btnInsCam.Size = new System.Drawing.Size(87, 50);
            this.btnInsCam.TabIndex = 56;
            this.btnInsCam.Text = "Capture From Camera";
            this.btnInsCam.UseVisualStyleBackColor = true;
            this.btnInsCam.Click += new System.EventHandler(this.btnInsCam_Click);
            // 
            // btnInsFile
            // 
            this.btnInsFile.Location = new System.Drawing.Point(12, 124);
            this.btnInsFile.Name = "btnInsFile";
            this.btnInsFile.Size = new System.Drawing.Size(87, 50);
            this.btnInsFile.TabIndex = 55;
            this.btnInsFile.Text = "Load Image";
            this.btnInsFile.UseVisualStyleBackColor = true;
            this.btnInsFile.Click += new System.EventHandler(this.btnInsFile_Click);
            // 
            // gbxOutputResult
            // 
            this.gbxOutputResult.Controls.Add(this.tbxOutput1);
            this.gbxOutputResult.Controls.Add(this.tbxOutput2);
            this.gbxOutputResult.Controls.Add(this.tbxOutput3);
            this.gbxOutputResult.Location = new System.Drawing.Point(12, 180);
            this.gbxOutputResult.Name = "gbxOutputResult";
            this.gbxOutputResult.Size = new System.Drawing.Size(180, 106);
            this.gbxOutputResult.TabIndex = 58;
            this.gbxOutputResult.TabStop = false;
            this.gbxOutputResult.Text = "Result";
            // 
            // tbxOutput1
            // 
            this.tbxOutput1.Location = new System.Drawing.Point(6, 19);
            this.tbxOutput1.Name = "tbxOutput1";
            this.tbxOutput1.Size = new System.Drawing.Size(168, 20);
            this.tbxOutput1.TabIndex = 49;
            // 
            // tbxOutput2
            // 
            this.tbxOutput2.Location = new System.Drawing.Point(6, 45);
            this.tbxOutput2.Name = "tbxOutput2";
            this.tbxOutput2.Size = new System.Drawing.Size(168, 20);
            this.tbxOutput2.TabIndex = 50;
            // 
            // tbxOutput3
            // 
            this.tbxOutput3.Location = new System.Drawing.Point(6, 71);
            this.tbxOutput3.Name = "tbxOutput3";
            this.tbxOutput3.Size = new System.Drawing.Size(168, 20);
            this.tbxOutput3.TabIndex = 51;
            // 
            // gbxCapture
            // 
            this.gbxCapture.Controls.Add(this.ibxCamCapture);
            this.gbxCapture.Location = new System.Drawing.Point(207, 16);
            this.gbxCapture.Name = "gbxCapture";
            this.gbxCapture.Size = new System.Drawing.Size(260, 270);
            this.gbxCapture.TabIndex = 59;
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
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(491, 314);
            this.Controls.Add(this.gbxCapture);
            this.Controls.Add(this.btnInsCam);
            this.Controls.Add(this.gbxOutputResult);
            this.Controls.Add(this.btnInsFile);
            this.Controls.Add(this.gbxCamera);
            this.Controls.Add(this.btnLoadTraning);
            this.Controls.Add(this.btnTraining);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Form1";
            this.Text = "PCA_Demo";
            this.gbxCamera.ResumeLayout(false);
            this.gbxOutputResult.ResumeLayout(false);
            this.gbxOutputResult.PerformLayout();
            this.gbxCapture.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.ibxCamCapture)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnLoadTraning;
        private System.Windows.Forms.Button btnTraining;
        private System.Windows.Forms.GroupBox gbxCamera;
        private System.Windows.Forms.ComboBox cbxCameras;
        private System.Windows.Forms.Button btnStartCam;
        private System.Windows.Forms.Button btnInsCam;
        private System.Windows.Forms.Button btnInsFile;
        private System.Windows.Forms.GroupBox gbxOutputResult;
        private System.Windows.Forms.TextBox tbxOutput1;
        private System.Windows.Forms.TextBox tbxOutput2;
        private System.Windows.Forms.TextBox tbxOutput3;
        private System.Windows.Forms.GroupBox gbxCapture;
        private Emgu.CV.UI.ImageBox ibxCamCapture;
    }
}

