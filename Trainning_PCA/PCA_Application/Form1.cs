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
using PCA_Application.Class;

namespace PCA_Application
{
    public partial class Form1 : Form
    {
        BinaryFormatter _baseMatrix, _meanVector, _elegenX;
        Stopwatch watch;

        long _matchTime;
        string finalResult;
        int address;
        public Bitmap testImage;

        VideoCapture _Capture;
        List<KeyValuePair<int, string>> ListCamerasData = new List<KeyValuePair<int, string>>();
        private bool _CaptureInProgress;
        private Mat _frameCapture;
       // public Bitmap testImage;
        public Bitmap outImage;
        private Mat modelImage, observedImage;
        private Mat resultImage;     
        //PCA Matrix Variables
        clsMatrixOperation _objMatrix = new clsMatrixOperation();
        double[][] EigenFaceImage = null;
        double[][] BaseMatrix = null;
        double[][] CopyImageMatrix_forallImages = null;
        double[][] EigenVectorforImage = null;
        double[][] MeanVector = null;
        double[][] MainImageMatrix_forAllImages = null;




        public Form1()
        {
            InitializeComponent();
            clsMatrixOperation.iDefaultImageCount = PCA_Application.Properties.Settings.Default.DefaultNumberOfImages;
            clsMatrixOperation.iDimensionalReduction = PCA_Application.Properties.Settings.Default.DReductionVal;
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

            if (cbxCameras.Items.Count > 1) cbxCameras.SelectedIndex = 1;
            _frameCapture = new Mat();

            btnInsFile.Enabled = false;
            btnInsCam.Enabled = false;
        }



        private void btnStartCam_Click(object sender, EventArgs e)
        {
            try
            {
                _Capture = new VideoCapture(cbxCameras.SelectedIndex);
                _Capture.SetCaptureProperty(CapProp.FrameWidth, 1024);
                _Capture.SetCaptureProperty(CapProp.FrameHeight, 1024);
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
                    //sttCamera.Text = "Camera OFF";
                }
                else
                {
                    //start the capture
                    btnStartCam.Text = "Stop";
                    _Capture.Start();
                    _CaptureInProgress = true; //Flag the state of the camera
                    //sttCamera.Text = "Camera ON";
                }
            }
        }


        private void ProcessFrame(object sender, EventArgs arg)
        {
            if (_Capture != null && _Capture.Ptr != IntPtr.Zero)
            {
                _Capture.Retrieve(_frameCapture, 0);
                //Show video capture
                ibxCamCapture.Image = _frameCapture;
            }
        }

        private void btnLoadTraning_Click(object sender, EventArgs e)
        {
            _baseMatrix = new BinaryFormatter();
            _meanVector = new BinaryFormatter();
            _elegenX = new BinaryFormatter();

            using (FileStream fs = new FileStream("_baseMatrix.dat", FileMode.Open))
                BaseMatrix = (double[][])_baseMatrix.Deserialize(fs);

            using (FileStream fs = new FileStream("_meanVector.dat", FileMode.Open))
                MeanVector = (double[][])_meanVector.Deserialize(fs);

            using (FileStream fs = new FileStream("_elegenX.dat", FileMode.Open))
                EigenFaceImage = (double[][])_elegenX.Deserialize(fs);

            btnInsFile.Enabled = true;
            btnInsCam.Enabled = true;
        }

        private void btnTraining_Click(object sender, System.EventArgs e)
        {
            GetImageMatrix();

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

        private void btnInsFile_Click(object sender, EventArgs e)
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

        private void btnInsCam_Click(object sender, EventArgs e)
        {
            watch = Stopwatch.StartNew();

            _frameCapture = _Capture.QueryFrame();
            ibxCamCapture.Image = _frameCapture;

            observedImage = new Mat();
            CvInvoke.CvtColor(_frameCapture, observedImage, ColorConversion.Bgr2Gray);
            //observedImage.Save(folderName + "\\O" + col.ToString() + "_" + row.ToString() + ".jpg");
            modelImage = CvInvoke.Imread("sample_stm8.jpg", ImreadModes.Grayscale);
            resultImage = DrawMatches.Draw(modelImage, observedImage, out _matchTime, out outImage);

           //CvInvoke.Resize(cropImage, cropImage, new Size(312, 312), 0, 0, Inter.Linear);

            //outImage.Save(folderName + "\\S" + col.ToString() + "_" + row.ToString() + ".jpg");
            //observedImage.Save(folderName + "\\S" + col.ToString() + "_" + row.ToString() + ".jpg");
            testImage = outImage;

            GetImageMatrixforTest(out address);
            if (address <= 9) finalResult = "OK";
            else if (address > 9 & address <= 39) finalResult = "Sai huong";
            else if (address > 39 & address <= 49) finalResult = "Sai mark";
            else if (address > 49 & address <= 59) finalResult = "Sai chan";

            watch.Stop();

            tbxOutput1.Text = String.Format("Result: {0} ({1})", finalResult, address.ToString());
            tbxOutput2.Text = String.Format("Matched in {0} milliseconds", watch.ElapsedMilliseconds);
        }

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
            PCALib.Matrix obj = new PCALib.Matrix(MainImageMatrix_Covariance);
            PCALib.IEigenvalueDecomposition EigenVal;
            EigenVal = obj.GetEigenvalueDecomposition();

            // Take only top 10 eigen values
            double[] EigenValforImage = new double[PCA_Application.Properties.Settings.Default.DReductionVal];
            double[][] TransposevalImage = new double[PCA_Application.Properties.Settings.Default.DReductionVal][];
            EigenFaceImage = new double[PCA_Application.Properties.Settings.Default.DReductionVal][];
            EigenVectorforImage = new double[PCA_Application.Properties.Settings.Default.DefaultNumberOfImages][];
            // double[][] PixelmatrixforImage = new double[FaceRecognition.Properties.Settings.Default.DefaultNumberOfImages][];
            _objMatrix.GetTopEigenValues(EigenVal.RealEigenvalues, ref EigenValforImage);

            // Taking top 10 Eigen vectors
            PCALib.IMatrix EigenVector = EigenVal.EigenvectorMatrix;

            _objMatrix.GetTopEigenVectors(EigenVector, ref EigenVectorforImage);
            // _objMatrix.Transpose(CopyImageMatrix, ref TransposevalImage);
            _objMatrix.Multiply(MainImageMatrix_forAllImages, EigenVectorforImage, ref EigenFaceImage);

            // Transpose the Eigen Face images so that we can use inbuilt max and min function in array.
            _objMatrix.Transpose(EigenFaceImage, ref TransposevalImage, PCA_Application.Properties.Settings.Default.DReductionVal);
            double[][] PixelmatrixforImage = new double[PCA_Application.Properties.Settings.Default.DReductionVal][];
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
            double[][] Transponse_EachImage = new double[PCA_Application.Properties.Settings.Default.DReductionVal][];
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


    }
}
