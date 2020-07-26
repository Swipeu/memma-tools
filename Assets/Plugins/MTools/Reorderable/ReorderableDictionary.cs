using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MTools.Reorderable
{
    [Serializable]
    public class ReorderableDictionary<Key, Value> : Dictionary<Key, Value>, ISerializationCallbackReceiver
    {
   
        [Serializable]
        public struct SerializableKeyValuePair
        {
            [SerializeField] public Key key;
            [SerializeField] public Value value;
            [SerializeField, HideInInspector] public bool conflicting; // is key in conflict with another key in the dictionary

            public SerializableKeyValuePair(KeyValuePair<Key, Value> keyValuePair)
                : this(keyValuePair.Key, keyValuePair.Value) { }
            public SerializableKeyValuePair(Key key, Value value)
            {
                this.key = key;
                this.value = value;
                conflicting = false;
            }
        }

        [SerializeField] List<SerializableKeyValuePair> serializedList = new List<SerializableKeyValuePair>();

        public void OnAfterDeserialize()
        {
            this.Clear();

            for (int i = 0; i < serializedList.Count; i++)
            {
                var pair = serializedList[i];

                // Mark conflicting keys
                if (HasConflict(pair, serializedList))
                {
                    pair.conflicting = true;
                    serializedList[i] = pair;
                    continue;
                }

                pair.conflicting = false;
                serializedList[i] = pair;

                this[pair.key] = pair.value;
            }
        }

        public void OnBeforeSerialize()
        {
            var oldSerializedList = new List<SerializableKeyValuePair>(serializedList);
            serializedList.Clear();

            // Refresh old pairs
            foreach (var pair in oldSerializedList)
            {
                if (HasConflict(pair, oldSerializedList))
                {
                    serializedList.Add(pair);
                    continue;
                }

                if (!this.TryGetValue(pair.key, out Value value))
                    continue;

                serializedList.Add(new SerializableKeyValuePair(pair.key, value));
            }

            // Add new pairs
            foreach (var pair in this)
            {
                if (serializedList.Exists(e => e.key.Equals(pair.Key)))
                    continue;

                serializedList.Add(new SerializableKeyValuePair(pair));
            }
        }

        bool HasConflict(SerializableKeyValuePair pair, List<SerializableKeyValuePair> list)
        {
            if (IsKeyNull(pair))
            {
                return list.FindAll(e => IsKeyNull(e)).Count > 1;
            }

            return list.FindAll(e => !IsKeyNull(e) && e.key.Equals(pair.key)).Count > 1;
        }

        // Safe method to check if key is null outside of main thread
        bool IsKeyNull(SerializableKeyValuePair pair)
        {
            try
            {
                return pair.key.Equals(null);
            }
            catch (Exception)
            {
                return true;
            }
        }
    }
}
