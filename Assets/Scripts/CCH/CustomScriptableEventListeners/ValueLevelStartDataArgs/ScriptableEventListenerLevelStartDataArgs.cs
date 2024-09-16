using AmuseEngine.Assets.Scripts.ScriptableEventListeners.Interface;
using AmuseEngine.Assets.Scripts.ScriptableEvents.BaseArgs;
using CCH.LevelStartInit.Controllers;
using UnityEngine;
using UnityEngine.Events;

namespace CCH.CustomScriptableEventListeners.ValueLevelStartDataArgs
{
    public class ScriptableEventListenerLevelStartDataArgs : MonoBehaviour, IScriptableEventListener<LevelStartDataStruct>
    {
        public ScriptableEventBaseArgs<LevelStartDataStruct> ScriptableEventToListen => scriptableEventVoidToListen;

        public UnityEvent<LevelStartDataStruct> Response => onScriptableEventResponse;

        [SerializeField]
        private ScriptableEventBaseArgs<LevelStartDataStruct> scriptableEventVoidToListen;

        [SerializeField]
        private UnityEvent<LevelStartDataStruct> onScriptableEventResponse;

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

        private void Callback(LevelStartDataStruct arg0)
        {
            onScriptableEventResponse.Invoke(arg0);
        }
    }
}
