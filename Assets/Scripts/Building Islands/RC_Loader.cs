using UnityEngine;
using TMPro;
using System;
using System.Collections;

public class RC_Loader : MonoBehaviour
{
    private const string LastExitTimeKey = "LastExitTime";
    private const string RubinsPerHourKey = "RubinsPerHour";
    private const string CoinsPerHourKey = "CoinsPerHour";
    private const int MaxAwayTimeInSeconds = 10000;

    private const string CoinsKey = "Coins";
    private const string RubinsKey = "Rubins";

    public TextMeshProUGUI CoinText;
    public TextMeshProUGUI RubinsText;
    public TextMeshPro AllCoinsText;
    public TextMeshPro AllRubinsText;
    public CoinsConverter coinsConverter;
    public CollectRCLoader collectRCLoader;

    public CoinAnimation coinAnimation;
    public RubinAnimation rubinAnimation;

    private int earnedRubins;
    private int earnedCoins;

    private void Start()
    {
        DateTime currentTime = DateTime.Now;

        string lastExitTimeString = PlayerPrefs.GetString(LastExitTimeKey, currentTime.ToString());
        DateTime lastExitTime = DateTime.Parse(lastExitTimeString);

        double secondsAway = (currentTime - lastExitTime).TotalSeconds;

        int rubinsPerHour = PlayerPrefs.GetInt(RubinsPerHourKey, 0);
        int coinsPerHour = PlayerPrefs.GetInt(CoinsPerHourKey, 0);

        if (secondsAway > MaxAwayTimeInSeconds)
        {
            earnedRubins = rubinsPerHour * 3;
            earnedCoins = coinsPerHour * 3;
        }
        else
        {
            earnedRubins = Mathf.FloorToInt((float)(rubinsPerHour * secondsAway / 3600));
            earnedCoins = Mathf.FloorToInt((float)(coinsPerHour * secondsAway / 3600));
        }

        CoinText.text = $"+ {earnedCoins}";
        RubinsText.text = $"+ {earnedRubins}";

        PlayerPrefs.SetString(LastExitTimeKey, currentTime.ToString());
        PlayerPrefs.Save();

        if (collectRCLoader != null)
        {
            collectRCLoader.SetSecondsAway((float)secondsAway);
        }
    }

    public void CollectCoinsAndRubins()
    {
        int currentCoins = PlayerPrefs.GetInt(CoinsKey, 0);
        int currentRubins = PlayerPrefs.GetInt(RubinsKey, 0);

        int newCoins = currentCoins + earnedCoins;
        int newRubins = currentRubins + earnedRubins;

        PlayerPrefs.SetInt(CoinsKey, newCoins);
        PlayerPrefs.SetInt(RubinsKey, newRubins);
        PlayerPrefs.Save();

        rubinAnimation.StartAnimation();
        coinAnimation.StartAnimation();

        StartCoroutine(AnimateCoinsText(currentCoins, newCoins));
        StartCoroutine(AnimateRubinsText(currentRubins, newRubins));
    }

    private IEnumerator AnimateCoinsText(int startValue, int endValue)
    {
        float animationDuration = 1f;
        float elapsedTime = 0f;

        while (elapsedTime < animationDuration)
        {
            elapsedTime += Time.deltaTime;
            int currentValue = (int)Mathf.Lerp(startValue, endValue, elapsedTime / animationDuration);
            AllCoinsText.text = currentValue.ToString();
            yield return null;
        }

        AllCoinsText.text = endValue.ToString();

        if (coinsConverter != null)
        {
            coinsConverter.UpdateCoinsText();
        }
    }

    private IEnumerator AnimateRubinsText(int startValue, int endValue)
    {
        float animationDuration = 1f;
        float elapsedTime = 0f;

        while (elapsedTime < animationDuration)
        {
            elapsedTime += Time.deltaTime;
            int currentValue = (int)Mathf.Lerp(startValue, endValue, elapsedTime / animationDuration);
            AllRubinsText.text = currentValue.ToString();
            yield return null;
        }

        AllRubinsText.text = endValue.ToString();
    }
}
