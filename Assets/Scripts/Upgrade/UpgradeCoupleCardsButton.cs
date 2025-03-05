using UnityEngine;

public class UpgradeCoupleCardsButton : MonoBehaviour
{
    public GameObject upgradeContent;
    public GameObject CoupleCardsUpgrade;

    public void OnUpgradeCoupleCardsButtonClicked()
    {
        upgradeContent.SetActive(false);
        CoupleCardsUpgrade.SetActive(true);
    }
}
