using System.Collections.Generic;
using AmuseEngine.Assets.Scripts.ArgsStructObjects.InputTouchInteractionArgs;
using CCH.HealingClasses.RemedyObjectSpawner;
using UnityEngine;

namespace CCH.HealingClasses.Remedy
{
    [System.Serializable]
    public class RemedyClass
    {
        public int RemedyPhaseID => remedyPhaseID;
        public List<RemedyObjectsToSpawnClass> ObjectsToSpawnList => objectsToSpawnList;
        public InputTouchInteractionArgsStruct RemedyInteractions => remedyInteractions;

        [SerializeField] 
        private int remedyPhaseID; 
        
        [SerializeField]
        private List<RemedyObjectsToSpawnClass> objectsToSpawnList;

        [SerializeField]
        private InputTouchInteractionArgsStruct remedyInteractions;

        public void DoSetRemedyInteractions(InputTouchInteractionArgsStruct newInteractions)
        {
            remedyInteractions = newInteractions;
        }
    };
}