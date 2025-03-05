using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class WaterAlpha : MonoBehaviour
{
    public float speed = 0.5f;

    private Image image;

    private void Start()
    {
        image = GetComponent<Image>();

        StartCoroutine(FadeAlpha());
    }

    private IEnumerator FadeAlpha()
    {
        while (true)
        {
            yield return StartCoroutine(FadeTo(1f));
            yield return StartCoroutine(FadeTo(0f));
        }
    }

    private IEnumerator FadeTo(float targetAlpha)
    {
        while (Mathf.Abs(image.color.a - targetAlpha) > 0.01f)
        {
            Color color = image.color;
            color.a = Mathf.MoveTowards(color.a, targetAlpha, speed * Time.deltaTime);
            image.color = color;

            yield return null;
        }

        Color finalColor = image.color;
        finalColor.a = targetAlpha;
        image.color = finalColor;
    }
}
