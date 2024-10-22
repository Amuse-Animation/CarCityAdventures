using System;
using AmuseEngine.Assets.Scripts.ScriptableVariables.UnityServicesData;
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
        private ScriptableVariableUnityServicesData scriptableVariableUnityServicesData;

        [SerializeField] 
        private Sprite lockedSpriteIcon;
        
        [SerializeField] 
        private Sprite downloadSpriteIcon;
        
        [SerializeField] 
        private Sprite playSpriteIcon;
        
        [SerializeField]
        private CarCityAdventureCharacterWorldButtonController carCityAdventureCharacterWorldButtonController;
        
        [SerializeField]
        private UnityEvent<CharacterWorldButtonClickedArgsStruct> onCharacterWorldButtonClicked;
        
        [SerializeField]
        private ScriptableEventCharacterWorldButtonClickedArgsStruct onCharacterWorldButtonClickedArgsScriptableEvent;

        private void OnEnable()
        {
            ShowCorrespondingCharacterWorldButtonIcon();
        }

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

        public void ShowCorrespondingCharacterWorldButtonIcon()
        {
            if (!scriptableVariableUnityServicesData.Value.IAPManager.UserHasAccessToContent)
            {
                carCityAdventureCharacterWorldButtonController.ChangeWorldButtonIcon(lockedSpriteIcon);
                return;
            }

            if (!HasWorldContentBeenPreviouslyDownloaded())
            {
                carCityAdventureCharacterWorldButtonController.ChangeWorldButtonIcon(downloadSpriteIcon);
                return;
            }

            carCityAdventureCharacterWorldButtonController.ChangeWorldButtonIcon(playSpriteIcon);
        }
        
        public bool HasWorldContentBeenPreviouslyDownloaded()
        {
            return carCityAdventureCharacterWorldButtonController.HasWorldContentBeenPreviouslyDownloaded();
        }
    }
}