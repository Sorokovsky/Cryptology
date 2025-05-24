using System.Numerics;
using System.Text.Json.Serialization;
using Cryptology.Common.Utils;

namespace Cryptology.Encryptions.ElGamal;

public class ElGamalGeneralKey : Key
{
    [JsonConstructor]
    public ElGamalGeneralKey(BigInteger p, BigInteger g)
    {
        P = p;
        G = g;
    }

    public BigInteger P { get; }
    public BigInteger G { get; }

    public static ElGamalGeneralKey FromJson(string json)
    {
        return Serializer.Deserialize<ElGamalGeneralKey>(json);
    }
}