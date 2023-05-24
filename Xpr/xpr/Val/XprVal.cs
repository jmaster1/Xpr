namespace Xpr.xpr;

public abstract class XprVal : Logger
{
    public abstract XprValType GetValType();

    public bool Is(XprValType type)
    {
        return type == GetValType();
    }
    
    public abstract float Eval(XprContext ctx);
    
    protected XprToken Require(XprToken token, XprTokenType type)
    {
        if (token == null || token.Type != type)
        {
            throw new ArgumentException($"{this} requires token of type {type}, got: {token}");
        }

        return token;
    }
    
    public abstract bool consumeLeft(XprVal val);

    public abstract bool consumeRight(XprVal val);
}