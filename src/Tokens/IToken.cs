public interface IToken
{
    public int line_number { get; set; }
    public int start_index { get; set; }
    public int end_index { get; set; }

    public object get_value_as_object();

    public string pretty_print();

}