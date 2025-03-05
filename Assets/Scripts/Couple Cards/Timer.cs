using UnityEngine;
using TMPro;

public class Timer : MonoBehaviour
{
    public float totalTime = 60f;
    public float timePassed;

    public TMP_Text timerText;

    private bool timerEnded = false;
    public GameObject winPanel;

    void Start()
    {
        ResetTimer();
        UpdateTimerDisplay();
    }

    void Update()
    {
        if (!timerEnded && !winPanel.activeSelf)
        {
            if (timePassed < totalTime)
            {
                timePassed += Time.deltaTime;
                UpdateTimerDisplay();
            }
            else
            {
                timePassed = totalTime;
                timerEnded = true;
                UpdateTimerDisplay();
            }
        }
        else if (winPanel.activeSelf)
        {
            timerEnded = true;
        }
    }

    public void ResetTimer()
    {
        timePassed = 0;
        timerEnded = false;
        UpdateTimerDisplay();
    }

    void UpdateTimerDisplay()
    {
        int remainingTime = Mathf.FloorToInt(totalTime - timePassed);
        int minutes = Mathf.FloorToInt(remainingTime / 60f);
        int seconds = Mathf.FloorToInt(remainingTime % 60f);
        string timerString = string.Format("{0:00}:{1:00}", minutes, seconds);

        timerText.text = timerString;
    }
}
