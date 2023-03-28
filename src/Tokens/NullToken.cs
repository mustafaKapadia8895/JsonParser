public class NullToken : Token
{
    public NullToken(int line_number, int start_index, int end_index) : base(line_number, start_index, end_index, null)
    {
    }
}