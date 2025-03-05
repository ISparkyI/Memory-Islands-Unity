using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections;

public class CardClickAnimation : MonoBehaviour, IPointerClickHandler
{
    private RectTransform rectTransform;
    private Vector3 originalScale;
    private float animationDuration = 0.3f;
    private CardFlipper cardFlipper;

    private void Start()
    {
        rectTransform = GetComponent<RectTransform>();
        originalScale = rectTransform.localScale;
        cardFlipper = FindAnyObjectByType<CardFlipper>();
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (cardFlipper.CanFlip())
        {
            StartCoroutine(AnimateClick());
        }
    }

    private IEnumerator AnimateClick()
    {
        float timer = 0f;

        while (timer < animationDuration / 2f)
        {
            float scale = Mathf.Lerp(1f, 0f, timer / (animationDuration / 2f));
            rectTransform.localScale = new Vector3(scale, originalScale.y, originalScale.z);
            timer += Time.deltaTime;
            yield return null;
        }

        timer = 0f;
        while (timer < animationDuration / 2f)
        {
            float scale = Mathf.Lerp(0f, 1f, timer / (animationDuration / 2f));
            rectTransform.localScale = new Vector3(scale, originalScale.y, originalScale.z);
            timer += Time.deltaTime;
            yield return null;
        }

        rectTransform.localScale = originalScale;
    }
}
