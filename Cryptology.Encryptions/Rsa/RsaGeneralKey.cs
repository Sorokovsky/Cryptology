using System.Numerics;
using Cryptology.Common.Utils;

namespace Cryptology.Encryptions.Rsa;

public class RsaGeneralKey : Key
{
    public BigInteger N { get; }
    public BigInteger EulerOfN { get; }
    public BigInteger E { get; }

    public RsaGeneralKey(BigInteger n, BigInteger eulerOfN, BigInteger e)
    {
        N = n;
        EulerOfN = eulerOfN;
        E = e;
    }

    public static RsaGeneralKey FromJson(string json) => Serializer.Deserialize<RsaGeneralKey>(json);
}