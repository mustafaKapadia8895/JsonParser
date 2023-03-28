public abstract class JsonValue
{
    public JsonValue(object value)
    {
        this.value = value;
    }
    public object value { get; set; }
}