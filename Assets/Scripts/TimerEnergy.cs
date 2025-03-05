using UnityEngine;
using TMPro;
using System.Collections;
using System;

public class TimerEnergy : MonoBehaviour
{
    public TMP_Text timerText;
    public GameObject timerToReload;
    public EnergyLoader energyLoader;
    public EnergyAnimation energyAnimation;

    private const string EnergyKey = "Energy";
    private const string TimerKey = "EnergyTimer";
    private const string LastCheckTimeKey = "LastCheckTime";

    public int countdownSeconds = 240;
    private float timer;

    private const int MaxEnergy = 30;

    void Start()
    {
        if (PlayerPrefs.HasKey(TimerKey) && PlayerPrefs.HasKey(LastCheckTimeKey))
        {
            timer = PlayerPrefs.GetFloat(TimerKey, countdownSeconds);
            DateTime lastCheckTime = DateTime.Parse(PlayerPrefs.GetString(LastCheckTimeKey));
            TimeSpan timeSinceLastCheck = DateTime.Now - lastCheckTime;

            float secondsSinceLastCheck = (float)timeSinceLastCheck.TotalSeconds;
            int energyToAdd = Mathf.FloorToInt(secondsSinceLastCheck / countdownSeconds);

            if (energyToAdd > 0)
            {
                AddEnergy(energyToAdd);
                timer = countdownSeconds - (secondsSinceLastCheck % countdownSeconds);
            }
            else
            {
                timer -= secondsSinceLastCheck;
                if (timer <= 0)
                {
                    timer = countdownSeconds;
                    AddEnergy(1);
                }
            }
        }
        else
        {
            timer = countdownSeconds;
            PlayerPrefs.SetFloat(TimerKey, timer);
            PlayerPrefs.Save();
        }

        UpdateTimerText();
        if (gameObject.activeInHierarchy)
        {
            StartCoroutine(Countdown());
        }
    }

    IEnumerator Countdown()
    {
        while (true)
        {
            if (timer > 1)
            {
                yield return new WaitForSeconds(1f);
                timer--;
                PlayerPrefs.SetFloat(TimerKey, timer);
                PlayerPrefs.SetString(LastCheckTimeKey, DateTime.Now.ToString());
                PlayerPrefs.Save();
                UpdateTimerText();
            }
            else
            {
                AddEnergy(1);
                timer = countdownSeconds;
            }
        }
    }

    void UpdateTimerText()
    {
        int minutes = Mathf.FloorToInt(timer / 60);
        int seconds = Mathf.FloorToInt(timer % 60);
        timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }

    void AddEnergy(int energyToAdd)
    {
        int currentEnergy = PlayerPrefs.GetInt(EnergyKey, 0);
        currentEnergy = Mathf.Min(currentEnergy + energyToAdd, MaxEnergy);
        PlayerPrefs.SetInt(EnergyKey, currentEnergy);
        PlayerPrefs.Save();

        if (energyLoader != null)
        {
            energyLoader.UpdateEnergyText();
        }

        if (energyAnimation != null)
        {
            energyAnimation.StartAnimation();
        }

        if (currentEnergy >= MaxEnergy)
        {
            timerToReload.SetActive(false);
            PlayerPrefs.DeleteKey(TimerKey);
            PlayerPrefs.DeleteKey(LastCheckTimeKey);
            PlayerPrefs.Save();
        }
    }
}
