using MTools.Reorderable;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public enum TestEnum 
{
    TEST1,
    TEST2,
    TEST3,
}

[Serializable]
public struct TestStruct 
{
    [SerializeField] string value1;
    [SerializeField] int value2;
}


public class ReorderableExample : MonoBehaviour
{
    [SerializeField] ReorderableList<TestEnum> test1;
    [SerializeField] ReorderableList<TestStruct> test2;
    [SerializeField] ReorderableDictionary<GameObject, string> test3;

    private void Start()
    {
        Debug.Log($"ReorderableDictionary: {{{string.Join("|", test3.Select(e => e.Key + ":" + e.Value))}}}");
    }
}
