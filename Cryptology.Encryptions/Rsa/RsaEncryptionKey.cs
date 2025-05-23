using System.Numerics;
using System.Text.Json.Serialization;
using Cryptology.Common.Utils;

namespace Cryptology.Encryptions.Rsa;

public class RsaEncryptionKey : Key
{
    public BigInteger N { get; }
    
    public BigInteger E { get; }

    [JsonConstructor]
    public RsaEncryptionKey(BigInteger n, BigInteger e)
    {
        N = n;
        E = e;
    }
    
    public static RsaEncryptionKey FromJson(string json) => Serializer.Deserialize<RsaEncryptionKey>(json);
}