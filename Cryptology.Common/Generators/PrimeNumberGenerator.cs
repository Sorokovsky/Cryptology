using System.Numerics;
using System.Security.Cryptography;

namespace Cryptology.Common.Generators;

public class PrimeNumberGenerator : IBigIntegerGenerator
{
    public BigInteger Generate(int bitLength, int millerRabinRounds = 40)
    {
        ArgumentOutOfRangeException.ThrowIfLessThan(bitLength, 2, nameof(bitLength));
        var randomizer = RandomNumberGenerator.Create();
        while (true)
        {
            var bytes = new byte[(bitLength + 7) / 8];
            randomizer.GetBytes(bytes);
            bytes[^1] |= 0b1000_0000;
            bytes[0] |= 1;
            var candidate = new BigInteger(bytes.Concat(new byte[] { 0 }).ToArray());

            if (IsPrime(candidate, millerRabinRounds))
                return candidate;
        }
    }
    
    private static bool IsPrime(BigInteger number, int limit)
    {
        if (number < 2) return false;
        if (number == 2 || number == 3) return true;
        if (number % 2 == 0) return false;
        var d = number - 1;
        var r = 0;
        while (d % 2 == 0)
        {
            d /= 2;
            r++;
        }
        var randomizer = RandomNumberGenerator.Create();
        var bytes = new byte[number.GetByteCount()];
        for (var i = 0; i < limit; i++)
        {
            BigInteger temp;
            do
            {
                randomizer.GetBytes(bytes);
                temp = new BigInteger(bytes) % (number - 3) + 2;
            } while (temp <= 1 || temp >= number - 1);
            var x = BigInteger.ModPow(temp, d, number);
            if(x == 1 || x == number - 1) continue;
            var passed = false;
            for (var j = 0; j < r - 1; j++)
            {
                x = BigInteger.ModPow(x, 2, number);
                if (x != number - 1) continue;
                passed = true;
                break;
            }

            if (passed is false) return false;
        }

        return true;
    }
}