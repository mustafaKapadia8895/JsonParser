public class JsonArray : JsonValue
{
    public JsonArray(IEnumerable<JsonValue> values) : base(values)
    {
    }
}