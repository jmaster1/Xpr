namespace Xpr.xpr.Math;

/**
 * math functions with 2 arguments
 */
public enum MathFunc2
{
    Add,
    Sub,
    Mul,
    Div,
    Mod,
    Pow
}

public static class MathFunc2Ex
{
    public static Func<float, float, float> GetFunc(this MathFunc2 val)
    {
        return val switch
        {
            MathFunc2.Add => Add,
            MathFunc2.Sub => Sub,
            MathFunc2.Mul => Mul,
            MathFunc2.Div => Div,
            MathFunc2.Mod => Mod,
            MathFunc2.Pow => Pow,
            _ => throw new ArgumentOutOfRangeException(nameof(val), val, null)
        };
    }

    private static float Pow(float arg1, float arg2)
    {
        return (float)System.Math.Pow(arg1, arg2);
    }

    private static float Mod(float arg1, float arg2)
    {
        return arg1 % arg2;
    }

    private static float Div(float arg1, float arg2)
    {
        return arg1 / arg2;
    }

    private static float Mul(float arg1, float arg2)
    {
        return arg1 * arg2;
    }

    private static float Sub(float arg1, float arg2)
    {
        return arg1 - arg2;
    }

    private static float Add(float arg1, float arg2)
    {
        return arg1 + arg2;
    }
}
