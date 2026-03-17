using Emgu.CV;
using Emgu.CV.Structure;
using System.Threading.Tasks;

namespace Algorithms.Utilities
{
    public class Utils
    {
        #region Compute histogram
        public static int[] ComputeHistogram(Image<Gray, byte> inputImage)
        {
            int[] histogram = new int[256];

            Parallel.For(0, inputImage.Height,
        () => new int[256],   // histogram local
        (y, state, localHist) =>
        {
            for (int x = 0; x < inputImage.Width; ++x)
            {
                localHist[inputImage.Data[y, x, 0]]++;
            }
            return localHist;
        },
        localHist =>
        {
            lock (histogram)
            {
                for (int i = 0; i < 256; i++)
                {
                    histogram[i] += localHist[i];
                }
            }
        });

            return histogram;
        }
        #endregion

        public static byte Clamp(double x)
        {
            if (x < 0)
            {
                return 0;
            }

            else if (x > 255)
            {
                return 255;
            }
            return (byte)(x + 0.5);
        }
    }
}