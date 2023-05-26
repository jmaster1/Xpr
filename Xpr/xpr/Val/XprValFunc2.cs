using Xpr.xpr.Math;

namespace Xpr.xpr.Val;

internal class XprValFunc2 : XprValFunc
{
    public XprVal? Arg1;
    
    public XprVal? Arg2;
    
    public Func<float, float, float>? Func;

    public XprValFunc2(string name) : base(name)
    {
    }

    public XprValFunc2(MathFunc2 mf2) : base(mf2.ToString())
    {
        Func = mf2.GetFunc();
    }

    public override float Eval(XprContext ctx)
    {
        var arg1Val = Arg1.Eval(ctx);
        var arg2Val = Arg2.Eval(ctx);
        Func ??= ctx.ResolveFunc2(Name);
        var result = Func.Invoke(arg1Val, arg2Val);
        return result;
    }
    
    public override string ToString()
    {
        return $"{Name}({Arg1}, {Arg2})";
    }
}
