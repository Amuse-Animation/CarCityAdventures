using CCA.CustomArgsStructObjects.MainMenuStruct.CharacterWorldButtonClickArgs;
using CCA.MainMenuScripts.PanningCamera.Manager;
using DG.Tweening;
using UnityEngine;

namespace CCA.MainMenuScripts.WorldButtonInteraction.Behaviour
{
    public class CarCityAdventureWorldButtonInteractionBehaviour
    {
        private PanningCameraManager panningCameraManager;

        public CarCityAdventureWorldButtonInteractionBehaviour(PanningCameraManager panningCameraManager)
        {
            this.panningCameraManager = panningCameraManager;
        }

        public void MoveCameraTowardsWorldButton(CharacterWorldButtonClickedArgsStruct worldButtonClickedArgs, float movementDuration, System.Action onMovementStart = null, System.Action onMovementEnd = null)
        {
            Vector3 finalPoint = worldButtonClickedArgs.CharacterWorldButtonRoot.position;
            finalPoint.z = -10f;
            panningCameraManager.CameraToMove.DOLocalMove(finalPoint, movementDuration)
                .OnStart(
                    () =>
                    {
                        panningCameraManager.DoActivateAutomaticMovement();
                        onMovementStart?.Invoke();
                    }).OnComplete(() =>
                    {
                        panningCameraManager.DoDeactivateAutomaticMovement();
                        onMovementEnd?.Invoke();
                    });
        }
    }
}