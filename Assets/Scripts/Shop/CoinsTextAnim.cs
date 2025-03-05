using UnityEngine;
using TMPro;
using System.Collections;

public class CoinsTextAnim : MonoBehaviour
{
    private TMP_Text textMeshPro;
    [SerializeField] private float minFontSize = 45f;
    [SerializeField] private float maxFontSize = 60f;
    [SerializeField] private float animationDuration = 1f;

    private Coroutine animationCoroutine;

    private void Awake()
    {
        textMeshPro = GetComponent<TMP_Text>();
    }

    private void OnEnable()
    {
        if (textMeshPro != null)
        {
            StartAnimation();
        }
    }

    private void OnDisable()
    {
        StopAnimation();
    }

    private void StartAnimation()
    {
        if (animationCoroutine != null)
        {
            StopCoroutine(animationCoroutine);
        }
        animationCoroutine = StartCoroutine(AnimateTextSize());
    }

    private void StopAnimation()
    {
        if (animationCoroutine != null)
        {
            StopCoroutine(animationCoroutine);
        }
    }

    private IEnumerator AnimateTextSize()
    {
        while (true)
        {
            float elapsedTime = 0f;

            while (elapsedTime < animationDuration)
            {
                textMeshPro.fontSize = Mathf.Lerp(minFontSize, maxFontSize, elapsedTime / animationDuration);
                elapsedTime += Time.deltaTime;
                yield return null;
            }
            textMeshPro.fontSize = maxFontSize;

            elapsedTime = 0f;
            while (elapsedTime < animationDuration)
            {
                textMeshPro.fontSize = Mathf.Lerp(maxFontSize, minFontSize, elapsedTime / animationDuration);
                elapsedTime += Time.deltaTime;
                yield return null;
            }
            textMeshPro.fontSize = minFontSize;
        }
    }
}
