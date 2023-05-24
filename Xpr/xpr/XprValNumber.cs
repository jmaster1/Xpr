namespace Xpr.xpr;

internal class XprValNumber : XprVal
{
    public readonly XprToken Token;
    
    public readonly float Value;
    
    public XprValNumber(XprToken token)
    {
        Token = token;
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

    public override XprVal consume(XprVal val)
    {
        return null;
    }
}