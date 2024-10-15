using CCA.MainMenuScripts.PanningCamera.Controller;
using UnityEngine;

namespace CCA.MainMenuScripts.PanningCamera.Manager
{
    public class PanningCameraManager : MonoBehaviour
    {
        public Transform CameraToMove => cameraToMove;

        [SerializeField] 
        private Transform cameraToMove;
        
        [SerializeField]
        private PanningCameraController panningCameraController;

        private void Awake()
        {
            panningCameraController.Init(cameraToMove);
        }

        public void DoActivateAutomaticMovement()
        {
            panningCameraController.ActivateAutomaticMovement();
        }

        public void DoDeactivateAutomaticMovement()
        {
            panningCameraController.DeactivateAutomaticMovement();
        }
    }
    

}