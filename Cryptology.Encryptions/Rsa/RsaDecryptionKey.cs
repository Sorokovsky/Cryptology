using System.Numerics;
using System.Text.Json.Serialization;
using Cryptology.Common.Utils;

namespace Cryptology.Encryptions.Rsa;

public class RsaDecryptionKey : Key
{
    public BigInteger N { get; }
    
    public BigInteger D { get; }

    [JsonConstructor]
    public RsaDecryptionKey(BigInteger n, BigInteger d)
    {
        N = n;
        D = d;
    }

    public static RsaDecryptionKey FromJson(string json) => Serializer.Deserialize<RsaDecryptionKey>(json);
}