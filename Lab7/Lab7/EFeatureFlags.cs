﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab7
{
    [Flags]
    public enum EFeatureFlags
    {
        Default = 0,
        Men = 1,
        Women = 2,
        Rectangle = 4,
        Round = 8,
        Aviator = 16,
        Red = 32,
        Blue = 64,
        Black = 128
    }
}
