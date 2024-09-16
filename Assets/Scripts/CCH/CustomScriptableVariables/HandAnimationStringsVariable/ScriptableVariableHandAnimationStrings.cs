using UnityEngine;

namespace CCH.CustomScriptableVariables.HandAnimationStringsVariable
{
    [CreateAssetMenu(fileName = "ScriptableVariableHandAnimationStrings", menuName = "CustomScriptableVariables/ScriptableVariableHandAnimationStrings", order = 1)]
    public class ScriptableVariableHandAnimationStrings : ScriptableObject
    {
        public string EMPTY => empty;
        public string HAND_IN_SLIDE_VERTICAL => handInSlideVertical;
        public string HAND_IN_SLIDE_HORIZONTAL => handInSlideHorizontal;
        public string HAND_IN_TOUCH => handInTouch;
        public string HAND_IN_TAP => handInTap;

        public string HAND_EXIT_SLIDE_VERTICAL => handExitSlideVertical;
        public string HAND_EXIT_SLIDE_HORIZONTAL => handExitSlideHorizontal;


        [SerializeField]
        private string empty = "Empty";
        [SerializeField]
        private string handInSlideVertical = "HandInSlideVertical"; 
        [SerializeField]
        private string handInSlideHorizontal = "HandInSlideHorizontal";
        [SerializeField]
        private string handInTouch = "HandInTouch"; 
        [SerializeField]
        private string handInTap = "HandInTap";
        [SerializeField]
        private string handExitSlideVertical = "HandSlideVerticalIdle";
        [SerializeField]
        private string handExitSlideHorizontal = "HandSlideHorizontalIdle"; 
        
        
    }
}