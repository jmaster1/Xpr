namespace Xpr.xpr;

internal class XprValOperator : XprVal
{
    private readonly MathOperator Op;

    private XprVal left, right;
    
    public XprValOperator(MathOperator op)
    {
        this.Op = op;
    }

    public override float Eval(XprContext ctx)
    {
        var l = left.Eval(ctx);
        var r = right.Eval(ctx);
        switch (Op)
        {
            case MathOperator.Plus:
                return l + r;
            case MathOperator.Minus:
                return l - r;
            case MathOperator.Multiply:
                return l * r;
            case MathOperator.Divide:
                return l / r;break;
            case MathOperator.Power:
                return (float)Math.Pow(l, r);
            default:
                throw new ArgumentOutOfRangeException();
        }
    }

    public override XprVal consume(XprVal val)
    {
        if (left == null)
        {
            left = val;
        } else if (right == null)
        {
            right = val;
        }
        else
        {
            throw new ArgumentOutOfRangeException();
        }

        return default;
    }
}