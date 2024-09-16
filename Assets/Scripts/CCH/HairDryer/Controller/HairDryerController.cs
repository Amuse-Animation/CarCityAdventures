using AmuseEngine.Assets.Scripts.HelplerStaticClasses.ComparingFloat;
using AmuseEngine.Assets.Scripts.InputTouchInteraction.Controllers.DragByTimerInDropZone;
using UnityAtoms.BaseAtoms;
using UnityEngine;

namespace CCH.HairDryer.Controller
{
    public class HairDryerController : DragByTimerInDropZoneController
    {
        [SerializeField] 
        private FloatVariable currentMeltingTimeVariable;
        [SerializeField] 
        private FloatVariable currentMeltingTimeNormalizedFloatVariable;

        protected override void Update()
        {
            if (!canDrag) return;
            if (detectedFinger == null) return;
            if (dragBehaviour == null) return;

            dragBehaviour.DoDragTransformToDragWithinLimits(detectedFinger, dragDirectionType, out canPerformDrag);

            if (!canPerformDrag) return;
            if (!isPerformingInteraction) return;
            if (detectedInteractionReceiverByTimer == null) return;

            currentTimer += Time.deltaTime;
            float normalizedTimer = Mathf.Clamp(currentTimer / desiredInteractionHolderTime, 0f, 1f);

            if (!ComparingFloatStaticClass.NearlyEqual(currentTimerNormalized, normalizedTimer, 0.0001f))
            {
                currentTimerNormalized = normalizedTimer;
                onCurrentInputTouchInteractionNormalizedChanged.Invoke(currentTimerNormalized);
            }

            detectedInteractionReceiverByTimer.SetCurrentDragTimerNormalized(currentTimerNormalized);
            currentMeltingTimeVariable.Value = currentTimer;

            if (currentTimerNormalized <.99f) return;

            PrepareInteractionHoldTimerFinished();
            InteractionHoldTimerPreFinished();
            InteractionHoldTimerFinished();
        }
    }
}