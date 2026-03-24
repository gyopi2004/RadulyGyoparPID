using Algorithms.Utilities;
using Emgu.CV;
using Emgu.CV.Structure;
using OpenTK.Graphics.OpenGL;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace Algorithms.Sections
{
    public class Filters
    {
        public static Image<Gray, byte> ApplyFilter(Image<Gray, byte> inputImage, double[,] kernel)
        {
            int h = kernel.GetLength(0);
            int w = kernel.GetLength(1);
            int hOffset = h / 2;
            int wOffset = w / 2;
            Image<Gray, byte> result = inputImage.Clone();

            for (int y = h / 2; y < inputImage.Height - h / 2; y++)
            {
                for (int x = w / 2; x < inputImage.Width - w / 2; x++)
                {
                    double sum = 0;
                    for (int i = -h / 2; i <= h / 2; i++)
                    {

                        for (int j = w / 2; j <= w / 2; j++)
                        {
                            sum += inputImage.Data[y + i, x + j, 0] * kernel[i + h / 2, j + w / 2];

                        }
                    }
                    result.Data[y, x, 0] = Utils.Clamp(sum);
                }
            }
            return result;
        }

        public static Image<Bgr, byte> ApplyFilter(Image<Bgr, byte> inputImage, double[,] kernel)
        {
            int h = kernel.GetLength(0);
            int w = kernel.GetLength(1);
            int hOffset = h / 2;
            int wOffset = w / 2;
            Image<Bgr, byte> result = inputImage.Clone();

            for (int y = h / 2; y < inputImage.Height - h / 2; y++)
            {
                for (int x = w / 2; x < inputImage.Width - w / 2; x++)
                {

                    for (int channel = 0; channel < 3; channel++)
                    {
                        double sum = 0;
                        for (int i = -h / 2; i <= h / 2; i++)
                        {

                            for (int j = w / 2; j <= w / 2; j++)
                            {
                                sum += inputImage.Data[y + i, x + j, 0] * kernel[i + h / 2, j + w / 2];

                            }
                        }
                        result.Data[y, x, channel] = Utils.Clamp(sum);
                    }
                }
            }
            return result;
        }


        public static double[,] GaussMask(double teta)
        {

            int size = (int)Math.Ceiling(4 * teta);

            if (size % 2 == 0)
                size++;

            int w = size;
            int h = size;

            double[,] kernel = new double[h, w];

            double sum = 0.0;

            for (int y = -h/2; y <= h/2; y++)
            {
                for (int x = w/2; x <= w/2; x++)
                {
                    double value = Math.Exp(-(x * x + y * y) / (2 * teta * teta));
                    value /= (2 * Math.PI * teta * teta);

                    int row = y + h/2;
                    int col = x + w/2;

                    kernel[row, col] = value;
                    sum += value;
                }
            }

            for (int i = 0; i < h; i++)
            {
                for (int j = 0; j < w; j++)
                {
                    kernel[i, j] /= sum;
                }
            }

            return kernel;
        }

        public static Image<Gray,byte> GaussFiltering(Image<Gray,byte>inputImage, double teta)
        {
            double[,] kernel = GaussMask(teta);
            Image<Gray, byte> result = ApplyFilter(inputImage, kernel);
            return result;
        }

        public static Image<Bgr, byte> GaussFiltering(Image<Bgr, byte> inputImage, double teta)
        {
            double[,] kernel = GaussMask(teta);

            Image<Bgr, byte> result = inputImage.Clone();

            int h = kernel.GetLength(0);
            int w = kernel.GetLength(1);

            for (int y = h/2; y < inputImage.Height - h/2; y++)
            {
                for (int x = w/2; x < inputImage.Width - w/2; x++)
                {
                    double sumB = 0;
                    double sumG = 0;
                    double sumR = 0;

                    for (int i = -h/2; i <= h/2; i++)
                    {
                        for (int j = -w / 2; j <= w / 2; j++)
                        {
                            double k = kernel[i + h / 2, j + w / 2];

                            sumB += inputImage.Data[y + i, x + j, 0] * k;
                            sumG += inputImage.Data[y + i, x + j, 1] * k;
                            sumR += inputImage.Data[y + i, x + j, 2] * k;
                        }
                    }

                    result.Data[y, x, 0] = Utils.Clamp(sumB);
                    result.Data[y, x, 1] = Utils.Clamp(sumG);
                    result.Data[y, x, 2] = Utils.Clamp(sumR);
                }
            }

            return result;
        }
    }
}