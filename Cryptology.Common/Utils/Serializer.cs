using System.Text.Json;

namespace Cryptology.Common.Utils;

public static class Serializer
{
    private static JsonSerializerOptions Options => new()
    {
        Converters = { new BigIntegerSerializationDeserializationConvertor() }
    };

    public static string Serialize(object obj)
    {
        return JsonSerializer.Serialize(obj, Options);
    }

    public static T Deserialize<T>(string json)
    {
        return JsonSerializer.Deserialize<T>(json, Options)!;
    }
}