using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

namespace MTools.Reorderable
{
    [Serializable]
    public class ReorderableList<Type> : List<Type>, ISerializationCallbackReceiver
    {
        [SerializeField] List<Type> serializedList = new List<Type>();

        public void OnAfterDeserialize()
        {
            this.Clear();
            AddRange(serializedList);
        }

        public void OnBeforeSerialize()
        {
            serializedList.Clear();
            serializedList.AddRange(this);
        }
    }
}
