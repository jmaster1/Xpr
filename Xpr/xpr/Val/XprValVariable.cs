namespace Xpr.xpr;

internal class XprValVariable : XprVal
{
    public readonly XprToken Token;

    public readonly string? Name;
    
    public XprValVariable(XprToken token)
    {
        Token = Require(token, XprTokenType.Variable);
        Name = Token.StringValue;
    }

    public override XprValType GetValType()
    {
        return XprValType.Variable;
    }

    public override float Eval(XprContext ctx)
    {
        return ctx.GetVariableValue(Name);
    }

    public override bool consumeLeft(XprVal val)
    {
        return false;
    }

    public override bool consumeRight(XprVal val)
    {
        return false;
    }
}