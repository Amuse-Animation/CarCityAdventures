using System.Collections.Generic;
using AmuseEngine.Assets.Scripts.ArgsStructObjects.DragNDropArgs;
using AmuseEngine.Assets.Scripts.ArgsStructObjects.InputTouchInteractionArgs;
using AmuseEngine.Assets.Scripts.InputTouchInteraction.Interface.InputTouchInteraction;
using AmuseEngine.Assets.Scripts.InputTouchInteraction.Manager.SpawnWithInputTouchInteractionActivated;
using AmuseEngine.Assets.Scripts.PoolSystem.Manager;
using AmuseEngine.Assets.Scripts.UnityPoolableObjects.ControllerBase;
using AmuseEngine.Assets.Scripts.UnityPoolableObjects.Interface.PoolableObject;
using CCH.CustomArgsStructsObjects.RemedyPhaseObjectArgs;
using CCH.HealingClasses.Illness;
using CCH.HealingClasses.Remedy;
using CCH.HealingClasses.RemedyObjectSpawner;
using CCH.Illness.Controller.Dealer;
using CCH.Illness.Controller.Receiver;
using CCH.RemedyButton.Controller;
using UnityAtoms.BaseAtoms;
using UnityEngine;
using UnityEngine.Events;

namespace CCH.Illness.Manager
{
    public class IllnessManager : MonoBehaviour
    {
        public List<PoolableObjectController> RemedyButtonSpawnedObjectList => remedyButtonSpawnedObjectList;

        public List<PoolableObjectController> RemedyPhaseSpawnedObjectList => remedyPhaseSpawnedObjectList;

        [SerializeField]
        private GenericPoolSystemManager poolManager;

        [SerializeField] 
        private IllnessDealerController illnessDealerController;

        [SerializeField]
        private List<PoolableObjectController> remedyButtonSpawnedObjectList;
        
        [SerializeField]
        private List<PoolableObjectController> remedyPhaseSpawnedObjectList;

        [SerializeField]
        private UnityEvent<GameObject> onIllnessSpawnedReadyForInteraction;

        [SerializeField]
        private GameObjectEvent onIllnessSpawnedReadyForInteractionGameObjectEvent;

        [SerializeField]
        private UnityEvent<RemedyPhaseObjectArgs> onRemedyPhaseObjectsSpawned;

        Dictionary<IllnessReceiverController, PoolableObjectController> startingIllnessDictionary;

        private List<IInputTouchInteraction> spawnedPhaseInputTouchInteractionsList;
        
        public void ShowRemedyButton(IllnessReceiverController illnessReceiverController)
        {
            ReturnSpawnedIllnessElementsToPool();
            IllnessClass currentIllness = illnessReceiverController.AssignedIllness;
            if (currentIllness == null) return;
            if (currentIllness.HasPatientAlreadyTookEveryRemedy()) return;
            currentIllness.SetHasInitializedCurrentHealingPhaseState(false);
            RemedyButtonSpawnClass remedyButtonSpawnData = currentIllness.GetCurrentRemedyButtonData();
            IPoolableObject remedyButton = poolManager.GetPoolableObject(remedyButtonSpawnData.RemedyButtonPrefab,
                                                                                 (Vector2)remedyButtonSpawnData.RemedyButtonSpawningPointReferencePoint.position + remedyButtonSpawnData.RemedyButtonSpawningPositionOffset);
            AddElementToRemedyButtonList(remedyButton.PoolableObjectControllerComponent);
            remedyButton.MonoBehaviourComponent.TryGetComponent(out RemedyButtonController remedyButtonController);
            if (remedyButtonController == null) return;
            remedyButtonController.InitInteractionHandler(remedyButtonSpawnData.NonInteractedSprite, remedyButtonSpawnData.InteractedSprite);

            if (remedyButtonSpawnData.DragOrTouchObjectPrefab == null) return;
            IPoolableObject interactableObject = poolManager.GetPoolableObject(remedyButtonSpawnData.DragOrTouchObjectPrefab, remedyButton.MonoBehaviourComponent.transform.position);
            AddElementToRemedyButtonList(interactableObject.PoolableObjectControllerComponent);
            currentIllness.DoIncreaseRemedyButtonIndex();

            if(remedyButtonSpawnData.ShouldBeCreatedAlongIllnessAtTheSameTime)
            {
                IllnessSpawnerStruct illnessSpawnerData = currentIllness.GetCurrentIllnessData();
                IPoolableObject spawnedIlnnes = poolManager.GetPoolableObject(illnessSpawnerData.IllnessObject, (Vector2)illnessSpawnerData.SpawningPointReferencePoint.position + illnessSpawnerData.SpawningPositionOffset);
              //  currentIllness.DoIncreaseIllnessSpawnIndex();
                AddStartingIllnessToDictionary(illnessReceiverController, spawnedIlnnes.PoolableObjectControllerComponent);
              //  currentIllness.SetHasIncreasedSpawnIndexOnSpawningHealingPhaseObjects(true);
            }

            onIllnessSpawnedReadyForInteraction.Invoke(startingIllnessDictionary[illnessReceiverController].PoolableObjectControllerComponent.gameObject);

            if (onIllnessSpawnedReadyForInteractionGameObjectEvent != null)
                onIllnessSpawnedReadyForInteractionGameObjectEvent.Raise(startingIllnessDictionary[illnessReceiverController].PoolableObjectControllerComponent.gameObject);

            if (!remedyButtonSpawnData.ShouldBeCreatedAlongRemedyPhaseAtTheSameTime) return;
            SpawnPhaseObjects(illnessReceiverController);
            
        }

