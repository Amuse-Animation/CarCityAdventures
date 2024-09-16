using CCH.CustomArgsStructsObjects.RemedyPhaseObjectArgs;
using CCH.Illness.Controller.Dealer;
using UnityEngine;

namespace CCH.PatientAnimator.Behaviour
{
    public class PatientAnimatorBehaviour
    {

        public string CurrentPlayingAnimation => currentPlayingAnimation;
        public float CurrentAnimationVariableProgress => currentAnimationVariableProgress;

        //public string StartAnimationPlaying => startAnimationPlaying;
        Animator patientAnimator;

        const string FROZEN_LAYER = "FrozenLayer";
        const string BRANCH_LAYER = "BranchLayer";
        const string BROKEN_LAYER = "BrokenLayer";
        const string REDEYE_LAYER = "RedEyeLayer";
        
        const string BLANKET_INSTANT = "BlanketInstant";
        const string SOUP_INSTANT = "SoupInstant";

        const string BRANCH_START = "BranchStart";
        const string BRANCH_MOVEMENT = "BranchMovement";
        
        const string PUMP_START = "PumpStart";
        const string AIR_PUMPING = "AirPumping";

        const string PATCH_START = "PatchStart";

        const string FIX_PROGRESS = "FixProgress";
        const string SPRAY_PROGRESS = "SprayProgress";
        const string BODY_PATCH_START = "BodyPatchStart";
        
        const string SPATULA_AMOUNT = "SpatulaAmount";
        const string SPRAY_AMOUNT = "SprayAmount";
        
        
        const string DROP_START = "DropStart";
        const string WAIT_FOR_DROP = "WaitForDrop";
        const string DROPPING = "Dropping";
        const string DROP_ENDEND = "DropEndend";
        
        const string FOAM_START = "FoamStart";
        const string WIPPER_PROGRESS = "WipperAmount";
        const string WIPPER_START = "WipperProgress";
        
        const string DRY_START = "DryProgress";
        const string DRY_PROGRESS = "DryAmount";
        //const string BRANCHREMOVED = "BranchRemoved";
        //const string FLATTENTIRE = "FlattenTire";

        IllnessType currentIllness;
        int currentLayerIndex;
        int currentRemedyIndex;

        private int currentAnimationLayer;

        private string currentPlayingAnimation;
        private float currentAnimationVariableProgress;

        public PatientAnimatorBehaviour(Animator patientAnimator, IllnessType assignedIllnessType)
        {
            this.patientAnimator = patientAnimator;
            SetCurrentIllness(assignedIllnessType);
        }

        public void SetCurrentIllness(IllnessType assignedIllnessType)
        {
            currentIllness = assignedIllnessType;
            string currentIllnessLayer = string.Empty;
            switch (assignedIllnessType)
            {
                case IllnessType.NONE:
                    break;
                case IllnessType.Frozen:
                    currentIllnessLayer = FROZEN_LAYER;
                    break;
                case IllnessType.Branch:
                    currentIllnessLayer = BRANCH_LAYER;
                    break;
                case IllnessType.Broken:
                    currentIllnessLayer = BROKEN_LAYER;
                    break;
                case IllnessType.RedEyes:
                    currentIllnessLayer = REDEYE_LAYER;
                    break;
                case IllnessType.Random:
                    break;
            }

            ActivateTheCorrespondingAnimationLayer(currentIllnessLayer);
        }

        public void OnPlayCorrespondingPhaseAnimation(RemedyPhaseObjectArgs remedyPhaseObjectArgs)
        {
            if (currentIllness != remedyPhaseObjectArgs.RemedyPhaseIllnessReceiverController.AssignedIllnesType) return;
            currentRemedyIndex = remedyPhaseObjectArgs.RemedyPhaseIllnessReceiverController.AssignedIllness.CurrentRemedyButtonIndex;

            switch (currentIllness)
            {
                case IllnessType.NONE:
                    break;
                case IllnessType.Frozen:
                    FrozenAnimationSwitchBetweenPhases(currentRemedyIndex);
                    break;
                case IllnessType.Branch:
                    BranchAnimationSwitchBetweenPhases(currentRemedyIndex);
                    break;
                case IllnessType.Broken:
                    BrokenAnimationSwitchBetweenPhases(currentRemedyIndex);
                    break;
                case IllnessType.RedEyes:
                    RedEyesAnimationSwitchBetweenPhases(currentRemedyIndex);

                    break;
                case IllnessType.Random:
                    break;
            }
        }

        public void SetInputInteractionNormalizedInAnimation(float currentInputInteractionNormalized)
        {
            switch (currentIllness)
            {
                case IllnessType.NONE:
                    break;
                case IllnessType.Frozen:
                    break;
                case IllnessType.Branch:
                    switch (currentRemedyIndex)
                    {
                        case 1:
                            SetAnimationProgress(BRANCH_MOVEMENT, currentInputInteractionNormalized);
                            break;
                        case 2:
                            SetAnimationProgress(AIR_PUMPING, currentInputInteractionNormalized);
                            break;
                        case 3:
                            break;
                        default:
                            break;
                    }
                    break;
                case IllnessType.Broken:
                    switch (currentRemedyIndex)
                    {
                        case 1:
                            SetAnimationProgress(SPATULA_AMOUNT, currentInputInteractionNormalized);
                            break;
                        case 2:
                            SetAnimationProgress(SPRAY_AMOUNT, currentInputInteractionNormalized);
                            break;
                        case 3:
                            break;
                        default:
                            break;
                    }
                    break;
                case IllnessType.RedEyes:
                    switch (currentRemedyIndex)
                    {
                        case 1:
                           // SetAnimationProgress(SPATULA_AMOUNT, currentInputInteractionNormalized);
                            break;
                        case 2:
                            SetAnimationProgress(WIPPER_PROGRESS, currentInputInteractionNormalized);
                            break;
                        case 3:
                            SetAnimationProgress(DRY_PROGRESS, currentInputInteractionNormalized);
                            break;
                        default:
                            break;
                    }
                    break;
                case IllnessType.Random:
                    break;
            }
            
        }
        
