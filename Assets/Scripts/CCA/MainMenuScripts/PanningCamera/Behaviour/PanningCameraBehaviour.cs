using System.Collections.Generic;
using CW.Common;
using Lean.Touch;
using UnityEngine;

namespace CCA.MainMenuScripts.PanningCamera.Behaviour
{
    public class PanningCameraBehaviour
    {

        private Transform cameraToMove;
        private LeanScreenDepth screenDepth;
        private Vector3 remainingDelta;
        private List<LeanFinger> detectedFingerList;
        private bool isAutomaticMovementActive;
        
        public PanningCameraBehaviour(Transform cameraToMove, LeanScreenDepth screenDepth)
        {
            this.cameraToMove = cameraToMove;
            this.screenDepth = screenDepth;
        }
        
        public void DetectFingersOnScreen(LeanFingerFilter Use)
        {
            detectedFingerList = Use.UpdateAndGetFingers();
        }

        public void HandleCameraPanning(float sensitivity, float damping, float inertia)
        {
            if (isAutomaticMovementActive) return;
            // Get the last and current screen point of all fingers
            var lastScreenPoint = LeanGesture.GetLastScreenCenter(detectedFingerList);
            var screenPoint     = LeanGesture.GetScreenCenter(detectedFingerList);

            // Get the world delta of them after conversion
            var worldDelta = screenDepth.ConvertDelta(lastScreenPoint, screenPoint, cameraToMove.gameObject);

            // Store the current position
            var oldPosition = cameraToMove.localPosition;
                
            MoveCameraWithDampen(worldDelta, oldPosition, sensitivity, damping, inertia);

        }
        
        private void MoveCameraWithDampen(Vector3 worldDelta, Vector3 oldPosition,float sensitivity, float damping, float inertia)
        {
            // Pan the camera based on the world delta
            cameraToMove.position -= worldDelta * sensitivity;

            // Add to remainingDelta
            remainingDelta += cameraToMove.localPosition - oldPosition;

            // Get t value
            var factor = CwHelper.DampenFactor(damping, Time.deltaTime);

            // Dampen remainingDelta
            var newRemainingDelta = Vector3.Lerp(remainingDelta, Vector3.zero, factor);

            // Shift this position by the change in delta
            cameraToMove.localPosition = oldPosition + remainingDelta - newRemainingDelta;

            if (detectedFingerList.Count == 0 && inertia > 0.0f && damping > 0.0f)
            {
                newRemainingDelta = Vector3.Lerp(newRemainingDelta, remainingDelta, inertia);
            }

            // Update remainingDelta with the dampened value
            remainingDelta = newRemainingDelta;
        }

        public void ActivateAutomaticMovement()
        {
            isAutomaticMovementActive = true;
        }

        public void DeactivateAutomaticMovement()
        {
            isAutomaticMovementActive = false;
        }
        
    }
}