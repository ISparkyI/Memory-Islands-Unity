using UnityEngine;
using TMPro;
using System.Collections;

public class ShopHintBuy : MonoBehaviour
{
    public TMP_Text SecHintCountText;
    public TMP_Text PercentHintCountText;
    public TMP_Text PairHintCountText;
    public TMP_Text CoinsText;

    public CoinsConverter coinsConverter;

    public NotEnoughEnergy notEnoughEnergy;
    private const string ShowCoinsErrorKey = "ShowCoinsError";

    private const string CoinsKey = "Coins";
    private int coins;

    private void Start()
    {
        UpdateHintCounts();
    }

    public void Buy3SecHint()
    {
        const int hintCost = 145;
        LoadCoins();
        if (coins >= hintCost)
        {
            int hintsRemaining = PlayerPrefs.GetInt("HintsRemaining", 3);
            hintsRemaining++;
            PlayerPrefs.SetInt("HintsRemaining", hintsRemaining);
            PlayerPrefs.Save();
            SpendCoins(hintCost);
            UpdateHintCounts();
        }
        else
        {
            PlayerPrefs.SetInt(ShowCoinsErrorKey, 1);
            PlayerPrefs.Save();
            StartCoroutine(notEnoughEnergy.WaitOneSecond());
        }
    }

    public void Buy50PercentHint()
    {
        const int hintCost = 225;
        LoadCoins();
        if (coins >= hintCost)
        {
            int hints50PercentRemaining = PlayerPrefs.GetInt("Hints50PercentRemaining", 3);
            hints50PercentRemaining++;
            PlayerPrefs.SetInt("Hints50PercentRemaining", hints50PercentRemaining);
            PlayerPrefs.Save();
            SpendCoins(hintCost);
            UpdateHintCounts();
        }
        else
        {
            PlayerPrefs.SetInt(ShowCoinsErrorKey, 1);
            PlayerPrefs.Save();
            StartCoroutine(notEnoughEnergy.WaitOneSecond());
        }
    }

    public void Buy1PairHint()
    {
        const int hintCost = 85;
        LoadCoins();
        if (coins >= hintCost)
        {
            int hints1PairRemaining = PlayerPrefs.GetInt("Hints1PairRemaining", 3);
            hints1PairRemaining++;
            PlayerPrefs.SetInt("Hints1PairRemaining", hints1PairRemaining);
            PlayerPrefs.Save();
            SpendCoins(hintCost);
            UpdateHintCounts();
        }
        else
        {
            PlayerPrefs.SetInt(ShowCoinsErrorKey, 1);
            PlayerPrefs.Save();
            StartCoroutine(notEnoughEnergy.WaitOneSecond());
        }
    }

    private void UpdateHintCounts()
    {
        int hintsRemaining = PlayerPrefs.GetInt("HintsRemaining", 3);
        int hints50PercentRemaining = PlayerPrefs.GetInt("Hints50PercentRemaining", 3);
        int hints1PairRemaining = PlayerPrefs.GetInt("Hints1PairRemaining", 3);

        SecHintCountText.text = "x" + hintsRemaining;
        PercentHintCountText.text = "x" + hints50PercentRemaining;
        PairHintCountText.text = "x" + hints1PairRemaining;
    }

    private void LoadCoins()
    {
        if (PlayerPrefs.HasKey(CoinsKey))
        {
            coins = PlayerPrefs.GetInt(CoinsKey);
        }
        else
        {
            coins = 500;
            SaveCoins();
        }

        UpdateCoinsText();
    }

    private void SpendCoins(int amount)
    {
        if (coins >= amount)
        {
            StartCoroutine(AnimateCoinsChange(coins, coins - amount, 0.5f));
            coins -= amount;
            SaveCoins();
        }
        else
        {
            PlayerPrefs.SetInt(ShowCoinsErrorKey, 1);
            PlayerPrefs.Save();
            StartCoroutine(notEnoughEnergy.WaitOneSecond());
        }
    }

    private void SaveCoins()
    {
        PlayerPrefs.SetInt(CoinsKey, coins);
        PlayerPrefs.Save();
    }

    private void UpdateCoinsText()
    {
        if (CoinsText != null)
        {
            CoinsText.text = coins.ToString();
        }

        if (coinsConverter != null)
        {
            coinsConverter.UpdateCoinsText();
        }
    }

    private IEnumerator AnimateCoinsChange(int startValue, int endValue, float duration)
    {
        float elapsedTime = 0f;
        while (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;
            int currentValue = Mathf.RoundToInt(Mathf.Lerp(startValue, endValue, elapsedTime / duration));
            CoinsText.text = currentValue.ToString();
            yield return null;
        }
        CoinsText.text = endValue.ToString();

        if (coinsConverter != null)
        {
            coinsConverter.UpdateCoinsText();
        }
    }
}