        public void SetInputInteractionInAnimation(int currentInputInteraction)
        {
            switch (currentIllness)
            {
                case IllnessType.NONE:
                    break;
                case IllnessType.Frozen:
                    break;
                case IllnessType.Branch:
                    break;
                case IllnessType.Broken:
                    break;
                case IllnessType.RedEyes:
                    switch (currentRemedyIndex)
                    {
                        case 1:
                            PlayDesiredAnimation(DROPPING);
                            break;
                        case 2:
                            //SetAnimationProgress(SPRAY_AMOUNT, currentInputInteractionNormalized);
                            break;
                        case 3:
                            break;
                        default:
                            break;
                    }
                    break;
                case IllnessType.Random:
                    break;
            }
            
        }

        public void PlayOriginalAnimation()
        {
            PlayDesiredAnimation("Init", currentAnimationLayer);
        }

        private void RedEyesAnimationSwitchBetweenPhases(int currentRemedyButtonPhase)
        {
            switch (currentRemedyButtonPhase)
            {
                case 1:
                    DoActivateRedEyesDropperCycle();
                    //DoActivateBrokenFixingCycle();
                    break;
                case 2:
                    DoActivateRedEyesFoamCycle();
                   // DoActivateSprayCycle();
                    break;
                case 3:
                    DoActivateRedEyesDryCycle();
                   // DoActivateBodyPatchCycle();
                    break;
                default:
                    break;
            }
        }

        private void BrokenAnimationSwitchBetweenPhases(int currentRemedyButtonPhase)
        {
            switch (currentRemedyButtonPhase)
            {
                case 1:
                    DoActivateBrokenFixingCycle();
                    break;
                case 2:
                    DoActivateSprayCycle();
                    break;
                case 3:
                    DoActivateBodyPatchCycle();
                    break;
                default:
                    break;
            }
        }

        private void BranchAnimationSwitchBetweenPhases(int currentRemedyButtonPhase)
        {
            switch (currentRemedyButtonPhase)
            {
                case 1:
                    DoActivateBranchRemovingCycle();
                    break;
                case 2:
                    DoActivatePumpCycle();
                    break;
                case 3:
                    DoActivatePatch();
                    break;
                default:
                    break;
            }
        }

        private void FrozenAnimationSwitchBetweenPhases(int currentRemedyButtonPhase)
        {
            switch (currentRemedyButtonPhase)
            {
                case 1:
                break;
                case 2:
                    DoActivateBlanketAnimation();
                break;
                case 3:
                    DoActivateSoupAnimation();
                break;
                default:
                break;
            }
        }

        private void DoActivateRedEyesDryCycle()
        {
            PlayDesiredAnimation(DRY_START);
        }
        private void DoActivateRedEyesFoamCycle()
        {
            PlayDesiredAnimation(WIPPER_START);
        }

        private void DoActivateRedEyesDropperCycle()
        {
            PlayDesiredAnimation(DROP_START);
        }

        private void DoActivateBlanketAnimation()
        {
            PlayDesiredAnimation(BLANKET_INSTANT);
        }

        private void DoActivateSoupAnimation()
        {
            PlayDesiredAnimation(SOUP_INSTANT);
        }

        private void DoActivateBranchRemovingCycle()
        {
            PlayDesiredAnimation(BRANCH_START);
        }

        private void DoActivatePumpCycle()
        {
            PlayDesiredAnimation(PUMP_START);
        }

        private void DoActivatePatch()
        {
            PlayDesiredAnimation(PATCH_START);
        }

        private void DoActivateBrokenFixingCycle()
        {
            PlayDesiredAnimation(FIX_PROGRESS);
        }

        private void DoActivateSprayCycle()
        {
            PlayDesiredAnimation(SPRAY_PROGRESS);
        }

        private void DoActivateBodyPatchCycle()
        {
            PlayDesiredAnimation(BODY_PATCH_START);
        }

        public void DoPlayDropEndedAnimation()
        {
            PlayDesiredAnimation(DROP_ENDEND);
        }

        private void PlayDesiredAnimation(string animationName)
        {
            patientAnimator.Play(animationName);
            currentPlayingAnimation = animationName;
        }
        
        private void PlayDesiredAnimation(string animationName, int layer)
        {
            patientAnimator.Play(animationName, layer);
            currentPlayingAnimation = animationName;
        }

        private void SetAnimationProgress(string animationParemeterName, float currentProgress)
        {
            patientAnimator.SetFloat(animationParemeterName, currentProgress);
            currentAnimationVariableProgress = currentProgress;
        }

        private void ActivateTheCorrespondingAnimationLayer(string layerName)
        {
            currentLayerIndex = patientAnimator.GetLayerIndex(layerName);
            currentAnimationLayer = patientAnimator.GetLayerIndex(layerName);
            patientAnimator.SetLayerWeight(currentLayerIndex, 1f);
        }
    }
}