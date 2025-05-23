using System.Numerics;

namespace Cryptology.EllipticalCurves;

public class BasePoint
{
    public BasePoint(BigInteger a, BigInteger b, BigInteger p)
    {
        A = a;
        B = b;
        P = p;
    }

    public BasePoint(BasePoint basePoint) : this(basePoint.A, basePoint.B, basePoint.P)
    {
    }

    public BigInteger A { get; }

    public BigInteger B { get; }

    public BigInteger P { get; }

    public static bool operator ==(BasePoint a, BasePoint b)
    {
        return a.A == b.A && a.B == b.B && a.P == b.P;
    }

    public static bool operator !=(BasePoint a, BasePoint b)
    {
        return !(a == b);
    }
}