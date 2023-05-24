namespace Xpr.xpr;

internal class XprValMathOp : XprVal
{
    private XprVal? _left, _right;

    private readonly MathOperator _mathOperator;
    private readonly XprToken _token;

    public XprValMathOp(XprToken operatorToken)
    {
        _token = Require(operatorToken, XprTokenType.Operator);
        _mathOperator = _token.MathOperator;
    }

    public override XprValType GetValType()
    {
        return XprValType.MathOp;
    }

    public override float Eval(XprContext ctx)
    {
        var l = _left.Eval(ctx);
        var r = _right.Eval(ctx);
        return _token.MathOperator.Apply(l, r);
    }

    public override bool consumeLeft(XprVal val)
    {
        Assert(_left == null);
        _left = val;
        return true;
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