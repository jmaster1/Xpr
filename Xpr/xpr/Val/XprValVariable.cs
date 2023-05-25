using Xpr.xpr.Token;

namespace Xpr.xpr.Val;

internal class XprValVariable : XprVal
{
    public readonly XprToken Token;

    public readonly string? Name;
    
    public XprValVariable(XprToken token)
    {
        Token = RequireToken(token, XprTokenType.Variable);
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
}