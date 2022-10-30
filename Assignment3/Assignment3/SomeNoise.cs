using System;


namespace Assignment3
{
    public sealed class SomeNoise : INoise
    {
        private int x = 10;
        private static int count = 0;
        private static int count2 = 0;

        public int GetNext(int level)
        {
            count++;
            count2++;

            if (count % 4 == 3 && count2 < 10)
            {
                return 10;
            }
            else
            {
                if (count2 >= 10)
                {
                    count2 = 0;
                }

                return 0;
            }

            
        }
    }
}
