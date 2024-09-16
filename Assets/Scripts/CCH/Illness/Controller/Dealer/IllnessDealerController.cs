using System;
using System.Collections.Generic;
using AmuseEngine.Assets.Scripts.ExtensionsScipts;
using AmuseEngine.Assets.Scripts.PoolSystem.Manager;
using AmuseEngine.Assets.Scripts.UnityPoolableObjects.ControllerBase;
using AmuseEngine.Assets.Scripts.UnityPoolableObjects.Interface.PoolableObject;
using CCH.HealingClasses.Illness;
using CCH.IceBlock.Controller;
using CCH.Illness.Controller.Receiver;
using CCH.Illness.Manager;
using UnityEngine;
using UnityEngine.Events;

namespace CCH.Illness.Controller.Dealer
{
    public class IllnessDealerController : MonoBehaviour
    {
        public UnityEvent<List<IllnessReceiverController>> OnPatientsSpawned => onPatientsSpawned;

        [SerializeField]
        private GenericPoolSystemManager poolManager;

        [SerializeField]
        private IllnessManager illnessManager;

        [SerializeField]
        private bool canStorageRepeatedIllness;

        [SerializeField]
        private bool shouldRandomizeSpawnPoints;

        [SerializeField]
        private List<Transform> spawnPointsToUse;

        [SerializeField]
        private List<IllnessAssignerArgsStruct> illnessAssignation;

        [SerializeField]
        private List<IllnessClass> illnessList;

        [SerializeField] 
        private List<IllnessReceiverController> spawnedPatientsList;

        [SerializeField] 
        private UnityEvent<List<IllnessReceiverController>> onPatientsSpawned;
        
        [SerializeField] 
        private UnityEvent<IllnessReceiverController> onStartingIllnessSpawned;


        public void PrepaprePatiens()
        {
            RandomizeIllnessess();
            
            if(shouldRandomizeSpawnPoints)
                RandomizeSpawningPoints();

            SpawnPatients();
            
            onPatientsSpawned.Invoke(spawnedPatientsList);
        }
        
        public void DoSpawnStartingIllness(IllnessReceiverController illnessReceiver, IllnessClass illnessToAssign)
        {
            if(illnessManager.IsAlreadySpawnedAndStoragedInDictionary(illnessReceiver)) return;
            IllnessSpawnerStruct currentIllness = illnessToAssign.GetCurrentDesiredIllnessData(0);
            IPoolableObject spawnedIlnnes = poolManager.GetPoolableObject(currentIllness.IllnessObject, (Vector2)currentIllness.SpawningPointReferencePoint.position + currentIllness.SpawningPositionOffset);
            illnessManager.AddStartingIllnessToDictionary(illnessReceiver, spawnedIlnnes.PoolableObjectControllerComponent);
            
            onStartingIllnessSpawned.Invoke(illnessReceiver);

            spawnedIlnnes.MonoBehaviourComponent.TryGetComponent(out IceBlockController iceBlockController);
            if(iceBlockController == null) return;
            iceBlockController.SetAssignIllness(illnessReceiver);
        }
        

        private void RandomizeIllnessess()
        {
            List<IllnessType> illness = new List<IllnessType>();
            List<IllnessType> illnessRandom = new List<IllnessType>();
            for (int i = 0; i < illnessAssignation.Count; i++)
            {
                if (illnessAssignation[i].DesiredIllnessType != IllnessType.Random && illnessAssignation[i].DesiredIllnessType != IllnessType.NONE)
                {
                    illness.Add(illnessAssignation[i].DesiredIllnessType);
                }

                else if (illnessAssignation[i].DesiredIllnessType == IllnessType.Random)
                {
                    illnessRandom.Add(illnessAssignation[i].DesiredIllnessType);
                }
            }
            int startingPoint = illness.Count;
            int enumSize = Enum.GetNames(typeof(IllnessType)).Length;

            while (illnessRandom.Count > 0)
            {
                if (!canStorageRepeatedIllness && illness.Count >= enumSize)
                {
                    illnessRandom.Clear();
                    break;
                }
                int randomValue = UnityEngine.Random.Range(0, enumSize);
                IllnessType randomIllness = (IllnessType)randomValue;
                if (randomIllness == IllnessType.NONE || randomIllness == IllnessType.Random || illness.Contains(randomIllness)) continue;
                illness.Add((IllnessType)randomValue);
                illnessRandom.RemoveAt(0);
            }
            for (int i = 0; i < illnessAssignation.Count; i++)
            {
                if (illnessAssignation[i].DesiredIllnessType != IllnessType.Random) continue;
                illnessAssignation[i].DesiredIllnessType = illness[startingPoint];
                startingPoint++;
            }
        }

