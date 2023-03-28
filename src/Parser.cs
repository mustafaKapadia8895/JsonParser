using System.Diagnostics;

public static class Parser
{
    public static JsonValue parse_value(List<IToken> tokens, int start_index, out int next_index)
    {
        var start_token = tokens[start_index];
        var start_value = start_token.get_value_as_object();

        if (is_simple_value(start_value))
        {
            next_index = start_index + 1;
            return new JsonSimpleValue(start_value);
        }
        else if (is_object_start(start_value))
        {
            return parse_object(tokens, start_index, out next_index);
        }
        else if (is_array_start(start_value))
        {
            return parse_array(tokens, start_index, out next_index);
        }
        else
        {
            throw new Exception();
        }
    }

    public static JsonObject parse_object(List<IToken> tokens, int start_index, out int next_index)
    {
        var current_index = start_index + 1;

        var properties = new Dictionary<string, JsonValue>();
        while (true)
        {
            var kvp = parse_property(tokens, current_index, out next_index);
            properties.Add(kvp.Key, kvp.Value);
            current_index = next_index;
            if (is_comma(tokens[current_index].get_value_as_object()))
            {
                current_index++;
            }
            else if (is_object_end(tokens[current_index].get_value_as_object()))
            {
                break;
            }
            else
            {
                Debug.Assert(false);
            }
        }

        next_index = current_index + 1;
        return new JsonObject(properties);
    }

    public static JsonArray parse_array(List<IToken> tokens, int start_index, out int next_index)
    {
        var current_index = start_index + 1;
        var values = new List<JsonValue>();

        while (true)
        {
            values.Add(parse_value(tokens, current_index, out next_index));
            current_index = next_index;

            if (is_comma(tokens[current_index].get_value_as_object()))
            {
                current_index++;
            }
            else if (is_array_end(tokens[current_index].get_value_as_object()))
            {
                break;
            }
            else
            {
                Debug.Assert(false);
            }
        }

        next_index = current_index + 1;
        return new JsonArray(values);
    }

    public static KeyValuePair<string, JsonValue> parse_property(List<IToken> tokens, int start_index, out int next_index)
    {
        var prop_key = tokens[start_index].get_value_as_object();
        Debug.Assert(prop_key is string);
        var colon = tokens[start_index + 1].get_value_as_object();
        Debug.Assert(is_colon(colon));

        return new KeyValuePair<string, JsonValue>(
            (string)prop_key,
            parse_value(tokens, start_index + 2, out next_index)
        );
    }

    public static bool is_simple_value(object value) => value is null or string or long or double or bool;

    public static bool is_symbol_of_type(object value, Symbol symbol)
    {
        if (value.GetType() == typeof(Symbol))
        {
            return ((Symbol)value) == symbol;
        }

        return false;
    }

    public static bool is_object_start(object value) => is_symbol_of_type(value, Symbol.OBJECT_START);

    public static bool is_object_end(object value) => is_symbol_of_type(value, Symbol.OBJECT_END);

    public static bool is_array_start(object value) => is_symbol_of_type(value, Symbol.ARRAY_START);

    public static bool is_array_end(object value) => is_symbol_of_type(value, Symbol.ARRAY_END);

    public static bool is_colon(object value) => is_symbol_of_type(value, Symbol.COLON);

    public static bool is_comma(object value) => is_symbol_of_type(value, Symbol.COMMA);
}