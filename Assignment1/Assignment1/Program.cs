using System;
using System.Diagnostics;
using System.Collections.Generic;

namespace Assignment1
{
    class Program
    {
        static void Main(string[] args)
        {
            Debug.Assert(BigNumberCalculator.GetOnesComplementOrNull("0") == null);
            Debug.Assert(BigNumberCalculator.GetOnesComplementOrNull("17") == null);

            Debug.Assert(BigNumberCalculator.GetTwosComplementOrNull("0") == null);
            Debug.Assert(BigNumberCalculator.GetTwosComplementOrNull("5") == null);

        }
    }
}
