using UnityEngine;
using UnityEngine.UI;
using System;
using GoogleMobileAds.Api;

public class ShopRewardAd : MonoBehaviour
{
    public Purchaser purchaser;
    public Button energyButton;
    public Button coinsButton;
    public Button rubinsButton;

    private string rewardType;

    private void Start()
    {
        if (energyButton != null) energyButton.onClick.AddListener(() => ShowRewardAd("energy"));
        if (coinsButton != null) coinsButton.onClick.AddListener(() => ShowRewardAd("coins"));
        if (rubinsButton != null) rubinsButton.onClick.AddListener(() => ShowRewardAd("rubins"));
    }

    private void ShowRewardAd(string type)
    {
        rewardType = type;
        AdsManager.instance.ShowRewardedAd(OnAdRewarded);
    }

    private void OnAdRewarded(Reward reward)
    {
        switch (rewardType)
        {
            case "energy":
                purchaser.AddEnergy(2); // ���������, 2 ����㳿
                break;
            case "coins":
                purchaser.AddCoins(200); // ���������, 200 �����
                break;
            case "rubins":
                purchaser.AddRubins(1); // ���������, 1 ����
                break;
        }
    }
}
