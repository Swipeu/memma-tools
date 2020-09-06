using System;
using UnityEngine;

namespace MTools.Trigger
{
    public interface ITriggerTarget<T>
        where T : ITriggerEvent
    {
        void OnTrigger(T triggerEvent);
    }
}
