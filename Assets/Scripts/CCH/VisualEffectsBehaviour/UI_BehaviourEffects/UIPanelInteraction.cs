using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIPanelInteraction : MonoBehaviour
{
    [SerializeField] private Transform m_gameObjectTransform;
    [SerializeField] private CanvasGroup m_gameObjectCanvas;
    [SerializeField] private Vector3 m_EnterScale;
    [SerializeField] private Vector3 m_ExitScale;
    [SerializeField] private Ease EnterEase;
    [SerializeField] private Ease ExitEase;
    [SerializeField] private float tweenCanvasDuration;
    [SerializeField] private float tweenDuration;
    private Sequence mySequenceTween;

    public void OnEnterMovement()
    {
        mySequenceTween.Append(m_gameObjectTransform.DOScale(m_EnterScale, tweenDuration).SetEase(EnterEase)).Join(m_gameObjectCanvas.DOFade(1,1));
    }

    public void OnExitMovement()
    {
        mySequenceTween.Append(m_gameObjectTransform.DOScale(m_ExitScale, tweenDuration).SetEase(ExitEase)).Join(m_gameObjectCanvas.DOFade(0, tweenCanvasDuration).OnComplete(() => m_gameObjectTransform.gameObject.SetActive(false))).AppendCallback(ResetScale);
        
    }

    private void ResetScale()
    {
        m_gameObjectTransform.DOScale(new Vector3(0,0,0), 0);
    }
}
