using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment3
{
    internal class Preserve
    {
        private const float DENOMINATOR = 5.0f;

        public static List<int> MakeSteps(int[] steps, INoise noise)
        {
            List<int> result = new List<int>(steps.Length);

            result.Add(steps[0]);

            makeStepsRecursive(result, steps, noise, 0);

            return result;
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
                        int newStep = (int)((end - start) / DENOMINATOR * j + start + noise.GetNext(level));
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
