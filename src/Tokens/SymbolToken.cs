public class SymbolToken : Token
{
    public SymbolToken(int line_number, int start_index, int end_index, Symbol value) : base(line_number, start_index, end_index, value)
    {
    }
}