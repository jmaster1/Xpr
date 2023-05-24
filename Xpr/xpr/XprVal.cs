namespace Xpr.xpr;

public abstract class XprVal
{
    public abstract XprValType GetValType();

    public bool Is(XprValType type)
    {
        return type == GetValType();
    }
    
    public abstract float Eval(XprContext ctx);

    public abstract XprVal consume(XprVal val);
}