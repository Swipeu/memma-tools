using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
        static int currentIdentifierNumber = int.MinValue;
        public UniqueIdentifier()
        {
            Identifier = GetAndModifyIdentifier().ToString();
        }

        private static int GetAndModifyIdentifier() 
            => currentIdentifierNumber++;
    }
}
