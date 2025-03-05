using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloseUpgradeButton : MonoBehaviour
{
    public GameObject upgradeContent;
    public GameObject CoupleCardsUpgrade;

    public void CloseUpgrades()
    {
        upgradeContent.SetActive(true);
        CoupleCardsUpgrade.SetActive(false);
    }
}