        private void RandomizeSpawningPoints()
        {
            int illnessAssignationListSize = illnessAssignation.Count;
            for (int i = 0; i < illnessAssignationListSize; i++)
            {
                if(spawnPointsToUse.Contains(illnessAssignation[i].PatientPrefabSpawnPoint)) continue;
                spawnPointsToUse.Add(illnessAssignation[i].PatientPrefabSpawnPoint);
            }

            spawnPointsToUse.Shuffle();
            for (int i = 0; i < illnessAssignationListSize; i++)
            {
                illnessAssignation[i].PatientPrefabSpawnPoint = spawnPointsToUse[i];
            }
        }

        private void SpawnPatients()
        {
            if (spawnedPatientsList == null)
                spawnedPatientsList = new List<IllnessReceiverController>();
            
            int illnessAssignationListSize = illnessAssignation.Count;
            IPoolableObject currentSpawnedObject = null;
            for (int i = 0; i < illnessAssignationListSize; i++)
            {
                currentSpawnedObject = poolManager.GetPoolableObject(illnessAssignation[i].PatientPrefab, illnessAssignation[i].PatientPrefabSpawnPoint.position);
                currentSpawnedObject.MonoBehaviourComponent.TryGetComponent(out IllnessReceiverController illnessReceiverController);
                if (illnessReceiverController == null) return;
                SetIllness(illnessReceiverController, illnessAssignation[i].DesiredIllnessType, illnessAssignation[i].CarID);
                spawnedPatientsList.Add(illnessReceiverController);
            }
        }

        private void SetIllness(IllnessReceiverController illnessReceiver, IllnessType assignedIllnessType, int ownerId)
        {
            if (assignedIllnessType == IllnessType.Random || assignedIllnessType == IllnessType.NONE) return;
            IllnessClass illnessToAssign = null;
            int illnessListSize = illnessList.Count;
            for (int i = 0; i < illnessListSize; i++)
            {
                if (!illnessList[i].IsIllnessName(assignedIllnessType.ToString())) continue;
                illnessToAssign = illnessList[i];
                illnessToAssign.SetOwnerId(ownerId);
                break;
            }
            if(illnessToAssign == null) return;
            bool hasAssignedSpawningPointSuccessfully = false;
            illnessReceiver.SetSpanwingPointsInAssignesIllnessClass(illnessToAssign, assignedIllnessType, out hasAssignedSpawningPointSuccessfully);
            if (!hasAssignedSpawningPointSuccessfully) return;
            SpawnStartingIllness(illnessReceiver, illnessToAssign);
        }

        private void SpawnStartingIllness(IllnessReceiverController illnessReceiver, IllnessClass illnessToAssign)
        {
            IllnessSpawnerStruct currentIllness = illnessToAssign.GetCurrentIllnessData();
            IPoolableObject spawnedIlnnes = poolManager.GetPoolableObject(currentIllness.IllnessObject, (Vector2)currentIllness.SpawningPointReferencePoint.position + currentIllness.SpawningPositionOffset);
            illnessToAssign.DoIncreaseIllnessSpawnIndex();
            illnessManager.AddStartingIllnessToDictionary(illnessReceiver, spawnedIlnnes.PoolableObjectControllerComponent);

            spawnedIlnnes.MonoBehaviourComponent.TryGetComponent(out IceBlockController iceBlockController);
            if(iceBlockController == null) return;
            iceBlockController.SetAssignIllness(illnessReceiver);
        }
    }


    [System.Serializable]
    public class IllnessAssignerArgsStruct
    {
        public int CarID;
        public PoolableObjectController PatientPrefab;
        public Transform PatientPrefabSpawnPoint;
        public IllnessType DesiredIllnessType;
    };

    [System.Serializable]
    public enum IllnessType
    {
        NONE,
        Frozen,
        Branch,
        Broken,
        RedEyes,
        Random
    };
}