using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections;

[System.Serializable]
public class BgButtons
{
    public Button buybgButton;
    public Button selected;
    public int index;
    public string bgObjectName;
    public int bgCost;
}

public class BGButtonSelected : MonoBehaviour
{
    public CoinsConverter coinsConverter;

    public BgButtons[] buttonPairs;
    public TMP_Text coinsText;
    private const string SelectedBgButtonIndexKey = "SelectedBgButtonIndex";
    private const string SelectedBgObjectNameKey = "SelectedBgObjectName";
    private const string CoinsKey = "Coins";

    private void Start()
    {
        LoadSelectedButton();
        UpdateCoinsDisplay();

        foreach (BgButtons item in buttonPairs)
        {
            item.buybgButton.onClick.AddListener(() => ButtonClicked(item, item.buybgButton));
            item.selected.onClick.AddListener(() => ButtonClicked(item, item.selected));
        }
    }

    private void LoadSelectedButton()
    {
        int lastSelectedIndex = PlayerPrefs.GetInt(SelectedBgButtonIndexKey, -1);

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

        string lastSelectedCardObjectName = PlayerPrefs.GetString(SelectedBgObjectNameKey, "");

        foreach (BgButtons item in buttonPairs)
        {
            if (item.bgObjectName == lastSelectedCardObjectName)
            {
                ButtonClicked(item, item.selected);
                break;
            }
        }
    }

    public void ButtonClicked(BgButtons clickedItem, Button clickedButton)
    {
        string cardID = clickedItem.bgObjectName;

        if (PlayerPrefs.GetInt(cardID, 0) == 1)
        {
            ChangeButtonStates(clickedItem);
            SaveSelectedButton(clickedItem);
        }
        else
        {
            int currentCoins = PlayerPrefs.GetInt(CoinsKey, 0);
            int cardCost = clickedItem.bgCost;

            if (currentCoins >= cardCost)
            {
                StartCoroutine(DeductCoinsAndChangeState(clickedItem, currentCoins, cardCost));
            }
        }
    }

    private IEnumerator DeductCoinsAndChangeState(BgButtons clickedItem, int currentCoins, int cardCost)
    {
        int newCoins = currentCoins - cardCost;
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
        PlayerPrefs.SetInt(clickedItem.bgObjectName, 1);
        PlayerPrefs.Save();

        UpdateCoinsDisplay();
        ChangeButtonStates(clickedItem);
        SaveSelectedButton(clickedItem);
    }

    private void ChangeButtonStates(BgButtons clickedItem)
    {
        foreach (BgButtons item in buttonPairs)
        {
            if (item == clickedItem)
            {
                item.selected.gameObject.SetActive(true);
                item.buybgButton.gameObject.SetActive(false);
            }
            else
            {
                item.selected.gameObject.SetActive(false);
                item.buybgButton.gameObject.SetActive(true);
            }
        }
    }

    private void SaveSelectedButton(BgButtons clickedItem)
    {
        int selectedIndex = clickedItem.index;
        PlayerPrefs.SetInt(SelectedBgButtonIndexKey, selectedIndex);
        PlayerPrefs.SetString(SelectedBgObjectNameKey, clickedItem.bgObjectName);
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