using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MenuButtonSelector : MonoBehaviour
{
    public GameObject UpgradeText;
    public GameObject IslandsText;
    public GameObject ShopText;

    public GameObject homeButton;
    public Sprite homeSprite;
    public Sprite homeSelectedSprite;

    public GameObject shopButton;
    public Sprite shopSprite;
    public Sprite shopSelectedSprite;

    public GameObject upgradeButton;
    public Sprite upgradeSprite;
    public Sprite upgradeSelectedSprite;

    public GameObject homeCanvas;
    public GameObject shopCanvas;
    public GameObject upgradeCanvas;

    private GameObject lastSelectedButton;

    public GameObject settings;
    public GameObject playButton;
    public GameObject zoomSlider;

    public TMP_Text CoinsText;
    public TMP_Text RubinsText;

    public GameObject[] objectsToDisable;
    public GameObject mainUpgradeMenu;

    public CoinsConverter coinsConverter;

    private const string CoinsKey = "Coins";
    private const string RubinsKey = "Rubins";

    public Color homeSkyBoxColor = new Color(0.5f, 0.5f, 0.5f); // 808080
    public Color shopSkyBoxColor = new Color(0.494f, 0.243f, 0.286f); // 7E3E49
    public Color upgradeSkyBoxColor = new Color(0.392f, 0.482f, 0.388f); // 647B63

    private void SetButtonSpritesAndSize(GameObject selectedButton, Sprite selectedSprite, GameObject otherButton1, Sprite otherSprite1, GameObject otherButton2, Sprite otherSprite2)
    {
        selectedButton.GetComponent<Image>().sprite = selectedSprite;
        otherButton1.GetComponent<Image>().sprite = otherSprite1;
        otherButton2.GetComponent<Image>().sprite = otherSprite2;

        if (lastSelectedButton != null && lastSelectedButton != selectedButton)
        {
            lastSelectedButton.GetComponent<RectTransform>().sizeDelta = new Vector2(lastSelectedButton.GetComponent<RectTransform>().sizeDelta.x, 171);
            lastSelectedButton.GetComponent<RectTransform>().anchoredPosition = new Vector2(lastSelectedButton.GetComponent<RectTransform>().anchoredPosition.x, lastSelectedButton.GetComponent<RectTransform>().anchoredPosition.y - 22);
        }

        selectedButton.GetComponent<RectTransform>().sizeDelta = new Vector2(selectedButton.GetComponent<RectTransform>().sizeDelta.x, 215);
        selectedButton.GetComponent<RectTransform>().anchoredPosition = new Vector2(selectedButton.GetComponent<RectTransform>().anchoredPosition.x, selectedButton.GetComponent<RectTransform>().anchoredPosition.y + 22);

        lastSelectedButton = selectedButton;
    }

    private void ShowText(GameObject textToShow, GameObject textToHide1, GameObject textToHide2)
    {
        textToShow.SetActive(true);
        textToHide1.SetActive(false);
        textToHide2.SetActive(false);
    }

    private void SetSkyBoxColor(Color color)
    {
        RenderSettings.skybox.SetColor("_Tint", color);
    }

    void Start()
    {
        homeCanvas.SetActive(true);
        shopCanvas.SetActive(false);
        upgradeCanvas.SetActive(false);
        settings.SetActive(true);
        playButton.SetActive(true);
        zoomSlider.SetActive(true);

        SetButtonSpritesAndSize(homeButton, homeSelectedSprite, shopButton, shopSprite, upgradeButton, upgradeSprite);
        ShowText(IslandsText, ShopText, UpgradeText);
        SetSkyBoxColor(homeSkyBoxColor);
    }

    public void OnHomeButtonClick()
    {
        if (lastSelectedButton == homeButton) return;

        homeCanvas.SetActive(true);
        shopCanvas.SetActive(false);
        upgradeCanvas.SetActive(false);
        settings.SetActive(true);
        playButton.SetActive(true);
        zoomSlider.SetActive(true);

        UpdateCurrencyDisplay();

        SetButtonSpritesAndSize(homeButton, homeSelectedSprite, shopButton, shopSprite, upgradeButton, upgradeSprite);
        ShowText(IslandsText, ShopText, UpgradeText);
        SetSkyBoxColor(homeSkyBoxColor);
    }

    public void OnShopButtonClick()
    {
        if (lastSelectedButton == shopButton) return;

        homeCanvas.SetActive(false);
        shopCanvas.SetActive(true);
        upgradeCanvas.SetActive(false);
        settings.SetActive(false);
        playButton.SetActive(false);
        zoomSlider.SetActive(false);

        UpdateCurrencyDisplay();

        SetButtonSpritesAndSize(shopButton, shopSelectedSprite, homeButton, homeSprite, upgradeButton, upgradeSprite);
        ShowText(ShopText, IslandsText, UpgradeText);
        SetSkyBoxColor(shopSkyBoxColor);
    }

    public void OnUpgradeButtonClick()
    {
        if (lastSelectedButton == upgradeButton) return;

        homeCanvas.SetActive(false);
        shopCanvas.SetActive(false);
        upgradeCanvas.SetActive(true);
        settings.SetActive(false);
        playButton.SetActive(false);
        zoomSlider.SetActive(false);
        DisableObjects();
        mainUpgradeMenu.SetActive(true);

        UpdateCurrencyDisplay();

        SetButtonSpritesAndSize(upgradeButton, upgradeSelectedSprite, homeButton, homeSprite, shopButton, shopSprite);
        ShowText(UpgradeText, IslandsText, ShopText);
        SetSkyBoxColor(upgradeSkyBoxColor);
    }

    public void UpdateCurrencyDisplay()
    {
        int coins = PlayerPrefs.GetInt(CoinsKey);
        int rubins = PlayerPrefs.GetInt(RubinsKey);

        CoinsText.text = coins.ToString();
        RubinsText.text = rubins.ToString();

        if (coinsConverter != null)
        {
            coinsConverter.UpdateCoinsText();
        }
    }

    public void DisableObjects()
    {
        foreach (GameObject obj in objectsToDisable)
        {
            if (obj != null)
            {
                obj.SetActive(false);
            }
        }
    }
}