        public void SpawnRemedyPhaseObject(IllnessReceiverController illnessReceiverController, DragNDropArgsStruct dragNDropArgsStruct = default)
        {
            ReturnStartingIllnessSpawnedObjectToDictionary(illnessReceiverController);
            ShowRemedyButtonActivatedAndReturnNonUsedElementsToPool();
            SpawnPhaseObjects(illnessReceiverController, dragNDropArgsStruct);

            onRemedyPhaseObjectsSpawned.Invoke(new RemedyPhaseObjectArgs
            {
                RemedyPhaseIllnessReceiverController = illnessReceiverController,
                RemedyInputTouchInteractionList = spawnedPhaseInputTouchInteractionsList
            });
        }

        public void PatientCured(IllnessReceiverController illnessReceiverController)
        {
            if (!illnessReceiverController.AssignedIllness.HasPatientAlreadyTookEveryRemedy()) return;
            ReturnSpawnedIllnessElementsToPool();
        }

        public void InterruptTreatment(IllnessReceiverController illnessReceiverController)
        {
            ReturnSpawnedIllnessElementsToPool();
            illnessReceiverController.AssignedIllness.DoDecreaseRemedyButtonIndex();
            
            if(illnessReceiverController.AssignedIllness.HasInitializedCurrentHealingPhase)
            {
                illnessReceiverController.AssignedIllness.DoDecreaseRemedyPhaseIndex();
                illnessReceiverController.AssignedIllness.SetHasInitializedCurrentHealingPhaseState(false);
            }

            //if (illnessReceiverController.AssignedIllness.HasIncreasedSpawnIndexOnSpawningHealingPhaseObjects)
            //{
            //    illnessReceiverController.AssignedIllness.DoDecreaseIllnessSpawnIndex();
            //    illnessReceiverController.AssignedIllness.SetHasIncreasedSpawnIndexOnSpawningHealingPhaseObjects(false);
            //}

            if (illnessReceiverController.AssignedIllness.CurrentIllnessSpawnIndex == 1)
            {
                illnessDealerController.DoSpawnStartingIllness(illnessReceiverController, illnessReceiverController.AssignedIllness);
            }

        }

        public void AddStartingIllnessToDictionary(IllnessReceiverController illnessReceiver, PoolableObjectController spawnedObject)
        {
            if(startingIllnessDictionary == null)
                startingIllnessDictionary = new Dictionary<IllnessReceiverController, PoolableObjectController> ();

            if (startingIllnessDictionary.ContainsKey(illnessReceiver)) return;
            startingIllnessDictionary.Add(illnessReceiver, spawnedObject);
        }

        public void ReturnStartingIllnessSpawnedObjectToDictionary(IllnessReceiverController illnessReceiver)
        {
            if (startingIllnessDictionary == null) return;
            if (!startingIllnessDictionary.ContainsKey(illnessReceiver)) return;
            startingIllnessDictionary.TryGetValue(illnessReceiver, out PoolableObjectController poolableObject);
            poolManager.ReturnObjectToPool(poolableObject);
            startingIllnessDictionary.Remove(illnessReceiver);
        }
        
        public bool IsAlreadySpawnedAndStoragedInDictionary(IllnessReceiverController illnessReceiver)
        {
            if (startingIllnessDictionary == null) return false;
            return startingIllnessDictionary.ContainsKey(illnessReceiver);
        }

        private void ReturnSpawnedIllnessElementsToPool()
        {
            ReturnRemedyPhaseSpawnedObjectsElementsToPool();
            ReturnRemedyButtonsToPool();
        }
        

        private void ShowRemedyButtonActivatedAndReturnNonUsedElementsToPool()
        {
            for (int i = remedyButtonSpawnedObjectList.Count - 1; i >= 0; i--)
            {
                remedyButtonSpawnedObjectList[i].TryGetComponent(out RemedyButtonController remedyButtonController);
                if (remedyButtonController == null)
                {
                    poolManager.ReturnObjectToPool(remedyButtonSpawnedObjectList[i]);
                    continue;
                }
                remedyButtonController.ShowButtonActivated();
            }
        }
        
        private void AddElementToRemedyButtonList(PoolableObjectController element)
        {
            remedyButtonSpawnedObjectList.Add(element);
        }
        
        private void AddElementToRemedyPhaseSpawnedObjectsList(PoolableObjectController element)
        {
            remedyPhaseSpawnedObjectList.Add(element);
        }

