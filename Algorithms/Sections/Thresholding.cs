using Emgu.CV.Structure;
using Emgu.CV;
using System.Xml;

namespace Algorithms.Sections
{
    public class Thresholding
    {
        private static int[] CreateHistogram(Image<Gray, byte> image)
        {
            int[] histogram = new int[256];
            for (int y = 0; y < image.Height; y++)
            {
                for (int x = 0; x < image.Width; x++)
                {
                    byte pixelValue = image.Data[y, x, 0];
                    ++histogram[pixelValue];
                }
            }
            return histogram;
        }
        public static byte OtsuTreshold(Image<Gray, byte> image)
        {
            var histogram=CreateHistogram(image);
            var totalPixels = image.Height * image.Width; 
            double[] p=new double[256];
            for (int y = 0; y < 256; y++)
            {
                p[y] = histogram[y] / totalPixels;
            }
            byte finalTreshold = 0;
            double interMaxVariance = 0.0;
            double P1 = p[0];
            double mu1 = 0;
            for (int t = 1; t <= 254; ++t)
            {
                P1 += p[t];
                mu1 += t * p[t];
               
                if (P1 != 0)
                {
                    mu1 /= P1;
                }

                double P2 = 1 - P1;
                double mu2 = 0;
                for(int k = t + 1; k <= 255; k++)
                {
                    mu2 += k * p[k];
                }
                if (P2 != 0)
                {
                    mu2 /= P2;
                }

                var interVarience = P1 * P2 * (mu1 - mu2) * (mu1 - mu2);
                if (interVarience > interMaxVariance)
                {
                    interMaxVariance = interVarience;
                    finalTreshold = (byte)t;
                }
            }
            return finalTreshold;
        }
    }
}