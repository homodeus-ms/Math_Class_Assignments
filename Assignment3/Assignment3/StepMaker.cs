using System;
using System.Collections.Generic;

namespace Assignment3
{
    public static class StepMaker
    {
        private const float DENOMINATOR = 5.0f;
        private static List<int> result;

        public static List<int> MakeSteps(int[] steps, INoise noise)
        {
            result = new List<int>();

            result.Add(steps[0]);

            MakeStepsRecursive(steps, noise, 0);
            
            return result;
        }
        public static void MakeStepsRecursive(int[] steps, INoise noise, int level)
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
                        int newStep = (int)((end - start) / DENOMINATOR * j + start + noise.GetNext(level));
                        newSteps[j] = newStep;
                    }

                    MakeStepsRecursive(newSteps, noise, ++level);
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
