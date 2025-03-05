using UnityEngine;
using TMPro;

public class CoinsResult : MonoBehaviour
{
    public Transform winStarsTransform;
    public TMP_Text coinsResultText;
    public TMP_Text levelText;

    private const string LevelKey = "Level_";

    public void ShowCoinsResult()
    {
        int levelID = ExtractLevelID(levelText.text);
        int starsCollected = CountActiveStars();
        int totalCoins = CalculateCoins(starsCollected);

        bool isFirstCompletion = IsFirstCompletion(levelID);

        if (!isFirstCompletion)
        {
            totalCoins /= 10;
        }
        else
        {
            PlayerPrefs.SetInt(LevelKey + levelID, 1);
            PlayerPrefs.Save();
        }

        coinsResultText.text = totalCoins.ToString();
    }

    private int CountActiveStars()
    {
        int activeStarsCount = 0;

        foreach (Transform child in winStarsTransform)
        {
            if (child.gameObject.activeSelf)
            {
                activeStarsCount++;
            }
        }

        return activeStarsCount;
    }

    private int CalculateCoins(int starsCount)
    {
        int totalCoins = 0;
        for (int i = 0; i < starsCount; i++)
        {
            totalCoins += Random.Range(50, 101);
        }

        return totalCoins;
    }

    private bool IsFirstCompletion(int levelID)
    {
        return PlayerPrefs.GetInt(LevelKey + levelID, 0) == 0;
    }

    private int ExtractLevelID(string levelText)
    {
        string[] parts = levelText.Split(':');
        if (parts.Length > 1 && int.TryParse(parts[1].Trim(), out int levelID))
        {
            return levelID;
        }
        Debug.LogError("Failed to extract level ID from text: " + levelText);
        return 0;
    }

    public void ResetCoinsResult()
    {
        coinsResultText.text = "0";
    }
}
