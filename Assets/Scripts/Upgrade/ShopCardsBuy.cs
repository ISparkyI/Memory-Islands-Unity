using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections;

[System.Serializable]
public class CardItem
{
    public GameObject cardObject; // Об'єкт картки
    public int cardCost;          // Вартість картки
    public TMP_Text buttonText;   // Текст на кнопці
    public GameObject coinImage;  // Іконка монети
    public Button buyButton;      // Кнопка купівлі
}

public class ShopCardsBuy : MonoBehaviour
{
    public CardItem[] cards;
    public TMP_Text coinsText;

    public CoinsConverter coinsConverter;

    public NotEnoughEnergy notEnoughEnergy;
    private const string ShowCoinsErrorKey = "ShowCoinsError";

    private const string CoinsKey = "Coins";
    private int coins;

    private void OnEnable()
    {
        foreach (CardItem card in cards)
        {
            string cardID = card.cardObject.name;
            if (PlayerPrefs.GetInt(cardID, 0) == 1)
            {
                SetCardAsOwned(card);
            }
            else
            {
                card.buyButton.onClick.AddListener(() => CardBuy(card));
            }
        }
    }

    public void CardBuy(CardItem card)
    {
        LoadCoins();
        string cardID = card.cardObject.name;

        if (PlayerPrefs.GetInt(cardID, 0) == 0)
        {
            if (coins >= card.cardCost)
            {
                SpendCoins(card.cardCost, () =>
                {
                    PlayerPrefs.SetInt(cardID, 1);
                    SetCardAsOwned(card);

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

    private void SetCardAsOwned(CardItem card)
    {
        string currentLanguage = GetCurrentLanguage();
        string ownedText = UpgradesMenu.Owned(currentLanguage);

        card.buttonText.text = ownedText;
        if (card.coinImage != null)
        {
            card.coinImage.SetActive(false);
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
