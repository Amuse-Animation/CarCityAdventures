using CCA.CustomArgsStructObjects.MainMenuStruct.CharacterWorldButton;
using UnityEngine;

namespace CCA.MainMenuScripts.CharacterWorldButton.Controller
{
    public class CarCityAdventureCharacterWorldButtonController : MonoBehaviour
    {
        [SerializeField]
        private CarCityAdventureCharacterWorldButtonDataArgsStruct characterWorldButtonDataArgs;

        public CarCityAdventureCharacterWorldButtonDataArgsStruct GetCharacterWorldButtonDataArgs()
        {
            return characterWorldButtonDataArgs;
        }
        
    }
}