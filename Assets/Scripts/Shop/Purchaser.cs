using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.Purchasing;

public class Purchaser : MonoBehaviour
{
    public TMP_Text coinsText;
    public TMP_Text rubinsText;
    public CoinsConverter coinsConverter;
    public float animationDuration = 1.0f;

    public GameObject energyBlockInShop;
    public GameObject timerEnergy;

    public GameObject infinityEnergy;
    public GameObject energyText;

    public CoinAnimation coinAnimation;
    public RubinAnimation rubinAnimation;
    public EnergyAnimation energyAnimation;
    public EnergyLoader energyLoader;

    private const string EnergyKey = "Energy";

    public void OnPurchaseCompleted(Product product)
    {
        switch (product.definition.id)
        {
            case "com.gamestagestudio.infinityenergy":
                InfinityEnergy();
                break;
            case "com.gamestagestudio.2000coins":
                AddCoins(2000);
                break;
            case "com.gamestagestudio.5000coins":
                AddCoins(5000);
                break;
            case "com.gamestagestudio.20000coins":
                AddCoins(20000);
                break;
            case "com.gamestagestudio.100000coins":
                AddCoins(100000);
                break;
            case "com.gamestagestudio.20rubins":
                AddRubins(20);
                break;
            case "com.gamestagestudio.50rubins":
                AddRubins(50);
                break;
            case "com.gamestagestudio.150rubins":
                AddRubins(150);
                break;
            case "com.gamestagestudio.500rubins":
                AddRubins(500);
                break;
        }
    }

    private void InfinityEnergy()
    {
        PlayerPrefs.SetInt("InfinityEnergy", 1);
        PlayerPrefs.Save();
        UpdateEnergyDisplay();
    }

    public void AddCoins(int amount)
    {
        int startCoins = PlayerPrefs.GetInt("Coins");
        int endCoins = startCoins + amount;
        PlayerPrefs.SetInt("Coins", endCoins);
        PlayerPrefs.Save();
        coinAnimation.StartAnimation();
        UpdateCoinsText(startCoins, endCoins);
    }

    public void AddRubins(int amount)
    {
        int startRubins = PlayerPrefs.GetInt("Rubins");
        int endRubins = startRubins + amount;
        PlayerPrefs.SetInt("Rubins", endRubins);
        PlayerPrefs.Save();
        rubinAnimation.StartAnimation();
        UpdateRubinsText(startRubins, endRubins);
    }

    public void AddEnergy(int amount)
    {
        int startEnergy = PlayerPrefs.GetInt(EnergyKey);
        int endEnergy = startEnergy + amount;
        PlayerPrefs.SetInt(EnergyKey, endEnergy);
        PlayerPrefs.Save();
        energyAnimation.StartAnimation();
        energyLoader.UpdateEnergyText();
        energyLoader.StartTimerIfNeeded();
    }

    private void UpdateEnergyDisplay()
    {
        if (PlayerPrefs.GetInt("InfinityEnergy") == 1)
        {
            energyText.SetActive(false);
            infinityEnergy.SetActive(true);
            energyBlockInShop.SetActive(false);
            timerEnergy.SetActive(false);
        }
    }

    private void UpdateCoinsText(int startValue, int endValue)
    {
        if (coinsText != null)
        {
            StartCoroutine(AnimateCoinsText(startValue, endValue));
        }
    }

    private void UpdateRubinsText(int startValue, int endValue)
    {
        if (rubinsText != null)
        {
            StartCoroutine(AnimateRubinsText(startValue, endValue));
        }
    }

    private IEnumerator AnimateCoinsText(int startValue, int endValue)
    {
        float elapsedTime = 0f;

        while (elapsedTime < animationDuration)
        {
            elapsedTime += Time.deltaTime;
            int currentValue = (int)Mathf.Lerp(startValue, endValue, elapsedTime / animationDuration);
            coinsText.text = currentValue.ToString();
            yield return null;
        }

        coinsText.text = endValue.ToString();
        if (coinsConverter != null)
        {
            coinsConverter.UpdateCoinsText();
        }
    }

    private IEnumerator AnimateRubinsText(int startValue, int endValue)
    {
        float elapsedTime = 0f;

        while (elapsedTime < animationDuration)
        {
            elapsedTime += Time.deltaTime;
            int currentValue = (int)Mathf.Lerp(startValue, endValue, elapsedTime / animationDuration);
            rubinsText.text = currentValue.ToString();
            yield return null;
        }

        rubinsText.text = endValue.ToString();
    }
}
