using UnityEngine;
using UnityEditor;
using MTools.Reorderable;
using MTools.Trigger.Event;
using MTools.Identifier;

namespace MTools.Trigger
{
    public abstract class TriggerSource : MonoBehaviour
    {
        [SerializeField] ReorderableList<Triggerable> triggerTargets;

        TriggerEvent _currentEvent;
        TriggerEvent CurrentEvent
        {
            get
            {
                if (_currentEvent == null)
                    _currentEvent = new TriggerEvent();

                return _currentEvent;
            }
            set
            {
                _currentEvent = value;
            }
        }

        protected void Trigger()
        {
            CurrentEvent.Lock();
            TriggerEvent copy = CurrentEvent;
            CurrentEvent = null;
            triggerTargets.ForEach(t => { if (t != null) t.Trigger(copy); });
        }

        protected void Add<T>(UniqueIdentifier<T> identifier, T data)
            => CurrentEvent.Set(identifier, data);
    }
}