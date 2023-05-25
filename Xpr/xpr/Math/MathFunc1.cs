namespace Xpr.xpr.Math;

/**
 * math functions with single argument
 */
public enum MathFunc1
{
    Negate,
    Sign,
    Abs,
    Sin,
    Cos
}

public static class MathFunc1Ex
{
    public static Func<float, float> GetFunc(this MathFunc1 val)
    {
        return val switch
        {
            MathFunc1.Negate => Negate,
            MathFunc1.Sign => Sign,
            MathFunc1.Abs => Abs,
            MathFunc1.Sin => Sin,
            MathFunc1.Cos => Cos,
            _ => throw new ArgumentOutOfRangeException(nameof(val), val, null)
        };
    }

    private static float Cos(float arg)
    {
        return (float)System.Math.Cos(arg);
    }

    private static float Sin(float arg)
    {
        return (float)System.Math.Sin(arg);
    }

    private static float Abs(float arg)
    {
        return System.Math.Abs(arg);
    }

    private static float Sign(float arg)
    {
        return System.Math.Sign(arg);
    }

    private static float Negate(float arg)
    {
        return -arg;
    }
}
