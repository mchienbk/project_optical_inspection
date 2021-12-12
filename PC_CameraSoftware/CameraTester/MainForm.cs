﻿/*----------------------------------------------------------------------------
 * DAI HOC BACH KHOA TP.HCM - KHOA CO KHI - BM CODIEN TU
 * LUAN VAN TOT NGHIEP DAI HOC - THIET KE MAY KIEM TRA NGOAI QUAN SAN PHAM
 * SVTH: TRAN MINH CHIEN  - MSSV:21300382
 * GVHD: TS.LE THANH HAI
 * Filename: MainForm.cs
----------------------------------------------------------------------------*/

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.Timers;

using System.IO;
using System.IO.Ports;
using System.Xml;
using System.Runtime.Serialization.Formatters.Binary;


using Emgu.CV;
using Emgu.CV.CvEnum;
using Emgu.CV.Structure;
using Emgu.CV.Util;
using Emgu.Util;
using Emgu.CV.OCR;
using Emgu.CV.UI;

using DirectShowLib;

using Microsoft.Office.Interop.Excel;

using XnaFan.ImageComparison;

using CameraTester.Class;
using PCALib;

namespace CameraTester
{
    delegate void SetTextCallback(string text);

    public partial class MainForm : Form
    {
     
        #region Variables

        //COM Port Variable
        static SerialPort _Com;
        static String _InputData = string.Empty;

        //Camera Variable
        VideoCapture _Capture;
        List<KeyValuePair<int, string>> ListCamerasData = new List<KeyValuePair<int, string>>();

        private bool _CaptureInProgress;
        private Mat _frameCapture;
        public Bitmap testImage;
        public Bitmap outImage;
        private Mat modelImage, observedImage;                          //Image input
        private Mat resultImage;                             //Image output
        //private Mat textImage, textResult, textCrop;                    //Image output
        //private Mat subMat, pinoutImage, pinoutResult, pinoutCrop;      //Image output


        long _matchTime;

        string dateTime = "";
        string folderName = "";

        int _distance = 0;
        long _pulseXmm = 6400;  //So xung tren 1 vong
        Int32 _xValue = 0;
        Int32 _yValue = 0;


        // Struct Data for RS-232
        byte[] IsMachineReady   = { 0x61 };
        byte[] MachineReady     = { 0x62 };
        byte[] OpenJogMode      = { 0x63 };
        byte[] JogModeOn        = { 0x64 };
        byte[] JogModeOff       = { 0x65 };
        byte[] MovingDone       = { 0x66 };
        //byte[] EmergencyStop    = { 0x80 };
        //byte[] MachineStopped   = { 0x81 };
        //byte[] ErrorLimitSwitch = { 0x90 };
        byte[] startInspection  = { 0x80, 0x00, 0x00 };
        byte[] stopInspection   = { 0x00, 0x01, 0x01 };
        byte[] goHome_X         = { 0x01, 0x01, 0x01 };
        byte[] goHome_Y         = { 0x02, 0x02, 0x02 };
        byte[] goHome_XY        = { 0x03, 0x03, 0x03 };
        byte[] X_forward        = { 0x30, 0x00, 0x00 };
        byte[] X_backward       = { 0x20, 0x00, 0x00 };
        byte[] Y_forward        = { 0x50, 0x00, 0x00 };
        byte[] Y_backward       = { 0x40, 0x00, 0x00 };

        //Serial port variables
        private System.Object SerialIncoming;
        int Timeout = 1000;
        Stopwatch watch;

        string finalResult;
        int address;

        BinaryFormatter _baseMatrix, _meanVector, _elegenX;

        //Excel Variables
        Microsoft.Office.Interop.Excel.Application exApp;
        Microsoft.Office.Interop.Excel._Workbook exWorkbook;
        Microsoft.Office.Interop.Excel._Worksheet exSheetResult;
        Microsoft.Office.Interop.Excel._Worksheet exSheetAdvanced;
        Microsoft.Office.Interop.Excel.Range exRange;

        //PCA Matrix Variables
        clsMatrixOperation _objMatrix = new clsMatrixOperation();
        double[][] EigenFaceImage = null;
        double[][] BaseMatrix = null;
        double[][] CopyImageMatrix_forallImages = null;
        double[][] EigenVectorforImage = null;
        double[][] MeanVector = null;
        double[][] MainImageMatrix_forAllImages = null;

        #endregion


