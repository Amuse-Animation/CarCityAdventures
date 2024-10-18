using CCA.AddressablesContent.Manager;
using CCA.CustomArgsStructObjects.MainMenuStruct.CharacterWorldButton;
using CCA.CustomArgsStructObjects.MainMenuStruct.CharacterWorldButtonClickedArgs;
using CCA.MainMenuScripts.PanningCamera.Manager;
using CCA.MainMenuScripts.WorldButtonInteraction.Behaviour;
using UnityEngine;

namespace CCA.MainMenuScripts.WorldButtonInteraction.Controller
{
    public class CarCityAdventureWorldButtonInteractionController : MonoBehaviour
    {
        [SerializeField] 
        private float movementDuration = 1f;
        
        private CarCityAdventureWorldButtonInteractionBehaviour carCityAdventureWorldButtonInteractionBehaviour;

        public void Init()
        {
            carCityAdventureWorldButtonInteractionBehaviour = new CarCityAdventureWorldButtonInteractionBehaviour();
        }
        
        public void MoveCameraTowardsWorldButton(PanningCameraManager panningCameraManager,CharacterWorldButtonClickedArgsStruct worldButtonClickedArgs, System.Action onMovementStart = null, System.Action onMovementEnd = null)
        {
            carCityAdventureWorldButtonInteractionBehaviour.MoveCameraTowardsWorldButton(panningCameraManager,worldButtonClickedArgs, movementDuration, onMovementStart, onMovementEnd);
        }

        public void InitiateCharacterWorldButtonGame(AddressableContentManager addressableContentManager, CarCityAdventureCharacterWorldButtonDataArgsStruct worldButtonClickedArgs)
        {
            carCityAdventureWorldButtonInteractionBehaviour.InitiateCharacterWorldButtonGame(addressableContentManager, worldButtonClickedArgs).Forget();
        }
    }
}