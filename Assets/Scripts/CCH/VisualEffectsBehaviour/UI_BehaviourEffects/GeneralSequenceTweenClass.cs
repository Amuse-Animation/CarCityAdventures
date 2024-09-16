using AmuseEngine.Assets.Scripts.Sequences.SequenceTween.BaseClass;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using System;


[System.Serializable]
public class GeneralSequenceTweenClass : MonoBehaviour
{

    private enum TweenType
    {
        Move,
        Scale,
        Rotate
    };

    [SerializeField] private TweenType myTween;

    [SerializeField]
    protected float duration;

    [SerializeField]
    protected float delay;

    [SerializeField]
    [Range(-1, 10)]
    protected int loops = 0;

    [SerializeField]
    protected LoopType loopType;

    [SerializeField]
    protected Ease animationCurveEasing;

    [SerializeField]
    protected bool useLocalSpace;

    public Tween CurrentTween;

    [SerializeField]
    private Transform transformToTween;

    [SerializeField]
    private Vector3 finalPoint;

        public void DoSequence()
        {

           if(myTween == TweenType.Scale) 
            {
                CurrentTween = transformToTween.DOScale(finalPoint, duration)
                .SetRelative(useLocalSpace).SetEase(animationCurveEasing).SetDelay(delay).SetLoops(loops);
            }

            if (myTween == TweenType.Move)
            {
                CurrentTween = transformToTween.DOMove(finalPoint, duration)
                .SetRelative(useLocalSpace).SetEase(animationCurveEasing).SetDelay(delay).SetLoops(loops);
            }

            if (myTween == TweenType.Rotate)
            {
                CurrentTween = transformToTween.DORotate(finalPoint, duration)
                .SetRelative(useLocalSpace).SetEase(animationCurveEasing).SetDelay(delay).SetLoops(loops);
            }

        }
    }