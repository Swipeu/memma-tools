using System;

namespace MTools.Identifier
{
    [Serializable]
    public class UniqueIdentifier<Type> : UniqueIdentifier
    {
        public UniqueIdentifier() : base() { }
    }

    [Serializable]
    public class UniqueIdentifier : IdentifierBase
    {
        public UniqueIdentifier()
        {
            Identifier = Guid.NewGuid().ToString();
        }
    }
}
