using UnityEngine;
using UnityEngine.UI;

public class FreeOrAdSlider : MonoBehaviour
{
    public Slider slider;
    public float speed = 1.2f;

    public ReloadLvl reloadLvl;

    private bool movingRight = true;

    public AdsManager adsManager;

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
        }
    }

    public void OnReloadButtonClicked()
    {
        float sliderPercentage = (slider.value - slider.minValue) / (slider.maxValue - slider.minValue);

        if (sliderPercentage < 0.5f)
        {
            ReloadLevel();
        }
        else
        {
            ShowAdAndReload();
        }
    }

    private void ShowAdAndReload()
    {
        if (adsManager != null && adsManager.CanShowAd())
        {
            adsManager.ShowRewardedCollectMoreCoinsAd();
            reloadLvl.ReloadLevel();
        }
        else
        {
            reloadLvl.ReloadLevel();
        }
    }

    private void ReloadLevel()
    {
        reloadLvl.ReloadLevel();
    }

    public void ShowAdAndExite()
    {

    }
}
