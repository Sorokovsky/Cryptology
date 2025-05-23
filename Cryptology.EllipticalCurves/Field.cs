using System.Numerics;

namespace Cryptology.EllipticalCurves;

public class Field
{
    public Field(BigInteger a, BigInteger b, BigInteger p)
    {
        A = a;
        B = b;
        P = p;
    }

    public Field(Field field) : this(field.A, field.B, field.P)
    {
    }

    public BigInteger A { get; }

    public BigInteger B { get; }

    public BigInteger P { get; }

    public static bool operator ==(Field a, Field b)
    {
        return a.A == b.A && a.B == b.B && a.P == b.P;
    }

    public static bool operator !=(Field a, Field b)
    {
        return !(a == b);
    }
}