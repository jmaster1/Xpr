using Common.Lang;
using Common.Util;
using Xpr.xpr.Math;
using Xpr.xpr.Util;

namespace Xpr.xpr;

public class XprContext : GenericEntity
{
    public static readonly XprContext DefaultContext = new XprContext().ApplyMath();

    private readonly Map<string, Func<float>> _funcs0 = new();

    private readonly Map<string, Func<float, float>> _funcs1 = new();

    private readonly Map<string, Func<float, float, float>> _funcs2 = new();
    
    public XprContext ApplyMath()
    {
        foreach (var mf1 in LangHelper.EnumValues<MathFunc1>())
        {
            _funcs1[mf1.ToString().ToLower()] = mf1.GetFunc();
        }
        foreach (var mf2 in LangHelper.EnumValues<MathFunc2>())
        {
            _funcs2[mf2.ToString()] = mf2.GetFunc();
        }
        return this;
    }
    
    public Func<ICollection<float>, float> ResolveFunc(string? name)
    {
        throw new NotImplementedException();
    }

    public Func<float> ResolveFunc0(string name)
    {
        Assert(name != null);
        return _funcs0.Get(name.ToLower());
    }
    
    public Func<float, float> ResolveFunc1(string name)
    {
        Assert(name != null);
        return _funcs1.Get(name.ToLower());
    }

    public Func<float, float, float> ResolveFunc2(string name)
    {
        Assert(name != null);
        return _funcs2.Get(name.ToLower());
    }


}
