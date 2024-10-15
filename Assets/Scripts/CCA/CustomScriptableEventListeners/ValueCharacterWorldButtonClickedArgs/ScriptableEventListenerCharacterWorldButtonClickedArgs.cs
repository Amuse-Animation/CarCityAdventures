using AmuseEngine.Assets.Scripts.ScriptableEventListeners.Interface;
using AmuseEngine.Assets.Scripts.ScriptableEvents.BaseArgs;
using CCA.CustomArgsStructObjects.MainMenuStruct.CharacterWorldButton;
using CCA.CustomArgsStructObjects.MainMenuStruct.CharacterWorldButtonClickArgs;
using UnityEngine;
using UnityEngine.Events;

namespace CCA.CustomScriptableEventListeners.ValueCharacterWorldButtonClickedArgs
{
    public class ScriptableEventListenerCharacterWorldButtonClickedArgs : MonoBehaviour, IScriptableEventListener<CharacterWorldButtonClickedArgsStruct>
    {
        public ScriptableEventBaseArgs<CharacterWorldButtonClickedArgsStruct> ScriptableEventToListen => scriptableEventVoidToListen;

        public UnityEvent<CharacterWorldButtonClickedArgsStruct> Response => onScriptableEventResponse;

        [SerializeField]
        private ScriptableEventBaseArgs<CharacterWorldButtonClickedArgsStruct> scriptableEventVoidToListen;

        [SerializeField]
        private UnityEvent<CharacterWorldButtonClickedArgsStruct> onScriptableEventResponse;

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

        private void Callback(CharacterWorldButtonClickedArgsStruct arg0)
        {
            onScriptableEventResponse.Invoke(arg0);
        }
    }
}