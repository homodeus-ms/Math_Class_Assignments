using System;
using System.Collections.Generic;

namespace Assignment3
{
    public static class StepMaker
    {
        private const float DENOMINATOR = 5.0f;
        private static List<int> mResult;

        public static List<int> MakeSteps(int[] steps, INoise noise)
        {
            mResult = new List<int>();

            mResult.Add(steps[0]);

            makeStepsRecursive(steps, noise, 0);
            
            return mResult;
        }
        private static void makeStepsRecursive(int[] steps, INoise noise, int level)
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

                    makeStepsRecursive(newSteps, noise, ++level);
                    level--;
                }

                else
                {
                    mResult.Add(steps[i + 1]);
                }
            }
        }
    }
}
