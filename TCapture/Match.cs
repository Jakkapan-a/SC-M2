using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Emgu.CV;
using Emgu.CV.Structure;

namespace TCapture
{
    public class Match
    {
        public Image<Bgr,byte> imgScene { get; set; }
        public Image<Bgr, byte> template { get; set; }
        Emgu.CV.Mat imgout;

        public Bitmap bitmap_out;
        public Match(Image<Bgr, byte> imgScene, Image<Bgr, byte> template)
        {
            this.imgScene = imgScene;
            this.template = template;
        }
        public Bitmap Matching()
        {
            try
            {
                imgout = new Emgu.CV.Mat();
                Emgu.CV.CvInvoke.MatchTemplate(imgScene, template, imgout, Emgu.CV.CvEnum.TemplateMatchingType.CcoeffNormed);

                double minVal = 0;
                double maxVal = 0;
                Point minLoc = new Point();
                Point maxLoc = new Point();

                Emgu.CV.CvInvoke.MinMaxLoc(imgout, ref minVal, ref maxVal, ref minLoc, ref maxLoc);
                Rectangle match = new Rectangle(maxLoc, template.Size);

                var img = imgScene.Copy(match);      
                CvInvoke.Rectangle(imgScene, match, new MCvScalar(0, 0, 255), 2);

                bitmap_out = new Bitmap(img.ToBitmap());
                return img.ToBitmap();                

            }
            catch(Exception)
            {
                return null;
            }
        }

        public static Bitmap Matching(Image<Bgr, byte> imgScene, Image<Bgr, byte> template)
        {
            try
            {
                Emgu.CV.Mat imgout = new Emgu.CV.Mat();
                Emgu.CV.CvInvoke.MatchTemplate(imgScene, template, imgout, Emgu.CV.CvEnum.TemplateMatchingType.CcoeffNormed);

                double minVal = 0;
                double maxVal = 0;
                Point minLoc = new Point();
                Point maxLoc = new Point();

                Emgu.CV.CvInvoke.MinMaxLoc(imgout, ref minVal, ref maxVal, ref minLoc, ref maxLoc);
                Rectangle match = new Rectangle(maxLoc, template.Size);

                var img = imgScene.Copy(match);
                CvInvoke.Rectangle(imgScene, match, new MCvScalar(0, 0, 255), 2);

                return img.ToBitmap();

            }
            catch (Exception)
            {
                return null;
            }
        }

        public static double Compare(string path_master, string path_slave)
        {
            try
            {
                using(Mat img_master = CvInvoke.Imread(path_master, Emgu.CV.CvEnum.ImreadModes.Grayscale))
                using(Mat img_slave = CvInvoke.Imread(path_slave, Emgu.CV.CvEnum.ImreadModes.Grayscale))
                {
                    Image<Gray, float> ima_m = new Image<Gray, float>(img_master.Size);
                    Image<Gray, float> ima_s = new Image<Gray, float>(img_slave.Size);

                    CvInvoke.Normalize(img_master, ima_m, 0, 1, Emgu.CV.CvEnum.NormType.MinMax, Emgu.CV.CvEnum.DepthType.Cv32F);
                    CvInvoke.Normalize(img_slave, ima_s, 0, 1, Emgu.CV.CvEnum.NormType.MinMax, Emgu.CV.CvEnum.DepthType.Cv32F);

                    return CvInvoke.CompareHist(ima_m, ima_s, Emgu.CV.CvEnum.HistogramCompMethod.Correl);
                }
            }
            catch (Exception)
            {
                return 0;
            }
        }

        
    } 
}
