using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class CanClaimXAd : MonoBehaviour
{
    public Button claimButton;
    public AdsManager adsManager;

    private float retryInterval = 1f;

    private void OnEnable()
    {
        InteractableClaimButton();
    }

    private void InteractableClaimButton()
    {
        if (adsManager != null && adsManager.CanShowAd())
        {
            claimButton.interactable = true;
        }
        else
        {
            claimButton.interactable = false;
            StartCoroutine(CheckAndReloadAd());
        }
    }

    private IEnumerator CheckAndReloadAd()
    {
        while (!adsManager.CanShowAd())
        {
            Debug.Log("Реклама не завантажена. Спроба повторного завантаження...");
            adsManager.LoadRewardedAd();

            yield return new WaitForSeconds(retryInterval);

            if (adsManager.CanShowAd())
            {
                Debug.Log("Реклама завантажена успішно.");
                claimButton.interactable = true;
                yield break;
            }
        }
    }
}
