using System.Numerics;
using Cryptology.Common.Utils;

namespace Cryptology.Encryptions.Rsa;

public class RsaGeneralKey : Key
{
    public RsaGeneralKey(BigInteger n, BigInteger eulerOfN, BigInteger e)
    {
        N = n;
        EulerOfN = eulerOfN;
        E = e;
    }

    public BigInteger N { get; }
    public BigInteger EulerOfN { get; }
    public BigInteger E { get; }

    public static RsaGeneralKey FromJson(string json)
    {
        return Serializer.Deserialize<RsaGeneralKey>(json);
    }
}