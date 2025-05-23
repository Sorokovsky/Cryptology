using Cryptology.Common.Utils;

namespace Cryptology.Encryptions;

public abstract class Key
{
    public override string ToString()
    {
        return Serializer.Serialize(this);
    }
}