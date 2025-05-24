using System.Numerics;
using Cryptology.Common.Extensions;

namespace Cryptology.Tests;

public class ModuleGeneratorTest
{
    [Test]
    public void ShouldCorrectGenerator()
    {
        var p = new BigInteger(467);
        var expected = new BigInteger(2);
        var result = p.CalculateGenerator();
        Assert.That(result, Is.EqualTo(expected));
    }
}