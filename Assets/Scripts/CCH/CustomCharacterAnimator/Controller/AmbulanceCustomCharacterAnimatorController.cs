using System.Collections;
using AmuseEngine.Assets.Scripts.CharacterAnimator.Controller;
using CCH.CustomScriptableVariables.AmbulanceAnimationStringsVariables;
using UnityEngine;

namespace CCH.CustomCharacterAnimator.Controller
{
    public class AmbulanceCustomCharacterAnimatorController : CharcaterAnimatorController
    {
        private CustomAmbulanceScriptableVariablePlayerAnimationStrings customAmbulanceScriptableVariablePlayerAnimationStrings;
        private bool IsRocketActivated;
        private bool IsOnRescueMode;
        private bool IsSledding;

        protected override void Awake()
        {
            customAmbulanceScriptableVariablePlayerAnimationStrings = (CustomAmbulanceScriptableVariablePlayerAnimationStrings)scriptableVariablePlayerAnimationStrings;
            base.Awake();
        }

        protected override void PopulatingDictionary()
        {
            base.PopulatingDictionary();
            animatorLayerDictionary.Add(customAmbulanceScriptableVariablePlayerAnimationStrings.RESCUE_LAYER, 8);
            animatorLayerDictionary.Add(customAmbulanceScriptableVariablePlayerAnimationStrings.SNOW_LAYER, 13);
            animatorLayerDictionary.Add(customAmbulanceScriptableVariablePlayerAnimationStrings.SLED_LAYER, 15);
            animatorLayerDictionary.Add(customAmbulanceScriptableVariablePlayerAnimationStrings.SPACIAL_SPRING_BOOST_LAYER, 19);
            
            animatorLayerCurrentWeightDictionary.Add(animatorLayerDictionary[customAmbulanceScriptableVariablePlayerAnimationStrings.RESCUE_LAYER], 0f);
            animatorLayerCurrentWeightDictionary.Add(animatorLayerDictionary[customAmbulanceScriptableVariablePlayerAnimationStrings.SNOW_LAYER], 0f);
            animatorLayerCurrentWeightDictionary.Add(animatorLayerDictionary[customAmbulanceScriptableVariablePlayerAnimationStrings.SLED_LAYER], 0f);
            animatorLayerCurrentWeightDictionary.Add(animatorLayerDictionary[customAmbulanceScriptableVariablePlayerAnimationStrings.SPACIAL_SPRING_BOOST_LAYER], 0f);
        }
        
        protected override void PlayBlinkAnimation()
        {
            characterAnimatorBehaviour.PlayAnimation("AmbulanceBlink");
        }

        protected override void PlayBoredAnimation()
        {
            characterAnimatorBehaviour.PlayAnimation("AmbulanceBored");
        }

        protected override void PlayOhAnimation()
        {
            characterAnimatorBehaviour.PlayAnimation("CHR_Ambulance_oh");
        }

        protected override void PlayCelebrationAnimation()
        {
            characterAnimatorBehaviour.PlayAnimation("CHR_Ambulance_celebration");
        }

        public override void DoSetIdleState()
        {
            if(IsSledding) return;
            if (IsOnRescueMode) return;
            if(IsRocketActivated) return;
            base.DoSetIdleState();
        }

        public override void DoSetMovementState()
        {
            if(IsSledding) return;
            if (IsOnRescueMode) return;
            if(IsRocketActivated) return;
            base.DoSetIdleState();
        }
        
        public override void OnFallingState(bool groundedState)
        {
            if (IsSledding) return;
            if(IsRocketActivated) return;
            base.OnFallingState(groundedState);
        }
        
