using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace MTools.Trigger
{
    public interface ITriggerTargetUnspecified
    {
        void OnTrigger(ITriggerEvent triggerEvent);
    }
}
