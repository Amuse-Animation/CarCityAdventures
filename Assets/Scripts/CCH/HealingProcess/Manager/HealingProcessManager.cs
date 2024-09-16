using System.Collections.Generic;
using AmuseEngine.Assets.Scripts.ArgsStructObjects.CameraTriggerArgs;
using AmuseEngine.Assets.Scripts.ArgsStructObjects.DragNDropArgs;
using AmuseEngine.Assets.Scripts.PoolSystem.Manager;
using AmuseEngine.Assets.Scripts.UnityPoolableObjects.ControllerBase;
using CCH.HealingProcess.Behaviour;
using CCH.Illness.Controller.Receiver;
using CCH.Illness.Manager;
using UnityAtoms.BaseAtoms;
using UnityEngine;
using UnityEngine.Events;

namespace CCH.HealingProcess.Manager
{
    public class HealingProcessManager : MonoBehaviour
    {
        [SerializeField]
        IllnessManager illnessManager;

        [SerializeField]
        private GenericPoolSystemManager poolManager;

        [SerializeField]
        private BoolVariable isHealingModeActivated;

        [SerializeField] 
        private UnityEvent onHealingModeActivated;
        
        [SerializeField]
        private VoidEvent onHealingModeActivatedVoidEvent;

        [SerializeField]
        private UnityEvent<List<PoolableObjectController>> onSpawnRemedyPhaseObject;

        [SerializeField]
        private VoidEvent onSpawnRemedyPhaseObjectVoidEvent;

        [SerializeField]
        private UnityEvent<IllnessReceiverController> onHealingProcessCompleted;

        [SerializeField]
        private VoidEvent onHealingProcessCompletedVoidEvent;

        private IllnessReceiverController currentIllnessReceiverController;
        private HealingProcessBehaviour healingPhaseBehaviour;

        private void Awake()
        {
            healingPhaseBehaviour = new HealingProcessBehaviour(poolManager);
        }

        #region PublicMethods
        public void OnHealingModeActivated(CameraTriggerArgsStruct cameraTriggerArgs)
        {
            cameraTriggerArgs.Instigated.TryGetComponent(out IllnessReceiverController illnessReceiverController);
            if (illnessReceiverController == null) return;
            currentIllnessReceiverController = illnessReceiverController;
            PrepareRemedyButton();
            
            onHealingModeActivated.Invoke();
            if(onHealingModeActivatedVoidEvent != null)
                onHealingModeActivatedVoidEvent.Raise();

            isHealingModeActivated.Value = true;
        }

        public void OnInitializeHealingPhase()
        {
            if (currentIllnessReceiverController.AssignedIllness.HasPatientAlreadyTookEveryRemedy()) return;
            illnessManager.SpawnRemedyPhaseObject(currentIllnessReceiverController);
            OnRemedyObjectsSpawned();
        }
        
        public void OnInitializeHealingPhase(DragNDropArgsStruct dragNDropArgs)
        {
            if (currentIllnessReceiverController.AssignedIllness.HasPatientAlreadyTookEveryRemedy()) return;
            illnessManager.SpawnRemedyPhaseObject(currentIllnessReceiverController, dragNDropArgs);
            OnRemedyObjectsSpawned();
        }

        public void OnHealingPhaseCompleted()
        {
            currentIllnessReceiverController.AssignedIllness.DoIncreaseIllnessSpawnIndex();
            PrepareRemedyButton();
        }

        public void OnInterruptHealingMode()
        {
            illnessManager.InterruptTreatment(currentIllnessReceiverController);
            isHealingModeActivated.Value = false;
        }
        
        #endregion

        #region PrivateMethods
        
        private void PrepareRemedyButton()
        {
            if (currentIllnessReceiverController.AssignedIllness.HasPatientAlreadyTookEveryRemedy()) 
            {
                illnessManager.PatientCured(currentIllnessReceiverController);
                OnHealingProcessCompleted();
                currentIllnessReceiverController = null;
                return; 
            }
            illnessManager.ShowRemedyButton(currentIllnessReceiverController);
        }
        
        #endregion

        #region Events
        
        private void OnRemedyObjectsSpawned()
        {
            onSpawnRemedyPhaseObject.Invoke(illnessManager.RemedyPhaseSpawnedObjectList);

            if (onSpawnRemedyPhaseObjectVoidEvent != null)
                onSpawnRemedyPhaseObjectVoidEvent.Raise();
        }
        
        private void OnHealingProcessCompleted()
        {
            onHealingProcessCompleted.Invoke(currentIllnessReceiverController);

            if (onHealingProcessCompletedVoidEvent != null)
                onHealingProcessCompletedVoidEvent.Raise();

            isHealingModeActivated.Value = false;
        }
        #endregion
    }
}