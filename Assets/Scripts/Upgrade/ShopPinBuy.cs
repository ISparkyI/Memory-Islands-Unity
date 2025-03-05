using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections;

[System.Serializable]
public class SkinItem
{
    public GameObject skinObject; // ��'��� ����
    public int skinCost;          // ������� ����
    public TMP_Text buttonText;   // ����� �� ������
    public GameObject coinImage;  // ������ ������
    public Button buyButton;      // ������ �����
}

public class ShopPinBuy : MonoBehaviour
{
    public SkinItem[] skins;
    public TMP_Text coinsText;

    public NotEnoughEnergy notEnoughEnergy;
    private const string ShowCoinsErrorKey = "ShowCoinsError";

    public CoinsConverter coinsConverter;

    private const string CoinsKey = "Coins";
    private int coins;

    void OnEnable()
    {
        foreach (SkinItem skin in skins)
        {
            string skinID = skin.skinObject.name;
            if (PlayerPrefs.GetInt(skinID, 0) == 1)
            {
                SetSkinAsOwned(skin);
            }
            else
            {
                skin.buyButton.onClick.AddListener(() => PinBuy(skin));
            }
        }
    }

    public void PinBuy(SkinItem skin)
    {
        LoadCoins();
        string skinID = skin.skinObject.name;

        if (PlayerPrefs.GetInt(skinID, 0) == 0)
        {
            if (coins >= skin.skinCost)
            {
                SpendCoins(skin.skinCost, () =>
                {
                    PlayerPrefs.SetInt(skinID, 1);
                    SetSkinAsOwned(skin);

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

    private void SetSkinAsOwned(SkinItem skin)
    {
        string currentLanguage = GetCurrentLanguage();
        string ownedText = UpgradesMenu.Owned(currentLanguage);

        skin.buttonText.text = ownedText;
        if (skin.coinImage != null)
        {
            skin.coinImage.SetActive(false);
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
                    return "�������";
                case "ua":
                    return "�������";
                default:
                    return "Owned";
            }
        }
    }
}
