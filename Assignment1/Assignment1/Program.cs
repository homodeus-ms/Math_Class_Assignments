using System;
using System.Diagnostics;
using System.Collections.Generic;

namespace Assignment1
{
    class Program
    {
        static void Main(string[] args)
        {
            Debug.Assert(BigNumberCalculator.GetTwosComplementOrNull("1") == null);
            Debug.Assert(BigNumberCalculator.GetTwosComplementOrNull("0x11") == null);
            Debug.Assert(BigNumberCalculator.GetTwosComplementOrNull("123") == null);
            Debug.Assert(BigNumberCalculator.GetTwosComplementOrNull("1b2") == null);
            Debug.Assert(BigNumberCalculator.GetTwosComplementOrNull("0xFF") == null);
            Debug.Assert(BigNumberCalculator.GetTwosComplementOrNull("0x012") == null);
            Debug.Assert(BigNumberCalculator.GetTwosComplementOrNull("0xBB") == null);
            Debug.Assert(BigNumberCalculator.GetTwosComplementOrNull("0xB14") == null);


        }
    }
}
