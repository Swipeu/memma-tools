using UnityEngine;
using MTools.Reorderable;
using MTools.Identifier;

namespace MTools.Trigger
{
    public abstract class TriggerSource<EventType> : MonoBehaviour
        where EventType : ITriggerEvent
    {
        [SerializeField] ReorderableList<TriggerTarget> triggerTargets;

        protected void Trigger(EventType triggerEvent)
        {
            triggerTargets.ForEach(triggerTarget => 
            {
                if (triggerTarget == null)
                    return;

                triggerTarget.Trigger();

                if (!(triggerTarget is ITriggerTarget<EventType> convertedTriggerTarget))
                    return;

                convertedTriggerTarget.Trigger(triggerEvent); 
            });
        }
    }
}