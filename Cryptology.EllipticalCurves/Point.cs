using System.Numerics;

namespace Cryptology.EllipticalCurves;

public class Point
{
    public Point(BigInteger x, BigInteger y)
    {
        X = x;
        Y = y;
    }

    public Point(Point point) : this(point.X, point.Y)
    {
    }

    public BigInteger X { get; }

    public BigInteger Y { get; }
}