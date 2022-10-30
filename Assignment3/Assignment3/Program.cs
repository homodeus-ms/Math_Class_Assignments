using Assignment3;
using System;
using System.Collections.Generic;
using System.Diagnostics;
namespace Assignment3
{
    class Program
    {
        static void Main(string[] args)
        {
            /*
            INoise noise = new ZeroNoise();

            int[] steps = new int[3] { 0, 300, 130 };

            List<int> result = StepMaker.MakeSteps(steps, noise);

            Console.WriteLine(string.Join(" ", result));
            */
            


            /*
            
            List<int> expectedValue1 = new List<int> { 100, 102, 112, 114, 116, 118, 120, 123, 125, 127, 130, 132, 135, 137, 139, 141, 143, 146, 148, 150, 153, 155, 158, 160, 162, 165, 167, 170 };
            List<int> expectedValue2 = new List<int> { 100, 102, 112, 115, 117, 120, 122, 124, 127, 129, 132, 134, 136, 139, 141, 143, 145, 147, 150, 152, 155, 157, 159, 162, 164, 166, 168, 170 };
            List<int> expectedValue3 = new List<int> { 100, 102, 112, 115, 116, 117, 117, 123, 122, 124, 128, 132, 138, 139, 143, 146, 151, 151, 161, 170 };
            int[] steps = new int[] { 100, 102, 112, 170 };
            INoise noise = new ZeroNoise();
            List<int> newSteps = StepMaker.MakeSteps(steps, noise);
            Debug.Assert(expectedValue1.Count == newSteps.Count);
            for (int i = 0; i < expectedValue1.Count; i++)
            {
                Debug.Assert(expectedValue1[i] == newSteps[i]);
            }
            noise = new ConstantNoise();
            newSteps = StepMaker.MakeSteps(steps, noise);
            Debug.Assert(expectedValue2.Count == newSteps.Count);
            for (int i = 0; i < expectedValue2.Count; i++)
            {
                Debug.Assert(expectedValue2[i] == newSteps[i]);
            }
            noise = new SineNoise();
            newSteps = StepMaker.MakeSteps(steps, noise);
            Debug.Assert(expectedValue3.Count == newSteps.Count);
            for (int i = 0; i < expectedValue3.Count; i++)
            {
                Debug.Assert(expectedValue3[i] == newSteps[i]);
            }
            List<int> temp = new List<int> { 185, 182, 180, 177, 175, 173, 170, 168, 165, 163, 161, 158, 156, 153, 151, 149, 146, 144, 141, 139, 137, 134, 132, 129, 127, 125, 127, 123, 119, 115, 111, 107, 116 };
            int[] temp1 = new int[] { 185, 125, 127, 107, 116 };
            newSteps = StepMaker.MakeSteps(temp1, new ZeroNoise());
            Debug.Assert(temp.Count == newSteps.Count);
            for (int i = 0; i < temp.Count; i++)
            {
                Debug.Assert(temp[i] == newSteps[i]);
            }
            List<int> expectedValue = new List<int> { 100, 104, 106, 108, 110, 111, 115, 117, 119, 121, 122, 132, 136, 138, 140, 142, 143, 153, 157, 159, 161, 163, 164, 168, 170, 172, 174, 175, 185, 189, 191, 193, 195, 196, 206, 210, 212, 214, 216, 217, 221, 223, 225, 227, 228, 238, 242, 244, 246, 248, 249, 259, 263, 265, 267, 269, 270, 274, 276, 278, 280, 281, 291, 295, 297, 299, 301, 302, 312, 316, 318, 320, 322, 323, 327, 329, 331, 333, 334, 344, 348, 350, 352, 354, 355, 365 };
            steps = new int[] { 100, 365 };
            noise = new LevelNoise();
            newSteps = StepMaker.MakeSteps(steps, noise);

            //Console.WriteLine(string.Join(" ", expectedValue));
            //Console.WriteLine(string.Join(" ", newSteps));

            Debug.Assert(expectedValue.Count == newSteps.Count);
            for (int i = 0; i < expectedValue.Count; i++)
            {
                Debug.Assert(expectedValue[i] == newSteps[i]);
            }
            temp = new List<int> { -11, -5, 0, 4, 9, 15, 19, 23, 28, 32, 37, 34, 31, 28, 25, 22, 19, 16, 13, 10, 7, 4, 1, 0, -3, -6, -9, -12, -15, -18, -21, -24, -27, -30, -33, -36, -27, -18, -9, 0, 8, 17, 26, 35, 44, 53, 62, 71, 80, 89, 98, 107, 116, 125, 134, 143, 152, 161, 170, 179, 188 };
            temp1 = new int[] { -11, 15, 37, -36, 188 };
            newSteps = StepMaker.MakeSteps(temp1, new ZeroNoise());
            Debug.Assert(temp.Count == newSteps.Count);
            for (int i = 0; i < temp.Count; i++)
            {
                Debug.Assert(temp[i] == newSteps[i]);
            }
            
            
            temp = new List<int> { 112, 111, 113, 117, 121, 128, 138, 146, 155, 157, 161, 165, 170, 170 };
            temp1 = new int[] { 112, 170 };
            newSteps = StepMaker.MakeSteps(temp1, new CosineNoise());
            Debug.Assert(temp.Count == newSteps.Count);
            for (int i = 0; i < temp.Count; i++)
            {
                Debug.Assert(temp[i] == newSteps[i]);
            }
            */

        }
    }
    /*
    public sealed class LevelNoise : INoise
    {
        public int GetNext(int level)
        {
            return level;
        }
    }

    public sealed class CosineNoise : INoise
    {
        private const double BASE_SAMPLING_WIDTH = Math.PI / 4;
        private double mX = -BASE_SAMPLING_WIDTH;

        public int GetNext(int level)
        {
            mX += BASE_SAMPLING_WIDTH / Math.Pow(2, level);
            return (int)(5 * Math.Cos(mX));
        }
    }
    */

}




