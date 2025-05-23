using System.Numerics;
using Cryptology.EllipticalCurves;

namespace Cryptology.Tests;

public class EllipticalPointTests
{
    private readonly Field _field = new(1, 1, 23);
    private readonly Point _first = new(3, 10);
    private readonly Point _fourth = new(5, 5);
    private readonly BigInteger _lambdaForDifferent = 11;
    private readonly Point _second = new(9, 7);
    private readonly Point _third = new(17, 20);

    [Test]
    public void ShouldCorrectLambdaForDifferentPoints()
    {
        var result = Point.CalculateLambda(_first, _second, _field);
        Assert.That(result, Is.EqualTo(_lambdaForDifferent));
    }

    [Test]
    public void ShouldCorrectAddDifferentPoints()
    {
        var result = Point.Add(_first, _second, _field);
        Assert.That(result, Is.EqualTo(_third));
    }

    [Test]
    public void ShouldThirdPointCorrectly()
    {
        var result = _third.IsCorrect(_field);
        Assert.That(result, Is.True);
    }

    [Test]
    public void ShouldSecondPointCorrectly()
    {
        var result = _second.IsCorrect(_field);
        Assert.That(result, Is.True);
    }

    [Test]
    public void ShouldFirstPointCorrectly()
    {
        var result = _first.IsCorrect(_field);
        Assert.That(result, Is.True);
    }

    [Test]
    public void ShouldFourthPointNotCorrectly()
    {
        var result = _fourth.IsCorrect(_field);
        Assert.That(result, Is.False);
    }

    [Test]
    public void ShouldCorrectMultiply()
    {
        var g = new Point(0, 1);
        var field = new Field(1, 1, 5);
        var expected = new Point(2, 1);
        var result = Point.Multiply(3, g, field);
        Assert.That(result, Is.EqualTo(expected));
    }
}