using UnityEngine;
using UnityEngine.Events;

public class MouseInteraction : MonoBehaviour
{
    public float scaler;
    public UnityEvent OnHover;
    public UnityEvent OnExit;
    void OnMouseEnter()
    {
        transform.localScale *= scaler;
        OnHover?.Invoke();
    }
    void OnMouseExit()
    {
        transform.localScale /= scaler;
        OnExit?.Invoke();
    }
}
