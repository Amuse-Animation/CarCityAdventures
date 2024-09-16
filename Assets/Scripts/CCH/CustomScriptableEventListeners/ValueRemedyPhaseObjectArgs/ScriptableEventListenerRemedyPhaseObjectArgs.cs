using AmuseEngine.Assets.Scripts.ScriptableEventListeners.Interface;
using AmuseEngine.Assets.Scripts.ScriptableEvents.BaseArgs;
using CCH.CustomArgsStructsObjects.RemedyPhaseObjectArgs;
using UnityEngine;
using UnityEngine.Events;

namespace CCH.CustomScriptableEventListeners.ValueRemedyPhaseObjectArgs
{
    public class ScriptableEventListenerRemedyPhaseObjectArgs : MonoBehaviour, IScriptableEventListener<RemedyPhaseObjectArgs>
    {
        public ScriptableEventBaseArgs<RemedyPhaseObjectArgs> ScriptableEventToListen => scriptableEventVoidToListen;

        public UnityEvent<RemedyPhaseObjectArgs> Response => onScriptableEventResponse;

        [SerializeField]
        private ScriptableEventBaseArgs<RemedyPhaseObjectArgs> scriptableEventVoidToListen;

        [SerializeField]
        private UnityEvent<RemedyPhaseObjectArgs> onScriptableEventResponse;

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

        private void Callback(RemedyPhaseObjectArgs arg0)
        {
            onScriptableEventResponse.Invoke(arg0);
        }
    }
}