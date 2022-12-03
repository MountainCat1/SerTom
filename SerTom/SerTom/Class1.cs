using System.Reflection.Metadata.Ecma335;
using System.Text;

namespace SerTom;

public static class SerTomSerializer
{
    public static TomlObject ParseToTomlObject(string tomlFile)
    {
        var words = tomlFile
            .Split(new[] { Environment.NewLine, " " }, StringSplitOptions.None)
            .Where(line => line != string.Empty)
            .ToArray();

        var tomlObject = new TomlObject();

        for (int i = 0; i < words.Length; i++)
        {
            var present = words[i];


            if (present.First() == '[')
            {
                // Its a sub-object

                if (present.Last() != ']')
                    throw new Exception();
            }

            if (words[i + 2] != "[")
            {
                // Its a property

                if (words.Length < i + 2)
                    throw new Exception();
                if (words[i + 1] != "=")
                    throw new Exception();

                var propertyKey = words[i];
                var propertyValue = words[i + 2];

                if (propertyValue.First() == '\"')
                {
                    int j = 1;

                    do
                    {
                        propertyValue += $" {words[i + 2 + j]}";
                    } while (words[i + 2 + j].Last() != '"');

                    i += j;
                }

                tomlObject.Properties.Add(propertyKey, new TomlPropertyValue(propertyValue));

                i += 2;

                continue;
            }

            if (present[i + 2] == '[')
            {
                // Its an array

                continue;
            }
        }

        return tomlObject;
    }
}

public class TomlObject
{
    public Dictionary<string, TomlPropertyValue> Properties { get; } = new();
    public Dictionary<string, TomlArray> Arrays { get; } = new();
    public Dictionary<string, TomlObject> SubObjects { get; } = new();

    public T CastTo<T>()
    {
        throw new NotImplementedException();
    }
}

public struct TomlPropertyValue
{
    public enum TomlPropertyValueType
    {
        String,
        Integer
    }

    public readonly TomlPropertyValueType ValueType;

    public TomlPropertyValue(string value)
    {
        _intValue = null;
        _stringValue = null;


        if (int.TryParse(value, out var result))
        {
            _intValue = result;
            ValueType = TomlPropertyValueType.Integer;

            return;
        }

        if (value.First() == '\"' && value.Last() == '\"')
        {
            var sb = new StringBuilder(value);
            sb.Remove(0, 1);
            sb.Remove(sb.Length - 1, 1);
            
            _stringValue = sb.ToString();
            ValueType = TomlPropertyValueType.String;
            
            return;
        }

        throw new Exception();
    }

    private int? _intValue;
    private string? _stringValue;


    public int GetInt()
    {
        if (ValueType != TomlPropertyValueType.Integer)
            throw new Exception();

        return (int)_intValue!;
    }

    public string GetString()
    {
        switch (ValueType)
        {
            case TomlPropertyValueType.Integer:
                return _intValue.ToString()!;

            default:
            {
                if (_stringValue is null)
                    throw new Exception();

                return _stringValue;
            }
        }
    }
}

public class ITomlNode
{
    public string Name { get; set; }
}

public class TomlArray : ITomlNode
{
}