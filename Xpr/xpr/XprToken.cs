namespace Xpr.xpr;

public class XprToken
{
    public readonly XprTokenType Type;

    public readonly object Value;

    public XprToken(XprTokenType type, object value)
    {
        this.Type = type;
        this.Value = value;
    }
}