using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ButtonScale : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    private Vector3 originalScale;
    private Vector3 shrinkScale;

    void Start()
    {
        originalScale = transform.localScale;
        shrinkScale = originalScale * 0.9f;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        transform.localScale = shrinkScale;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        transform.localScale = originalScale;
    }
}