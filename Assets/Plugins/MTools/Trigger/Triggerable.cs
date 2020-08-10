using MTools.Trigger.Event;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MTools.Trigger
{
    public abstract class Triggerable : MonoBehaviour
    {
        abstract public void Trigger(TriggerEvent triggerEvent);
    }
}
