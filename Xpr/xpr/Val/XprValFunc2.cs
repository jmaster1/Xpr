using Xpr.xpr.Math;

namespace Xpr.xpr.Val;

internal class XprValFunc2 : XprValFunc
{
    public XprVal? arg1;
    
    public XprVal? arg2;
    
    Func<float, float, float>? func;

    public XprValFunc2(string name) : base(name)
    {
    }

    public XprValFunc2(MathFunc2 mf2) : base(mf2.ToString())
    {
        func = mf2.GetFunc();
    }

    public override float Eval(XprContext ctx)
    {
        var arg1Val = arg1.Eval(ctx);
        var arg2Val = arg2.Eval(ctx);
        func ??= ctx.ResolveFunc2(Name);
        var result = func.Invoke(arg1Val, arg2Val);
        return result;
    }
}
