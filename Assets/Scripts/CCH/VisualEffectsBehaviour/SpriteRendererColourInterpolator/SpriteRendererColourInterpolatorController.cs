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
                .OnStart(onColourBeginToInterpolate.Invoke).OnComplete(onColourFinishedToBeInterpolated.Invoke);
        }
        public override void CancelTween()
        {
            DOTween.Kill(interpolationNameID);
        }
    }
}