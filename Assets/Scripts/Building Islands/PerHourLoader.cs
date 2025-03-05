using TMPro;
using UnityEngine;

public class PerHourLoader : MonoBehaviour
{
    [Header("Text References")]
    public TextMeshProUGUI rubinsPerHourText;
    public TextMeshProUGUI coinsPerHourText;

    private void Start()
    {
        UpdateText();
    }

    public void UpdateText()
    {
        UpdateRubinsPerHourText();
        UpdateCoinsPerHourText();
    }

    private string GetCurrentLanguage()
    {
        return PlayerPrefs.GetString("SelectedLanguage", "en");
    }

    private string GetRubinsPerHourText(string language)
    {
        switch (language)
        {
            case "en":
                return "per hour";
            case "ua":
                return "за годину";
            case "ru":
                return "в час";
            default:
                return "per hour";
        }
    }

    private string GetCoinsPerHourText(string language)
    {
        switch (language)
        {
            case "en":
                return "per hour";
            case "ua":
                return "за годину";
            case "ru":
                return "в час";
            default:
                return "per hour";
        }
    }

    public void UpdateCoinsPerHourText()
    {
        int coinsPerHour = PlayerPrefs.GetInt("CoinsPerHour", 0);
        string language = GetCurrentLanguage();
        string translation = GetCoinsPerHourText(language);
        coinsPerHourText.text = $"+ {coinsPerHour} {translation}";
    }

    public void UpdateRubinsPerHourText()
    {
        int rubinsPerHour = PlayerPrefs.GetInt("RubinsPerHour", 0);
        string language = GetCurrentLanguage();
        string translation = GetRubinsPerHourText(language);
        rubinsPerHourText.text = $"+ {rubinsPerHour} {translation}";
    }
}
