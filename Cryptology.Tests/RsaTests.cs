using System.Numerics;
using System.Text;
using Cryptology.Encryptions;
using Cryptology.Encryptions.Rsa;

namespace Cryptology.Tests;

public class RsaTests
{
    private const string ExpectedDecryptedString = "Hello, my third world";
    private const string ExpectedEncryptedString = "uAshBekC6QKJCKYCyAffCOcByAd0A3oIawxsCe0GyAdTBIkIbAnpAu0G";
    private readonly Encoding _encoding = Encoding.UTF8;
    private readonly RsaEncryption _encryption = new();
    private readonly BigInteger _n = 3233;
    private Key EncryptionKey => new RsaEncryptionKey(_n, 17);
    private Key DecryptionKey => new RsaDecryptionKey(_n, 2753);

    [Test]
    public void ShouldCorrectEncrypt()
    {
        var decryptedBytes = _encoding.GetBytes(ExpectedDecryptedString);
        var encryptedBytes = _encryption.Encrypt(decryptedBytes, EncryptionKey);
        var encryptedString = Convert.ToBase64String(encryptedBytes);
        Assert.That(encryptedString, Is.EqualTo(ExpectedEncryptedString));
    }

    [Test]
    public void ShouldCorrectDecrypt()
    {
        var encryptedBytes = Convert.FromBase64String(ExpectedEncryptedString);
        var decryptedBytes = _encryption.Decrypt(encryptedBytes, DecryptionKey);
        var decryptedString = _encoding.GetString(decryptedBytes);
        Assert.That(decryptedString, Is.EqualTo(ExpectedDecryptedString));
    }
}