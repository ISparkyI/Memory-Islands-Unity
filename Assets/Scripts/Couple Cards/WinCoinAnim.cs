using UnityEngine;
using System.Collections;

public class WinCoinAnim : MonoBehaviour
{
    public float animationDuration = 0.3f;
    public Vector3 initialScale = Vector3.zero;
    public Vector3 finalScale = Vector3.one;

    public float shakeMagnitude = 0.8f;
    public float shakeFrequency = 30f;

    private bool isShaking = false;
    private Vector3 initialPosition;

    public MainCoin mainCoin;

    public void StartWinCoinAnim()
    {
        foreach (Transform child in transform)
        {
            GameObject.Destroy(child.gameObject);
        }
        transform.localScale = initialScale;
        initialPosition = transform.localPosition;
    }

    private void Update()
    {
        if (isShaking)
        {
            ShakeCoin();
        }
    }

    public void StartCoinAnimation()
    {
        StartCoroutine(AnimateCoinAppearance());
        isShaking = true;
    }

    private IEnumerator AnimateCoinAppearance()
    {
        float timer = 0f;

        while (timer < animationDuration)
        {
            float progress = timer / animationDuration;
            transform.localScale = Vector3.Lerp(initialScale, finalScale, progress);
            timer += Time.deltaTime;
            yield return null;
        }

        transform.localScale = finalScale;
        mainCoin.StartSpawningCoins();
    }

    private void ShakeCoin()
    {
        float shakeOffsetX = Mathf.Sin(Time.time * shakeFrequency) * shakeMagnitude;
        float shakeOffsetY = Mathf.Cos(Time.time * shakeFrequency) * shakeMagnitude;

        transform.localPosition = initialPosition + new Vector3(shakeOffsetX, shakeOffsetY, 0);
    }
}
