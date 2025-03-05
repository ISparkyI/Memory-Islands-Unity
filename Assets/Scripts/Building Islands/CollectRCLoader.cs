using UnityEngine;
using System.Collections;

public class CollectRCLoader : MonoBehaviour
{
    public GameObject collectRC;
    public float delayBeforeShowing = 1f;
    public float animationDuration = 0.5f;

    private const string RubinsPerHourKey = "RubinsPerHour";
    private const string CoinsPerHourKey = "CoinsPerHour";
    private const float MinTimeBetweenShows = 1800f;

    private float secondsAway;

    void Start()
    {
        StartCoroutine(WaitAndCheck());
    }

    public void SetSecondsAway(float value)
    {
        secondsAway = value;
    }

    private IEnumerator WaitAndCheck()
    {
        yield return new WaitForSeconds(delayBeforeShowing);

        if (secondsAway >= MinTimeBetweenShows)
        {
            CheckAndActivateCollectRC();
        }
        else
        {
            CloseCollectRC();
        }
    }

    private void CheckAndActivateCollectRC()
    {
        int rubinsPerHour = PlayerPrefs.GetInt(RubinsPerHourKey, 0);
        int coinsPerHour = PlayerPrefs.GetInt(CoinsPerHourKey, 0);

        if (rubinsPerHour > 0 || coinsPerHour > 0)
        {
            if (collectRC != null)
            {
                collectRC.SetActive(true);
                StartCoroutine(AnimateCollectRC());
            }
        }
        else
        {
            CloseCollectRC();
        }
    }

    private IEnumerator AnimateCollectRC()
    {
        RectTransform rt = collectRC.GetComponent<RectTransform>();
        if (rt == null) yield break;

        Vector2 originalSize = rt.sizeDelta;
        rt.sizeDelta = Vector2.zero;
        rt.localScale = Vector3.zero;

        float elapsedTime = 0f;
        while (elapsedTime < animationDuration)
        {
            elapsedTime += Time.deltaTime;
            float t = Mathf.Clamp01(elapsedTime / animationDuration);

            rt.sizeDelta = Vector2.Lerp(Vector2.zero, originalSize, t);
            rt.localScale = Vector3.Lerp(Vector3.zero, Vector3.one, t);

            yield return null;
        }

        rt.sizeDelta = originalSize;
        rt.localScale = Vector3.one;
    }

    public void CloseCollectRC()
    {
        if (collectRC != null)
        {
            collectRC.SetActive(false);
        }
    }
}
