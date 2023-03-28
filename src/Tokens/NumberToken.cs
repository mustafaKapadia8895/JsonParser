public class NumberToken : Token
{
    public NumberToken(int line_number, int start_index, int end_index, long value) : base(line_number, start_index, end_index, value)
    {
    }
}