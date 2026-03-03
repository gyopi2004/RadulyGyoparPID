using Algorithms.Utilities;
using Emgu.CV;
using Emgu.CV.Structure;
using OpenTK.Graphics.OpenGL;
using System;
using System.Collections.Generic;
using System.Runtime.Remoting.Messaging;
using System.Windows.Forms;
using System.Windows.Input;

namespace Algorithms.Sections
{
    public class PointwiseOperations
    {
        public static byte[] CreareLinearOpLUT(float alpha, float beta)
        {
            byte[] table = new byte[256];
            for (int r = 0; r < 256; r++)
            {
                table[r] = Utils.Clamp(alpha * r + beta);
            }
            return table;
        }

        public static byte[] CreateGammaOpLUT(double gamma)
        {
            byte[] table = new byte[256];
            double c = (float)Math.Pow(255.0, 1 - gamma);
            for (int r = 0; r < 256; r++)
            {

                table[r] = Utils.Clamp((float)(c * Math.Pow(r, gamma)));
            }
            return table ;
        }

        public static Image<Gray, byte> ApplyLut(Image<Gray, byte> inputImage, byte[] lut)
        {
            Image<Gray, byte> result = new Image<Gray, byte>(inputImage.Size);

            for (int y = 0; y < inputImage.Height; ++y)
            {
                for (int x = 0; x < inputImage.Width; ++x)
                {
                    byte r = inputImage.Data[y, x, 0];
                    byte s = lut[r];
                    result.Data[y, x, 0] = s;
                }
            }
                return result;
             }

        public static Image<Bgr, byte> ApplyLut(Image<Bgr, byte> inputImage, byte[] lut)
        {
            Image<Bgr, byte> result = new Image<Bgr, byte>(inputImage.Size);

            for (int y = 0; y < inputImage.Height; ++y)
            {
                for (int x = 0; x < inputImage.Width; ++x)
                {
                    for (byte c = 0; c < 3; c++)
                    {
                        byte r = inputImage.Data[y, x, c];
                        byte s = lut[r];
                        result.Data[y, x, c] = s;
                    }

                }
            }
                return result;
            
        }

        


        
    }
}