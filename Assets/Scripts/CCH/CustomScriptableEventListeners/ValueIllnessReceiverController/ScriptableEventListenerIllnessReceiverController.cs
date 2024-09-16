using AmuseEngine.Assets.Scripts.ScriptableEventListeners.Interface;
using AmuseEngine.Assets.Scripts.ScriptableEvents.BaseArgs;
using CCH.Illness.Controller.Receiver;
using UnityEngine;
using UnityEngine.Events;

namespace CCH.CustomScriptableEventListeners.ValueIllnessReceiverController
{
    public class ScriptableEventListenerIllnessReceiverController: MonoBehaviour, IScriptableEventListener<IllnessReceiverController>
    {
        public ScriptableEventBaseArgs<IllnessReceiverController> ScriptableEventToListen => scriptableEventVoidToListen;
 
        public UnityEvent<IllnessReceiverController> Response => onScriptableEventResponse;

        [SerializeField]
        private ScriptableEventBaseArgs<IllnessReceiverController> scriptableEventVoidToListen;

        [SerializeField]
        private UnityEvent<IllnessReceiverController> onScriptableEventResponse;

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

        private void Callback(IllnessReceiverController arg0)
        {
            onScriptableEventResponse.Invoke(arg0);
        }
    }
}