using AmuseEngine.Assets.Scripts.CameraNavigation.PanningCamera.Manager;
using AmuseEngine.Assets.Scripts.InternetContent.AddressablesContent.Manager;
using CCA.CustomArgsStructObjects.MainMenuStruct.CharacterWorldButton;
using CCA.CustomArgsStructObjects.MainMenuStruct.CharacterWorldButtonClickedArgs;
using CCA.MainMenuScripts.CharacterWorldButton.Manager;
using CCA.MainMenuScripts.WorldButtonInteraction.Behaviour;
using Cysharp.Threading.Tasks;
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

        public void InitiateCharacterWorldButtonGame(AddressableContentManager addressableContentManager,CarCityAdventureCharacterWorldButtonManager characterWorldButtonManagerClicked, string sceneNameToUnload = "")
        {
            carCityAdventureWorldButtonInteractionBehaviour.InitiateCharacterWorldButtonGame(addressableContentManager, characterWorldButtonManagerClicked, sceneNameToUnload).Forget();
        }

        public UniTaskVoid InitiateCharacterWorldButtonGame(AddressableContentManager addressableContentManager,CarCityAdventureCharacterWorldButtonManager characterWorldButtonManagerClicked, string sceneNameToUnload = "", System.Action<float> onProgress = null, System.Action onCompleted = null)
        {
            return carCityAdventureWorldButtonInteractionBehaviour.InitiateCharacterWorldButtonGame(addressableContentManager, characterWorldButtonManagerClicked, sceneNameToUnload, onProgress, onCompleted);
        }
    }
}