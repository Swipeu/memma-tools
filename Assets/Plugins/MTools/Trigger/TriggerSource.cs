using UnityEngine;
using MTools.Reorderable;
using MTools.Identifier;

namespace MTools.Trigger
{
    public abstract class TriggerSource<EventType> : MonoBehaviour
        where EventType : ITriggerEvent
    {
        [SerializeField] ReorderableList<MonoBehaviour> triggerTargets;

        protected void Trigger(EventType triggerEvent)
        {
            triggerTargets.ForEach(triggerTarget => 
            {
                if (triggerTarget == null)
                    return;

                if (!(triggerTarget is ITriggerTarget<EventType> convertedTriggerTarget))
                {
                    if(triggerTarget is ITriggerTargetUnspecified triggerTargetUnspecified)
                        triggerTargetUnspecified.OnTrigger(triggerEvent);

                    return;
                }

                convertedTriggerTarget.OnTrigger(triggerEvent); 
            });
        }
    }
}