using Newtonsoft.Json;
using Xpr.xpr.Util;

namespace Xpr.xpr.Token
{
    public class JsonSrcRangeConverter : JsonConverterGeneric<SrcRange>
    {
        protected override void WriteJson(JsonWriter writer, SrcRange value, JsonSerializer serializer)
        {
            serializer.Serialize(writer, value.ToString());
        }

        protected override SrcRange ReadJson(JsonReader reader, SrcRange value, JsonSerializer serializer)
        {
            return default;
        }
    }
}