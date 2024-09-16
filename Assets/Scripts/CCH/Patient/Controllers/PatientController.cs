using System.Collections.Generic;
using CCH.HealingClasses.Illness;
using CCH.HealingClasses.Remedy;
using CCH.HealingClasses.RemedyObjectSpawner;
using UnityEngine;

namespace CCH.Patient.Controllers
{
    public class PatientController : MonoBehaviour
    {
       
        [SerializeField] 
        private List<IllnessClass> illnessList;
        
        private IllnessClass assignedIllness;

        public void InitPatient(int patientIndex)
        {
            if(patientIndex >= illnessList.Count) return;
            assignedIllness = illnessList[patientIndex];
            
            illnessList.Clear();
        }

        public bool IsPatientAlreadyCured()
        {
            return assignedIllness.HasPatientAlreadyTookEveryRemedy();
        }
        
        public RemedyClass GetCurrentRemedy()
        {
            return assignedIllness.GetCurrentRemedyPhase();
        }

        public void ApplyNextRemedyPhase()
        {
            assignedIllness.DoIncreaseRemedyPhaseIndex();
        }

        public List<RemedyObjectsToSpawnClass> ObjectsToSpawnList()
        {
            return assignedIllness.GetCurrentRemedyObjectsToSpawnList(); 
        }

        public List<IllnessSpawnerStruct> IllnessSpawnsList()
        {
            return assignedIllness.IllnessSpawnsList;
        }
    }
}