using GoogleMobileAds;
using GoogleMobileAds.Api;
using System;
using UnityEngine;

public class AdsManager : MonoBehaviour
{
    private InterstitialAd _interstitialAd;

    private RewardedAd _rewardedAd;
    public AdNotLoadedMessage adNotLoadedMessage;
    public static AdsManager instance;

    private void Start()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }

        MobileAds.Initialize((InitializationStatus initStatus) =>
        {
            LoadInterstitialAd();
            LoadRewardedAd();
        });
    }

    public bool CanShowAd()
    {
        return (_rewardedAd != null && _rewardedAd.CanShowAd());
    }

    //------------------------------------------------------------------------------------------------------//


#if UNITY_ANDROID
    private string _adInterstitialUnitId = "";
#elif UNITY_IPHONE
  private string _adInterstitialUnitId = "";
#else
  private string _adInterstitialUnitId = "unused";
#endif


    public void LoadInterstitialAd()
    {
        if (_interstitialAd != null)
        {
            _interstitialAd.Destroy();
            _interstitialAd = null;
        }

        Debug.Log("Loading the interstitial ad.");

        var adRequest = new AdRequest();

        InterstitialAd.Load(_adInterstitialUnitId, adRequest,
            (InterstitialAd ad, LoadAdError error) =>
            {
                if (error != null || ad == null)
                {
                    Debug.LogError("interstitial ad failed to load an ad " +
                                   "with error : " + error);
                    return;
                }

                Debug.Log("Interstitial ad loaded with response : "
                          + ad.GetResponseInfo());

                _interstitialAd = ad;

                RegisterEventHandlers(_interstitialAd);
            });
    }



    public void ShowInterstitialAd()
    {
        if (_interstitialAd != null && _interstitialAd.CanShowAd())
        {
            Debug.Log("Showing interstitial ad.");
            _interstitialAd.Show();
        }
        else
        {

        }
    }

    private void RegisterEventHandlers(InterstitialAd interstitialAd)
    {
        interstitialAd.OnAdPaid += (AdValue adValue) =>
        {
            Debug.Log(String.Format("Interstitial ad paid {0} {1}.",
                adValue.Value,
                adValue.CurrencyCode));
        };
        interstitialAd.OnAdImpressionRecorded += () =>
        {
            Debug.Log("Interstitial ad recorded an impression.");
        };
        interstitialAd.OnAdClicked += () =>
        {
            Debug.Log("Interstitial ad was clicked.");
        };
        interstitialAd.OnAdFullScreenContentOpened += () =>
        {
            Debug.Log("Interstitial ad full screen content opened.");
        };
        interstitialAd.OnAdFullScreenContentClosed += () =>
        {
            LoadInterstitialAd();
            Debug.Log("Interstitial ad full screen content closed.");

        };
        interstitialAd.OnAdFullScreenContentFailed += (AdError error) =>
        {
            LoadInterstitialAd();
            Debug.LogError("Interstitial ad failed to open full screen content " +
                           "with error : " + error);
        };
    }




    //--------------------------------------------------------------------------------------------------------------------//


#if UNITY_ANDROID
    private string _adRewardedUnitId = "";
#elif UNITY_IPHONE
  private string _adRewardedUnitId = "";
#else
  private string _adRewardedUnitId = "unused";
#endif

    public void LoadRewardedAd()
    {
        if (_rewardedAd != null)
        {
            _rewardedAd.Destroy();
            _rewardedAd = null;
        }

        Debug.Log("Loading the rewarded ad.");

        var adRequest = new AdRequest();

        RewardedAd.Load(_adRewardedUnitId, adRequest,
            (RewardedAd ad, LoadAdError error) =>
            {
                if (error != null || ad == null)
                {
                    Debug.LogError("Rewarded ad failed to load an ad " +
                                   "with error : " + error);
                    return;
                }

                Debug.Log("Rewarded ad loaded with response : "
                          + ad.GetResponseInfo());

                _rewardedAd = ad;

                RegisterEventHandlers(_rewardedAd);
            });
    }


    public void ShowRewardedAd(Action<Reward> onRewardedAdComplete)
    {
        if (_rewardedAd != null && _rewardedAd.CanShowAd())
        {
            Debug.Log("Showing rewarded ad.");
            _rewardedAd.Show((Reward reward) =>
            {
                Debug.Log($"Rewarded ad rewarded the user. Type: {reward.Type}, Amount: {reward.Amount}");
                onRewardedAdComplete?.Invoke(reward);
            });
        }
        else
        {
            adNotLoadedMessage.ShowAdNotLoadedMessage();
        }
    }

    public void ShowRewardedIconAd()
    {
        if (_rewardedAd != null && _rewardedAd.CanShowAd())
        {
            Debug.Log("Showing rewarded ad.");
            _rewardedAd.Show((Reward reward) =>
            {


                Debug.Log($"Rewarded ad rewarded the user. Type: {reward.Type}, Amount: {reward.Amount}");
            });
        }
        else
        {
            Debug.LogError("Rewarded ad is not ready yet.");
        }
    }

    public void ShowRewardedCollectMoreCoinsAd()
    {
        if (_rewardedAd != null && _rewardedAd.CanShowAd())
        {
            Debug.Log("Showing rewarded ad.");
            _rewardedAd.Show((Reward reward) =>
            {


                Debug.Log($"Rewarded ad rewarded the user. Type: {reward.Type}, Amount: {reward.Amount}");
            });
        }
        else
        {
            Debug.LogError("Rewarded ad is not ready yet.");
        }
    }

    private void RegisterEventHandlers(RewardedAd ad)
    {
        ad.OnAdPaid += (AdValue adValue) =>
        {
            Debug.Log(String.Format("Rewarded ad paid {0} {1}.",
                adValue.Value,
                adValue.CurrencyCode));
        };
        ad.OnAdImpressionRecorded += () =>
        {
            Debug.Log("Rewarded ad recorded an impression.");
        };
        ad.OnAdClicked += () =>
        {
            Debug.Log("Rewarded ad was clicked.");
        };
        ad.OnAdFullScreenContentOpened += () =>
        {
            Debug.Log("Rewarded ad full screen content opened.");
        };
        ad.OnAdFullScreenContentClosed += () =>
        {
            LoadRewardedAd();
            Debug.Log("Rewarded ad full screen content closed.");
        };
        ad.OnAdFullScreenContentFailed += (AdError error) =>
        {
            LoadRewardedAd();
            Debug.LogError("Rewarded ad failed to open full screen content " +
                           "with error : " + error);
        };
    }

}
