namespace Xpr.xpr.Math;

public enum MathOperator
{
    Undefined,
    Plus,
    Minus,
    Multiply,
    Divide,
    Modulus,
    Power
}

public static class MathOperatorEx
{
    public static readonly char[] Chars = {'?', '+', '-', '*', '/', '%', '^'};
    
    public static readonly int[] Priorities = {0, 0, 1, 1, 1, 2};

    public static readonly MathFunc[] MathFuncs =
    {
        MathFunc.Undefined,
        MathFunc.Add,
        MathFunc.Sub,
        MathFunc.Mul,
        MathFunc.Div,
        MathFunc.Mod,
        MathFunc.Pow
    };
    
    public static char GetChar(this MathOperator val)
    {
        return Chars[(int)val];
    }
    
    public static int GetPriority(this MathOperator val)
    {
        return Priorities[(int)val];
    }
    
    public static MathFunc GetMathFunc(this MathOperator val)
    {
        return MathFuncs[(int)val];
    }
    
    public static float Apply(this MathOperator val, float l, float r)
    {
        switch (val)
        {
            case MathOperator.Plus:
                return l + r;
            case MathOperator.Minus:
                return l - r;
            case MathOperator.Multiply:
                return l * r;
            case MathOperator.Divide:
                return l / r;
            case MathOperator.Modulus:
                return l % r;
            case MathOperator.Power:
                return (float)System.Math.Pow(l, r);
            default:
                throw new ArgumentOutOfRangeException(nameof(val), val, null);
        }
    }

    public static bool resolve(char next, out MathOperator op)
    {
        var index = Array.IndexOf(Chars, next);
        if (index != -1)
        {
            op = (MathOperator)index;
        }
        else
        {
            op = MathOperator.Undefined;
        }

        return index != -1;
    }
}