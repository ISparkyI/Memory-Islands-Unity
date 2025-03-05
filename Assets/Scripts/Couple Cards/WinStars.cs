using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class WinStars : MonoBehaviour
{
    public Image[] stars;
    private int starsToShow = 0;
    private int levelIndex;

    private Vector3 defaultScale = Vector3.one;
    private float animationDuration = 0.5f;
    private float delay = 0.3f;
    private float starDelay = 0.2f;
    private float initialRotationAmplitude = 10f;
    private int rotationAmplitudeMultiplier = 2;
    public AfterStarsButton afterStarsButton;

    public WinCoinAnim winCoinAnim;

    private bool skipAnimation = false;

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            skipAnimation = true;
        }
    }

    public void StartWinStars()
    {
        skipAnimation = false;

        levelIndex = PlayerPrefs.GetInt("CurrentLevel");
        transform.localScale = Vector3.zero;

        foreach (Image star in stars)
        {
            star.gameObject.SetActive(false);
        }

        StartCoroutine(AnimateAppearance());
    }

    public void SetStarsToShow(int count)
    {
        starsToShow = count;
    }

    private IEnumerator AnimateAppearance()
    {
        yield return new WaitForSeconds(delay);

        float timer = 0f;
        while (timer < animationDuration)
        {
            if (skipAnimation)
            {
                transform.localScale = defaultScale;
                break;
            }
            transform.localScale = Vector3.Lerp(Vector3.zero, defaultScale, timer / animationDuration);
            timer += Time.deltaTime;
            yield return null;
        }

        transform.localScale = defaultScale;

        for (int i = 0; i < starsToShow; i++)
        {
            stars[i].gameObject.SetActive(true);
            yield return StartCoroutine(AnimateStarAppearance(stars[i].transform));

            if (skipAnimation)
            {
                break;
            }
            yield return new WaitForSeconds(starDelay);
        }

        ShowCorrectStars();

        winCoinAnim.StartCoinAnimation();

        if (afterStarsButton != null)
        {
            afterStarsButton.ActivateButtons();
        }
    }

    private void ShowCorrectStars()
    {
        int currentStars = PlayerPrefs.GetInt("StarsLevel_" + levelIndex, 0);

        if (starsToShow > currentStars)
        {
            PlayerPrefs.SetInt("StarsLevel_" + levelIndex, starsToShow);
            PlayerPrefs.Save();
        }

        for (int i = 0; i < stars.Length; i++)
        {
            stars[i].gameObject.SetActive(i < starsToShow);
        }
    }

    private IEnumerator AnimateStarAppearance(Transform star)
    {
        Vector3 starInitialScale = star.localScale;
        Quaternion startRotation = star.localRotation;
        float timer = 0f;
        float currentAmplitude = initialRotationAmplitude;

        while (timer < animationDuration && !skipAnimation)
        {
            float progress = timer / animationDuration;
            float rotationAngle = Mathf.Sin(progress * Mathf.PI * 2) * currentAmplitude;
            star.localRotation = startRotation * Quaternion.Euler(0f, 0f, rotationAngle);

            star.localScale = Vector3.Lerp(Vector3.zero, starInitialScale, progress);
            timer += Time.deltaTime;

            if (progress < 0.5f)
            {
                currentAmplitude = Mathf.Lerp(initialRotationAmplitude, initialRotationAmplitude * rotationAmplitudeMultiplier, progress * 2f);
            }
            else
            {
                currentAmplitude = Mathf.Lerp(initialRotationAmplitude * rotationAmplitudeMultiplier, initialRotationAmplitude, (progress - 0.5f) * 2f);
            }

            yield return null;
        }

        star.localScale = starInitialScale;
        star.localRotation = startRotation;

        skipAnimation = false;
    }
}
