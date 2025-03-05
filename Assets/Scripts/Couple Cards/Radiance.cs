using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Radiance : MonoBehaviour
{
    private Image image;
    private Vector3 initialScale;
    private float scaleDuration = 1.0f;
    private float alphaDuration = 2.0f;
    private float scaleTimer = 0f;
    private float alphaTimer = 0f;
    private bool isScalingUp = true;
    private bool isIncreasingAlpha = true;
    private bool isAppearing = true;

    private bool isInitialized = false;

    public void StartRadiance()
    {
        if (!isInitialized)
        {
            image = GetComponent<Image>();
            initialScale = transform.localScale;
            isInitialized = true;
        }

        transform.localScale = Vector3.zero;
        isAppearing = true;
        scaleTimer = 0f;
        alphaTimer = 0f;
        StartCoroutine(AnimateAppearanceWithDelay());
    }

    private void Update()
    {
        if (!isAppearing)
        {
            AnimateScale();
            AnimateAlpha();
        }
        RotateObject();
    }

    private void AnimateScale()
    {
        scaleTimer += Time.deltaTime;
        float progress = scaleTimer / scaleDuration;

        if (isScalingUp)
        {
            transform.localScale = Vector3.Lerp(initialScale, initialScale * 1.2f, progress);
        }
        else
        {
            transform.localScale = Vector3.Lerp(initialScale * 1.2f, initialScale, progress);
        }

        if (progress >= 1f)
        {
            scaleTimer = 0f;
            isScalingUp = !isScalingUp;
        }
    }

    private void AnimateAlpha()
    {
        alphaTimer += Time.deltaTime;
        float progress = alphaTimer / alphaDuration;

        Color color = image.color;
        if (isIncreasingAlpha)
        {
            color.a = Mathf.Lerp(0.1f, 0.2f, progress);
        }
        else
        {
            color.a = Mathf.Lerp(0.2f, 0.1f, progress);
        }
        image.color = color;

        if (progress >= 1f)
        {
            alphaTimer = 0f;
            isIncreasingAlpha = !isIncreasingAlpha;
        }
    }

    private IEnumerator AnimateAppearanceWithDelay()
    {
        yield return new WaitForSeconds(0.4f);

        float duration = 0.5f;
        float timer = 0f;

        while (timer < duration)
        {
            transform.localScale = Vector3.Lerp(Vector3.zero, initialScale, timer / duration);
            timer += Time.deltaTime;
            yield return null;
        }

        transform.localScale = initialScale;
        isAppearing = false;
    }

    private void RotateObject()
    {
        transform.Rotate(0, 0, 10 * Time.deltaTime);
    }
}
