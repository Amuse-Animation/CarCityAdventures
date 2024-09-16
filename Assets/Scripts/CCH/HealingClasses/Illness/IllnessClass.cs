using System.Collections.Generic;
using AmuseEngine.Assets.Scripts.UnityPoolableObjects.ControllerBase;
using CCH.HealingClasses.Remedy;
using CCH.HealingClasses.RemedyObjectSpawner;
using UnityEngine;

namespace CCH.HealingClasses.Illness
{
    [System.Serializable]
    public class IllnessClass
    {
        public string IllnessNameId => illnessNameId;

        public int OwnerID => ownerID;

        public PatientIdEnum PatientIdEnum => patientIdEnum;

        public int CurrentRemedyIndex => currentRemedy;
        public int CurrentRemedyButtonIndex => currentRemedyButton;
        public int CurrentIllnessSpawnIndex => currentIllnessSpawn;

        public List<RemedyClass> RemediesPhasesList=> remediesPhasesList;

        public List<IllnessSpawnerStruct> IllnessSpawnsList => illnessSpawnsList;

        public List<RemedyButtonSpawnClass> RemedyButtonSpawnList => remedyButtonSpawnList;

        [SerializeField] 
        private string illnessNameId;

        [SerializeField]
        private List<IllnessSpawnerStruct> illnessSpawnsList;
        
        [SerializeField] 
        private List<RemedyButtonSpawnClass> remedyButtonSpawnList;
        
        [SerializeField]
        private List<RemedyClass> remediesPhasesList;

        private int ownerID;
        private int currentRemedy;
        private int currentRemedyButton;
        private int currentIllnessSpawn;
        private PatientIdEnum patientIdEnum;

        public bool HasInitializedCurrentHealingPhase => hasInitializedCurrentHealingPhase;
        private bool hasInitializedCurrentHealingPhase;

        public bool HasIncreasedSpawnIndexOnSpawningHealingPhaseObjects => hasIncreasedSpawnIndexOnSpawningHealingPhaseObjects;
        private bool hasIncreasedSpawnIndexOnSpawningHealingPhaseObjects;

        public IllnessSpawnerStruct GetCurrentIllnessData()
        {
            int illnessDataIndex = Mathf.Min(currentIllnessSpawn, remediesPhasesList.Count -1);
            return illnessSpawnsList[illnessDataIndex];
        }

        public IllnessSpawnerStruct GetCurrentDesiredIllnessData(int desiredIllnessDataIndex)
        {
            return illnessSpawnsList[desiredIllnessDataIndex];
        }

        public void DoDecreaseIllnessSpawnIndex()
        {
            currentIllnessSpawn--;
            
            if(currentIllnessSpawn > 0) return;
            currentIllnessSpawn = Mathf.Max(0, currentIllnessSpawn);
        }

        public void DoIncreaseIllnessSpawnIndex()
        {
            currentIllnessSpawn++;
            currentIllnessSpawn = Mathf.Min(currentIllnessSpawn, remediesPhasesList.Count);
        }

        public RemedyButtonSpawnClass GetCurrentRemedyButtonData()
        {
            return remedyButtonSpawnList[currentRemedyButton];
        }

        public void DoDecreaseRemedyButtonIndex()
        {
            currentRemedyButton--;
            currentRemedyButton = Mathf.Max(0, currentRemedyButton);
        }

        public void DoIncreaseRemedyButtonIndex()
        {
            currentRemedyButton++;
            currentRemedyButton = Mathf.Min(currentRemedyButton, remediesPhasesList.Count);
        }

        public bool HasPatientAlreadyTookEveryRemedy()
        {
            return currentRemedy >= remediesPhasesList.Count;
        }

        public RemedyClass GetCurrentRemedyPhase()
        {
            return remediesPhasesList[currentRemedy];
        }
        
        public void DoDecreaseRemedyPhaseIndex()
        {
            currentRemedy--;
            currentRemedy = Mathf.Max(0, currentRemedy);
        }

        public void DoIncreaseRemedyPhaseIndex()
        {
            currentRemedy++;
            currentRemedy = Mathf.Min(currentRemedy, remediesPhasesList.Count);
        }

        public List<RemedyObjectsToSpawnClass> GetCurrentRemedyObjectsToSpawnList()
        {
            if (currentRemedy >= remediesPhasesList.Count)
                return null;
            return remediesPhasesList[currentRemedy].ObjectsToSpawnList;
        }

        public bool IsIllnessName(string name)
        {
            if (string.IsNullOrEmpty(name)) return false;
            return illnessNameId.Contains(name);
        }

        public void SetHasInitializedCurrentHealingPhaseState(bool state)
        {
            hasInitializedCurrentHealingPhase = state;
        }

        public void SetHasIncreasedSpawnIndexOnSpawningHealingPhaseObjects(bool state)
        {
            hasIncreasedSpawnIndexOnSpawningHealingPhaseObjects = state;
        }

        public void SetOwnerId(int id)
        {
            ownerID = id;
            patientIdEnum = (PatientIdEnum)id;
        }
    }

    [System.Serializable]
    public class IllnessSpawnerStruct
    {
        public PoolableObjectController IllnessObject;
        public Transform SpawningPointReferencePoint;
        public Vector2 SpawningPositionOffset;
    }

    [System.Serializable]
    public class RemedyButtonSpawnClass
    {
        public int RemedyPhaseID => remedyPhaseID;
        [SerializeField] 
        private int remedyPhaseID;
        public Sprite NonInteractedSprite;
        public Sprite InteractedSprite;
        public PoolableObjectController RemedyButtonPrefab;
        public PoolableObjectController DragOrTouchObjectPrefab;
        public Transform RemedyButtonSpawningPointReferencePoint;
        public Vector2 RemedyButtonSpawningPositionOffset;
        public bool ShouldBeCreatedAlongIllnessAtTheSameTime;
        public bool ShouldBeCreatedAlongRemedyPhaseAtTheSameTime;
    }

    [System.Serializable]
    public enum PatientIdEnum
    {
        None = -1,
        FireTruck = 0,
        KidGreen = 1,
        Police = 2,
        RaceCar = 3,
    }
}