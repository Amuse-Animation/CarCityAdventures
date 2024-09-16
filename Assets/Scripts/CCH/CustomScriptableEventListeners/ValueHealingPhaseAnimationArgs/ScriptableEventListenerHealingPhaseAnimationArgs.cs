using AmuseEngine.Assets.Scripts.ScriptableEventListeners.Interface;
using AmuseEngine.Assets.Scripts.ScriptableEvents.BaseArgs;
using CCH.CustomArgsStructsObjects.HealingPhaseAnimationArgs;
using UnityEngine;
using UnityEngine.Events;

namespace CCH.CustomScriptableEventListeners.ValueHealingPhaseAnimationArgs
{
    public class ScriptableEventListenerHealingPhaseAnimationArgs : MonoBehaviour, IScriptableEventListener<HealingPhaseAnimationArgs>
    {
        public ScriptableEventBaseArgs<HealingPhaseAnimationArgs> ScriptableEventToListen => scriptableEventVoidToListen;

        public UnityEvent<HealingPhaseAnimationArgs> Response => onScriptableEventResponse;

        [SerializeField]
        private ScriptableEventBaseArgs<HealingPhaseAnimationArgs> scriptableEventVoidToListen;

        [SerializeField]
        private UnityEvent<HealingPhaseAnimationArgs> onScriptableEventResponse;

        #region UnityEvents

        private void OnEnable()
        {
            scriptableEventVoidToListen.OnScriptableEvent.AddListener(Callback);
        }

        private void OnDisable()
        {
            scriptableEventVoidToListen.OnScriptableEvent.RemoveListener(Callback);
        }

        #endregion

        private void Callback(HealingPhaseAnimationArgs arg0)
        {
            onScriptableEventResponse.Invoke(arg0);
        }
    }
}
