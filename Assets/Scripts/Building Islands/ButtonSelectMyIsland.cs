using TMPro;
using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ButtonSelectMyIsland : MonoBehaviour
{
    public Button leftButton;
    public Button rightButton;
    public GameObject[] islandObjects;
    public GameObject[] additionalObjects;
    public GameObject[] buttonsObjects;

    public CoinsConverter coinsConverter;

    private int currentIndex = 0;

    private void Start()
    {
        UpdateObjects();

        leftButton.onClick.AddListener(OnLeftButtonClick);
        rightButton.onClick.AddListener(OnRightButtonClick);
    }

    private void OnLeftButtonClick()
    {
        currentIndex--;
        if (currentIndex < 0)
        {
            currentIndex = islandObjects.Length - 1;
        }
        UpdateObjects();
        coinsConverter.UpdateCoinsText();
    }

    private void OnRightButtonClick()
    {
        currentIndex++;
        if (currentIndex >= islandObjects.Length)
        {
            currentIndex = 0;
        }
        UpdateObjects();
        coinsConverter.UpdateCoinsText();
    }

    private void UpdateObjects()
    {
        for (int i = 0; i < islandObjects.Length; i++)
        {
            islandObjects[i].SetActive(i == currentIndex);
            if (i == currentIndex)
            {
                LoadUpgradeLevelsForIsland(islandObjects[i]);
            }
        }

        for (int i = 0; i < additionalObjects.Length; i++)
        {
            additionalObjects[i].SetActive(i == currentIndex);
        }

        for (int i = 0; i < buttonsObjects.Length; i++)
        {
            buttonsObjects[i].SetActive(i == currentIndex);
        }
    }

    private void LoadUpgradeLevelsForIsland(GameObject island)
    {
        var upgradeManager = island.GetComponent<IslandUpgradeManager>();
        if (upgradeManager != null)
        {
            upgradeManager.LoadUpgradeLevels();
        }
    }
}