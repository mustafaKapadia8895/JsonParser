public class Token<T> : IToken
{
    public Token(int line_number, int start_index, int end_index, T value)
    {
        this.line_number = line_number;
        this.start_index = start_index;
        this.end_index = end_index;
        this.value = value;
    }

    public int line_number { get; set; }
    public int start_index { get; set; }
    public int end_index { get; set; }

    public T value { get; set; }

    public Type get_type()
    {
        return typeof(T);
    }

    public string pretty_print()
    {
        return $"[ln{line_number}, col{start_index + 1} - {end_index + 1}] : {value}({this.GetType()})";
    }
}