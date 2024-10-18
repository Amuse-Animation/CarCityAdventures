using CCA.AddressablesContent.Manager;
using CCA.CustomArgsStructObjects.MainMenuStruct.CharacterWorldButton;
using CCA.CustomArgsStructObjects.MainMenuStruct.CharacterWorldButtonClickedArgs;
using CCA.MainMenuScripts.PanningCamera.Manager;
using CCA.MainMenuScripts.WorldButtonInteraction.Controller;
using UnityEngine;

namespace CCA.MainMenuScripts.WorldButtonInteraction.Manager
{
    public class CarCityAdventureWorldButtonInteractionManager : MonoBehaviour
    {
        [SerializeField]
        private PanningCameraManager panningCameraManager;
        
        [SerializeField]
        private AddressableContentManager addressableContentManager;
        
        [SerializeField]
        private CarCityAdventureWorldButtonInteractionController carCityAdventureWorldButtonInteractionController;

        private void Awake()
        {
            carCityAdventureWorldButtonInteractionController.Init();
        }

        public void OnWorldButtonClicked(CharacterWorldButtonClickedArgsStruct worldButtonClickedArgs)
        {
            carCityAdventureWorldButtonInteractionController.MoveCameraTowardsWorldButton(panningCameraManager, worldButtonClickedArgs, null,
                () =>
                {
                    carCityAdventureWorldButtonInteractionController.InitiateCharacterWorldButtonGame(addressableContentManager, worldButtonClickedArgs.CharacterWorldButtonClicked.GetCharacterWorldButtonDataArgs());
                });

        }
    }
}