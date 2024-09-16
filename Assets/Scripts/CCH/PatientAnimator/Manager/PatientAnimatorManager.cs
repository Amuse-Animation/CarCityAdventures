using System.Collections.Generic;
using AmuseEngine.Assets.Scripts.HelplerStaticClasses.InputTouchInterfaceCaster;
using AmuseEngine.Assets.Scripts.InputTouchInteraction.Interface.InputTouchInteraction;
using AmuseEngine.Assets.Scripts.InputTouchInteraction.Interface.InteractionByAmountOfTouch;
using CCH.CustomArgsStructsObjects.HealingPhaseAnimationArgs;
using CCH.CustomArgsStructsObjects.RemedyPhaseObjectArgs;
using CCH.CustomScriptableEvents.ValueHealingPhaseAnimationArgs;
using CCH.Illness.Controller.Receiver;
using CCH.PatientAnimator.Behaviour;
using UnityEngine;

namespace CCH.PatientAnimator.Manager
{
    public class PatientAnimatorManager : MonoBehaviour
    {
        [SerializeField]
        private ScriptableEventHealingPhaseAnimationArgs scriptableEventHealingPhaseAnimationArgs;

        private Dictionary<IllnessReceiverController, PatientAnimatorBehaviour> patientAnimatorDictionary;
        private Dictionary<IllnessReceiverController, List<IInputTouchInteraction>> interactionListenersDictionary;

        private IllnessReceiverController currentSelectedPatient;

        public void OnPatientsSpawnedEvent(List<IllnessReceiverController> illnessReceiverControllersList)
        {
            if (patientAnimatorDictionary == null)
                patientAnimatorDictionary = new Dictionary<IllnessReceiverController, PatientAnimatorBehaviour>();

            int patientsSize = illnessReceiverControllersList.Count;
            IllnessReceiverController currentIllnessReceiverController = null;
            for (int i = 0; i < patientsSize; i++)
            {
                currentIllnessReceiverController = illnessReceiverControllersList[i]; 
                patientAnimatorDictionary.Add(currentIllnessReceiverController, new PatientAnimatorBehaviour(currentIllnessReceiverController.PatientAnimator, currentIllnessReceiverController.AssignedIllnesType));
            }
        }

        public void OnPlayCorrespondingPhaseAnimation(RemedyPhaseObjectArgs remedyButtonActivatedStruct)
        {
            if (remedyButtonActivatedStruct.RemedyInputTouchInteractionList.Count > 0)
            {
                if (interactionListenersDictionary == null)
                    interactionListenersDictionary = new Dictionary<IllnessReceiverController, List<IInputTouchInteraction>>();
                
                ListenToInputTouchChanges(remedyButtonActivatedStruct.RemedyPhaseIllnessReceiverController, remedyButtonActivatedStruct.RemedyInputTouchInteractionList);
            }
            
            currentSelectedPatient = remedyButtonActivatedStruct.RemedyPhaseIllnessReceiverController;
            patientAnimatorDictionary[currentSelectedPatient].OnPlayCorrespondingPhaseAnimation(remedyButtonActivatedStruct);

            scriptableEventHealingPhaseAnimationArgs.InvokeEvent(new HealingPhaseAnimationArgs(currentSelectedPatient.transform,
                                                                                               currentSelectedPatient.transform,
                                                                                               Vector3.zero,
                                                                                               currentSelectedPatient,
                                                                                               patientAnimatorDictionary[remedyButtonActivatedStruct.RemedyPhaseIllnessReceiverController].CurrentPlayingAnimation,
                                                                                               currentSelectedPatient.AssignedIllness.CurrentRemedyButtonIndex));
        }
        
        public void DoPlayOriginalAnimation(IllnessReceiverController illnessReceiverController)
        {
           patientAnimatorDictionary[illnessReceiverController].PlayOriginalAnimation();
        }

        public void ListenToInputTouchChanges(IllnessReceiverController illnessReceiverController, List<IInputTouchInteraction> inputTouchInteraction)
        {

            if (interactionListenersDictionary.ContainsKey(illnessReceiverController))
            {
                int listenersListSize = interactionListenersDictionary[illnessReceiverController].Count;
                IInteractionByAmountOfTouch touchAmountInterfaceToRemove = null;
                for (int i = 0; i < listenersListSize; i++)
                {
                    touchAmountInterfaceToRemove = InputTouchInterfaceCasterStaticClass.CastToAInheritorInterface<IInteractionByAmountOfTouch>(interactionListenersDictionary[illnessReceiverController][i]);
                    if (touchAmountInterfaceToRemove != null)
                    {
                        RemoveListenerToTouchEvents(touchAmountInterfaceToRemove);
                    }
                    else
                    {
                        if (interactionListenersDictionary[illnessReceiverController][i] == null) continue;
                        RemoveListenerToGenericTouchEvents(interactionListenersDictionary[illnessReceiverController][i]);
                    }
                }

                interactionListenersDictionary.Remove(illnessReceiverController);
            }
            
            int newListenersListSize = inputTouchInteraction.Count;
            IInteractionByAmountOfTouch currentTouchAmountInterface = null;
            for (int i = 0; i < newListenersListSize; i++)
            {
                currentTouchAmountInterface = InputTouchInterfaceCasterStaticClass.CastToAInheritorInterface<IInteractionByAmountOfTouch>(inputTouchInteraction[i]);
                if (currentTouchAmountInterface != null)
                {
                    ListenToTouchEvents(currentTouchAmountInterface);
                }
                else
                {
                    if (inputTouchInteraction[i] == null) continue;
                    ListenToGenericTouchEvents(inputTouchInteraction[i]);
                }
            }
            
            interactionListenersDictionary.Add(illnessReceiverController, inputTouchInteraction);
        }

        

        private void RemoveListenerToTouchEvents(IInteractionByAmountOfTouch touchInteractions)
        {
            touchInteractions.OnTouchPerfomed.RemoveAllListeners();
            touchInteractions.OnInputTouchInteractionFinished.RemoveAllListeners();
        }
        
        private void ListenToTouchEvents(IInteractionByAmountOfTouch touchInteractions)
        {
            touchInteractions.OnTouchPerfomed.AddListener(OnTouchReceived);
            touchInteractions.OnInputTouchInteractionFinished.AddListener(OnAllTouchReceived);
        }
        private void ListenToGenericTouchEvents(IInputTouchInteraction touchInteractions)
        {
            touchInteractions.OnCurrentInputTouchInteractionNormalizedChanged.AddListener(OnReceivedInteractionNormalized);
        }
        
        private void RemoveListenerToGenericTouchEvents(IInputTouchInteraction touchInteractions)
        {
            touchInteractions.OnCurrentInputTouchInteractionNormalizedChanged.RemoveAllListeners();
        }
        

        private void OnAllTouchReceived()
        {
            patientAnimatorDictionary[currentSelectedPatient].DoPlayDropEndedAnimation();
        }

        private void OnTouchReceived(int touchIndex)
        {
            patientAnimatorDictionary[currentSelectedPatient].SetInputInteractionInAnimation(touchIndex);
        }

        private void OnReceivedInteractionNormalized(float interactionNormalized)
        {
           patientAnimatorDictionary[currentSelectedPatient].SetInputInteractionNormalizedInAnimation(interactionNormalized);
        }
    }
}