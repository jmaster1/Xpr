namespace Xpr.xpr.Math;

/**
 * math functions with single argument
 */
public enum MathFunc1
{
    Undefined,
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
        switch (val)
        {
            case MathFunc1.Negate:
                return Negate;
            case MathFunc1.Sign:
                return Sign;
            case MathFunc1.Abs:
                return Abs;
            case MathFunc1.Sin:
                return Sin;
            case MathFunc1.Cos:
                return Cos;
            case MathFunc1.Undefined:
            default:
                throw new ArgumentOutOfRangeException(nameof(val), val, null);
        }
        return Negate;
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