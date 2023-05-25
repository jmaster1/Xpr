namespace Xpr.xpr;

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

    public override bool consumeLeft(XprVal val)
    {
        return false;
    }
    
    public override bool consumeRight(XprVal? val)
    {
        return false;
    }

    public override string ToString()
    {
        return GetValType() + "=" + Value;
    }
}