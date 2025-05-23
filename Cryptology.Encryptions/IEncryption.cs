namespace Cryptology.Encryptions;

public interface IEncryption
{
    public byte[] Encrypt(byte[] input, Key key);

    public byte[] Decrypt(byte[] input, Key key);

    public Key GeneratePublicKey(Key key);

    public Key GeneratePrivateKey(Key key);

    public Key GenerateGeneralForKeys();
}