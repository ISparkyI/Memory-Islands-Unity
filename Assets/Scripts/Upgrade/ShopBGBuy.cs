using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections;

[System.Serializable]
public class BgItem
{
    public GameObject bgObject; // Об'єкт картки
    public int bgCost;          // Вартість картки
    public TMP_Text buttonText;   // Текст на кнопці
    public GameObject coinImage;  // Іконка монети
    public Button buybgButton;      // Кнопка купівлі
}
public class ShopBGBuy : MonoBehaviour
{
    public BgItem[] bgs;
    public TMP_Text coinsText;

    public CoinsConverter coinsConverter;

    public NotEnoughEnergy notEnoughEnergy;
    private const string ShowCoinsErrorKey = "ShowCoinsError";

    private const string CoinsKey = "Coins";
    private int coins;

    private void OnEnable()
    {
        foreach (BgItem bgskin in bgs)
        {
            string bgskinID = bgskin.bgObject.name;
            if (PlayerPrefs.GetInt(bgskinID, 0) == 1)
            {
                SetbgskinAsOwned(bgskin);
            }
            else
            {
                bgskin.buybgButton.onClick.AddListener(() => bgskinBuy(bgskin));
            }
        }
    }

    public void bgskinBuy(BgItem bgskin)
    {
        LoadCoins();
        string bgskinID = bgskin.bgObject.name;

        if (PlayerPrefs.GetInt(bgskinID, 0) == 0)
        {
            if (coins >= bgskin.bgCost)
            {
                SpendCoins(bgskin.bgCost, () =>
                {
                    PlayerPrefs.SetInt(bgskinID, 1);
                    SetbgskinAsOwned(bgskin);

                    if (coinsConverter != null)
                    {
                        coinsConverter.UpdateCoinsText();
                    }
                });
            }
            else
            {
                PlayerPrefs.SetInt(ShowCoinsErrorKey, 1);
                PlayerPrefs.Save();
                StartCoroutine(notEnoughEnergy.WaitOneSecond());
            }
        }
    }

    private void SetbgskinAsOwned(BgItem bgskin)
    {
        string currentLanguage = GetCurrentLanguage();
        string ownedText = UpgradesMenu.Owned(currentLanguage);

        bgskin.buttonText.text = ownedText;
        if (bgskin.coinImage != null)
        {
            bgskin.coinImage.SetActive(false);
        }
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

    private void SpendCoins(int amount, System.Action onCompleted)
    {
        if (coins >= amount)
        {
            int newCoins = coins - amount;
            StartCoroutine(AnimateCoinsChange(coins, newCoins, 0.5f, () =>
            {
                coins = newCoins;
                SaveCoins();
                onCompleted?.Invoke();
            }));
        }
    }


    private void SaveCoins()
    {
        PlayerPrefs.SetInt(CoinsKey, coins);
        PlayerPrefs.Save();
    }

    private void UpdateCoinsText()
    {
        if (coinsText != null)
        {
            coinsText.text = coins.ToString();
        }

        if (coinsConverter != null)
        {
            coinsConverter.UpdateCoinsText();
        }
    }

    private IEnumerator AnimateCoinsChange(int startValue, int endValue, float duration, System.Action onCompleted)
    {
        float elapsedTime = 0f;
        while (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;
            int currentValue = Mathf.RoundToInt(Mathf.Lerp(startValue, endValue, elapsedTime / duration));
            coinsText.text = currentValue.ToString();
            yield return null;
        }
        coinsText.text = endValue.ToString();
        onCompleted?.Invoke();

        if (coinsConverter != null)
        {
            coinsConverter.UpdateCoinsText();
        }
    }

    private string GetCurrentLanguage()
    {
        return PlayerPrefs.GetString("SelectedLanguage", "en");
    }

    private class UpgradesMenu
    {
        public static string Owned(string language)
        {
            switch (language)
            {
                case "en":
                    return "Owned";
                case "ru":
                    return "Куплено";
                case "ua":
                    return "Куплено";
                default:
                    return "Owned";
            }
        }
    }
}