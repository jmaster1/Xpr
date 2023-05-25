using Xpr.xpr.Math;
using Xpr.xpr.Token;

namespace Xpr.xpr.Val;

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
    
    public override string ToString()
    {
        return GetValType() + "=" + MathOperator;
    }
}