        private void ReturnRemedyButtonsToPool()
        {
            if (remedyButtonSpawnedObjectList == null || remedyButtonSpawnedObjectList.Count <= 0) return;
            int pooledObjectListSize = remedyButtonSpawnedObjectList.Count;
            for (int i = 0; i < pooledObjectListSize; i++)
            {
                poolManager.ReturnObjectToPool(remedyButtonSpawnedObjectList[i]);
            }
            remedyButtonSpawnedObjectList.Clear();
        }
        
        private void ReturnRemedyPhaseSpawnedObjectsElementsToPool()
        {
            if (remedyPhaseSpawnedObjectList == null || remedyPhaseSpawnedObjectList.Count <= 0) return;
            int pooledObjectListSize = remedyPhaseSpawnedObjectList.Count;
            for (int i = 0; i < pooledObjectListSize; i++)
            {
                poolManager.ReturnObjectToPool(remedyPhaseSpawnedObjectList[i]);
            }
            remedyPhaseSpawnedObjectList.Clear();
        }
        
        
        private void SpawnPhaseObjects(IllnessReceiverController illnessReceiverController, DragNDropArgsStruct dragNDropArgsStruct = default)
        {
            Vector2 finalSpawnPoint = Vector2.zero;
            ReturnRemedyPhaseSpawnedObjectsElementsToPool();
            IllnessClass currentIllness = illnessReceiverController.AssignedIllness;
            if (currentIllness == null) return;
            if (currentIllness.HasPatientAlreadyTookEveryRemedy()) return;

            if (spawnedPhaseInputTouchInteractionsList == null)
                spawnedPhaseInputTouchInteractionsList = new List<IInputTouchInteraction>();
            
            spawnedPhaseInputTouchInteractionsList.Clear();
            
            currentIllness.SetHasInitializedCurrentHealingPhaseState(true);
            RemedyClass currentRemedyPhase = currentIllness.GetCurrentRemedyPhase();
            List<RemedyObjectsToSpawnClass> remedyPhaseSpawningObjectsList = currentIllness.GetCurrentRemedyObjectsToSpawnList();
            int remedyPhaseSpawningObjectsListSize = remedyPhaseSpawningObjectsList.Count;
            IPoolableObject remedyPhaseSpawnedObject = null;
            for (int i = 0; i < remedyPhaseSpawningObjectsListSize; i++)
            {
                RemedyObjectsToSpawnClass currentRemedyObjectToSpawn = remedyPhaseSpawningObjectsList[i];
                InputTouchInteractionArgsStruct remedyInteraction = currentRemedyPhase.RemedyInteractions;
                remedyInteraction.OwnerController = illnessReceiverController;

                // si no tiene ninguna instancia asignada se entiende que el struct tiene valores de default
                
                // if (dragNDropArgsStruct.DraggedTransform == null)
                //     currentRemedyObjectToSpawn.CanBeOverwrittenSpawningPoint = false;
                // else
                //     currentRemedyObjectToSpawn.CanBeOverwrittenSpawningPoint = true;

                
                if (currentRemedyObjectToSpawn.CanBeOverwrittenSpawningPoint)
                {
                    remedyInteraction.ShouldBeginInteracted = true;
                    remedyInteraction.DragNDropArgs = dragNDropArgsStruct;
                }
                
                //currentRemedyPhase.DoSetRemedyInteractions(remedyInteraction);

                if(currentRemedyObjectToSpawn.PoolableObject != null)
                {
                    finalSpawnPoint = (dragNDropArgsStruct.DraggedTransform != null && currentRemedyObjectToSpawn.CanBeOverwrittenSpawningPoint) ? dragNDropArgsStruct.ContactPointWithDroppingZone : currentRemedyObjectToSpawn.PoolableObjectSpawningPointReference.position;
                    remedyPhaseSpawnedObject = poolManager.GetPoolableObject(currentRemedyObjectToSpawn.PoolableObject,
                        finalSpawnPoint +
                        currentRemedyObjectToSpawn.PoolableObjectSpawningPositionOffset);

                    AddElementToRemedyPhaseSpawnedObjectsList(remedyPhaseSpawnedObject.PoolableObjectControllerComponent);

                    remedyPhaseSpawnedObject.MonoBehaviourComponent.TryGetComponent(out SpawnWithInputTouchInteractionActivatedManager remedyObjectController);
                    if (remedyObjectController == null) continue;
                    remedyObjectController.InitInteractionHandler(/*currentRemedyPhase.RemedyInteractions*/ remedyInteraction);
                    spawnedPhaseInputTouchInteractionsList.Add(remedyObjectController.CurrentInputInteraction);
                    illnessReceiverController.SetOwnedInputInteractionManager(remedyObjectController);
                   // illnessReceiverController.SetSpawnedInteractionTouch(remedyObjectController.CurrentInputInteraction);
                }
            }
            currentIllness.DoIncreaseRemedyPhaseIndex();
        }

    }
    
    

}