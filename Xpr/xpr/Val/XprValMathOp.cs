using Xpr.xpr.Math;

namespace Xpr.xpr.Val;

internal class XprValMathOp : XprVal
{
    public XprVal? _left, _right;

    public readonly MathOperator MathOperator;
    
    public XprValMathOp(MathOperator op, XprVal left)
    {
        MathOperator = op;
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
        return MathOperator.Apply(l, r);
    }
    
    public override string ToString()
    {
        return $"{_left} {MathOperator.GetChar()} {_right}";
    }
}