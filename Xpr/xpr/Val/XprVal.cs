using Newtonsoft.Json;
using Xpr.xpr.Token;
using JsonConverter = Newtonsoft.Json.JsonConverter;

namespace Xpr.xpr;

public abstract class XprVal : GenericEntity
{
    public static readonly JsonSerializerSettings JsonSettings = new() {
        Converters = new List<JsonConverter>()
        {
            new Newtonsoft.Json.Converters.StringEnumConverter(),
            new JsonSrcRangeConverter()
        },
        Formatting = Formatting.Indented,
        TypeNameHandling = TypeNameHandling.Auto
    };
    
    public abstract XprValType GetValType();

    public bool Is(XprValType type)
    {
        return type == GetValType();
    }
    
    public abstract float Eval(XprContext ctx);
    
    public XprToken RequireToken(XprToken token, XprTokenType type)
    {
        if (token == null || token.Type != type)
        {
            throw new ArgumentException($"{this} requires token of type {type}, got: {token}");
        }

        return token;
    }
    
    public XprVal? RequireVal(XprVal? val, XprValType type)
    {
        if (val == null || val.GetValType() != type)
        {
            throw new ArgumentException($"{this} requires val of type {type}, got: {val}");
        }

        return val;
    }
    
    public XprVal? RequireVal(XprVal? val)
    {
        if (val == null)
        {
            throw new ArgumentException($"{this} requires val, got: {val}");
        }

        return val;
    }
    
    public abstract bool consumeLeft(XprVal val);

    public abstract bool consumeRight(XprVal? val);

    public string ToStringDeep()
    {
        return ToJson(this);
    }
    
    public static string ToJson(object src)
    {
        return JsonConvert.SerializeObject(src, Formatting.Indented, JsonSettings);
    }
}