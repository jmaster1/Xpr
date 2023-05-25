using Common.Lang;
using Common.Util;
using Xpr.xpr.Math;

namespace Xpr.xpr;

public class XprContext
{
    public static readonly XprContext DefaultContext = new XprContext().ApplyMath();

    public readonly Map<string, Func<float, float>> funcs1 = new();
    
    public readonly Map<string, Func<float, float, float>> funcs2 = new();

    public float GetVariableValue(string? name)
    {
        throw new NotImplementedException();
    }

    public XprContext ApplyMath()
    {
        foreach (var mf1 in LangHelper.EnumValues<MathFunc1>())
        {
            funcs1[mf1.ToString().ToLower()] = mf1.GetFunc();
        }
        foreach (var mf2 in LangHelper.EnumValues<MathFunc2>())
        {
            funcs2[mf2.ToString()] = mf2.GetFunc();
        }
        return this;
    }
    
    public Func<ICollection<float>, float> ResolveFunc(string? name)
    {
        throw new NotImplementedException();
    }

    public Func<float, float> ResolveFunc1(string name)
    {
        return funcs1.Get(name.ToLower());
    }

    public Func<float, float, float> ResolveFunc2(string name)
    {
        return funcs2.Get(name.ToLower());
    }
}
