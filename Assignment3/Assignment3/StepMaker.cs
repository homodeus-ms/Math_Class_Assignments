using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace Assignment3
{
    public static class StepMaker
    {
        private const float DENOMINATOR = 5.0f;

        public static List<int> MakeSteps(int[] steps, INoise noise)
        {
            List<int> result = new List<int>(steps.Length);

            result = steps.ToList();
            
            makeStepsRecursive(result, steps, noise, -1);

            return result;
        }
        private static void makeStepsRecursive(List<int> result, int[] steps, INoise noise, int level)
        {
            level++;
            bool bStillHigh = false;

            for(int i = 0; i < result.Count - 1; ++i)
            {
                int start = result[i];
                int end = result[i + 1];

                if (Math.Abs(end - start) <= 10)
                {
                    continue;
                }

                else
                {
                    bStillHigh = true;

                    for (int j = 1; j < 5; ++j)
                    {
                        int newStep = (int)((end - start) / 5.0 * j + start);
                        newStep += noise.GetNext(level);

                        result.Insert(i + j, newStep);
                    }

                    i += 4;
                }
            }

            if (!bStillHigh)
            {
                return;
            }

            makeStepsRecursive(result, steps, noise, level);
        }
    }
}