        #region Form Initializing
//---------------------------------------------------------------------------------------------------
        public MainForm()
        {
            InitializeComponent();

            //Initialize COM Port
            _Com = new SerialPort();
            _Com.ReadTimeout = 500;
            _Com.WriteTimeout = 500;
            //_Com.DataReceived += new SerialDataReceivedEventHandler(DataReceive);
            _Com.DataReceived += new SerialDataReceivedEventHandler(DataReceivedHandler);

            string[] Ports = SerialPort.GetPortNames();
            cbxPort.Items.AddRange(Ports);

            // Create a List to store for ComboCameras
            List<KeyValuePair<int, string>> ListCamerasData = new List<KeyValuePair<int, string>>();
            // Find systems cameras with DirectShow.Net dll
            DsDevice[] _SystemCamereas = DsDevice.GetDevicesOfCat(FilterCategory.VideoInputDevice); 

            int _DeviceIndex = 0;
            foreach (DirectShowLib.DsDevice _Camera in _SystemCamereas)
            {
                ListCamerasData.Add(new KeyValuePair<int, string>(_DeviceIndex, _Camera.Name));
                _DeviceIndex++;
            }
            cbxCameras.DataSource = null;   // Clear the combobox
            cbxCameras.Items.Clear();

            cbxCameras.DataSource = new BindingSource(ListCamerasData, null);   // Bind the combobox
            cbxCameras.DisplayMember = "Value";
            cbxCameras.ValueMember = "Key";

            //Control Panel Setting
            string[] _Speed = { "1", "5", "10", "20", "100", "200" };
            cbxSpeed.Items.AddRange(_Speed);
            SerialIncoming = new Object();

            _frameCapture = new Mat();

        }
//---------------------------------------------------------------------------------------------------
        private void MainForm_Load(object sender, EventArgs e)
        {
            //Item select
            if (cbxCameras.Items.Count > 1) cbxCameras.SelectedIndex = 1;
            else cbxCameras.SelectedIndex = 0;
            if (cbxPort.Items.Count > 0) cbxPort.SelectedIndex = 0;
            cbxSpeed.SelectedIndex = 0;

            _xValue = 0; tbxXValue.Text = _xValue.ToString();
            _yValue = 0; tbxYValue.Text = _yValue.ToString();

            //Get setting data
            clsMatrixOperation.iDefaultImageCount = CameraTester.Properties.Settings.Default.DefaultNumberOfImages;
            clsMatrixOperation.iDimensionalReduction = CameraTester.Properties.Settings.Default.DReductionVal;
            //Initialize All source image
            GetImageMatrix();

            //_baseMatrix = new BinaryFormatter();
            //_meanVector = new BinaryFormatter();
            //_elegenX = new BinaryFormatter();

            //using (FileStream fs = new FileStream("_baseMatrix.dat", FileMode.Open))
            //    BaseMatrix = (double[][])_baseMatrix.Deserialize(fs);

            //using (FileStream fs = new FileStream("_meanVector.dat", FileMode.Open))
            //    MeanVector = (double[][])_meanVector.Deserialize(fs);

            //using (FileStream fs = new FileStream("_elegenX.dat", FileMode.Open))
            //    EigenFaceImage = (double[][])_elegenX.Deserialize(fs);


            
            
        }
//---------------------------------------------------------------------------------------------------
        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (_Com.IsOpen) _Com.Close();
            _Capture.Stop();
            _Capture.Dispose();
            exWorkbook.Close();
            exApp.Quit();
            System.Runtime.InteropServices.Marshal.ReleaseComObject(exWorkbook);
            System.Runtime.InteropServices.Marshal.ReleaseComObject(exApp);
        }
//---------------------------------------------------------------------------------------------------
        #endregion


        #region Config Camera
//---------------------------------------------------------------------------------------------------
        private void btnStartCam_Click(object sender, EventArgs e)
        {
            try
            {
                _Capture = new VideoCapture(cbxCameras.SelectedIndex);
                _Capture.SetCaptureProperty(CapProp.FrameWidth, 1080);
                _Capture.SetCaptureProperty(CapProp.FrameHeight, 1080);
                _Capture.ImageGrabbed += ProcessFrame;
            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message);
                return;
            }

            if (_Capture != null)
            {
                if (_CaptureInProgress)
                {  //stop the capture
                    btnStartCam.Text = "Start";
                    _Capture.Stop();
                    _Capture.Dispose();
                    _CaptureInProgress = false; //Flag the state of the camera
                    sttCamera.Text = "Camera OFF";
                }
                else
                {
                    //start the capture
                    btnStartCam.Text = "Stop";
                    _Capture.Start();
                    _CaptureInProgress = true; //Flag the state of the camera
                    sttCamera.Text = "Camera ON";
                }
            }

        }
//---------------------------------------------------------------------------------------------------           
        private void ProcessFrame(object sender, EventArgs arg)
        {
            if (_Capture != null && _Capture.Ptr != IntPtr.Zero)
            {
                _Capture.Retrieve(_frameCapture, 0);
                //Show video capture
                ibxCamReview.Image = _frameCapture;
            }
        }
