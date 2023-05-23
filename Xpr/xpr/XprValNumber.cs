namespace Xpr.xpr;

internal class XprValNumber : XprVal
{
    private readonly float Value;
    
    public XprValNumber(float value)
    {
        Value = value;
    }

    public override float Eval(XprContext ctx)
    {
        return Value;
    }

    public override XprVal consume(XprVal val)
    {
        throw new NotImplementedException();
    }
}