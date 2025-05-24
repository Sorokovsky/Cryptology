using System.Numerics;
using System.Text.Json.Serialization;
using Cryptology.Common.Utils;

namespace Cryptology.Encryptions.ElGamal;

public class ElGamalPrivateKey : Key
{
    [JsonConstructor]
    public ElGamalPrivateKey(BigInteger x)
    {
        X = x;
    }

    public BigInteger X { get; }

    public static ElGamalPrivateKey FromJson(string json)
    {
        return Serializer.Deserialize<ElGamalPrivateKey>(json);
    }
}