//---------------------------------------------------------------------------------------------------
        #endregion


        #region Config COM Port
//---------------------------------------------------------------------------------------------------
        // Start COM Port
        private void btnOpenCOM_Click(object sender, EventArgs e)
        {
            if (!_Com.IsOpen)
            {
                _Com.PortName = cbxPort.Text.ToString();
                _Com.BaudRate = 9600;
                _Com.DataBits = 8;
                _Com.StopBits = StopBits.One;
                _Com.Parity = Parity.None;
                _Com.Handshake = Handshake.None;
                sttCom.Text = _Com.PortName + " " + _Com.BaudRate + "bps   ++";
                _Com.Open();

                //Change text
                btnOpenCOM.Text = "Close";
            }
            else
            {
                sttCom.Text = "COM Port is closed  ++";
                _Com.Close();

                btnOpenCOM.Text = "Open";
            }
        }
//---------------------------------------------------------------------------------------------------
        //Data Recieve Function
        private static void DataReceivedHandler(object sender, SerialDataReceivedEventArgs e)
        {
            SerialPort sp = (SerialPort)sender;
            string indata = sp.ReadExisting();
            //if (_InputData == "f") reading = true;

        }
//---------------------------------------------------------------------------------------------------
        private void Proxy(Object unused1, SerialDataReceivedEventArgs unused2)
        {
            //Console.WriteLine("Data arrived!");
            lock (SerialIncoming)
            {
                Monitor.Pulse(SerialIncoming);
            }
        }
//---------------------------------------------------------------------------------------------------
        public bool WaitForAData(int Timeout)
        {
            lock (SerialIncoming)//waits N seconds for a condition variable
            {
                if (!Monitor.Wait(SerialIncoming, Timeout))
                {//if timeout
                    //Console.WriteLine("Time out");
                    return false;
                }
                return true;
            }
        }
//---------------------------------------------------------------------------------------------------
        private void SetText(string text)
        {
            if (this.tbxReceive.InvokeRequired)
            {
                SetTextCallback d = new SetTextCallback(SetText); // khởi tạo 1 delegate mới gọi đến SetText
                this.Invoke(d, new object[] { text });
            }
            else this.tbxReceive.Text += text;
        }
//---------------------------------------------------------------------------------------------------
        #endregion


        #region Excel
//---------------------------------------------------------------------------------------------------
        private void OpenExcelFile()
        {
            //Start Excel and get Application object.
            exApp = new Microsoft.Office.Interop.Excel.Application();
            
            //Get a new workbook.
            exWorkbook = (Microsoft.Office.Interop.Excel._Workbook)(exApp.Workbooks.Add(""));
            
            //Open existing workbook.
            //exWorkbook = exApp.Workbooks.Open(workbookPath, 0, false, 5, "", "", false, Microsoft.Office.Interop.Excel.XlPlatform.xlWindows, "", true, false, 0, true, false, false);
            exApp.Visible = false;       //true or false

            //Select sheet 1
            exSheetAdvanced = (Microsoft.Office.Interop.Excel._Worksheet)exWorkbook.ActiveSheet;
            exSheetAdvanced.Activate();
            exSheetAdvanced.Name = "Time(ms)";

            //Add new sheet
            exSheetResult = (Microsoft.Office.Interop.Excel.Worksheet)exWorkbook.Worksheets.Add();
            exSheetResult = (Microsoft.Office.Interop.Excel._Worksheet)exWorkbook.ActiveSheet;
            exSheetResult.Activate();
            exSheetResult.Name = "Result";
        }
//---------------------------------------------------------------------------------------------------
        private void WriteResultData(int col, int row, string dataSave)
        {
            exSheetResult.Cells[row, col] = dataSave;
        }
//---------------------------------------------------------------------------------------------------
        private void WriteAdvancedData(int col, int row, string dataSave)
        {
            exSheetAdvanced.Cells[row, col] = dataSave;
        }