        public void DoActivateRescueMode()
        {
            if(IsSledding) return;
            if(IsTurboActive) return;
            if (characterAnimatorBehaviour.CurrentActiveLayer.Equals(customAmbulanceScriptableVariablePlayerAnimationStrings.RESCUE_LAYER)) return;
            characterAnimatorBehaviour.DeactivateAllLayersAndActivateDesiredLayer(customAmbulanceScriptableVariablePlayerAnimationStrings.RESCUE_LAYER);
            IsOnRescueMode = true;
            characterAnimatorBehaviour.SetBoolValue(nameof(IsOnRescueMode), IsOnRescueMode);
            UpdateCurrentLayer();
        }


        public void DoDeactivateRescueMode()
        {
            IsOnRescueMode = false;
            StartCoroutine(WaitForAnimationForOnRescueModeFalse(0.01f, DoSetIdleState));
        }
        
        public override void DoDeactivateSlipping()
        {
            if (!characterAnimatorBehaviour.CurrentActiveLayer.Equals(scriptableVariablePlayerAnimationStrings.SLIP_LAYER)) return;
            StartCoroutine(WaitForAnimationForSlippingFalse(0.1f, ()=> 
            {
                IsSledding = false;
                characterAnimatorBehaviour.SetBoolValue(nameof(IsSledding), IsSledding);
                DoSetIdleState();
            }));
        }

        public virtual void DoTriggerSnow()
        {
            if (characterAnimatorBehaviour.CurrentActiveLayer.Equals(customAmbulanceScriptableVariablePlayerAnimationStrings.SNOW_LAYER)) return;
            characterAnimatorBehaviour.DeactivateAllLayersAndActivateDesiredLayer(customAmbulanceScriptableVariablePlayerAnimationStrings.SNOW_LAYER);
            characterAnimatorBehaviour.SetTriggerValue("HasTriggeredSnow");
            UpdateCurrentLayer();
            hasTriggeredSpecialAnimation = true;
        }

        public virtual void DoTriggerSnowOut()
        {
            if (!hasTriggeredSpecialAnimation) return;
            if (!characterAnimatorBehaviour.CurrentActiveLayer.Equals(customAmbulanceScriptableVariablePlayerAnimationStrings.SNOW_LAYER)) return;
            characterAnimatorBehaviour.DeactivateAllLayersAndActivateDesiredLayer(customAmbulanceScriptableVariablePlayerAnimationStrings.SNOW_LAYER);
            characterAnimatorBehaviour.SetTriggerValue("HasTriggeredSnowOut");
            UpdateCurrentLayer();
            StartCoroutine(WaitForAnimationForAnimationTime(leavesTime, () =>
            {
                hasTriggeredSpecialAnimation = false;
                DoSetIdleState();
            }));
        }
        
        public void DoActivateSledding()
        {
            if (characterAnimatorBehaviour.CurrentActiveLayer.Equals(customAmbulanceScriptableVariablePlayerAnimationStrings.SLED_LAYER)) return;
            characterAnimatorBehaviour.DeactivateAllLayersAndActivateDesiredLayer(customAmbulanceScriptableVariablePlayerAnimationStrings.SLED_LAYER);
            IsSledding = true;
            characterAnimatorBehaviour.SetBoolValue(nameof(IsSledding), IsSledding);
            UpdateCurrentLayer();
        }

        public void DoDeactivateSledding()
        {
            if (!characterAnimatorBehaviour.CurrentActiveLayer.Equals(customAmbulanceScriptableVariablePlayerAnimationStrings.SLED_LAYER)) return;
            StartCoroutine(WaitForAnimationForSleddingFalse(1f, DoSetIdleState));
        }
        
        public void DoActivateSpacialSpringBoost()
        {
            if (characterAnimatorBehaviour.CurrentActiveLayer.Equals(customAmbulanceScriptableVariablePlayerAnimationStrings.SPACIAL_SPRING_BOOST_LAYER)) return;
            characterAnimatorBehaviour.DeactivateAllLayersAndActivateDesiredLayer(customAmbulanceScriptableVariablePlayerAnimationStrings.SPACIAL_SPRING_BOOST_LAYER);
            IsRocketActivated = true;
            characterAnimatorBehaviour.SetBoolValue(nameof(IsRocketActivated), IsRocketActivated);
            UpdateCurrentLayer();
        }


