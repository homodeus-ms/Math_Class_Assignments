using Assignment4.Image;
using System;
using System.ComponentModel;
using System.Reflection.Metadata.Ecma335;

namespace Assignment4
{
    public static class SignalProcessor
    {
        public static double[] GetGaussianFilter1D(double sigma)
        {
            int length = getArraySize(sigma);

            double[] result = new double[length];

            int centerIDX = length / 2;

            for (int i = 0; i <= length / 2; ++i)
            {
                double value = getGaussianFilterValue(centerIDX - i, 0, sigma, true);

                result[i] = value;

                if (i == centerIDX)
                {
                    break;
                }

                result[length - 1 - i] = value;
            }

            return result;
        }

        public static double[] Convolve1D(double[] signal, double[] filter)
        {
            double[] result = new double[signal.Length];

            for (int i = 0; i < signal.Length; ++i)
            {
                double sum = 0.0;

                for (int j = 0; j < filter.Length; ++j)
                {
                    int signalIDX = i - j + (filter.Length - 1) / 2;

                    if (signalIDX >= 0 && signalIDX < signal.Length)
                    {
                        sum += signal[signalIDX] * filter[j];
                    }
                }

                result[i] = sum;
            }
            
            return result;
        }

        public static double[,] GetGaussianFilter2D(double sigma)
        {
            int length = getArraySize(sigma);

            double[,] result = new double[length, length];

            int centerIDX = length / 2;

            for (int i = 0; i <= centerIDX; ++i)
            {
                for (int j = 0; j <= centerIDX; ++j)
                {
                    double value = getGaussianFilterValue(centerIDX - j, centerIDX - i, sigma, false);

                    result[i, j] = value;

                    if (i == centerIDX && j == centerIDX)
                    {
                        break;
                    }

                    result[length - 1 - i, j] = value;
                    result[i, length - 1 - j] = value;
                    result[length - 1 - i, length - 1 - j] = value;
                }
            }
            
            return result;
        }

        public static Bitmap ConvolveImage(Bitmap bitmap, double[,] filter)
        {
            int width = bitmap.Width;
            int height = bitmap.Height;

            Bitmap result = new Bitmap(width, height);

            int pivot = (filter.GetLength(1) - 1) / 2;

            for (int i = 0; i < height; ++i)
            {
                for (int j = 0; j < width; ++j)
                {
                    double r = 0.0;
                    double g = 0.0;
                    double b = 0.0;

                    for (int k = 0; k < filter.GetLength(0); ++k)
                    {
                        for (int m = 0; m < filter.GetLength(1); ++m)
                        {
                            int indexX = j - m + pivot;
                            int indexY = i - k + pivot;

                            if (indexX >= 0 && indexX < width && indexY >= 0 && indexY < height)
                            {
                                r += filter[k, m] * bitmap.GetPixel(indexX, indexY).R;
                                g += filter[k, m] * bitmap.GetPixel(indexX, indexY).G;
                                b += filter[k, m] * bitmap.GetPixel(indexX, indexY).B;
                            }
                        }
                    }

                    Color pixel = new Color((byte)r, (byte)g, (byte)b);

                    result.SetPixel(j, i, pixel);
                }
            }

            return result;
        }

        private static int getArraySize(double sigma)
        {
            int size = (int)Math.Ceiling(sigma * 6);

            size = size % 2 == 1 ? size : ++size;

            return size;
        }
        private static double getGaussianFilterValue(int x, int y, double sigma, bool b1d)
        {
            double denominator = b1d ? sigma * Math.Sqrt(2 * Math.PI) : 2 * Math.PI * Math.Pow(sigma, 2);

            double exponent = -1 * (Math.Pow(x, 2) + Math.Pow(y, 2));

            exponent /= 2 * Math.Pow(sigma, 2);

            double result = Math.Exp(exponent) / denominator;

            return result;
        }
    }
}
