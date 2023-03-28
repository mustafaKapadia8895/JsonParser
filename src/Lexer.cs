using System.Diagnostics;

public static class Lexer
{
    static readonly Dictionary<char, Symbol> symbol_mapper = new()
    {
        { '{', Symbol.OBJECT_START },
        { '}', Symbol.OBJECT_END },
        { '[', Symbol.ARRAY_START },
        { ']', Symbol.ARRAY_END },
        { ',', Symbol.COMMA },
        { ':', Symbol.COLON }
    };

    public static List<IToken> tokenize(string s, int line_number = 0)
    {
        var tokens = new List<IToken>();

        var i = 0;

        while (i < s.Length)
        {
            var character = s[i];

            if (Char.IsWhiteSpace(character))
            {
                // don't care about white spaces
                i++;
            }
            else if (symbol_mapper.TryGetValue(character, out var type))
            {
                // single character tokens
                tokens.Add(new SymbolToken(line_number, i, i, type));
                i++;
            }
            else if (character == '"')
            {
                tokens.Add(read_string());
            }
            else if (is_number(character))
            {
                tokens.Add(read_number());
            }
            else if (Char.ToLower(character) == 't')
            {
                tokens.Add(read_true());
            }
            else if (Char.ToLower(character) == 'f')
            {
                tokens.Add(read_false());
            }
            else
            {
                // unrecognized character
                Debug.Assert(false);
            }

            IToken read_number()
            {
                string num = "";
                var start_index = i;

                // Read everything until we get a non numeric character
                while (is_number(character))
                {
                    num += character;
                    if (++i >= s.Length)
                    {
                        break;
                    }
                    character = s[i];
                }

                // Can be a decimal
                if (num.Contains('.'))
                {
                    double d;
                    // Parsing catches number format errors
                    Debug.Assert(double.TryParse(num, out d));
                    return new DecimalToken(line_number, start_index, i - 1, d);
                }

                long l;
                Debug.Assert(long.TryParse(num, out l));
                return new NumberToken(line_number, start_index, i - 1, l);
            }

            IToken read_true()
            {
                var start_index = i;
                var maybe_true = s.Substring(i, 4);
                Debug.Assert(bool.Parse(maybe_true));
                i += 4;
                return new BoolToken(line_number, start_index, i - 1, true);
            }

            IToken read_false()
            {
                var start_index = i;
                var maybe_false = s.Substring(i, 5);
                Debug.Assert(!bool.Parse(maybe_false));
                i += 5;
                return new BoolToken(line_number, start_index, i - 1, false); ;
            }

            IToken read_string()
            {
                // read everything between "..."
                var start_index = i;
                character = s[++i];
                string word = "";

                while (character != '"')
                {
                    word += character;
                    if (++i >= s.Length)
                    {
                        Debug.Assert(false);
                    }
                    character = s[i];
                }
                i++;
                return new StringToken(line_number, start_index, i - 1, word);
            }
        }
        return tokens;
    }

    static bool is_number(char c)
    {
        return Char.IsNumber(c) || c == '-' || c == '.';
    }
}