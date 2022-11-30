using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace Lab11
{
    class Program
    {
        static void Main(string[] args)
        {
            int[] data = new int[] { -2, 3, 4, 5, 6, 8 };

            var result = FrequencyTable.GetFrequencyTable(data, 30);

            Console.WriteLine(string.Join(" ", result));
        }
    }
}