using Assignment3;
using System;

public sealed class CosineNoise : INoise
{
    private const double BASE_SAMPLING_WIDTH = Math.PI / 4;
    private double mX = -BASE_SAMPLING_WIDTH;

    public int GetNext(int level)
    {
        mX += BASE_SAMPLING_WIDTH / Math.Pow(2, level);
        return (int)(5 * Math.Cos(mX));
    }
}

