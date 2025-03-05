using UnityEngine;
using UnityEngine.UI;
using TMPro;
using GoogleMobileAds.Api;

public class XSlider : MonoBehaviour
{
    public Slider slider;
    public float speed = 1.2f;
    public TMP_Text XCoinsText;
    public TMP_Text CoinsResultText;
    public TMP_Text ClaimText;

    private bool movingRight = true;

    void Update()
    {
        if (slider != null)
        {
            if (movingRight)
            {
                slider.value += speed * Time.deltaTime;
                if (slider.value >= slider.maxValue)
                {
                    movingRight = false;
                }
            }
            else
            {
                slider.value -= speed * Time.deltaTime;
                if (slider.value <= slider.minValue)
                {
                    movingRight = true;
                }
            }

            UpdateXCoinsText();
            UpdateClaimText();
        }
    }

    private void UpdateXCoinsText()
    {
        if (int.TryParse(CoinsResultText.text, out int baseCoins))
        {
            float multiplier = GetMultiplier(slider.value);
            int xCoins = Mathf.RoundToInt(baseCoins * multiplier);
            XCoinsText.text = xCoins.ToString();
        }
        else
        {
            Debug.LogError("Не вдалося перетворити текст CoinsResultText на число.");
        }
    }

    private void UpdateClaimText()
    {
        string language = GetCurrentLanguage();
        float multiplier = GetMultiplier(slider.value);
        string claimText = GetClaimText(language);
        ClaimText.text = $"{claimText} X{multiplier}";
    }

    private float GetMultiplier(float sliderValue)
    {
        if (sliderValue <= 0.2f) return 1.5f;      // 0% - 20% (1.5Х)
        if (sliderValue <= 0.4f) return 2.0f;      // 20% - 40% (2Х)
        if (sliderValue <= 0.6f) return 3.0f;      // 40% - 60% (3Х)
        if (sliderValue <= 0.8f) return 2.0f;      // 60% - 80% (2Х)
        return 1.5f;                               // 80% - 100% (1.5Х)
    }

    private string GetCurrentLanguage()
    {
        return PlayerPrefs.GetString("SelectedLanguage", "en");
    }

    private string GetClaimText(string language)
    {
        switch (language)
        {
            case "en":
                return "Claim";
            case "ua":
                return "Забрати";
            case "ru":
                return "Забрать";
            default:
                return "Claim";
        }
    }
}
