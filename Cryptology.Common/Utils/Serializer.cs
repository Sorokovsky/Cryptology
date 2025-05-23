using System.Text.Json;

namespace Cryptology.Common.Utils;

public class Serializer
{
    private static JsonSerializerOptions Options => new()
    {
        Converters = { new BigIntegerSerializationDeserializationConvertor() }
    };

    public static string Serialize(object obj) => JsonSerializer.Serialize(obj, Options);
    
    public static T Deserialize<T>(string json) => JsonSerializer.Deserialize<T>(json, Options)!;
}