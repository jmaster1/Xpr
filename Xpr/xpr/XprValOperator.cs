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
        
    }

    public override XprVal consume(XprVal val)
    {
        
    }
}