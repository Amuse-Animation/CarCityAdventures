using AmuseEngine.Assets.Scripts.ArgsStructObjects.TweenMovementArgs;
using AmuseEngine.Assets.Scripts.Tweens.ColourInterpolator.ControllerBase;
using DG.Tweening;
using UnityEngine;

namespace CCH.VisualEffectsBehaviour.SpriteRendererColourInterpolator
{
    public class SpriteRendererColourInterpolatorController : ColourInterpolatorControllerBase<SpriteRenderer>
    {
        protected override void PerformTween()
        {

            DOTween.To(() => objectToInterpolateColour.color, x => objectToInterpolateColour.color = x, finishColour, tweenTimeDuration).SetEase(easing).SetDelay(delay)
                .SetLoops(loops, loopType).SetId(interpolationNameID)
                .OnStart(onColourBeginToInterpolate.Invoke).OnComplete(TweenFinished);
        }
        public override void CancelTween()
        {
            DOTween.Kill(interpolationNameID);
        }

        protected override void TweenFinished()
        {
            onTweenFinished.Invoke();
            if(onTweenFinishedVoidEvent != null)
                onTweenFinishedVoidEvent.Raise();

            onMovingTransformFinishedTweenMovementArgsStruct.Invoke(new TweenMovementArgsStruct());
            onColourFinishedToBeInterpolated.Invoke();
        }
    }
}