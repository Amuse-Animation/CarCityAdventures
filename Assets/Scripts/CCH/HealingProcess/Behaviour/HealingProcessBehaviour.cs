using System.Collections;
using System.Collections.Generic;
using AmuseEngine.Assets.Scripts.ArgsStructObjects.DragNDropArgs;
using AmuseEngine.Assets.Scripts.ArgsStructObjects.InputTouchInteractionArgs;
using AmuseEngine.Assets.Scripts.InputTouchInteraction.Manager.SpawnWithInputTouchInteractionActivated;
using AmuseEngine.Assets.Scripts.PoolSystem.Manager;
using AmuseEngine.Assets.Scripts.UnityPoolableObjects.ControllerBase;
using CCH.HealingClasses.Remedy;
using CCH.HealingClasses.RemedyObjectSpawner;
using CCH.Patient.Controllers;
using UnityEngine;

namespace CCH.HealingProcess.Behaviour
{
    public class HealingProcessBehaviour
    {
        private GenericPoolSystemManager poolManager;
        private List<PoolableObjectController> spawnedPoolableObjects;

        public HealingProcessBehaviour(GenericPoolSystemManager poolManager)
        {
            this.poolManager = poolManager;
        }

        public void CleanSpawnedObjectsList()
        {
            if(spawnedPoolableObjects == null) return;
            int spawnedObjectsListSize = spawnedPoolableObjects.Count;
            for (int i = 0; i < spawnedObjectsListSize; i++)
            {
                poolManager.ReturnObjectToPool(spawnedPoolableObjects[i]);
            }
            
            spawnedPoolableObjects.Clear();
        }
        
        public void ChangeRemedy(PatientController currentPatient, System.Action onNextRemedyPhase)
        {
            currentPatient.ApplyNextRemedyPhase();
            onNextRemedyPhase.Invoke();
        }
        
        public IEnumerator WaitForRemedyDelay(RemedyClass currentRemedy, System.Action<RemedyClass> onRemedyDelayBegin ,System.Action<RemedyClass> onRemedyDelayFinished)
        {
            onRemedyDelayBegin?.Invoke(currentRemedy);
            yield return new WaitForSeconds(currentRemedy.RemedyInteractions.InteractionDelayTime);
            onRemedyDelayFinished?.Invoke(currentRemedy);
        }
        
        
        public void InitRemedySpawnedObject(PoolableObjectController poolableObject, RemedyObjectsToSpawnClass remedyPhaseArgs, InputTouchInteractionArgsStruct remedyInteraction)
        {
            poolableObject.TryGetComponent(out SpawnWithInputTouchInteractionActivatedManager inputTouchInteractionManager);
            if (inputTouchInteractionManager != null)
            {
                inputTouchInteractionManager.InitInteractionHandler(remedyInteraction);
            }
            AddSpawnedObjectsToList(poolableObject);
        }

        public void PlaceRemedySpawnPointInFingerPosition(RemedyObjectsToSpawnClass remedyToSpawn, DragNDropArgsStruct dragNDropArgs)
        {
            remedyToSpawn.PoolableSpawningPoint = dragNDropArgs.ContactPointWithDroppingZone;
        }

        public void AllowToSpawnObjectAlreadyInteracted(RemedyClass currentRemedy, DragNDropArgsStruct dragNDropArgs)
        {
            //InputTouchInteractionArgsStruct remedyInteraction = currentRemedy.RemedyInteractions;
            //remedyInteraction.ShouldBeginInteracted = true;
            //remedyInteraction.DragNDropArgs = dragNDropArgs;
            //currentRemedy.DoSetRemedyInteractions(remedyInteraction);

        }
        
        public void AddSpawnedObjectsToList(PoolableObjectController poolableObjectController)
        {
            if (spawnedPoolableObjects == null)
                spawnedPoolableObjects = new List<PoolableObjectController>();
            
            spawnedPoolableObjects.Add(poolableObjectController);
        }
        
    }
}