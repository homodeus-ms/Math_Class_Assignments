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

            result.Add(steps[0]);

            makeStepsRecursive(result, steps, noise, 0);

            List<int> result2 = new List<int>(result.Count);

            for (int i = 0; i < result.Count; ++i)
            {
                result2.Add(result[i]);
            }

            return result2;
        }
        private static void makeStepsRecursive(List<int> result, int[] steps, INoise noise, int level)
        {
            for (int i = 0; i < steps.Length - 1; ++i)
            {
                if (Math.Abs(steps[i + 1] - steps[i]) > 10)
                {
                    int start = steps[i];
                    int end = steps[i + 1];

                    int[] newSteps = new int[6];

                    newSteps[0] = start;
                    newSteps[newSteps.Length - 1] = end;

                    for (int j = 1; j < 5; ++j)
                    {
                        int newStep = (int)((end - start) / DENOMINATOR * j + start);
                        newStep += noise.GetNext(level);
                        newSteps[j] = newStep;
                    }

                    makeStepsRecursive(result, newSteps, noise, ++level);
                    level--;
                }

                else
                {
                    result.Add(steps[i + 1]);
                }
            }
        }
    }
}
