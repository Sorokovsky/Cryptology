using System.Numerics;
using System.Text.Json.Serialization;
using Cryptology.Common.Utils;

namespace Cryptology.Encryptions.ElGamal;

public class ElGamalForPublic : ElGamalPrivateKey
{
    [JsonConstructor]
    public ElGamalForPublic(BigInteger x, BigInteger g, BigInteger p) : base(x)
    {
        P = p;
        G = g;
    }

    public BigInteger P { get; }

    public BigInteger G { get; }

    public new static ElGamalForPublic FromJson(string json)
    {
        return Serializer.Deserialize<ElGamalForPublic>(json);
    }
}