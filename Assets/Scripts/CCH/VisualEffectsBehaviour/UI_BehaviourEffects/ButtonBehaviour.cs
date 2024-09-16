using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ButtonBehaviour : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    [SerializeField] private Button mybutton;
    [SerializeField] private GameObject myIconObject;
    private RectTransform myRectTransform;
    [SerializeField] private Vector2 myIconPosition;

    private void Start()
    {
        myRectTransform = myIconObject.GetComponent<RectTransform>();
    }
    public void OnPointerDown(PointerEventData eventData)
    {
       myRectTransform.anchoredPosition = myIconPosition;
       
    }
    public void OnPointerUp(PointerEventData eventData)
    {
        myRectTransform.anchoredPosition = new Vector2(0f,0f);

    }
}