//---------------------------------------------------------------------------------------------------
        private void CloseExcelFile()
        {
            //Fakedata

            WriteResultData(1, 1, "OK");
            WriteResultData(1, 2, "OK");
            WriteResultData(2, 1, "NG");
            WriteResultData(2, 2, "OK");
            WriteResultData(3, 1, "OK");
            WriteResultData(3, 2, "NG");

            //exSheetResult.Cells[1, 1] = "OK";
            //exSheetResult.Cells[1, 2] = "Sai chan";
            //exSheetResult.Cells[1, 3] = "Sai chan";

            //exSheetResult.Cells[1, 1] = "Sai mark";
            //exSheetResult.Cells[1, 2] = "Sai mark";
            //exSheetResult.Cells[1, 3] = "OK";

            //exSheetResult.Cells[1, 1] = "Sai mark";
            //exSheetResult.Cells[1, 2] = "OK";
            //exSheetResult.Cells[1, 3] = "OK";
            //
            exApp.Visible = true; // Visible = true or false
            exApp.UserControl = true; // Editable = true or false
            //exWorkbook.Save();
            exWorkbook.SaveAs("D:\\ImageInspection\\" + DateTime.Now.ToString("yyyyMMdd-HHmm") + 
                ".xlsx", XlFileFormat.xlWorkbookDefault, null, null, false, false, 
                XlSaveAsAccessMode.xlExclusive, false, false, false, false, false);

            //exWorkbook.Close();
            //exApp.Quit();
            //System.Runtime.InteropServices.Marshal.ReleaseComObject(exWorkbook);
            //System.Runtime.InteropServices.Marshal.ReleaseComObject(exApp);
        }
//---------------------------------------------------------------------------------------------------
        #endregion


        #region Button Click
