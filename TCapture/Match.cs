using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using Emgu.CV;
using Emgu.CV.Structure;
//using 
namespace TCapture
{
    public class Match
    {
        public Image<Bgr,byte> imgScene { get; set; }
        public Image<Bgr, byte> template { get; set; }
        Emgu.CV.Mat imgout;

        public Bitmap bitmap_out;

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

        public static  Bitmap Matching(Bitmap imageMaster, Bitmap imageSlave, string pathSave = null)
        {

            //try
            //{
                var imgScene = imageSlave.ToImage<Bgr, byte>();
                var template = imageMaster.ToImage<Bgr, byte>();

                Emgu.CV.Mat imgout = new Emgu.CV.Mat();
                Emgu.CV.CvInvoke.MatchTemplate(imgScene, template, imgout, Emgu.CV.CvEnum.TemplateMatchingType.CcoeffNormed);

                double minVal = 0;
                double maxVal = 0;
                Point minLoc = new Point();
                Point maxLoc = new Point();

                Emgu.CV.CvInvoke.MinMaxLoc(imgout, ref minVal, ref maxVal, ref minLoc, ref maxLoc);

                Rectangle r = new Rectangle(maxLoc, template.Size);

                Bitmap bit = new Bitmap(r.Width, r.Height);

                using(Graphics graphics= Graphics.FromImage(bit))
                {
                    graphics.DrawImage(imageSlave, 0, 0, r, GraphicsUnit.Pixel);
                }

            if (pathSave != null)
            { bit.Save(pathSave); }

             CvInvoke.Rectangle(imgScene,r, new MCvScalar(0, 0, 255), 2);
            return bit;
            //}
            //catch (Exception ex)
            //{
            //    MessageBox.Show("E007 " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            //    return null;
            //}
        }

        public static double CompareImage(string path_master, string path_slave)
        {
            try
            {
                Image<Gray, byte> imageMaster = new Image<Gray, byte>(path_master);
                Image<Gray, byte> imageSlave = new Image<Gray, byte>(path_slave);
                if (imageMaster.Width != imageSlave.Width || imageMaster.Height != imageSlave.Height)
                {
                    return 0;
                }
                var diffImage = new Image<Gray, byte>(imageMaster.Width, imageMaster.Height);
                // Get the image of different pixels.
                CvInvoke.AbsDiff(imageMaster, imageSlave, diffImage);
                var threadholdImage = new Image<Gray, byte>(imageMaster.Width, imageMaster.Height);
                // Check the pixies difference.
                // For instance, if difference between the same pixel on both image are less then 20,
                // we can say that this pixel is the same on both images.
                // After threadholding we would have matrix on which we would have 0 for pixels which are "nearly" the same and 1 for pixes which are different.
                CvInvoke.Threshold(diffImage, threadholdImage, 20, 1, Emgu.CV.CvEnum.ThresholdType.Binary);
                int diff = CvInvoke.CountNonZero(threadholdImage);
                // Take the percentage of the pixels which are different.
                var deffPrecentage = diff / (double)(imageMaster.Width * imageMaster.Height);
                // If the amount of different pixeles more then 15% then we can say that those immages are different.
                var percent = deffPrecentage * 100;
                imageMaster.Dispose();
                imageSlave.Dispose();
                // round off
                return Math.Round(100 - percent, 3);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return 0;
            }
        }

        
    } 
}
