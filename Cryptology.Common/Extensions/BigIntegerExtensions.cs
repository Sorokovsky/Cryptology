using System.Numerics;

namespace Cryptology.Common.Extensions;

public static class BigIntegerExtensions
{
    public static byte[] TrimLeadingZeros(this byte[] bytes)
    {
        var i = bytes.Length - 1;
        while (i > 0 && bytes[i] == 0) i--;
        return bytes.Take(i + 1).ToArray();
    }

    public static byte[] ToFixedByteArray(this BigInteger value, int length)
    {
        var bytes = value.ToByteArray();
        if (bytes.Length > length)
            bytes = bytes.Take(length).ToArray();

        if (bytes.Length >= length) return bytes;
        var padded = new byte[length];
        Array.Copy(bytes, 0, padded, 0, bytes.Length);
        return padded;
    }

    public static int GetBlockSize(this BigInteger n)
    {
        var nBytes = n.ToByteArray();
        return nBytes.Length - 1;
    }

    public static List<BigInteger> SplitToBlocks(this byte[] data, int blockSize)
    {
        ArgumentOutOfRangeException.ThrowIfLessThan(blockSize, 1);

        var blocks = new List<BigInteger>();

        for (var i = 0; i < data.Length; i += blockSize)
        {
            var chunk = data.Skip(i).Take(blockSize).ToArray();
            var block = new BigInteger(chunk.Concat(new byte[] { 0 }).ToArray());
            blocks.Add(block);
        }

        return blocks;
    }


    public static BigInteger Inverse(this BigInteger number, BigInteger mod)
    {
        BigInteger m0 = mod, y = 0, x = 1;

        if (mod == 1) return 0;

        while (number > 1)
        {
            var q = number / mod;
            var t = mod;

            mod = number % mod;
            number = t;
            t = y;

            y = x - q * y;
            x = t;
        }

        if (x < 0)
            x += m0;

        return x;
    }
}