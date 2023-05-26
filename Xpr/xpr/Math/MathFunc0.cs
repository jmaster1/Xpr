namespace Xpr.xpr.Math;

/**
 * math functions with no arguments
 */
public enum MathFunc0
{
    Pi,
    E,
    Rnd
}

public static class MathFunc0Ex
{
    
    static readonly Random Rnd = new Random();
    public static Func<float> GetFunc(this MathFunc0 val)
    {
        return val switch
        {
            MathFunc0.Pi => () => (float)System.Math.PI,
            MathFunc0.E => () => (float)System.Math.E,
            MathFunc0.Rnd => () => (float)Rnd.NextSingle(),
            _ => throw new ArgumentOutOfRangeException(nameof(val), val, null)
        };
    }
}
