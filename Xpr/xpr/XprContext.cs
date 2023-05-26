using Common.Lang;
using Common.Util;
using Xpr.xpr.Math;
using Xpr.xpr.Util;

namespace Xpr.xpr;

public class XprContext : GenericEntity
{
    public static readonly XprContext DefaultContext = new XprContext().ApplyMath();

    public readonly Map<string, Func<float>> Funcs0 = new();

    public readonly Map<string, Func<float, float>> Funcs1 = new();

    public readonly Map<string, Func<float, float, float>> Funcs2 = new();
    
    public readonly Map<string, Func<float[], float>> FuncsN = new();
    
    public XprContext ApplyMath()
    {
        foreach (var mf0 in LangHelper.EnumValues<MathFunc0>())
        {
            Funcs0[mf0.ToString().ToLower()] = mf0.GetFunc();
        }
        foreach (var mf1 in LangHelper.EnumValues<MathFunc1>())
        {
            Funcs1[mf1.ToString().ToLower()] = mf1.GetFunc();
        }
        foreach (var mf2 in LangHelper.EnumValues<MathFunc2>())
        {
            Funcs2[mf2.ToString().ToLower()] = mf2.GetFunc();
        }
        foreach (var mfN in LangHelper.EnumValues<MathFuncN>())
        {
            FuncsN[mfN.ToString().ToLower()] = mfN.GetFunc();
        }
        return this;
    }

    public Func<float> ResolveFunc0(string name)
    {
        Assert(name != null);
        return Funcs0.Get(name.ToLower());
    }
    
    public Func<float, float> ResolveFunc1(string name)
    {
        Assert(name != null);
        return Funcs1.Get(name.ToLower());
    }

    public Func<float, float, float> ResolveFunc2(string name)
    {
        Assert(name != null);
        return Funcs2.Get(name.ToLower());
    }
    
    public Func<float[], float> ResolveFuncN(string name)
    {
        Assert(name != null);
        return FuncsN.Get(name.ToLower());
    }
    
    public static XprContext CreateDefault()
    {
        var ctx = new XprContext();
        ctx.ApplyMath();
        return ctx;
    }
}
