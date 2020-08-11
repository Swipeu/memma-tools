using UnityEngine;
using MTools.Reorderable;
using MTools.Identifier;

namespace MTools.Trigger
{
    public abstract class TriggerSource<T> : MonoBehaviour
        where T : ITriggerEvent
    {
        [SerializeField] ReorderableList<TriggerTarget> triggerTargets;

        protected void Trigger(T triggerEvent)
        {
            triggerTargets.ForEach(triggerTarget => 
            {
                if (triggerTarget == null)
                    return;

                triggerTarget.Trigger();

                if (!(triggerTarget is ITriggerTarget<T> convertedTriggerTarget))
                    return;

                convertedTriggerTarget.Trigger(triggerEvent); 
            });
        }
    }
}