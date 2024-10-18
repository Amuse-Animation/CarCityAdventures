using CCA.CustomArgsStructObjects.MainMenuStruct.CharacterWorldButton;
using CCA.CustomArgsStructObjects.MainMenuStruct.CharacterWorldButtonClickedArgs;
using CCA.CustomScriptableEvents.ValueCharacterWorldButtonClickedArgs;
using CCA.MainMenuScripts.CharacterWorldButton.Controller;
using UnityEngine;
using UnityEngine.Events;

namespace CCA.MainMenuScripts.CharacterWorldButton.Manager
{
    public class CarCityAdventureCharacterWorldButtonManager : MonoBehaviour
    {
        [SerializeField] 
        private Transform characterWorldButtonRoot; 
        
        [SerializeField] 
        private Transform characterWorldButtonModel;
        
        [SerializeField]
        private CarCityAdventureCharacterWorldButtonController carCityAdventureCharacterWorldButtonController;
        
        [SerializeField]
        private UnityEvent<CharacterWorldButtonClickedArgsStruct> onCharacterWorldButtonClicked;
        
        [SerializeField]
        private ScriptableEventCharacterWorldButtonClickedArgsStruct onCharacterWorldButtonClickedArgsScriptableEvent;

        public void OnCharacterWorldButtonClicked()
        {
            CharacterWorldButtonClickedArgsStruct characterWorldButtonClickedArgsStruct = new CharacterWorldButtonClickedArgsStruct
                {
                    CharacterWorldButtonClicked = this,
                    CharacterWorldButtonModel = characterWorldButtonModel,
                    CharacterWorldButtonRoot = characterWorldButtonRoot
                };
            
            onCharacterWorldButtonClicked.Invoke(characterWorldButtonClickedArgsStruct);
            
            if(onCharacterWorldButtonClickedArgsScriptableEvent != null)
                onCharacterWorldButtonClickedArgsScriptableEvent.InvokeEvent(characterWorldButtonClickedArgsStruct);
        }
        
        public CarCityAdventureCharacterWorldButtonDataArgsStruct GetCharacterWorldButtonDataArgs()
        {
            return carCityAdventureCharacterWorldButtonController.GetCharacterWorldButtonDataArgs();
        }
    }
}