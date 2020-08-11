using UnityEngine;
using UnityEditor;

namespace MTools.Identifier
{
    [IdentifierRoot]
    public static class IdentifierRootExample
    {
        public static UniqueIdentifier<int> test2 { get; } = new UniqueIdentifier<int>();

        public static class SubRoot
        {
            public static UniqueIdentifier<int> test2 { get; } = new UniqueIdentifier<int>();
        }
    }
}