using System.Numerics;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Cryptology.Common.Utils;

public class BigIntegerSerializationDeserializationConvertor : JsonConverter<BigInteger>
{
    public override BigInteger Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        string? numberString = reader.GetString();
        return BigInteger.Parse(numberString!);
    }

    public override void Write(Utf8JsonWriter writer, BigInteger value, JsonSerializerOptions options)
    {
        writer.WriteStringValue(value.ToString());
    }
}