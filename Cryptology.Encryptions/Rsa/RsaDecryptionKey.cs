using System.Numerics;
using System.Text.Json.Serialization;
using Cryptology.Common.Utils;

namespace Cryptology.Encryptions.Rsa;

public class RsaDecryptionKey : Key
{
    [JsonConstructor]
    public RsaDecryptionKey(BigInteger n, BigInteger d)
    {
        N = n;
        D = d;
    }

    public BigInteger N { get; }

    public BigInteger D { get; }

    public static RsaDecryptionKey FromJson(string json)
    {
        return Serializer.Deserialize<RsaDecryptionKey>(json);
    }
}