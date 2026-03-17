using Algorithms.Utilities;
using Emgu.CV;
using Emgu.CV.Structure;
using OpenTK.Graphics.OpenGL;
using System.Drawing;

namespace Algorithms.Sections
{
    public class Filters
    {
        public static Image<Gray,byte> ApplyFilter(Image<Gray,byte> inputImage, double[,] kernel)
        {
            int h=kernel.GetLength(0);
            int w=kernel.GetLength(1);
            int hOffset = h / 2;
            int wOffset = w / 2;
            Image<Gray,byte>result=inputImage.Clone();
           
            for(int y = h / 2; y < inputImage.Height-h/2; y++)
            {
                for(int x = w / 2; x < inputImage.Width - w / 2; x++)
                {
                    double sum = 0;
                    for(int i = -h / 2; i <= h / 2; i++)
                    {

                        for(int j = w / 2; j <= w / 2; j++)
                        {
                            sum += inputImage.Data[y + i, x + j, 0] * kernel[i + h / 2, j + w / 2];

                        }
                    }
                    result.Data[y, x, 0] = Utils.Clamp(sum);
                }
            }
            return result;
        }
    }
}