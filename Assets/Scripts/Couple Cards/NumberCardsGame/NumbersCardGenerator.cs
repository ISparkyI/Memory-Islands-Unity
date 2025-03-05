using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NumbersCardGenerator : MonoBehaviour
{
    [SerializeField] private GameObject cardPrefab;
    [SerializeField] private Transform lvlPanel;

    private int numberOfCards;

    public int GetNumberOfCards()
    {
        return numberOfCards;
    }

    public void SetNumberOfCards(int cards)
    {
        numberOfCards = cards;
        GenerateCards();
    }

    private void GenerateCards()
    {
        foreach (Transform child in lvlPanel)
        {
            Destroy(child.gameObject);
        }

        GridLayoutGroup gridLayoutGroup = lvlPanel.GetComponent<GridLayoutGroup>();

        if (gridLayoutGroup == null)
        {
            Debug.LogError("LVLPanel does not have a GridLayoutGroup component.");
            return;
        }

        for (int i = 0; i < numberOfCards; i++)
        {
            GameObject card = Instantiate(cardPrefab, lvlPanel);
            card.transform.localPosition = Vector3.zero;
            card.transform.localScale = Vector3.one;
            card.GetComponent<RectTransform>().sizeDelta = gridLayoutGroup.cellSize;
            card.name = "Card_" + i;
        }
    }
}
