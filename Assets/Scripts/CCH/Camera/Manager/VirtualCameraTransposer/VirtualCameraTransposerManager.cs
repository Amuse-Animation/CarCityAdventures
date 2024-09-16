using System.Collections;
using AmuseEngine.Assets.Scripts.ArgsStructObjects.CameraTriggerArgs;
using AmuseEngine.Assets.Scripts.Cameras.Manager.CameraBrain;
using AmuseEngine.Assets.Scripts.Cameras.Manager.CinemachineBody.Transposer;
using UnityAtoms.BaseAtoms;
using UnityEngine;
using UnityEngine.Events;

namespace CCH.Camera.Manager.VirtualCameraTransposer
{
    public class VirtualCameraTransposerManager : MonoBehaviour
    {
        [SerializeField]
        private CameraBrainManagerController cameraBrainManagerController;

        [SerializeField]
        private CinemachineTransposerManagerController transposerManagerController;

        [SerializeField]
        UnityEvent onCameraTriggerEnterActivated;

        [SerializeField]
        UnityEvent onCameraTriggerExitActivated;

        [SerializeField]
        UnityEvent onCameraTriggerBlendingFinished;

        [SerializeField]
        private VoidEvent onCameraTriggerBlendingFinishedVoidEvent;


        public void OnCameraTriggerEnterEvent(CameraTriggerArgsStruct cameraTriggerArgsStruct)
        {
            transposerManagerController.ActivateCamera();
            transposerManagerController.SetCinemachineTrackedOffsetIgnoreZ(cameraTriggerArgsStruct.FollowTargetOffset);
            transposerManagerController.SetTransformToFollow(cameraTriggerArgsStruct.TransformToFollow);
            transposerManagerController.SetCameraInDesiredPositionIgnoreZ((Vector2)cameraTriggerArgsStruct.PlacingPoint.position + cameraTriggerArgsStruct.DesiredCameraPositionOffset);
            transposerManagerController.SetDesiredOrthoSize(cameraTriggerArgsStruct.DesiredOrthoSize);
            cameraBrainManagerController.SetBlendBetweenCameras(cameraTriggerArgsStruct.BlendingTime, cameraTriggerArgsStruct.DesiredBlendStyle);
            StartCoroutine(WaitForCameraBlend(cameraTriggerArgsStruct));
            onCameraTriggerEnterActivated.Invoke();
           
        }

        public void OnCameraTriggerExitEvent(CameraTriggerArgsStruct cameraTriggerArgsStruct)
        {
            ResetAndDeactivateCamera();
        }

        public void ResetAndDeactivateCamera()
        {
            cameraBrainManagerController.ResetBlendBetweenCamerasToOriginalValues();
            transposerManagerController.ResetLayerMaskToMainCamera();
            transposerManagerController.SetTransformToFollow(null);
            transposerManagerController.DeactivateCamera();
            onCameraTriggerExitActivated.Invoke();
        }

        IEnumerator WaitForCameraBlend(CameraTriggerArgsStruct cameraTriggerArgsStruct)
        {
            yield return new WaitForSeconds(cameraTriggerArgsStruct.BlendingTime);
            transposerManagerController.SetLayerMaskToMainCamera(cameraTriggerArgsStruct.LayerMaskToRenderInCamera);
            onCameraTriggerBlendingFinished.Invoke();

            if(onCameraTriggerBlendingFinishedVoidEvent  != null)
            {
                onCameraTriggerBlendingFinishedVoidEvent.Raise();
            }
        }
    }
}