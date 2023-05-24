using NUnit.Framework;

namespace Xpr.xpr;

internal class XprValMathOp : XprVal
{
    private readonly XprVal _left;
    private XprVal _right;

    private readonly MathOperator _mathOperator;
    private readonly XprToken token;

    public XprValMathOp(XprToken operatorToken, XprVal left)
    {
        token = Require(operatorToken, XprTokenType.Operator);
        _left = left;
    }

    public override XprValType GetValType()
    {
        return XprValType.MathOp;
    }

    public override float Eval(XprContext ctx)
    {
        var l = _left.Eval(ctx);
        var r = _right.Eval(ctx);
        return token.MathOperator.Apply(l, r);
    }

    public override XprVal consume(XprVal val)
    {
        throw new NotImplementedException();
    }
    
    public override bool consumeRight(XprVal val)
    {
        Assert(_right == null);
        _right = val;
        return true;
    }
    
    public override string ToString()
    {
        return GetValType() + "=" + _mathOperator;
    }
}