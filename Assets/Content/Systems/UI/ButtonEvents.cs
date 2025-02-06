using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.Events;
using TMPro;

public class ButtonEvents : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    public float scaler;
    public UnityEvent OnClick;
    public UnityEvent OnEnter;
    public UnityEvent OnExit;

    public void OnPointerEnter(PointerEventData pointerEventData)
    {
        transform.localScale = transform.localScale * scaler;

        OnEnter?.Invoke();
    }

    //Detect when Cursor leaves the GameObject
    public void OnPointerExit(PointerEventData pointerEventData)
    {
        transform.localScale = Vector3.one;
 
        OnExit?.Invoke();
    }

    public void OnPointerClick(PointerEventData pointerEventData)
    {
        OnClick?.Invoke();
    }
}
