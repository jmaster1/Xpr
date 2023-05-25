using Newtonsoft.Json;
using Xpr.xpr.Math;
using Xpr.xpr.Util;

namespace Xpr.xpr.Token;

/**
 * represents token parsed from character stream
 */
public class XprToken : GenericEntity
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

    [JsonIgnore]
    public float NumberValue => (float)(Value ?? float.NaN);
    
    [JsonIgnore]
    public string? StringValue => (string)(Value ?? null)!;
    
    [JsonIgnore]
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