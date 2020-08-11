using UnityEngine;
using UnityEditor;
using MTools.Identifier;
using System;


[Serializable]
public class StringIdentifier<Type> : StringIdentifier
{
    public StringIdentifier(object stringIdentifier) : base(stringIdentifier) { }
}

[Serializable]
public class StringIdentifier : IdentifierBase
{
    public StringIdentifier(object stringIdentifier)
    {
        Identifier = stringIdentifier.ToString();
    }
}