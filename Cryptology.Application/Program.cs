using System.Text;
using Cryptology.Encryptions.Rsa;

var encoding = Encoding.UTF8;
var encryption = new RsaEncryption();
const int n = 3233;
var encryptionKey = new RsaEncryptionKey(n, 17);
var privateKey = new RsaDecryptionKey(n, 2753);
const string inputString = "Hello, my third world";
var inputBytes = encoding.GetBytes(inputString);
var outputBytes = encryption.Encrypt(inputBytes, encryptionKey);
var outputString = Convert.ToBase64String(outputBytes);
outputBytes = Convert.FromBase64String(outputString);
var decryptBytes = encryption.Decrypt(outputBytes, privateKey);
var decryptedString = encoding.GetString(decryptBytes);

Console.WriteLine($"Input: {inputString}");
Console.WriteLine($"Encrypted: {outputString}");
Console.WriteLine($"Decrypted: {decryptedString}");