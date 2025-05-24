using System.Numerics;
using System.Text.Json.Serialization;
using Cryptology.Common.Utils;

namespace Cryptology.Encryptions.ElGamal;

public class ElGamalPublicKey : ElGamalGeneralKey
{
    [JsonConstructor]
    public ElGamalPublicKey(BigInteger p, BigInteger y, BigInteger g) : base(p, g)
    {
        Y = y;
    }

    public BigInteger Y { get; }

    public new static ElGamalPublicKey FromJson(string json)
    {
        return Serializer.Deserialize<ElGamalPublicKey>(json);
    }
}