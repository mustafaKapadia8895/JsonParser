public class BoolToken : Token<bool>
{
    public BoolToken(int line_number, int start_index, int end_index, bool value) : base(line_number, start_index, end_index, value)
    {
    }
}