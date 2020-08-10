using MTools.Identifier;
using System.Collections.Generic;
using System.Diagnostics;

namespace MTools.Trigger.Event
{
    public class TriggerEvent
    {
        Dictionary<UniqueIdentifier, object> EventData = new Dictionary<UniqueIdentifier, object>();
        bool locked = false;
        public void Lock()
        {
            locked = true;
        }
        public void Set<T>(UniqueIdentifier<T> identifier, T data)
        {
            if(locked)
            {
                UnityEngine.Debug.LogWarning("Tried to add data to a locked TriggerEvent");
                return;
            }
                
            EventData[identifier] = data;
        }
        public T Get<T>(UniqueIdentifier<T> identifier)
        {
            if (EventData.TryGetValue(identifier, out object value))
                return default;

            return (T)value;
        }
    }
}