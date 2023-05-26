using Xpr.xpr.Math;

namespace Xpr.xpr.Val;

/**
 * function with 1 argument
 */
internal class XprValFunc1 : XprValFunc
{
    public XprVal? Arg;
    
    public Func<float, float>? Func;

    public XprValFunc1(string name) : base(name)
    {
    }

    public XprValFunc1(MathFunc1 mf1) : base(mf1.ToString())
    {
        Func = mf1.GetFunc();
    }

    public override float Eval(XprContext ctx)
    {
        var argVal = Arg.Eval(ctx);
        Func ??= ctx.ResolveFunc1(Name);
        var result = Func.Invoke(argVal);
        return result;
    }
    
    public override string ToString()
    {
        return $"{Name}({Arg})";
    }
}
