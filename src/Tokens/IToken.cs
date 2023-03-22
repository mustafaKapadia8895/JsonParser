public interface IToken
{
    public int line_number { get; set; }
    public int start_index { get; set; }
    public int end_index { get; set; }
    public Type get_type();
    public string pretty_print();

}