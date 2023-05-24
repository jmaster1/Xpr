namespace Xpr.xpr;

/**
 * represents token parsed from character stream
 */
public class XprToken
{
    public readonly XprTokenType Type;

    public readonly object Value;

    public XprToken(XprTokenType type, object value)
    {
        Type = type;
        Value = value;
    }

    public float NumberValue => (float)Value;
    
    public MathOperator MathOperatorValue => (MathOperator)Value;
}