public static class Main
{
    public static void main(string fileLocation)
    {
        var fileStream = File.OpenText(fileLocation);

        var tokens = new List<IToken>();
        int i = 1;

        while (!fileStream.EndOfStream)
        {
            tokens.AddRange(Lexer.tokenize(fileStream.ReadLine(), i++));
        }

        var parsed = Parser.parse_value(tokens, 0, out _);

        foreach (var token in tokens)
        {
            Console.WriteLine(token.pretty_print());
        }
    }
}