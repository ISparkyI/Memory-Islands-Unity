using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections;

[System.Serializable]
public class CardButtons
{
    public Button buyButton;
    public Button selected;
    public int index;
    public string cardObjectName;
    public int cardCost;
}

public class CardsButtonSelected : MonoBehaviour
{
    public CoinsConverter coinsConverter;

    public CardButtons[] buttonPairs;
    public TMP_Text coinsText;
    private const string SelectedCardButtonIndexKey = "SelectedCardButtonIndex";
    private const string SelectedCardObjectNameKey = "SelectedCardObjectName";
    private const string CoinsKey = "Coins";

    private void Start()
    {
        LoadSelectedButton();
        UpdateCoinsDisplay();

        foreach (CardButtons item in buttonPairs)
        {
            item.buyButton.onClick.AddListener(() => ButtonClicked(item, item.buyButton));
            item.selected.onClick.AddListener(() => ButtonClicked(item, item.selected));
        }
    }

    private void LoadSelectedButton()
    {
        int lastSelectedIndex = PlayerPrefs.GetInt(SelectedCardButtonIndexKey, -1);

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

        string lastSelectedCardObjectName = PlayerPrefs.GetString(SelectedCardObjectNameKey, "");

        foreach (CardButtons item in buttonPairs)
        {
            if (item.cardObjectName == lastSelectedCardObjectName)
            {
                ButtonClicked(item, item.selected);
                break;
            }
        }
    }

    public void ButtonClicked(CardButtons clickedItem, Button clickedButton)
    {
        string cardID = clickedItem.cardObjectName;

        if (PlayerPrefs.GetInt(cardID, 0) == 1)
        {
            ChangeButtonStates(clickedItem);
            SaveSelectedButton(clickedItem);
        }
        else
        {
            int currentCoins = PlayerPrefs.GetInt(CoinsKey, 0);
            int cardCost = clickedItem.cardCost;

            if (currentCoins >= cardCost)
            {
                StartCoroutine(DeductCoinsAndChangeState(clickedItem, currentCoins, cardCost));
            }
        }
    }

    private IEnumerator DeductCoinsAndChangeState(CardButtons clickedItem, int currentCoins, int cardCost)
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
        PlayerPrefs.SetInt(clickedItem.cardObjectName, 1);
        PlayerPrefs.Save();

        UpdateCoinsDisplay();
        ChangeButtonStates(clickedItem);
        SaveSelectedButton(clickedItem);
    }

    private void ChangeButtonStates(CardButtons clickedItem)
    {
        foreach (CardButtons item in buttonPairs)
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

    private void SaveSelectedButton(CardButtons clickedItem)
    {
        int selectedIndex = clickedItem.index;
        PlayerPrefs.SetInt(SelectedCardButtonIndexKey, selectedIndex);
        PlayerPrefs.SetString(SelectedCardObjectNameKey, clickedItem.cardObjectName);
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
