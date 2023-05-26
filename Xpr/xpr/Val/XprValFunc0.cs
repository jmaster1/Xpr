using Xpr.xpr.Math;

namespace Xpr.xpr.Val;

/**
 * function with 0 arguments
 */
internal class XprValFunc0 : XprValFunc
{
    public Func<float>? Func;

    public XprValFunc0(string name) : base(name)
    {
    }

    public XprValFunc0(MathFunc0 mf0) : base(mf0.ToString())
    {
        Func = mf0.GetFunc();
    }

    public override float Eval(XprContext ctx)
    {
        Func ??= ctx.ResolveFunc0(Name);
        var result = Func.Invoke();
        return result;
    }
    
    public override string ToString()
    {
        return $"{Name}()";
    }
}
