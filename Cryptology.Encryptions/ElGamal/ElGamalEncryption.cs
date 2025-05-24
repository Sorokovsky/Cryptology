using System.Numerics;
using Cryptology.Common.Extensions;
using Cryptology.Common.Generators;

namespace Cryptology.Encryptions.ElGamal;

public class ElGamalEncryption : IEncryption
{
    private readonly PrimeNumberGenerator _generator = new();
    private readonly int _keySize;

    public ElGamalEncryption(int keySize = 100)
    {
        _keySize = keySize;
    }

    public byte[] Encrypt(byte[] input, Key key)
    {
        throw new NotImplementedException();
    }

    public byte[] Decrypt(byte[] input, Key key)
    {
        throw new NotImplementedException();
    }

    public Key GeneratePublicKey(Key key)
    {
        if (key is not ElGamalForPublic forPublic) throw new ArgumentException(null, nameof(key));
        var y = BigInteger.ModPow(forPublic.G, forPublic.X, forPublic.P);
        return new ElGamalPublicKey(forPublic.P, y, forPublic.G);
    }

    public Key GeneratePrivateKey(Key key)
    {
        if (key is not ElGamalGeneralKey gamalGeneral) throw new ArgumentException(null, nameof(key));
        var x = _generator.Generate(_keySize);
        while (x <= 1 || x >= gamalGeneral.P - 1) x = _generator.Generate(_keySize);
        return new ElGamalPrivateKey(x);
    }

    public Key GenerateGeneralForKeys()
    {
        var p = _generator.Generate(_keySize);
        var g = p.CalculateGenerator();
        return new ElGamalGeneralKey(p, g);
    }
}