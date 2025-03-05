using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections;

[System.Serializable]
public class TvButtons
{
    public Button buytvButton;
    public Button selected;
    public int index;
    public string tvObjectName;
    public int tvCost;
}

public class TVButtonSelected : MonoBehaviour
{
    public CoinsConverter coinsConverter;

    public TvButtons[] buttonPairs;
    public TMP_Text coinsText;
    private const string SelectedTvButtonIndexKey = "SelectedTvButtonIndex";
    private const string SelectedTvObjectNameKey = "SelectedTvObjectName";
    private const string CoinsKey = "Coins";

    private void Start()
    {
        LoadSelectedButton();
        UpdateCoinsDisplay();

        foreach (TvButtons item in buttonPairs)
        {
            item.buytvButton.onClick.AddListener(() => ButtonClicked(item, item.buytvButton));
            item.selected.onClick.AddListener(() => ButtonClicked(item, item.selected));
        }
    }

    private void LoadSelectedButton()
    {
        int lastSelectedIndex = PlayerPrefs.GetInt(SelectedTvButtonIndexKey, -1);

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

        string lastSelectedCardObjectName = PlayerPrefs.GetString(SelectedTvObjectNameKey, "");

        foreach (TvButtons item in buttonPairs)
        {
            if (item.tvObjectName == lastSelectedCardObjectName)
            {
                ButtonClicked(item, item.selected);
                break;
            }
        }
    }

    public void ButtonClicked(TvButtons clickedItem, Button clickedButton)
    {
        string cardID = clickedItem.tvObjectName;

        if (PlayerPrefs.GetInt(cardID, 0) == 1)
        {
            ChangeButtonStates(clickedItem);
            SaveSelectedButton(clickedItem);
        }
        else
        {
            int currentCoins = PlayerPrefs.GetInt(CoinsKey, 0);
            int cardCost = clickedItem.tvCost;

            if (currentCoins >= cardCost)
            {
                StartCoroutine(DeductCoinsAndChangeState(clickedItem, currentCoins, cardCost));
            }
        }
    }

    private IEnumerator DeductCoinsAndChangeState(TvButtons clickedItem, int currentCoins, int cardCost)
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
        PlayerPrefs.SetInt(clickedItem.tvObjectName, 1);
        PlayerPrefs.Save();

        UpdateCoinsDisplay();
        ChangeButtonStates(clickedItem);
        SaveSelectedButton(clickedItem);
    }

    private void ChangeButtonStates(TvButtons clickedItem)
    {
        foreach (TvButtons item in buttonPairs)
        {
            if (item == clickedItem)
            {
                item.selected.gameObject.SetActive(true);
                item.buytvButton.gameObject.SetActive(false);
            }
            else
            {
                item.selected.gameObject.SetActive(false);
                item.buytvButton.gameObject.SetActive(true);
            }
        }
    }

    private void SaveSelectedButton(TvButtons clickedItem)
    {
        int selectedIndex = clickedItem.index;
        PlayerPrefs.SetInt(SelectedTvButtonIndexKey, selectedIndex);
        PlayerPrefs.SetString(SelectedTvObjectNameKey, clickedItem.tvObjectName);
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
