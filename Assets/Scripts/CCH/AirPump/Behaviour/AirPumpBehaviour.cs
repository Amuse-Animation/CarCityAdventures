using AmuseEngine.Assets.Scripts.EnumClasses.DragDirection;
using AmuseEngine.Assets.Scripts.HelplerStaticClasses.ComparingFloat;
using AmuseEngine.Assets.Scripts.InputTouchInteraction.Behaviours.Drag;
using Lean.Touch;
using UnityEngine;

namespace CCH.AirPump.Behaviour
{
    public class AirPumpBehaviour : DragBehaviour
    {
        public float DisplacementAmount => displacementAmount;

        private float displacementAmount;

        private float displacementTotal;

        private float offset;

        private bool hasCapturatedInitialData;

        public AirPumpBehaviour(Transform transformToDrag, UnityEngine.Camera currentCamera) : base(transformToDrag, currentCamera)
        {
        }

        public override void DoDragTransformToDragWithinLimits(LeanFinger detectedFinger, DragDirectionType dragDirectionType,
            out bool canPerformDrag)
        {
            currentDraggingPoint = currentCamera.WorldToScreenPoint(transformToDrag.position);
            currentFingerPoint = detectedFinger.LastScreenPosition;
            currentDragDirectionNormalized = (currentFingerPoint - startingDragFingerPoint).normalized;

            DetectingFingerInsideLimits();

            canPerformDrag = false;
             
            switch (dragDirectionType)
            {
                case DragDirectionType.NONE:
                    canPerformDrag = false;
                    break;
                case DragDirectionType.RightDragOneWay:
                    canPerformDrag = currentDragDirectionNormalized.x < 0f;
                    if(canPerformDrag)
                        SetNewStartDragFingerPoint(currentFingerPoint);
                    break;
                case DragDirectionType.LeftDragOneWay:
                    canPerformDrag = currentDragDirectionNormalized.x > 0f;
                    if(canPerformDrag)
                        SetNewStartDragFingerPoint(currentFingerPoint);
                    break;
                case DragDirectionType.HorizontalDragTwoWays:
                    canPerformDrag = !ComparingFloatStaticClass.NearlyEqual(currentDragDirectionNormalized.x, 0f, 0.01f);
                    break;
                case DragDirectionType.UpDragOneWay:
                    canPerformDrag = currentDragDirectionNormalized.y > 0f;
                    if(canPerformDrag)
                        SetNewStartDragFingerPoint(currentFingerPoint);
                    break;
                case DragDirectionType.DownDragOneWay:
                    canPerformDrag = currentDragDirectionNormalized.y < 0f;
                    if(canPerformDrag)
                        SetNewStartDragFingerPoint(currentFingerPoint);
                    break;
                case DragDirectionType.VerticalDragTwoWays:

                    //canPerformDrag = !ComparingFloatStaticClass.NearlyEqual(currentDragDirectionNormalized.y, 0f, 0.01f);

                    //if (canPerformDrag)
                    //{

                    //}

                    currentFingerPoint.x = startingDragFingerPoint.x;
                    float calculatedDisplacement = Vector2.Distance(transformToDrag.position, startingDragFingerPointScreenWorldPoint);
                    displacementAmount = (calculatedDisplacement / displacementTotal) - offset;
                    DetectingFingerInsideLimits();
                    canPerformDrag = true;
                    break;
                case DragDirectionType.AnyDrag:
                    canPerformDrag = true;
                    break;
            }

            if (canPerformDrag && assignedDragPoint != Vector2.zero)
            {
                Vector2 finalDragPoint = (Vector2)currentCamera.ScreenToWorldPoint(assignedDragPoint) + startingDragPoint;
                finalDragPoint.x = transformToDrag.position.x;
                //transformToDrag.position = (Vector2)currentCamera.ScreenToWorldPoint(assignedDragPoint) + startingDragPoint;
                transformToDrag.position = finalDragPoint;
            }
            
            //
            if (transformToDrag.position.y < minimumLimits.y)
            {
                transformToDrag.position = minimumLimits;
               // canPerformDrag = false;
            }
            //
            if (transformToDrag.position.y > maximumLimits.y)
            {
                transformToDrag.position = maximumLimits;
               // canPerformDrag = false;
            }
        }

        protected override void DetectingFingerInsideLimits()
        {
           if (transformToDrag.position.y >= minimumLimits.y && transformToDrag.position.y <= maximumLimits.y)
                assignedDragPoint = currentFingerPoint;
        }

        public override void DoResetDragVariables()
        {
            startingDragPoint = Vector2.zero;
        }

        public override void DoSetDragLimits(Vector2 maxLimit, Vector2 minLimit)
        {
            base.DoSetDragLimits(maxLimit, minLimit);

            displacementTotal = Vector2.Distance(maxLimit, minLimit);
        }

        public override void DoSetStartingDragPoint(LeanFinger detectedFinger)
        {
            if(!hasCapturatedInitialData)
            {
                base.DoSetStartingDragPoint(detectedFinger);
                float calculatedDisplacement = Vector2.Distance(transformToDrag.position, startingDragFingerPointScreenWorldPoint);
                offset = calculatedDisplacement / displacementTotal;
                hasCapturatedInitialData = true;
            }
        }
    }
}