namespace Xpr.xpr;

public abstract class XprVal : Logger
{
    public abstract XprValType GetValType();

    public static void Assert(bool condition)
    {
        if (!condition)
        {
            throw new Exception();
        }
    }
    
    public bool Is(XprValType type)
    {
        return type == GetValType();
    }
    
    public abstract float Eval(XprContext ctx);

    public abstract XprVal consume(XprVal val);
    
    protected XprToken Require(XprToken token, XprTokenType type)
    {
        if (token == null || token.Type != type)
        {
            throw new ArgumentException(string.Format("{} requires token of type {}, got: {}", this, type, token));
        }

        return token;
    }

    public abstract bool consumeRight(XprVal val);
}