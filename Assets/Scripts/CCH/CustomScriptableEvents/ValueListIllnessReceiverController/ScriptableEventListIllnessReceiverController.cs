using System.Collections.Generic;
using AmuseEngine.Assets.Scripts.ScriptableEvents.BaseArgs;
using CCH.Illness.Controller.Receiver;
using UnityEngine;

namespace CCH.CustomScriptableEvents.ValueListIllnessReceiverController
{
    [CreateAssetMenu(fileName = "CustomScriptableListIllnessReceiverController", menuName = "CustomScriptableEventsArgs/ListIllnessReceiverController", order = 1)]
    public class ScriptableEventListIllnessReceiverController : ScriptableEventBaseArgs<List<IllnessReceiverController>>
    {
        
    }
}