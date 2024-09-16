using CCH.CustomArgsStructsObjects.HealingPhaseAnimationArgs;
using CCH.CustomScriptableEvents.ValueHealingPhaseAnimationArgs;
using CCH.Illness.Controller.Receiver;
using UnityEngine;

namespace CCH.PatientAnimatorVFX.Controller
{
    public class PatientAnimatorVFXController : MonoBehaviour
    {
        [SerializeField]
        private Transform owner;

        [SerializeField]
        private ScriptableEventHealingPhaseAnimationArgs patientAnimatorHealing;

        public void PatientAnimatorVFXCallback(string currentPlayingAnimation)
        {
            owner.TryGetComponent(out IllnessReceiverController illnessReceiverController);
            if(illnessReceiverController != null)
            {
                patientAnimatorHealing.InvokeEvent(new HealingPhaseAnimationArgs(owner,
                                                                                 owner,
                                                                                 Vector2.zero,
                                                                                 illnessReceiverController,
                                                                                 currentPlayingAnimation,
                                                                                 0));

            }
        }

        public void PatientAnimatorVFXCallbackNoIllnessReceiverController(string currentPlayingAnimation)
        {
            patientAnimatorHealing.InvokeEvent(new HealingPhaseAnimationArgs(owner,
                                                                             owner,
                                                                             Vector2.zero,
                                                                             null,
                                                                             currentPlayingAnimation,
                                                                             0));
        }
    }
}
