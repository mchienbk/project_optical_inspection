//----------------------------------------------------------------------------
//  Copyright (C) 2004-2017 by EMGU Corporation. All rights reserved.       
//----------------------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using Emgu.CV;
using Emgu.CV.CvEnum;
using Emgu.CV.Features2D;
using Emgu.CV.Flann;
using Emgu.CV.Structure;
using Emgu.CV.Util;
using System.Linq;

namespace CameraTester
{
    public static class DrawMatches
    {
        public static void FindMatch(Mat modelImage, Mat observedImage, out long matchTime, 
            out VectorOfKeyPoint modelKeyPoints, out VectorOfKeyPoint observedKeyPoints, 
            VectorOfVectorOfDMatch matches, out Mat mask, out Mat homography)
        {
            int k = 2;
            double uniquenessThreshold = 0.80;

            Stopwatch watch;
            homography = null;

            modelKeyPoints = new VectorOfKeyPoint();
            observedKeyPoints = new VectorOfKeyPoint();

            using (UMat uModelImage = modelImage.GetUMat(AccessType.Read))
            using (UMat uObservedImage = observedImage.GetUMat(AccessType.Read))
            {
                KAZE featureDetector = new KAZE();

                //extract features from the object image
                Mat modelDescriptors = new Mat();
                featureDetector.DetectAndCompute(uModelImage, null, modelKeyPoints, modelDescriptors, false);

                watch = Stopwatch.StartNew();

                // extract features from the observed image
                Mat observedDescriptors = new Mat();
                featureDetector.DetectAndCompute(uObservedImage, null, observedKeyPoints, observedDescriptors, false);

                // Bruteforce, slower but more accurate
                // You can use KDTree for faster matching with slight loss in accuracy
                using (Emgu.CV.Flann.LinearIndexParams ip = new Emgu.CV.Flann.LinearIndexParams()) 
                using (Emgu.CV.Flann.SearchParams sp = new SearchParams())
                using (DescriptorMatcher matcher = new FlannBasedMatcher(ip, sp))
                {
                    matcher.Add(modelDescriptors);

                    matcher.KnnMatch(observedDescriptors, matches, k, null);
                    mask = new Mat(matches.Size, 1, DepthType.Cv8U, 1);
                    mask.SetTo(new MCvScalar(255));
                    Features2DToolbox.VoteForUniqueness(matches, uniquenessThreshold, mask);

                    int nonZeroCount = CvInvoke.CountNonZero(mask);
                    if (nonZeroCount >= 4)
                    {
                        nonZeroCount = Features2DToolbox.VoteForSizeAndOrientation(modelKeyPoints, observedKeyPoints,
                            matches, mask, 1.5, 20);
                        if (nonZeroCount >= 4)
                            homography = Features2DToolbox.GetHomographyMatrixFromMatchedFeatures(modelKeyPoints,
                                observedKeyPoints, matches, mask, 2);
                    }
                }
                watch.Stop();

            }
            matchTime = watch.ElapsedMilliseconds;
        }

        /// <summary>
        /// Draw the model image and observed image, the matched features and homography projection.
        /// </summary>
        /// <param name="modelImage">The model image</param>
        /// <param name="observedImage">The observed image</param>
        /// <param name="matchTime">The output total time for computing the homography matrix.</param>
        /// <returns>The model image and observed image, the matched features and homography projection.</returns>
        public static Mat Draw(Mat modelImage, Mat observedImage, out long matchTime, out Mat _croppedMat)
        {
            Mat homography;
           
            VectorOfKeyPoint modelKeyPoints;
            VectorOfKeyPoint observedKeyPoints;
            using (VectorOfVectorOfDMatch matches = new VectorOfVectorOfDMatch())
            {
                Mat mask;
                FindMatch(modelImage, observedImage, out matchTime, out modelKeyPoints, out observedKeyPoints, matches,
                   out mask, out homography);

                //Draw the matched keypoints
                Mat result = new Mat();
                //Mat _croppedMat = new Mat();
                _croppedMat = new Mat();
                Mat _temp = new Mat();
                // Mat _crop= new Mat();
                Features2DToolbox.DrawMatches(modelImage, modelKeyPoints, observedImage, observedKeyPoints,
                   matches, result, new MCvScalar(255, 255, 255), new MCvScalar(255, 255, 255), mask);

                #region draw the projected region on the image

                if (homography != null)
                {
                    //draw a rectangle along the projected model
                    Rectangle rect = new Rectangle(Point.Empty, modelImage.Size);
                    PointF[] pts = new PointF[]
                    {
                  new PointF(rect.Left, rect.Bottom),
                  new PointF(rect.Right, rect.Bottom),
                  new PointF(rect.Right, rect.Top),
                  new PointF(rect.Left, rect.Top)
                    };
                    pts = CvInvoke.PerspectiveTransform(pts, homography);

                    //Mat _cropMat = new Mat(observedImage, rect);
                    
#if NETFX_CORE  
               Point[] points = Extensions.ConvertAll<PointF, Point>(pts, Point.Round);
#else
                    Point[] points = Array.ConvertAll<PointF, Point>(pts, Point.Round);

                    Point _startPoint = points[3]; //Get cordinate of point
#endif
                    using (VectorOfPoint vp = new VectorOfPoint(points))
                    {
                        CvInvoke.Polylines(result, vp, true, new MCvScalar(0, 0, 255, 255), 5);
                    }

                    //**

                    //CvInvoke.CvtColor(observedImage, _temp, ColorConversion.Gray2Bgr);
                    //using (VectorOfPoint vp2 = new VectorOfPoint(points))
                    //{
                    //    CvInvoke.Polylines(_temp, vp2, true, new MCvScalar(0, 0, 255, 255), 5);
                    //}

                    //Rectangle _crop = new Rectangle(170, 120, rect.Width, rect.Height);
                    Rectangle _cropRegion = new Rectangle(_startPoint.X, _startPoint.Y, rect.Width, rect.Height);


                    //Image<Bgr, Byte> _bufferImg = _temp.ToImage<Bgr, Byte>();  //Return Croped-Image with Red-Rectangle
                    Image<Bgr, Byte> _bufferImg = observedImage.ToImage<Bgr, Byte>();   //Return Croped-Image with no Red-Rectangle
                    _bufferImg.ROI = _cropRegion;
                    Image<Bgr, Byte> _croppedImg = _bufferImg.Copy();

                    _croppedMat = _croppedImg.Mat;

                }
                #endregion

                //return new ReturnMat(result, _croppedMat);

                return result;


            }
        }
    }
}
