using UnityEngine;
using UnityEngine.UI;

public class CardGenerator : MonoBehaviour
{
    [SerializeField] private GameObject cardPrefab;
    [SerializeField] private Transform lvlPanel;

    private int numberOfPairs;

    public int GetNumberOfPairs()
    {
        return numberOfPairs;
    }

    void Start()
    {
    }

    public void SetNumberOfPairs(int pairs)
    {
        numberOfPairs = pairs;
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

        for (int i = 0; i < numberOfPairs; i++)
        {
            GameObject card1 = Instantiate(cardPrefab, lvlPanel);
            card1.transform.localPosition = Vector3.zero;
            card1.transform.localScale = Vector3.one;
            card1.GetComponent<RectTransform>().sizeDelta = gridLayoutGroup.cellSize;
            card1.name = "Card_" + i + "_1";

            GameObject card2 = Instantiate(cardPrefab, lvlPanel);
            card2.transform.localPosition = Vector3.zero;
            card2.transform.localScale = Vector3.one;
            card2.GetComponent<RectTransform>().sizeDelta = gridLayoutGroup.cellSize;
            card2.name = "Card_" + i + "_2";
        }
    }
}
