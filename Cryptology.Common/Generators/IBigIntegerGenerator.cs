using System.Numerics;

namespace Cryptology.Common.Generators;

public interface IBigIntegerGenerator
{
    public BigInteger Generate(int bitLength, int millerRabinRounds);
}