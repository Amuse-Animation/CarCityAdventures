using CCH.CustomScriptableVariables.HandAnimationStringsVariable;
using UnityEngine;

namespace CCH.InstructionsHand.Behaviour
{
    public class InstructionsHandBehaviour
    {
        private Animator animatorToUse;
        private ScriptableVariableHandAnimationStrings scriptableVariableHandAnimationStrings;
        
        private string selectedAnimation;
        private string followingAnimation;
        private string savingPoint;

        public InstructionsHandBehaviour(Animator animatorToUse, ScriptableVariableHandAnimationStrings scriptableVariableHandAnimationStrings)
        {
            this.animatorToUse = animatorToUse;
            this.scriptableVariableHandAnimationStrings = scriptableVariableHandAnimationStrings;
        }

        public void PlaySlideVerticalAnimation()
        {
            animatorToUse.Play(scriptableVariableHandAnimationStrings.HAND_IN_SLIDE_VERTICAL);
            
            selectedAnimation = scriptableVariableHandAnimationStrings.HAND_IN_SLIDE_VERTICAL;
            followingAnimation = scriptableVariableHandAnimationStrings.HAND_EXIT_SLIDE_VERTICAL;
            savingPoint = followingAnimation;
        }

        public void PlaySlideHorizontalAnimation()
        {
            animatorToUse.Play(scriptableVariableHandAnimationStrings.HAND_IN_SLIDE_HORIZONTAL);
            
            selectedAnimation = scriptableVariableHandAnimationStrings.HAND_IN_SLIDE_HORIZONTAL;
            followingAnimation = scriptableVariableHandAnimationStrings.HAND_EXIT_SLIDE_HORIZONTAL;
            savingPoint = followingAnimation;
        }

        public void PlayTouchAnimation()
        {
            animatorToUse.Play(scriptableVariableHandAnimationStrings.HAND_IN_TOUCH);
            selectedAnimation = scriptableVariableHandAnimationStrings.HAND_IN_TOUCH;
        }

        public void PlayTapAnimation()
        {
            animatorToUse.Play(scriptableVariableHandAnimationStrings.HAND_IN_TAP);
            selectedAnimation = scriptableVariableHandAnimationStrings.HAND_IN_TAP;
        }

        public void PlayFinishCycleAnimation()
        {
            if (string.IsNullOrEmpty(followingAnimation)) return;
            animatorToUse.Play(followingAnimation);
            followingAnimation = string.Empty;
        }

        public void PlaySelectedAnimation()
        {
            if(string.IsNullOrEmpty(selectedAnimation)) return;
            animatorToUse.Play(selectedAnimation);
            followingAnimation = savingPoint;
        }

        public void PlayEmptyState()
        {
            animatorToUse.Play(scriptableVariableHandAnimationStrings.EMPTY);
        }
    }
}