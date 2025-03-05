using UnityEngine;
using TMPro;
using System.Collections;
using System;

public class EnergyLoader : MonoBehaviour
{
    public TMP_Text energyText;
    public GameObject timerEnergy;
    public GameObject infinityEnergy;
    public GameObject energyBlockInShop;

    private const string EnergyKey = "Energy";
    private const int MaxEnergy = 30;
    private const string LastExitTimeKey = "LastExitTime";
    private const string InfinityEnergyKey = "InfinityEnergy";

    void Start()
    {
        if (PlayerPrefs.GetInt(InfinityEnergyKey, 0) == 1)
        {
            if (energyText != null) energyText.gameObject.SetActive(false);
            if (timerEnergy != null) timerEnergy.SetActive(false);
            infinityEnergy.SetActive(true);
            energyBlockInShop.SetActive(false);
            timerEnergy.SetActive(false);
        }
        else
        {
            if (!PlayerPrefs.HasKey(EnergyKey))
            {
                InitializeEnergy();
            }
            RestoreEnergyOnGameReturn();
            UpdateEnergyText();
            StartTimerIfNeeded();
        }
    }

    private void InitializeEnergy()
    {
        if (!PlayerPrefs.HasKey(EnergyKey))
        {
            PlayerPrefs.SetInt(EnergyKey, MaxEnergy);
            PlayerPrefs.Save();
        }
    }

    public void UpdateEnergyText()
    {
        int currentEnergy = PlayerPrefs.GetInt(EnergyKey, MaxEnergy);
        energyText.text = currentEnergy + "/" + MaxEnergy;
    }

    public void StartTimerIfNeeded()
    {
        int currentEnergy = PlayerPrefs.GetInt(EnergyKey, MaxEnergy);
        if (currentEnergy < MaxEnergy)
        {
            if (timerEnergy != null)
            {
                timerEnergy.SetActive(true);
                StartCoroutine(CheckEnergy());
            }
        }
        else
        {
            StopTimer();
        }
    }

    private void StopTimer()
    {
        if (timerEnergy != null)
        {
            timerEnergy.SetActive(false);
        }
    }

    IEnumerator CheckEnergy()
    {
        while (timerEnergy.activeSelf && PlayerPrefs.GetInt(EnergyKey, MaxEnergy) < MaxEnergy)
        {
            yield return new WaitForSeconds(1f);
            UpdateEnergyText();
        }
    }

    void OnApplicationPause(bool pauseStatus)
    {
        if (pauseStatus)
        {
            SaveLastExitTime();
        }

    }

    void OnApplicationQuit()
    {
        SaveLastExitTime();
    }

    void SaveLastExitTime()
    {
        PlayerPrefs.SetString(LastExitTimeKey, DateTime.Now.ToString());
        PlayerPrefs.Save();
    }

    void RestoreEnergyOnGameReturn()
    {
        if (PlayerPrefs.HasKey(LastExitTimeKey))
        {
            string dateString = PlayerPrefs.GetString(LastExitTimeKey);
            DateTime lastExitTime = DateTime.Parse(dateString);
            DateTime currentTime = DateTime.Now;
            TimeSpan timeSinceLastExit = currentTime - lastExitTime;

            int currentEnergy = PlayerPrefs.GetInt(EnergyKey, MaxEnergy);

            if (currentEnergy < MaxEnergy)
            {
                float minutesPassed = (float)timeSinceLastExit.TotalMinutes;
                int energyToAdd = Mathf.FloorToInt(minutesPassed / 4f);

                currentEnergy = Mathf.Min(currentEnergy + energyToAdd, MaxEnergy);

                PlayerPrefs.SetInt(EnergyKey, currentEnergy);
                PlayerPrefs.Save();

                UpdateEnergyText();
            }

            PlayerPrefs.SetString(LastExitTimeKey, currentTime.ToString());
            PlayerPrefs.Save();
        }
    }

}
