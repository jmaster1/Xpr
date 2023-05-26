namespace Xpr.xpr.Token;

public enum XprTokenType
{
    Number,
    BracketOpen,
    BracketClose,
    Operator,
    Variable,
    ArgSeparator
}

public static class XprTokenTypeEx
{
    public static bool Is(this XprTokenType val, XprToken? token)
    {
        return token != null && token.Is(val);
    }
}
