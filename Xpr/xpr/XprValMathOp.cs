namespace Xpr.xpr;

internal class XprValMathOp : XprVal
{
    private XprVal left, right;

    private readonly MathOperator _mathOperator;
    private readonly XprToken? token;

    public XprValMathOp(XprToken operatorToken, XprVal left)
    {
        token = Require(operatorToken, XprTokenType.Operator);
        this.left = left;
    }



    public override XprValType GetValType()
    {
        return XprValType.MathOp;
    }

    public override float Eval(XprContext ctx)
    {
        var l = left.Eval(ctx);
        var r = right.Eval(ctx);
        return token.MathOperator.Apply(l, r);
    }

    public override XprVal consume(XprVal val)
    {
        throw new NotImplementedException();
    }
}