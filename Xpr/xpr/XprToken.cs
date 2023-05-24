namespace Xpr.xpr;

/**
 * represents token parsed from character stream
 */
public class XprToken
{
    public readonly XprTokenType Type;

    public readonly object? Value;

    public readonly SrcRange Range;

    public XprToken(XprTokenType type, object? value, SrcRange range)
    {
        Type = type;
        Value = value;
        Range = range;
    }

    public float NumberValue => (float)(Value ?? float.NaN);
    
    public MathOperator MathOperator => (MathOperator)(Value ?? MathOperator.Undefined);

    public bool Is(XprTokenType type)
    {
        return type == Type;
    }
}