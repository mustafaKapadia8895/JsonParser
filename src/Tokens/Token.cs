public abstract class Token : IToken
{
    public Token(int line_number, int start_index, int end_index, object value)
    {
        this.line_number = line_number;
        this.start_index = start_index;
        this.end_index = end_index;
        this.value = value;
    }

    public int line_number { get; set; }
    public int start_index { get; set; }
    public int end_index { get; set; }

    public object value { get; set; }

    public object get_value_as_object()
    {
        return value;
    }

    public string pretty_print()
    {
        return $"[ln{line_number}, col{start_index + 1} - {end_index + 1}] : {value}({this.GetType()})";
    }
}