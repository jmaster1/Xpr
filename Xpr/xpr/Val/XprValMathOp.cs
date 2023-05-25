namespace Xpr.xpr;

internal class XprValMathOp : XprVal
{
    public XprVal? _left, _right;

    public MathOperator MathOperator => _token.MathOperator;
    
    private readonly XprToken _token;

    public XprValMathOp(XprToken operatorToken)
    {
        _token = RequireToken(operatorToken, XprTokenType.Operator);
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
        //
        // check if left is lower priority math op
        if (val.Is(XprValType.MathOp))
        {
            var mathOp = (XprValMathOp)val;
            if (mathOp.MathOperator.GetPriority() < MathOperator.GetPriority())
            {
                val = mathOp._right;
                mathOp._right = this;
            }
        }
        Assert(_left == null);
        _left = val;
        return true;
    }
    
    public override bool consumeRight(XprVal val)
    {
        if (_right != null) return false;
        _right = val;
        return true;
    }
    
    public override string ToString()
    {
        return GetValType() + "=" + MathOperator;
    }
}