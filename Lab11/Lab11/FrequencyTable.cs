using System;
using System.Collections.Generic;
using System.Globalization;

namespace Lab11
{
    public static class FrequencyTable
    {
        public static List<Tuple<Tuple<int, int>, int>> GetFrequencyTable(int[] data, int maxBinCount)
        {
            List<Tuple<Tuple<int, int>, int>> result = new List<Tuple<Tuple<int, int>, int>>(maxBinCount);

            int max = data[0];
            int min = data[0];
            int range;
            int sectionCount;

            for (int i = 0; i < data.Length; ++i)
            {
                max = data[i] > max ? data[i] : max;
                min = data[i] < min ? data[i] : min;
            }

            // 최대값 최소값이 같은 경우 빠른 리턴

            if (max == min)
            {
                result.Add(new Tuple<Tuple<int, int>, int>(new Tuple<int, int>(min, max + 1), data.Length));

                return result;
            }

            // 범위, 필요한 구간의 갯수, 각 데이터의 빈도값 구하기  

            range = (max - min) / maxBinCount + 1;

            sectionCount = (int)((max - min) / (double)range + 1);

            int[] frequency = new int[sectionCount];

            for (int i = 0; i < data.Length; ++i)
            {
                frequency[(data[i] - min) / range]++;
            }

            // 결과 List에 튜플값 넣기

            for (int i = 0; i < frequency.Length; ++i)
            {
                int sectionMinValue = min + range * i;
                int sectionMaxValue = min + range * (i + 1);

                result.Add(new Tuple<Tuple<int, int>, int>(new Tuple<int, int>(sectionMinValue, sectionMaxValue), frequency[i]));
            }

            return result;
        }
    }
    
}
