namespace Xpr.xpr;

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