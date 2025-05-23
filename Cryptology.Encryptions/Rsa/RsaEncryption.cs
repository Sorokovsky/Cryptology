using System.Numerics;
using Cryptology.Common.Extensions;
using Cryptology.Common.Generators;

namespace Cryptology.Encryptions.Rsa;

public class RsaEncryption : IEncryption
{
    private readonly int _keySize;

    public RsaEncryption(int keySize = 300)
    {
        _keySize = keySize;
    }

    public byte[] Encrypt(byte[] input, Key key)
    {
        if (key is not RsaEncryptionKey rsaEncryptionKey)
            throw new ArgumentException(null, nameof(key));
        var e = rsaEncryptionKey.E;
        var n = rsaEncryptionKey.N;
        var blockSize = n.GetBlockSize();
        return input.SplitToBlocks(blockSize)
            .Select(block => BigInteger.ModPow(block, e, n).ToFixedByteArray(blockSize + 1))
            .Aggregate((array, current) => array.Concat(current).ToArray());
    }

    public byte[] Decrypt(byte[] input, Key key)
    {
        if (key is not RsaDecryptionKey rsaDecryptionKey)
            throw new ArgumentException(null, nameof(key));
        var d = rsaDecryptionKey.D;
        var n = rsaDecryptionKey.N;
        var blockSize = n.GetBlockSize() + 1;
        var result = new List<byte>();
        for (var i = 0; i < input.Length; i += blockSize)
        {
            var blockBytes = input.Skip(i).Take(blockSize).ToArray();
            var block = new BigInteger(blockBytes);
            var decryptedBlock = BigInteger.ModPow(block, d, n);
            var plainBytes = decryptedBlock.ToByteArray().TrimLeadingZeros();
            result.AddRange(plainBytes);
        }

        return result.ToArray();
    }

    public Key GeneratePublicKey(Key key)
    {
        if(key is not RsaGeneralKey rsaGeneralKey) throw new ArgumentException(null, nameof(key));
        return new RsaEncryptionKey(rsaGeneralKey.N, rsaGeneralKey.E);
    }

    public Key GeneratePrivateKey(Key key)
    {
        if(key is not RsaGeneralKey rsaGeneralKey) throw new ArgumentException(null, nameof(key));
        var d = rsaGeneralKey.E.Inverse(rsaGeneralKey.EulerOfN);
        return new RsaDecryptionKey(rsaGeneralKey.N, d);
    }

    public Key GenerateGeneralForKeys()
    {
        var generator = new PrimeNumberGenerator();
        var p = generator.Generate(_keySize);
        var q = generator.Generate(_keySize);
        while(p == q) q = generator.Generate(_keySize);
        var n = p * q;
        var eulerOfN = (p - 1) * (q - 1);
        var e = generator.Generate(_keySize);
        while (BigInteger.GreatestCommonDivisor(e, eulerOfN) != 1)
        {
            e = generator.Generate(_keySize);
        }
        return new RsaGeneralKey(n, eulerOfN, e);
    }
}