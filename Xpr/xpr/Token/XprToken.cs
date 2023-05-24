namespace Xpr.xpr;

/**
 * represents token parsed from character stream
 */
public class XprToken : Logger
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
    
    public string? StringValue => (string)(Value ?? null)!;
    
    public MathOperator MathOperator => (MathOperator)(Value ?? MathOperator.Undefined);

    public bool Is(XprTokenType type)
    {
        return type == Type;
    }
    
    public override string ToString()
    {
        return Type + "=" + Value + " (" + Range + ")";
    }
}