using AmuseEngine.Assets.Scripts.ArgsStructObjects.InteractionArgs;
using AmuseEngine.Assets.Scripts.HelplerStaticClasses.ComparingFloat;
using AmuseEngine.Assets.Scripts.InputTouchInteraction.Controllers.DragByTimerInDropZoneCustomNonResetTimer;
using CCH.AirPump.Behaviour;
using UnityEngine;

namespace CCH.AirPump.Controller
{
    public class AirPumpController : DragByTimerInDropZoneCustomNonResetTimerController
    {
        [SerializeField]
        private float airPressure = .5f;

        private bool hasCapturedInitialPoint;

        AirPumpBehaviour pumpBehaviour;

        private bool isInflating;

        protected override void OnEnable()
        {
            base.OnEnable();
            hasCapturedInitialPoint = false;
        }

        protected override void OnDisable()
        {
            base.OnDisable();
            hasCapturedInitialPoint = false;
        }

        public override void DoBeginDrag(InteractionArgsStruct interactionArgsStruct)
        {
            if (!canDrag)
            {
                if (currentCamera == null)
                {
                    currentCamera = UnityEngine.Camera.main;

                    if (dragBehaviour == null)
                    {
                        pumpBehaviour = new AirPumpBehaviour(transformToDrag, currentCamera);
                        dragBehaviour = pumpBehaviour;
                    }
                }

                DoActivateDrag(interactionArgsStruct);
            }
        }

        protected override void DoActivateDrag(InteractionArgsStruct interactionArgsStruct)
        {
            currentTouchSelectionsArgs = interactionArgsStruct.CurrentTouchSelectionArgs;
            detectedFinger = interactionArgsStruct.CurrentTouchSelectionArgs.FingerSelector;

            if (!hasCapturedInitialPoint)
            {
                Vector2 maximumPoint = new Vector2(horizontalRightLimit , verticalTopLimit);
                Vector2 minimumPoint = new Vector2(horizontalLeftLimit, verticalBottomLimit);

                dragBehaviour.DoSetDragLimits(maximumPoint + (Vector2)transformToDrag.position, minimumPoint + (Vector2)transformToDrag.position);
                hasCapturedInitialPoint = true;
            }
            
            dragBehaviour.DoSetStartingDragPoint(detectedFinger);

            canDrag = true;

            isPerformingInteraction = true;
            OnPerformInteraction(isPerformingInteraction);
        }
        
        protected override void DoDeactivateDrag()
        {
            base.DoDeactivateDrag();

            if (currentTimerNormalized > 0.01f && !ComparingFloatStaticClass.NearlyEqual(currentTimerNormalized, 1f, 0.0001f))
            {
                InteractionInterrupted();
            }

            if (shouldResetTimeOnStopDragging)
            {
                ResetCurrentTimer();
                ResetInteractionHoldTimer();
            }

            OnPerformInteraction(false);
            InterruptDragInDetectedInteractionReceiver();

            isPerformingInteraction = false;
        }

        protected override void Update()
        {
            if (!canDrag) return;
            if (detectedFinger == null) return;
            if (dragBehaviour == null) return;

            dragBehaviour.DoDragTransformToDragWithinLimits(detectedFinger, dragDirectionType, out canPerformDrag);
            
            if (!canPerformDrag) return;
            if (!isPerformingInteraction) return;
            if(detectedInteractionReceiverByTimer == null) return;

            if (pumpBehaviour.DisplacementAmount >= .50f)
            {
                if (isInflating) return;
                currentTimer += airPressure;
                float normalizedTimer = Mathf.Clamp(currentTimer / desiredInteractionHolderTime, 0f, 1f);
                if (!ComparingFloatStaticClass.NearlyEqual(currentTimerNormalized, normalizedTimer, 0.0001f))
                {
                    currentTimerNormalized = normalizedTimer;
                    onCurrentInputTouchInteractionNormalizedChanged.Invoke(currentTimerNormalized);
                }

                isInflating = true;

                detectedInteractionReceiverByTimer.SetCurrentDragTimerNormalized(currentTimerNormalized);
                if (currentTimerNormalized > .99f)
                {
                    PrepareInteractionHoldTimerFinished();
                    InteractionHoldTimerPreFinished();
                    InteractionHoldTimerFinished();
                }
            }

            else
            {
                isInflating = false;
            }
        }
    }
}