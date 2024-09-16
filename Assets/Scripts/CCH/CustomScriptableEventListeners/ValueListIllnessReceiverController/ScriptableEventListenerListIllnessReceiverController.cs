using System.Collections.Generic;
using AmuseEngine.Assets.Scripts.ScriptableEventListeners.Interface;
using AmuseEngine.Assets.Scripts.ScriptableEvents.BaseArgs;
using CCH.Illness.Controller.Receiver;
using UnityEngine;
using UnityEngine.Events;

namespace CCH.CustomScriptableEventListeners.ValueListIllnessReceiverController
{
    public class ScriptableEventListenerListIllnessReceiverController: MonoBehaviour, IScriptableEventListener<List<IllnessReceiverController>>
    {
        public ScriptableEventBaseArgs<List<IllnessReceiverController>> ScriptableEventToListen => scriptableEventVoidToListen;

        public UnityEvent<List<IllnessReceiverController>> Response => onScriptableEventResponse;

        [SerializeField]
        private ScriptableEventBaseArgs<List<IllnessReceiverController>> scriptableEventVoidToListen;

        [SerializeField]
        private UnityEvent<List<IllnessReceiverController>> onScriptableEventResponse;

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

        private void Callback(List<IllnessReceiverController> arg0)
        {
            onScriptableEventResponse.Invoke(arg0);
        }
    }
}