using AmuseEngine.Assets.Scripts.UnityPoolableObjects.ControllerBase;
using UnityEngine;

namespace CCH.HealingClasses.RemedyObjectSpawner
{
    [System.Serializable]
    public class RemedyObjectsToSpawnClass
    {
        public PoolableObjectController PoolableObject => poolableObject;
        public Transform PoolableObjectSpawningPointReference
        {
            get
            {
                return poolableObjectSpawningPointReference;
            }
            set => poolableObjectSpawningPointReference = value;
        }
        public Vector2 PoolableObjectSpawningPositionOffset 
        {
            get => poolableObjectSpawningPositionOffset;
            set => poolableObjectSpawningPositionOffset = value;
        }

        public bool CanBeOverwrittenSpawningPoint
        {
            get => canBeOverwrittenSpawningPoint;
            set => canBeOverwrittenSpawningPoint = value;
        }
        public Vector2 PoolableSpawningPoint 
        { 
            get
            {
                
                if (canBeOverwrittenSpawningPoint)
                    return poolableSpawningPoint;
                else
                    return poolableObjectSpawningPointReference.position;
            }
            set => poolableSpawningPoint = value; 
        }


        [SerializeField]
        private PoolableObjectController poolableObject;
        [SerializeField]
        private Transform poolableObjectSpawningPointReference;
        [SerializeField]
        private Vector3 poolableObjectSpawningPositionOffset;
        [SerializeField]
        private bool canBeOverwrittenSpawningPoint;

        private Vector2 poolableSpawningPoint = Vector2.zero;


        public void SetStateToOverwriteSpawnPoint(bool state)
        {
            canBeOverwrittenSpawningPoint = state;
        }

    }
}