using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TVLvlsManager : MonoBehaviour
{
    [SerializeField] private TVCardsGenerator cardGenerator;
    [SerializeField] private int numberOfCards;
    [SerializeField] private TMP_Text levelText;
    [SerializeField] private Timer timer;
    [SerializeField] private float levelTime;
    [SerializeField] private Stars stars;
    [SerializeField] private float changeInterval;
    [SerializeField] private TVScreenCard screenCard;
    [SerializeField] private int movesLeftPerLevel;
    [SerializeField] private TMP_Text movesLeftText;

    private TMP_Text lvl;

    private void Start()
    {
        lvl = GetComponentInChildren<TMP_Text>();
    }

    public void OnLevelButtonClick()
    {
        SetLevel();
        UpdateLevelText();
        SetTimer();
        ResetStars();
        SetMovesLeft();
        UpdateLevelNumber();
        SetChangeInterval();
    }

    private void SetLevel()
    {
        if (cardGenerator != null)
        {
            cardGenerator.SetNumberOfCards(numberOfCards);
        }
    }

    private void UpdateLevelText()
    {
        if (levelText != null && lvl != null)
        {
            string language = GetCurrentLanguage();
            string levelPrefix = GetLevelPrefix(language);
            levelText.text = $"{levelPrefix} : {lvl.text}";
        }
    }

    private void UpdateLevelNumber()
    {
        if (lvl != null)
        {
            int currentLevel;
            if (int.TryParse(lvl.text, out currentLevel))
            {
                PlayerPrefs.SetInt("CurrentLevel", currentLevel);
                PlayerPrefs.Save();
            }
        }
    }

    private void SetTimer()
    {
        if (timer != null)
        {
            timer.totalTime = levelTime;
            timer.ResetTimer();
        }
    }

    private void ResetStars()
    {
        if (stars != null)
        {
            stars.ResetStars();
        }
    }

    private void SetChangeInterval()
    {
        if (screenCard != null)
        {
            screenCard.SetChangeInterval(changeInterval);
        }
    }

    private void SetMovesLeft()
    {
        if (movesLeftText != null)
        {
            movesLeftText.text = movesLeftPerLevel.ToString();
        }
    }

    private string GetCurrentLanguage()
    {
        return PlayerPrefs.GetString("SelectedLanguage", "en");
    }

    private string GetLevelPrefix(string language)
    {
        switch (language)
        {
            case "en":
                return "LVL";
            case "ua":
                return "Рівень";
            case "ru":
                return "Уровень";
            default:
                return "LVL";
        }
    }
}
