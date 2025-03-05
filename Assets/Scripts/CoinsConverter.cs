using UnityEngine;
using TMPro;

public class CoinsConverter : MonoBehaviour
{
    public TMP_Text coinsText;
    private const string CoinsKey = "Coins";

    public void UpdateCoinsText()
    {
        int coins = PlayerPrefs.GetInt(CoinsKey);
        coinsText.text = FormatCoins(coins);
    }

    string FormatCoins(int coins)
    {
        if (coins < 10000)
        {
            return coins.ToString();
        }
        else if (coins < 100000)
        {
            return (coins / 1000f).ToString("0.#") + "k";
        }
        else if (coins < 1000000)
        {
            return (coins / 1000f).ToString("0.0") + "k";
        }
        else if (coins < 10000000)
        {
            return (coins / 1000000f).ToString("0.#") + "M";
        }
        else
        {
            return (coins / 1000000f).ToString("0.0") + "M";
        }
    }
}
