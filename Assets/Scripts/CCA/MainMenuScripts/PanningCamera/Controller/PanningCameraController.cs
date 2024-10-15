using System;
using CCA.MainMenuScripts.PanningCamera.Behaviour;
using CW.Common;
using Lean.Touch;
using UnityEngine;

namespace CCA.MainMenuScripts.PanningCamera.Controller
{
    public class PanningCameraController : LeanDragCamera
    {
        private PanningCameraBehaviour panningCameraBehaviour;

        public void Init(Transform cameraToMove)
        {
            panningCameraBehaviour = new PanningCameraBehaviour(cameraToMove, ScreenDepth);
        }
        
        protected override void LateUpdate()
        {
            panningCameraBehaviour.DetectFingersOnScreen(Use);
            panningCameraBehaviour.HandleCameraPanning(Sensitivity, Damping, Inertia);
        }
        
        public void ActivateAutomaticMovement()
        {
            panningCameraBehaviour.ActivateAutomaticMovement();
        }

        public void DeactivateAutomaticMovement()
        {
            panningCameraBehaviour.DeactivateAutomaticMovement();
        }
    }
    
#if UNITY_EDITOR
    namespace Lean.Touch.Editor
    {
        using UnityEditor;

        [CanEditMultipleObjects]
        [CustomEditor(typeof(PanningCameraController), true)]
        public class CarCityAdventurePanningCameraManager_Editor : CwEditor
        {
            protected override void OnInspector()
            {
                PanningCameraController tgt; PanningCameraController[] tgts; GetTargets(out tgt, out tgts);

                Draw("Use");
                Draw("ScreenDepth");
                Draw("sensitivity", "The movement speed will be multiplied by this.\n\n-1 = Inverted Controls.");
                Draw("damping", "If you want this component to change smoothly over time, then this allows you to control how quick the changes reach their target value.\n\n-1 = Instantly change.\n\n1 = Slowly change.\n\n10 = Quickly change.");
                Draw("inertia", "This allows you to control how much momentum is retained when the dragging fingers are all released.\n\nNOTE: This requires <b>Damping</b> to be above 0.");
                Draw("defaultPosition", "This allows you to set the target position value when calling the <b>ResetPosition</b> method.");
            }
        }
    }
#endif
}