using System.Diagnostics;

public class JsonSimpleValue : JsonValue
{
    public JsonSimpleValue(object value) : base(value)
    {
        Debug.Assert(value is null or string or long or double or bool);
    }
}