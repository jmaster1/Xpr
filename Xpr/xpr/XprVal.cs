namespace Xpr.xpr;

public abstract class XprVal
{

    public abstract float Eval(XprContext ctx);

    public abstract XprVal consume(XprVal val);
}