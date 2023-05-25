using Xpr.xpr.Math;

namespace Xpr.xpr.Val;

internal class XprValFunc1 : XprValFunc
{
    public XprVal? arg;
    
    Func<float, float>? func;

    public XprValFunc1(string name) : base(name)
    {
    }

    public XprValFunc1(MathFunc1 mf1) : base(mf1.ToString())
    {
        func = mf1.GetFunc();
    }

    public override float Eval(XprContext ctx)
    {
        var argVal = arg.Eval(ctx);
        func ??= ctx.ResolveFunc1(Name);
        var result = func.Invoke(argVal);
        return result;
    }
    
    public override string ToString()
    {
        return $"{Name}({arg})";
    }
}
