using CCH.LevelStartInit.Controllers;
using UnityEngine;
using UnityEngine.Events;

namespace CCH.LoadingScreenPlacer.Controllers
{
    public class LoadingScreenPlacerController : MonoBehaviour
    {
        [SerializeField]
        private Transform DoorTransform;

        [SerializeField]
        private UnityEvent onElementsPlaced;

        public void DoPlaceTransformElementsInPosition(LevelStartDataStruct levelStartDataStruct)
        {
            DoorTransform.transform.position = (Vector2) levelStartDataStruct.LevelStartPosition;
            //currentGameIconTransform.transform.position = (Vector2)levelStartDataStruct.LevelStartPosition;
            //mainCamera.TryGetComponent(out CinemachineVirtualCamera virtualCamera);
            //if(virtualCamera != null)
            //{
            //    virtualCamera.m_Lens.OrthographicSize = levelStartDataStruct.LensOrthoSize;
            //}

            onElementsPlaced.Invoke();
        }
    }
}