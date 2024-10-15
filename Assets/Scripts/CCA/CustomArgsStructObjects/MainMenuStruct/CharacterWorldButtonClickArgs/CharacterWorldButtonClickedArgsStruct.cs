using CCA.MainMenuScripts.CharacterWorldButton.Manager;
using UnityEngine;

namespace CCA.CustomArgsStructObjects.MainMenuStruct.CharacterWorldButtonClickArgs
{
    [System.Serializable]
    public struct CharacterWorldButtonClickedArgsStruct
    {
        public CarCityAdventureCharacterWorldButtonManager CharacterWorldButtonClicked;
        public Transform CharacterWorldButtonRoot;
        public Transform CharacterWorldButtonModel;
        
    }
}