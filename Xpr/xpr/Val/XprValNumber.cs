using Xpr.xpr.Token;

namespace Xpr.xpr.Val;

internal class XprValNumber : XprVal
{
    public readonly XprToken Token;
    
    public readonly float Value;
    
    public XprValNumber(XprToken token)
    {
        Token = RequireToken(token, XprTokenType.Number);
        Value = token.NumberValue;
    }

    public override XprValType GetValType()
    {
        return XprValType.Number;
    }

    public override float Eval(XprContext ctx)
    {
        return Value;
    }

    public override string ToString()
    {
        return GetValType() + "=" + Value;
    }
}