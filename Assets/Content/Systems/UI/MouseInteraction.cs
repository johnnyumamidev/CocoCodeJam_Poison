using UnityEngine;

public class MouseInteraction : MonoBehaviour
{
    public float scaler;

    void OnMouseEnter()
    {
        transform.localScale *= scaler;
    }
    void OnMouseExit()
    {
        transform.localScale /= scaler;
    }
}
