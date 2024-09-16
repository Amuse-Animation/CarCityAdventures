using System.Collections.Generic;
using AmuseEngine.Assets.Scripts.CharacterAnimator.Behaviour;
using UnityEngine;

namespace CCH.CustomCharacterAnimator.Behaviour
{
    public class AmbulanceCustomCharacterAnimatorBehaviour : CharacterAnimatorBehaviour
    {
        public AmbulanceCustomCharacterAnimatorBehaviour(Animator animator, Dictionary<string, int> animatorLayerDictionary, Dictionary<int, float> animatorLayerCurrentWeightDictionary) : base(animator, animatorLayerDictionary, animatorLayerCurrentWeightDictionary)
        {
        }

    }
}