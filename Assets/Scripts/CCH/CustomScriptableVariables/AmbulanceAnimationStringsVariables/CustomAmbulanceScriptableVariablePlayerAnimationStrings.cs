using AmuseEngine.Assets.Scripts.ScriptableVariables.AnimationStrings.Player;
using UnityEngine;

namespace CCH.CustomScriptableVariables.AmbulanceAnimationStringsVariables
{
    [CreateAssetMenu(fileName = "CustomAmbulanceScriptableVariableAnimationStrings", menuName = "CustomScriptableVariables/AmbulanceScriptableVariableAnimationStrings", order = 2)]
    public class CustomAmbulanceScriptableVariablePlayerAnimationStrings : ScriptableVariablePlayerAnimationStrings
    {
        public string RESCUE_LAYER => rescueLayer;
        public string SPACIAL_SPRING_BOOST_LAYER => spacialSpringBoostLayer;
        public string SNOW_LAYER => snowLayer;
        public string SLED_LAYER => sledLayer;
        
        [SerializeField]
        private string rescueLayer = "RescueLayer";
        [SerializeField]
        private string spacialSpringBoostLayer = "SpacialSpringBoosterLayer";
        [SerializeField]
        private string snowLayer = "SnowLayer";
        [SerializeField]
        private string sledLayer = "SledLayer";
    }
}