        public void DoDeactivateSpacialSpringBoost()
        {
            if (!characterAnimatorBehaviour.CurrentActiveLayer.Equals(customAmbulanceScriptableVariablePlayerAnimationStrings.SPACIAL_SPRING_BOOST_LAYER)) return;
            StartCoroutine(WaitForAnimationForSpacialSpringBoostFalse(1f, DoSetIdleState));
        }

        public override void DoActivateDiving()
        {
            StartCoroutine(WaitForDeactivateSlippingAnimation(0.4f, ()=> 
            { 
                base.DoActivateDiving(); 
                SetWheelsActiveState(false);
            })) ;

        }

        public override void DoTriggerSpringBoost()
        {
            base.DoTriggerSpringBoost();
            SetWheelsActiveState(true);
        }

        public override void OnSlope(bool isSlopping)
        {
            if (IsDiving || IsInATunel || hasTriggeredSpecialAnimation || IsSledding) 
            {
                capturedYPosition = 0f;
                hasCapturedYPosition = false;
                IsUphillActivated = false;
                IsDownhillActivated = false;

                characterAnimatorBehaviour.SetBoolValue(nameof(IsUphillActivated), IsUphillActivated);
                characterAnimatorBehaviour.SetBoolValue(nameof(IsDownhillActivated), IsDownhillActivated);
                return; 
            }

            if (!isSlopping)
            {
                capturedYPosition = 0f;
                hasCapturedYPosition = false;
                IsUphillActivated = false;
                IsDownhillActivated = false;

                characterAnimatorBehaviour.SetBoolValue(nameof(IsUphillActivated), IsUphillActivated);
                characterAnimatorBehaviour.SetBoolValue(nameof(IsDownhillActivated), IsDownhillActivated);
                
                DoSetMovementState();
                return;
            }


            if (!hasCapturedYPosition)
            {
                capturedYPosition = transform.position.y;
                hasCapturedYPosition = true;
            }



            if (capturedYPosition > transform.position.y)
            {
                SetDownhillState();
            }

            else if (capturedYPosition < transform.position.y)
            {
                SetClimbingState();
            }
        }
        
        private IEnumerator WaitForAnimationForSleddingFalse(float animationTime, System.Action callback)
        {
            yield return new WaitForEndOfFrame();
            IsSledding = false;
            characterAnimatorBehaviour.SetBoolValue(nameof(IsSledding), IsSledding);
            yield return new WaitForSeconds(animationTime);
            callback.Invoke();
        }
        
        
        private IEnumerator WaitForAnimationForOnRescueModeFalse(float animationTime, System.Action callback)
        {
            yield return new WaitForEndOfFrame();
            IsOnRescueMode = false;
            characterAnimatorBehaviour.SetBoolValue(nameof(IsOnRescueMode), IsOnRescueMode);
            yield return new WaitForSeconds(animationTime);
            callback.Invoke();
        }
        
        private IEnumerator WaitForAnimationForSpacialSpringBoostFalse(float animationTime, System.Action callback)
        {
            yield return new WaitForEndOfFrame();
            IsRocketActivated = false;
            characterAnimatorBehaviour.SetBoolValue(nameof(IsRocketActivated), IsRocketActivated);
            yield return new WaitForSeconds(animationTime);
            callback.Invoke();
        }
        
        private IEnumerator WaitForDeactivateSlippingAnimation(float animationTime, System.Action callback)
        {
          // DoDeactivateSlipping();
            yield return new WaitForEndOfFrame();
            yield return new WaitForSeconds(animationTime);
          IsSledding = false;
          characterAnimatorBehaviour.SetBoolValue(nameof(IsSlipping), IsSlipping);
            callback.Invoke();
        }

    }
}