//---------------------------------------------------------------------------------------------------
        private void btnStartMachine_Click(object sender, EventArgs e)
        {
            if (_Com.IsOpen)
            {
                //Send Star command
                _Com.Write(IsMachineReady, 0, IsMachineReady.Length);
            }
            else
            {
                MessageBox.Show("Select port and try again!", "Serial port is not open!",
                MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
//---------------------------------------------------------------------------------------------------
        private void btnOpenFolder_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("explorer.exe", folderName);

        }
//---------------------------------------------------------------------------------------------------
        private void btnExcel_Click(object sender, EventArgs e)
        {

            //GetImageMatrix();

            _baseMatrix = new BinaryFormatter();
            _meanVector = new BinaryFormatter();
            _elegenX = new BinaryFormatter();

            using (FileStream fs = new FileStream("_baseMatrix.dat", FileMode.Create))
                _baseMatrix.Serialize(fs, BaseMatrix);
            using (FileStream fs = new FileStream("_meanVector.dat", FileMode.Create))
                _baseMatrix.Serialize(fs, MeanVector);
            using (FileStream fs = new FileStream("_elegenX.dat", FileMode.Create))
                _baseMatrix.Serialize(fs, EigenFaceImage);
        }
//---------------------------------------------------------------------------------------------------
        private void btnAbout_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Email: minhchienbku@gmail.com \r\n"
                + "Phone: 097.545.2473 \r\n"
                + "Copyright (C) 20017 by MINHCHIEN Bach Khoa University",
                "Automated Inspection Controller", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
//---------------------------------------------------------------------------------------------------
        private void btnClose_Click(object sender, EventArgs e)
        {
            if (_Com.IsOpen) _Com.Close();
            if (_CaptureInProgress)
            {
                _Capture.Stop();
                _Capture.Dispose();
            }
            this.Close();
        }
//---------------------------------------------------------------------------------------------------
        private void btnSend_Click(object sender, EventArgs e)
        {
            if (_Com.IsOpen) _Com.WriteLine(tbxSend.Text);
            else MessageBox.Show("Try it later", "Serial port is error!",
                MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
//---------------------------------------------------------------------------------------------------
        private void btnClear_Click(object sender, EventArgs e)
        {
            this.tbxReceive.Text = "";
        }
//---------------------------------------------------------------------------------------------------      
        #endregion


        #region Moving Function
//---------------------------------------------------------------------------------------------------
        private void goX_forward()
        {
            _pulseXmm = 5040;
            X_forward[2] = (byte)(_pulseXmm & 0xFF);
            X_forward[1] = (byte)((_pulseXmm >> 8) & 0xFF);
            _Com.Write(X_forward, 0, X_forward.Length);
        }
//---------------------------------------------------------------------------------------------------
        private void goX_backward()
        {
            _pulseXmm = 5040;
            X_backward[2] = (byte)(_pulseXmm & 0xFF);
            X_backward[1] = (byte)((_pulseXmm >> 8) & 0xFF);
            _Com.Write(X_backward, 0, X_backward.Length);
        }
//---------------------------------------------------------------------------------------------------
        private void goY_forward()
        {
            _pulseXmm = 5040;
            Y_forward[2] = (byte)(_pulseXmm & 0xFF);
            Y_forward[1] = (byte)((_pulseXmm >> 8) & 0xFF);
            _Com.Write(Y_forward, 0, Y_forward.Length);
        }
//---------------------------------------------------------------------------------------------------
        private void goY_backward()
        {
            _pulseXmm = 3*5040;
            Y_backward[2] = (byte)(_pulseXmm & 0xFF);
            Y_backward[1] = (byte)((_pulseXmm >> 8) & 0xFF);
            _Com.Write(Y_backward, 0, Y_backward.Length);
        }
//---------------------------------------------------------------------------------------------------
        private void Go_Home()
        {
            _Com.Write(goHome_XY, 0, goHome_XY.Length);
            _xValue = 0; _yValue = 0;
            RefreshData();
        }
//---------------------------------------------------------------------------------------------------
        private void GoX(int x)
        {
            if (x > 0)
            {
                _pulseXmm = x * 40;
                X_forward[2] = (byte)(_pulseXmm & 0xFF);
                X_forward[1] = (byte)((_pulseXmm >> 8) & 0xFF);
                _Com.Write(X_forward, 0, X_forward.Length);
            }
            else
            {
                _pulseXmm = Math.Abs(x) * 40;
                X_backward[2] = (byte)(_pulseXmm & 0xFF);
                X_backward[1] = (byte)((_pulseXmm >> 8) & 0xFF);
                _Com.Write(X_backward, 0, X_backward.Length);
            }
            _xValue += x;
            RefreshData();
        }
//---------------------------------------------------------------------------------------------------
        private void GoY(int y)
        {
            if (y > 0)
            {
                _pulseXmm = y * 40;
                Y_forward[2] = (byte)(_pulseXmm & 0xFF);
                Y_forward[1] = (byte)((_pulseXmm >> 8) & 0xFF);
                _Com.Write(Y_forward, 0, Y_forward.Length);
            }
            else
            {
                _pulseXmm = Math.Abs(y) * 40;
                Y_backward[2] = (byte)(_pulseXmm & 0xFF);
                Y_backward[1] = (byte)((_pulseXmm >> 8) & 0xFF);

                _Com.Write(Y_backward, 0, Y_backward.Length);
            }
            _yValue += y;
            RefreshData();
        }
//---------------------------------------------------------------------------------------------------
        private void RefreshData()
        {
            tbxXValue.Text = _xValue.ToString();
            tbxYValue.Text = _yValue.ToString();
        }
//---------------------------------------------------------------------------------------------------      
        #endregion

        
        #region Control Button Click
//---------------------------------------------------------------------------------------------------
        private void btnXplus_Click(object sender, EventArgs e)
        {
            _distance = Int16.Parse(cbxSpeed.SelectedItem.ToString());
            GoX(_distance);
        }
//---------------------------------------------------------------------------------------------------
        private void btnXsub_Click(object sender, EventArgs e)
        {
            _distance = Int16.Parse(cbxSpeed.SelectedItem.ToString());
            GoX(-_distance);
        }
//---------------------------------------------------------------------------------------------------
        private void btnYplus_Click(object sender, EventArgs e)
        {
            _distance = Int16.Parse(cbxSpeed.SelectedItem.ToString());
            GoY(_distance);
        }
//---------------------------------------------------------------------------------------------------
        private void btnYsub_Click(object sender, EventArgs e)
        {
            _distance = Int16.Parse(cbxSpeed.SelectedItem.ToString());
            GoY(-_distance);
        }
//---------------------------------------------------------------------------------------------------
        private void btnHome_Click(object sender, EventArgs e)
        {
            Go_Home();
        }
//---------------------------------------------------------------------------------------------------
        private void btnHomeX_Click(object sender, EventArgs e)
        {
            _Com.Write(goHome_X, 0, goHome_X.Length);
            _xValue = 0;
            RefreshData();
        }
//---------------------------------------------------------------------------------------------------
        private void btnHomeY_Click(object sender, EventArgs e)
        {
            _Com.Write(goHome_Y, 0, goHome_Y.Length);
            _yValue = 0;
            RefreshData();
        }
//---------------------------------------------------------------------------------------------------       
        #endregion


        #region PCA Algorithm
//---------------------------------------------------------------------------------------------------
        private void OpenImage()
        {
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Filter = "jpeg files (*.jpg)|*.jpg|(*.gif)|gif||";
            if (DialogResult.OK == dialog.ShowDialog())
            {
                testImage = new Bitmap(dialog.FileName);
                Image<Bgr, Byte> imageCV = new Image<Bgr, byte>(testImage);
                ibxCamCapture.Image = imageCV.Mat;
                //this.pbTest.Image = testImage;
                FileInfo finfo = new FileInfo(dialog.FileName);
            }
        }
//---------------------------------------------------------------------------------------------------
        public void GetImageMatrixforTest(out int strResult)
        {
            clsMatrixOperation.iDefaultImageCount = 1;
            //Code for Intializing Matrix
            double[][] MainImageMatrix = new double[1][];
            double[][] CopyImageMatrix = new double[1][];
            //InitializeMatrix_1(ref MainImageMatrix);
            // Call function to fetch all image in a matrix format
            _objMatrix.InitializeMatrix_ForTest(ref MainImageMatrix, testImage);
            _objMatrix.CopyMatrix(MainImageMatrix, ref CopyImageMatrix);


            // Take only top 10 eigen values
            double[] ResultMatrix = new double[1];
            double[][] TransposevalImage = new double[1][];
            double[][] EigenFaceImage_test = new double[1][];
            double[][] OutputMatrix = new double[1][];

            _objMatrix.getMeanAdjustedMatrix(ref MainImageMatrix, MeanVector, true);
            _objMatrix.Transpose(MainImageMatrix, ref TransposevalImage, 1, 1);
            _objMatrix.Multiply(TransposevalImage, EigenFaceImage, ref EigenFaceImage_test);
            _objMatrix.GetEucledianDistance(EigenFaceImage_test, BaseMatrix, ref ResultMatrix);

            strResult = 0;


            _objMatrix.GetImageOK(ResultMatrix, CopyImageMatrix_forallImages, ref strResult);

            //_objMatrix.GetTopfiveGuess(ResultMatrix, CopyImageMatrix_forallImages, ref OutputMatrix, ref HKResult);

            //_objMatrix.Transpose(OutputMatrix, ref TransposevalImage, FaceRecognition.Properties.Settings.Default.DReductionVal);
            //int iControl = 9;
            //foreach (Control cobj in gbxTest.Controls)
            //{
            //    if (cobj is PictureBox)
            //    {
            //        cobj.BackgroundImage = _objMatrix.DrawFaceValue(TransposevalImage, iControl);
            //        iControl--;
            //    }

            //}
        }
//---------------------------------------------------------------------------------------------------
        public void GetImageMatrix()
        {
            //Code for Intializing Matrix
            MainImageMatrix_forAllImages = new double[1][];             //Ma tran anh
            double[][] MainImageMatrix_Covariance = new double[1][];    //Hiep phuong sai
            CopyImageMatrix_forallImages = new double[1][];
            //InitializeMatrix_1(ref MainImageMatrix);
            // Call function to fetch all image in a matrix format
            _objMatrix.InitializeMatrix(ref MainImageMatrix_forAllImages);
            _objMatrix.CopyMatrix(MainImageMatrix_forAllImages, ref CopyImageMatrix_forallImages);
            //Code for finding mean vector (VT trung binh) from the MainImage Matrix
            MeanVector = new double[1][];
            MeanVector = _objMatrix.GetMeanVector(ref MainImageMatrix_forAllImages);

            //Performing Step 2 of Algorithm

            _objMatrix.getMeanAdjustedMatrix(ref MainImageMatrix_forAllImages, MeanVector);

            //Compute Covariance matrix (Ma tran HPS)

            MainImageMatrix_Covariance = _objMatrix.getCovarianceMatrix(MainImageMatrix_forAllImages);

            //Computing Eigen values and Eigen vecotors by suding MaPack library
            PCALib.Matrix obj = new Matrix(MainImageMatrix_Covariance);
            IEigenvalueDecomposition EigenVal;
            EigenVal = obj.GetEigenvalueDecomposition();

            // Take only top 10 eigen values
            double[] EigenValforImage = new double[CameraTester.Properties.Settings.Default.DReductionVal];
            double[][] TransposevalImage = new double[CameraTester.Properties.Settings.Default.DReductionVal][];
            EigenFaceImage = new double[CameraTester.Properties.Settings.Default.DReductionVal][];
            EigenVectorforImage = new double[CameraTester.Properties.Settings.Default.DefaultNumberOfImages][];
            // double[][] PixelmatrixforImage = new double[FaceRecognition.Properties.Settings.Default.DefaultNumberOfImages][];
            _objMatrix.GetTopEigenValues(EigenVal.RealEigenvalues, ref EigenValforImage);

            // Taking top 10 Eigen vectors
            PCALib.IMatrix EigenVector = EigenVal.EigenvectorMatrix;

            _objMatrix.GetTopEigenVectors(EigenVector, ref EigenVectorforImage);
            // _objMatrix.Transpose(CopyImageMatrix, ref TransposevalImage);
            _objMatrix.Multiply(MainImageMatrix_forAllImages, EigenVectorforImage, ref EigenFaceImage);

            // Transpose the Eigen Face images so that we can use inbuilt max and min function in array.
            _objMatrix.Transpose(EigenFaceImage, ref TransposevalImage, CameraTester.Properties.Settings.Default.DReductionVal);
            double[][] PixelmatrixforImage = new double[CameraTester.Properties.Settings.Default.DReductionVal][];
            _objMatrix.ConvertinPixelScale(TransposevalImage, ref PixelmatrixforImage, EigenFaceImage.Length);
            //  _objMatrix.Transpose(PixelmatrixforImage, ref TransposevalImage,EigenFaceImage.Length);

            /*int iControl = 0;
            foreach (Control cobj in gbxImages.Controls)
             {
                 if (cobj is PictureBox)
                 {
                     cobj.BackgroundImage = _objMatrix.DrawFaceValue(PixelmatrixforImage, iControl);
                     iControl++;
                 }

             }*/

            BaseMatrix = new double[clsMatrixOperation.iDefaultImageCount][];
            double[][] ProductMatrix = null;
            double[][] Transponse_EachImage = new double[CameraTester.Properties.Settings.Default.DReductionVal][];
            for (int irow = 0; irow < clsMatrixOperation.iDefaultImageCount; irow++)
            {
                Transponse_EachImage = new double[EigenFaceImage.Length][];
                _objMatrix.Transpose(MainImageMatrix_forAllImages, ref Transponse_EachImage, 1, irow + 1);
                _objMatrix.Multiply(Transponse_EachImage, EigenFaceImage, ref ProductMatrix);
                for (int iColumn = 0; iColumn < clsMatrixOperation.iDimensionalReduction; iColumn++)
                {
                    if (iColumn == 0)
                        BaseMatrix[irow] = new double[clsMatrixOperation.iDimensionalReduction];
                    BaseMatrix[irow][iColumn] = new double();
                    BaseMatrix[irow][iColumn] = ProductMatrix[0][iColumn];
                }
            }
            //  _objMatrix.Multiply(CopyImageMatrix, EigenFaceImage, ref BaseMatrix);
        }
//---------------------------------------------------------------------------------------------------       
        #endregion


        #region Inspection Function
//---------------------------------------------------------------------------------------------------
        private void btnManual_Click(object sender, EventArgs e)
        {
            OpenImage();
            watch = Stopwatch.StartNew();

            GetImageMatrixforTest(out address);
            watch.Stop();

            if (address <= 9) finalResult = "OK";
            else if (address > 9 & address <= 39) finalResult = "Sai huong";
            else if (address > 39 & address <= 49) finalResult = "Sai mark";
            else if (address > 49 & address <= 59) finalResult = "Sai chan";

            tbxOutput1.Text = String.Format("Result: {0} ({1})", finalResult, address.ToString());
            tbxOutput2.Text = String.Format("Matched in {0} milliseconds", watch.ElapsedMilliseconds);
        }
//---------------------------------------------------------------------------------------------------
        private void btnInspection_Click(object sender, EventArgs e)
        {
            //CameraorFile Inspection
            Camera_Inspection();
            //File_Inspecton();
        }
//---------------------------------------------------------------------------------------------------
        private void File_Inspecton()
        {
            dateTime = DateTime.Now.ToString("yyyyMMdd-HHmm");
            folderName = "D:\\ImageInspection\\" + dateTime;
            bool exist = System.IO.Directory.Exists(folderName);
            if (!exist) System.IO.Directory.CreateDirectory(folderName);

            OpenExcelFile();

            string[] inputFilePath = Directory.GetFiles(@"C:\Users\minhc\Desktop\New folder", "*.jpg");

            testImage = new Bitmap(inputFilePath[0]);
            outImage = new Bitmap(780, 780);
            //FileInfo finfo = new FileInfo(inputFilePath[0]);
            for (int ImageCount = 0; ImageCount < 9; ImageCount++)
            {
                watch = Stopwatch.StartNew();

                testImage = new Bitmap(inputFilePath[ImageCount]);

                
                Image<Bgr, Byte> imageCV  = new Image<Bgr, byte>(testImage);
                observedImage = imageCV.Mat;

                CvInvoke.CvtColor(observedImage, observedImage, ColorConversion.Bgr2Gray);
                modelImage = CvInvoke.Imread("sample_stm8.jpg", ImreadModes.Grayscale);
                resultImage = DrawMatches.Draw(modelImage, observedImage, out _matchTime, out outImage);

                //CvInvoke.Resize(outImage, outImage, new Size(312, 312), 0, 0, Inter.Linear);

                outImage.Save(folderName + "\\S" + ImageCount.ToString() + ".jpg");

                testImage = outImage;

                GetImageMatrixforTest(out address);
                if (address <= 9) finalResult = "OK";
                else if (address > 9 & address <= 39) finalResult = "Sai huong";
                else if (address > 39 & address <= 49) finalResult = "Sai mark";
                else if (address > 49 & address <= 59) finalResult = "Sai chan";

                watch.Stop();

                WriteResultData(1, ImageCount + 1, finalResult);
                WriteResultData(2, ImageCount + 1, watch.ElapsedMilliseconds.ToString() + " mms");
            }

            CloseExcelFile();
        }
//---------------------------------------------------------------------------------------------------
        private void Camera_Inspection()
        {
            _Com.Write(startInspection, 0, startInspection.Length);

            //Make new Path folder
            dateTime = DateTime.Now.ToString("yyyyMMdd-HHmm");
            folderName = "D:\\ImageInspection\\" + dateTime;

            bool exist = System.IO.Directory.Exists(folderName);
            if (!exist) System.IO.Directory.CreateDirectory(folderName);

            OpenExcelFile();
            //AutoInspection();

            _Com.Close();
            _Com.DataReceived += Proxy;
            _Com.Open();

            //Firt of tray (16,67)
            //GoX(188); while (!WaitForAData(Timeout)) ;
            //GoY(673); while (!WaitForAData(Timeout)) ;
            //GoX(185); while (!WaitForAData(Timeout)) ;
            //GoY(660); while (!WaitForAData(Timeout)) ;
            GoX(228); while (!WaitForAData(Timeout)) ;
            GoY(697); while (!WaitForAData(Timeout)) ;
            //Begin loop
            for (int col = 1; col <= 3; col = col + 1)       //25 coulumn
            {

                for (int row = 1; row <= 2; row = row + 1)       //10 row
                {
                    watch = Stopwatch.StartNew();

                    _frameCapture = _Capture.QueryFrame();
                    ibxCamCapture.Image = _frameCapture;

                    observedImage = new Mat();
                    CvInvoke.CvtColor(_frameCapture, observedImage, ColorConversion.Bgr2Gray);
                    observedImage.Save(folderName + "\\O" + col.ToString() + "_" + row.ToString() + ".jpg");
                    modelImage = CvInvoke.Imread("sample.jpg", ImreadModes.Grayscale);
                    resultImage = DrawMatches.Draw(modelImage, observedImage, out _matchTime, out outImage);

                    //CvInvoke.Resize(outImage, outImage, new Size(312, 312), 0, 0, Inter.Linear);

                    outImage.Save(folderName + "\\S" + col.ToString() + "_" + row.ToString() + ".jpg");
                    observedImage.Save(folderName + "\\S" + col.ToString() + "_" + row.ToString() + ".jpg");
                    testImage = outImage;

                    GetImageMatrixforTest(out address);
                    //if (address <= 9) finalResult = "OK";
                    //else if (address > 9 & address <= 39) finalResult = "Sai huong";
                    //else if (address > 39 & address <= 49) finalResult = "Sai mark";
                    //else if (address > 49 & address <= 59) finalResult = "Sai chan";

                    watch.Stop();

                    WriteResultData(col, row, finalResult);
                    WriteAdvancedData(col, row, watch.ElapsedMilliseconds.ToString());


                    tbxOutput1.Text = String.Format("Result: {0} ({1})", finalResult, address.ToString());
                    tbxOutput2.Text = String.Format("Matched in {0} milliseconds", watch.ElapsedMilliseconds);


                    GoY(159); while (!WaitForAData(Timeout)) ;
                }
                GoY(-318); while (!WaitForAData(Timeout)) ;
                GoX(151); while (!WaitForAData(Timeout)) ;
            }

            _Com.Write(stopInspection, 0, stopInspection.Length);
            CloseExcelFile();
            Go_Home();
        }

        private void btnManual2_Click(object sender, EventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Filter = "jpeg files (*.jpg)|*.jpg|(*.gif)|gif||";
            watch = Stopwatch.StartNew();
            if (DialogResult.OK == dialog.ShowDialog())
            {
                testImage = new Bitmap(dialog.FileName);

                Image<Bgr, Byte> imageCV = new Image<Bgr, byte>(testImage);

                
                ibxCamCapture.Image = imageCV.Mat;

                observedImage = imageCV.Mat;

                CvInvoke.CvtColor(observedImage, observedImage, ColorConversion.Bgr2Gray);
                modelImage = CvInvoke.Imread("sample_stm8.jpg", ImreadModes.Grayscale);
                resultImage = DrawMatches.Draw(modelImage, observedImage, out _matchTime, out outImage);

                ibxCamResult.Image = resultImage;
                //CvInvoke.Resize(outImage, outImage, new Size(312, 312), 0, 0, Inter.Linear);
                

                testImage = outImage;
                //ibxCamCapture.Image = testImage.Mat;

            }
            
            

            GetImageMatrixforTest(out address);
            watch.Stop();

            if (address <= 9) finalResult = "OK";
            else if (address > 9 & address <= 39) finalResult = "Sai huong";
            else if (address > 39 & address <= 49) finalResult = "Sai mark";
            else if (address > 49 & address <= 59) finalResult = "Sai chan";

            tbxOutput1.Text = String.Format("Result: {0} ({1})", finalResult, address.ToString());
            tbxOutput2.Text = String.Format("Matched in {0} milliseconds", watch.ElapsedMilliseconds);
        }
//---------------------------------------------------------------------------------------------------
        #endregion

    }
}
