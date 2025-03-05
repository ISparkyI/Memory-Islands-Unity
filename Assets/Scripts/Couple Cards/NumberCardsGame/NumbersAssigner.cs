using UnityEngine;
using TMPro;

public class NumbersAssigner : MonoBehaviour
{
    public GameObject lvlPanel;
    public NumberCardSizeController numberCardSizeController;
    public NumbersShuffler numbersShuffler;
    private void OnEnable()
    {
        if (numberCardSizeController != null)
        {
            numberCardSizeController.OnCardSizeUpdated += AssignNumbersToCards;
        }
    }

    private void OnDisable()
    {
        if (numberCardSizeController != null)
        {
            numberCardSizeController.OnCardSizeUpdated -= AssignNumbersToCards;
        }
    }
    public void AssignNumbersToCards()
    {
        int totalCards = lvlPanel.transform.childCount;
        int number = 0;

        foreach (Transform child in lvlPanel.transform)
        {
            if (!child.name.Equals("FreeSpace"))
            {
                TMP_Text numberText = child.GetComponentInChildren<TMP_Text>();
                if (numberText != null)
                {
                    numberText.text = number.ToString();
                    number++;
                }
            }
        }
        if (numbersShuffler != null)
        {
            numbersShuffler.ShuffleCards();
        }
    }
}
