using System.Numerics;
using System.Text.Json.Serialization;
using Cryptology.Common.Utils;

namespace Cryptology.Encryptions.Rsa;

public class RsaEncryptionKey : Key
{
    [JsonConstructor]
    public RsaEncryptionKey(BigInteger n, BigInteger e)
    {
        N = n;
        E = e;
    }

    public BigInteger N { get; }

    public BigInteger E { get; }

    public static RsaEncryptionKey FromJson(string json)
    {
        return Serializer.Deserialize<RsaEncryptionKey>(json);
    }
}