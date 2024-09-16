using System.Collections.Generic;
using AmuseEngine.Assets.Scripts.ArgsClassObjects.PoolableObjectSpawnPointArgs;
using AmuseEngine.Assets.Scripts.InputTouchInteractionOwner.Controller;
using CCH.HealingClasses.Illness;
using CCH.Illness.Controller.Dealer;
using UnityAtoms.BaseAtoms;
using UnityEngine;

namespace CCH.Illness.Controller.Receiver
{
    public class IllnessReceiverController : InputTouchInteractionOwnerController
    {
        public IllnessType AssignedIllnesType => illnessType;
        public IllnessClass AssignedIllness => assignedIllness;
        public Animator PatientAnimator => patientAnimator;

        [SerializeField] 
        private Animator patientAnimator;

        [SerializeField] 
        private Color paintSprayColor;
        
        [SerializeField] 
        private ColorVariable paintSprayColorVariable;
        
        [SerializeField]
        private List<IllnessSpawningPoints> illnessSpawningPointsList;

        IllnessType illnessType;
        private IllnessClass assignedIllness;

        public void SetSpanwingPointsInAssignesIllnessClass(IllnessClass illnessClass, IllnessType assignedIllnessType, out bool hasSetSpawningPointsSuccessfully)
        {
            hasSetSpawningPointsSuccessfully = false;

            IllnessSpawningPoints desiredSpawningPoint = null;
            int illnessSpawningPointsListSize = illnessSpawningPointsList.Count;
            for (int i = 0; i < illnessSpawningPointsListSize; i++)
            {
                if (!illnessSpawningPointsList[i].IsIllnessName(illnessClass.IllnessNameId)) continue;
                desiredSpawningPoint = illnessSpawningPointsList[i];
                break;
            }

            if (desiredSpawningPoint == null) return;
            if (!desiredSpawningPoint.IsIllnessName(illnessClass.IllnessNameId)) return;

            if (illnessClass.IllnessSpawnsList != null && illnessClass.IllnessSpawnsList.Count > 0)
            {
                int startIllnessSpawnSize = illnessClass.IllnessSpawnsList.Count;
                for (int i = 0; i < startIllnessSpawnSize; i++)
                {
                    illnessClass.IllnessSpawnsList[i].SpawningPointReferencePoint = desiredSpawningPoint.StartingIllnessSpawningPoints[i].PoolableObjectSpawningPointReference;
                    illnessClass.IllnessSpawnsList[i].SpawningPositionOffset = desiredSpawningPoint.StartingIllnessSpawningPoints[i].PoolableObjectSpawningPositionOffset;
                }
            }

            if (illnessClass.RemedyButtonSpawnList != null && illnessClass.RemedyButtonSpawnList.Count > 0)
            {
                int buttonSpawnListSize = illnessClass.RemedyButtonSpawnList.Count;
                for (int i = 0; i < buttonSpawnListSize; i++)
                {
                    illnessClass.RemedyButtonSpawnList[i].RemedyButtonSpawningPointReferencePoint = desiredSpawningPoint.RemedyButtonSpawnList[i].PoolableObjectSpawningPointReference;
                    illnessClass.RemedyButtonSpawnList[i].RemedyButtonSpawningPositionOffset = desiredSpawningPoint.RemedyButtonSpawnList[i].PoolableObjectSpawningPositionOffset;
                }
            }

            if (illnessClass.RemediesPhasesList != null && illnessClass.RemediesPhasesList.Count > 0)
            {
                int remediesPhasesSize = illnessClass.RemediesPhasesList.Count;
                for (int i = 0; i < remediesPhasesSize; i++)
                {
                    int objectsToSpawnSize = illnessClass.RemediesPhasesList[i].ObjectsToSpawnList.Count;
                    for (int j = 0; j < objectsToSpawnSize; j++)
                    {
                        illnessClass.RemediesPhasesList[i].ObjectsToSpawnList[j].PoolableObjectSpawningPointReference = desiredSpawningPoint.RemediesPhasesSpawningPoints[i].RemediesPhasesSpawningPoints[j].PoolableObjectSpawningPointReference;
                        illnessClass.RemediesPhasesList[i].ObjectsToSpawnList[j].PoolableObjectSpawningPositionOffset = desiredSpawningPoint.RemediesPhasesSpawningPoints[i].RemediesPhasesSpawningPoints[j].PoolableObjectSpawningPositionOffset;
                    }
                }
            }
            assignedIllness = illnessClass;
            illnessType = assignedIllnessType;
            hasSetSpawningPointsSuccessfully = true;
            illnessSpawningPointsList.Clear();

            if(illnessType == IllnessType.Broken)
                paintSprayColorVariable.Value = paintSprayColor;
        }



    }

    [System.Serializable]   
    class IllnessSpawningPoints
    {
        public List<PoolableObjectSpawnPointArgsClass> StartingIllnessSpawningPoints => startingIllnessSpawningPoints;
        public List<RemediesSpawningPoints> RemediesPhasesSpawningPoints => remediesPhasesSpawningPoints;

        public List<PoolableObjectSpawnPointArgsClass> RemedyButtonSpawnList => remedyButtonSpawnList;

        [SerializeField]
        string illnessNameId;
        [SerializeField]
        private List<PoolableObjectSpawnPointArgsClass> startingIllnessSpawningPoints;
        [SerializeField]
        private List<PoolableObjectSpawnPointArgsClass> remedyButtonSpawnList;
        [SerializeField]
        private List<RemediesSpawningPoints> remediesPhasesSpawningPoints;

        public bool IsIllnessName(string name)
        {
            if (string.IsNullOrEmpty(name)) return false;
            return illnessNameId.Equals(name);
        }
    }

    [System.Serializable]
    class RemediesSpawningPoints
    {
        public List<PoolableObjectSpawnPointArgsClass> RemediesPhasesSpawningPoints;
    }
}