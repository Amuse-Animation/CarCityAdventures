using UnityEngine;

namespace CCH.CustomArgsStructsObjects.PoolableObjectArgsClass
{
    [System.Serializable]
    public class AmbulanceCustomPoolableObjectArgsClass
    {
        public Transform Instigator;
        public Transform VFXPlacingPoint;
        public Vector3 VFXPlacingPointOffset;

        public AmbulanceCustomPoolableObjectArgsClass(Transform instigator, Transform vFXPlacingPoint, Vector3 vFXPlacingPointOffset)
        {
            Instigator = instigator;
            VFXPlacingPoint = vFXPlacingPoint;
            VFXPlacingPointOffset = vFXPlacingPointOffset;
        }
    }
}