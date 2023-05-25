namespace Xpr.xpr.Math;

public enum MathFunc
{
    Undefined,
    Add,
    Sub,
    Mul,
    Div,
    Mod,
    Pow,
    Sin,
    Cos
}

public static class MathFuncEx
{
    public static float add(ICollection<float> list)
    {
        return list.Sum();
    }
    
    public static float sub(ICollection<float> list)
    {
        float ret = 0;
        var first = true;
        foreach (var f in list)
        {
            if (first)
            {
                ret = f;
            }
            else
            {
                ret -= f;
                first = false;
            }
        }

        return ret;
    }
    
    public static float mul(ICollection<float> list)
    {
        float ret = 1;
        foreach (var f in list)
        {
            ret *= f;
        }

        return ret;
    }
    
    public static float div(ICollection<float> list)
    {
        float ret = 0;
        var first = true;
        foreach (var f in list)
        {
            if (first)
            {
                ret = f;
            }
            else
            {
                ret /= f;
                first = false;
            }
        }

        return ret;
    }
    
    public static Func<ICollection<float>, float> GetFunc(this MathFunc val)
    {
        switch (val)
        {
            case MathFunc.Add:
                return add;
            case MathFunc.Sub:
                return sub;
            case MathFunc.Mul:
                return mul;
            case MathFunc.Div:
                return div;
            case MathFunc.Mod:
            case MathFunc.Pow:
            default:
                throw new ArgumentOutOfRangeException(nameof(val), val, null);
        }
    }
}