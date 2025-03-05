using TMPro;
using UnityEngine;
using System.Collections;

public class WinPanelTime : MonoBehaviour
{
    public Timer timerScript;
    public TMP_Text timeText;

    public void StartWinPanelTime()
    {
        Color textColor = timeText.color;
        textColor.a = 0f;
        timeText.color = textColor;

        StartCoroutine(ShowTimeTextDelayed());
    }

    IEnumerator ShowTimeTextDelayed()
    {
        yield return new WaitForSeconds(3.8f);

        StartCoroutine(FadeInText());
    }

    IEnumerator FadeInText()
    {
        float duration = 1f;
        float timer = 0f;

        Color startColor = timeText.color;
        Color endColor = new Color(startColor.r, startColor.g, startColor.b, 1f);

        while (timer < duration)
        {
            float progress = timer / duration;
            Color currentColor = timeText.color;
            currentColor.a = Mathf.Lerp(0f, 1f, progress);
            timeText.color = currentColor;

            timer += Time.deltaTime;
            yield return null;
        }

        timeText.color = endColor;
    }

    void Update()
    {
        if (timerScript != null)
        {
            UpdateTimeDisplay();
        }
    }

    void UpdateTimeDisplay()
    {
        if (timeText != null && timerScript != null)
        {
            float timePassed = timerScript.timePassed;
            int minutes = Mathf.FloorToInt(timePassed / 60f);
            int seconds = Mathf.FloorToInt(timePassed % 60f);
            string timeString = string.Format("{0:00}:{1:00}", minutes, seconds);

            timeText.text = timeString;
        }
    }


}
