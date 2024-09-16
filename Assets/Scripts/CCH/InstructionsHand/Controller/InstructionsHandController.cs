using CCH.CustomScriptableVariables.HandAnimationStringsVariable;
using CCH.InstructionsHand.Behaviour;
using UnityAtoms.BaseAtoms;
using UnityEngine;
using UnityEngine.Events;

namespace CCH.InstructionsHand.Controller
{
    public class InstructionsHandController : MonoBehaviour
    {
        public Transform HandRootTransform => handRootTransform;

        public SpriteRenderer CircleTouchSpriteRenderer => circleTouchSpriteRenderer;

        [SerializeField]
        private Transform handRootTransform;

        [SerializeField]
        private SpriteRenderer circleTouchSpriteRenderer;

        [SerializeField] 
        private Animator animatorToUse;

        [SerializeField]
        private ScriptableVariableHandAnimationStrings scriptableVariableHandAnimationStrings;

        [SerializeField]
        private BoolVariable isInteractingWithAnImportantObjectVariable;

        [SerializeField]
        private UnityEvent onFinishAnimationCycleLoop;

        [SerializeField]
        private UnityEvent onInteractingWithAnImportantObject;

        [SerializeField]
        private UnityEvent onNotInteractingWithAnImportantObject;

        private InstructionsHandBehaviour instructionsHandBehaviour;

        private bool hasDoneAllRegularCycle;
        private bool isPlayingAnimation;

        private void Awake()
        {
            if(instructionsHandBehaviour == null)
                instructionsHandBehaviour = new InstructionsHandBehaviour(animatorToUse, scriptableVariableHandAnimationStrings);
        }

        private void OnEnable()
        {
            if(isInteractingWithAnImportantObjectVariable != null)
                isInteractingWithAnImportantObjectVariable.Changed.Register(OnIsInteractingCallback);
        }

        private void OnDisable()
        {
            if(isInteractingWithAnImportantObjectVariable != null)
                isInteractingWithAnImportantObjectVariable.Changed.Unregister(OnIsInteractingCallback);
        }

        public void DoPlaySlideVerticalAnimation()
        {
            if(!isPlayingAnimation)
            {
                instructionsHandBehaviour.PlaySlideVerticalAnimation();
                isPlayingAnimation = true;
            }
        }

        public void DoPlaySlideHorizontalAnimation()
        {
            if(!isPlayingAnimation)
            {
                instructionsHandBehaviour.PlaySlideHorizontalAnimation();
                isPlayingAnimation = true;
            }
        }

        public void DoPlayTouchAnimation()
        {
            if(!isPlayingAnimation)
            {
                instructionsHandBehaviour.PlayTouchAnimation();
                isPlayingAnimation = true;
            }
        }

        public void DoPlayTapAnimation()
        {
            if(!isPlayingAnimation)
            {
                instructionsHandBehaviour.PlayTapAnimation();
                isPlayingAnimation = true;
            }
        }

        public void DoStopAnimation()
        {
            if (isPlayingAnimation) 
            {
                isPlayingAnimation = false;
            }
        }

        public void DoPlayFinishCycleAnimation()
        {
            instructionsHandBehaviour.PlayFinishCycleAnimation();
            hasDoneAllRegularCycle = true;
            onFinishAnimationCycleLoop.Invoke();
        }

        public void DoPlaySelectedAnimation()
        {
            if (isPlayingAnimation) return;
            instructionsHandBehaviour.PlaySelectedAnimation();
            hasDoneAllRegularCycle = false;
        }

        private void OnIsInteractingCallback(bool  isInteracting)
        {
           // if (!hasDoneAllRegularCycle) return;
            if(isInteracting)
                onInteractingWithAnImportantObject.Invoke();
            else
                onNotInteractingWithAnImportantObject.Invoke();
        }
    }
}