using CCA.CustomArgsStructObjects.MainMenuStruct.CharacterWorldButtonClickArgs;
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

        public void Init(PanningCameraManager panningCameraManager)
        {
            carCityAdventureWorldButtonInteractionBehaviour = new CarCityAdventureWorldButtonInteractionBehaviour(panningCameraManager);
        }
        
        public void MoveCameraTowardsWorldButton(CharacterWorldButtonClickedArgsStruct worldButtonClickedArgs, System.Action onMovementStart = null, System.Action onMovementEnd = null)
        {
            carCityAdventureWorldButtonInteractionBehaviour.MoveCameraTowardsWorldButton(worldButtonClickedArgs, movementDuration, onMovementStart, onMovementEnd);
        }
    }
}