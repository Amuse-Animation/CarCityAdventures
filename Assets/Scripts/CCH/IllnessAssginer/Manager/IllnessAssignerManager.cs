using System.Collections.Generic;
using AmuseEngine.Assets.Scripts.ExtensionsScipts;
using AmuseEngine.Assets.Scripts.UnityPool.Manager;
using AmuseEngine.Assets.Scripts.UnityPoolableObjects.ControllerBase;
using CCH.HealingClasses.Illness;
using CCH.Patient.Controllers;
using UnityEngine;

namespace CCH.IllnessAssginer.Manager
{
    public class IllnessAssignerManager : MonoBehaviour
    {
        [SerializeField] 
        private UnityObjectPoolManager poolManager;

        [SerializeField] 
        private List<PatientController> patientList;

        [SerializeField] 
        private List<Transform> spawnPointList;

        public void DoAssignIllnessToPatients()
        {
            if(patientList.Count != spawnPointList.Count) return;
            ShuffleLists();
            SpawnPatients();
        }

        private void ShuffleLists()
        {
            patientList.Shuffle();
            spawnPointList.Shuffle();
        }
        
        private void SpawnPatients()
        {
            int patientListSize = patientList.Count;
            PoolableObjectController currentSpawnedObject = null;
            for (int i = 0; i < patientListSize; i++)
            {
                patientList[i].TryGetComponent(out PoolableObjectController poolableObjectController);
                if(poolableObjectController == null) continue;
                currentSpawnedObject = poolManager.GetPoolableObject(poolableObjectController, spawnPointList[i].position);
                currentSpawnedObject.TryGetComponent(out PatientController patientController);
                if(patientController == null) continue;
                patientController.InitPatient(i);

                List<IllnessSpawnerStruct> illnessToSpawn = patientController.IllnessSpawnsList();

                if(illnessToSpawn != null && illnessToSpawn.Count > 0)
                {
                    int illnessSpawnListSize = illnessToSpawn.Count;

                    for (int j = 0; j < illnessSpawnListSize; j++)
                    {
                        poolManager.GetPoolableObject(illnessToSpawn[j].IllnessObject, (Vector2)spawnPointList[i].position + illnessToSpawn[j].SpawningPositionOffset);
                    }
                }
            }
        }
    }

    
}