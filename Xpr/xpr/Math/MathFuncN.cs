namespace Xpr.xpr.Math;

/**
 * math functions with arbitrary arguments
 */
public enum MathFuncN
{
    Sum,
    Avg
}

public static class MathFuncNEx
{
    public static Func<float[], float> GetFunc(this MathFuncN val)
    {
        return val switch
        {
            MathFuncN.Sum => Sum,
            MathFuncN.Avg => Avg,
            _ => throw new ArgumentOutOfRangeException(nameof(val), val, null)
        };
    }

    private static float Sum(float[] arg)
    {
        return arg.Sum();
    }
    
    private static float Avg(float[] arg)
    {
        return arg.Average();
    }
}
