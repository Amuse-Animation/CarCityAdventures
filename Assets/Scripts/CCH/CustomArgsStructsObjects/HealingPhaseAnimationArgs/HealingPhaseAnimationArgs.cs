using CCH.CustomArgsStructsObjects.PoolableObjectArgsClass;
using CCH.Illness.Controller.Receiver;
using UnityEngine;

namespace CCH.CustomArgsStructsObjects.HealingPhaseAnimationArgs
{
    [System.Serializable]
    public class HealingPhaseAnimationArgs : AmbulanceCustomPoolableObjectArgsClass
    {
        public IllnessReceiverController RemedyPhaseIllnessReceiverController;
        public string CurrentPlayingAnimation;
        public int CurrentRemedyButtonIndex;

        public HealingPhaseAnimationArgs(Transform instigator,
                                         Transform placePointReference,
                                         Vector3 placePointReferenceOffset,
                                         IllnessReceiverController remedyPhaseIllnessReceiverController,
                                         string currentPlayingAnimation,
                                         int currentRemedyButtonIndex) : base(instigator, placePointReference, placePointReferenceOffset)
        {
            RemedyPhaseIllnessReceiverController = remedyPhaseIllnessReceiverController;
            CurrentPlayingAnimation = currentPlayingAnimation;
            CurrentRemedyButtonIndex = currentRemedyButtonIndex;
        }
    }
}
