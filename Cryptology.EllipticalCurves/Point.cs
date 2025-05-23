using System.Numerics;
using System.Text.Json.Serialization;
using Cryptology.Common.Extensions;
using Cryptology.Common.Utils;

namespace Cryptology.EllipticalCurves;

public class Point
{
    [JsonConstructor]
    public Point(BigInteger x, BigInteger y, bool isInfinity = false)
    {
        X = x;
        Y = y;
        IsInfinity = isInfinity;
    }

    public Point(Point point) : this(point.X, point.Y, point.IsInfinity)
    {
    }

    public BigInteger X { get; }

    public BigInteger Y { get; }

    public bool IsInfinity { get; }

    public static Point Infinity => new(0, 0, true);

    private bool Equals(Point other)
    {
        return X.Equals(other.X) && Y.Equals(other.Y) && IsInfinity == other.IsInfinity;
    }

    public override bool Equals(object? obj)
    {
        if (obj is null) return false;
        if (ReferenceEquals(this, obj)) return true;
        return obj.GetType() == GetType() && Equals((Point)obj);
    }

    public override string ToString()
    {
        return Serializer.Serialize(this);
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(X, Y, IsInfinity);
    }

    public static BigInteger CalculateLambda(Point first, Point second, BasePoint basePoint)
    {
        BigInteger upper, downer;
        if (first == second)
        {
            upper = 3 * BigInteger.Pow(first.X, 2) + basePoint.A;
            downer = 2 * first.Y;
        }
        else
        {
            upper = second.Y - first.Y;
            downer = second.X - first.X;
        }

        if (downer == BigInteger.Zero) throw new DivideByZeroException();
        var inverseDowner = downer.Inverse(basePoint.P);
        var result = inverseDowner * upper;
        return result.Mod(basePoint.P);
    }

    public bool IsCorrect(BasePoint basePoint)
    {
        var first = BigInteger.ModPow(Y, 2, basePoint.P);
        var second = (BigInteger.Pow(X, 3) + basePoint.A * X + basePoint.B).Mod(basePoint.P);
        return first == second;
    }

    public Point InversedY(BasePoint basePoint)
    {
        var y = (BigInteger.MinusOne * Y).Mod(basePoint.P);
        return new Point(X, y, IsInfinity);
    }

    public static Point FromJson(string json)
    {
        return Serializer.Deserialize<Point>(json);
    }

    public static Point Add(Point first, Point second, BasePoint basePoint)
    {
        if (first.IsInfinity && second.IsInfinity is false) return second;
        if (!first.IsInfinity && second.IsInfinity) return first;
        if (second.IsInfinity && first.IsInfinity is false) return Infinity;
        try
        {
            var lambda = CalculateLambda(first, second, basePoint);
            var x = (BigInteger.ModPow(lambda, 2, basePoint.P) - first.X - second.X).Mod(basePoint.P);
            var y = (lambda * (first.X - x) - first.Y).Mod(basePoint.P);
            return new Point(x, y);
        }
        catch (DivideByZeroException)
        {
            return Infinity;
        }
    }

    public static Point Multiply(BigInteger first, Point second, BasePoint basePoint)
    {
        if (first == BigInteger.Zero) return Infinity;
        if (first < BigInteger.Zero) return Multiply(BigInteger.Abs(first), second.InversedY(basePoint), basePoint);
        if (first == BigInteger.One) return second;
        var result = second;
        for (var i = BigInteger.One; i < first; i++) result = Add(result, second, basePoint);

        return result;
    }

    public static bool operator ==(Point first, Point second)
    {
        return first.X == second.X && first.Y == second.Y;
    }

    public static bool operator !=(Point first, Point second)
    {
        return !(first == second);
    }
}