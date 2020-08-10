using MTools.Reorderable;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class IdentifierExample : MonoBehaviour
{
    [SerializeField] MTools.Identifier.UniqueIdentifier<int> test13 = default;

    private void Start()
    {
    }

    [ContextMenu("Show value")]
    public void ShowValue()
    {
        Debug.Log(test13);
    }
}
