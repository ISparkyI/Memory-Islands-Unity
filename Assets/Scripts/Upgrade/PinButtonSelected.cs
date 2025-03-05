using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections;
using System.Drawing;

[System.Serializable]
public class SkinButtons
{
    public Button buyButton;
    public Button selected;
    public int index;
    public string skinObjectName;
    public int skinCost;
}

public class PinButtonSelected : MonoBehaviour
{
    public CoinsConverter coinsConverter;

    public SkinButtons[] buttonPairs;
    public TMP_Text coinsText;
    private const string SelectedButtonIndexKey = "SelectedButtonIndex";
    private const string SelectedSkinObjectNameKey = "SelectedSkinObjectName";
    private const string CoinsKey = "Coins";

    private void Start()
    {
        LoadSelectedButton();
        UpdateCoinsDisplay();

        foreach (SkinButtons item in buttonPairs)
        {
            item.buyButton.onClick.AddListener(() => ButtonClicked(item, item.buyButton));
            item.selected.onClick.AddListener(() => ButtonClicked(item, item.selected));
        }
    }

    private void LoadSelectedButton()
    {
        int lastSelectedIndex = PlayerPrefs.GetInt(SelectedButtonIndexKey, -1);

        if (lastSelectedIndex != -1 && lastSelectedIndex < buttonPairs.Length)
        {
            ButtonClicked(buttonPairs[lastSelectedIndex], buttonPairs[lastSelectedIndex].selected);
        }
        else
        {
            if (buttonPairs.Length > 0)
            {
                ButtonClicked(buttonPairs[0], buttonPairs[0].selected);
            }
        }

        string lastSelectedSkinObjectName = PlayerPrefs.GetString(SelectedSkinObjectNameKey, "");

        foreach (SkinButtons item in buttonPairs)
        {
            if (item.skinObjectName == lastSelectedSkinObjectName)
            {
                ButtonClicked(item, item.selected);
                break;
            }
        }
    }

    public void ButtonClicked(SkinButtons clickedItem, Button clickedButton)
    {
        string skinID = clickedItem.skinObjectName;

        if (PlayerPrefs.GetInt(skinID, 0) == 1)
        {
            ChangeButtonStates(clickedItem);
            SaveSelectedButton(clickedItem);
        }
        else
        {
            int currentCoins = PlayerPrefs.GetInt(CoinsKey, 0);
            int skinCost = clickedItem.skinCost;

            if (currentCoins >= skinCost)
            {
                StartCoroutine(DeductCoinsAndChangeState(clickedItem, currentCoins, skinCost));
            }
        }
    }

    private IEnumerator DeductCoinsAndChangeState(SkinButtons clickedItem, int currentCoins, int skinCost)
    {
        int newCoins = currentCoins - skinCost;
        float duration = 0.5f;
        float elapsedTime = 0f;

        while (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;
            int currentDisplayCoins = Mathf.RoundToInt(Mathf.Lerp(currentCoins, newCoins, elapsedTime / duration));
            coinsText.text = currentDisplayCoins.ToString();
            yield return null;
        }

        PlayerPrefs.SetInt(CoinsKey, newCoins);
        PlayerPrefs.SetInt(clickedItem.skinObjectName, 1);
        PlayerPrefs.Save();

        UpdateCoinsDisplay();
        ChangeButtonStates(clickedItem);
        SaveSelectedButton(clickedItem);
    }

    private void ChangeButtonStates(SkinButtons clickedItem)
    {
        foreach (SkinButtons item in buttonPairs)
        {
            if (item == clickedItem)
            {
                item.selected.gameObject.SetActive(true);
                item.buyButton.gameObject.SetActive(false);
            }
            else
            {
                item.selected.gameObject.SetActive(false);
                item.buyButton.gameObject.SetActive(true);
            }
        }
    }

    private void SaveSelectedButton(SkinButtons clickedItem)
    {
        int selectedIndex = clickedItem.index;
        PlayerPrefs.SetInt(SelectedButtonIndexKey, selectedIndex);
        PlayerPrefs.SetString(SelectedSkinObjectNameKey, clickedItem.skinObjectName);
        PlayerPrefs.Save();
    }

    private void UpdateCoinsDisplay()
    {
        int currentCoins = PlayerPrefs.GetInt(CoinsKey, 0);
        if (coinsText != null)
        {
            coinsText.text = currentCoins.ToString();
        }

        if (coinsConverter != null)
        {
            coinsConverter.UpdateCoinsText();
        }
    }
}
