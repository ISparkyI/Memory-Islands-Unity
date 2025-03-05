using System.Collections;
using UnityEngine;
using TMPro;

public class Hint3Sec : MonoBehaviour
{
    public GameObject lvlPanel;
    public TMP_Text Hint3secText;
    private bool isHintActive = false;
    private CardFlipper cardFlipper;
    private int hintsRemaining;

    private void Start()
    {
        cardFlipper = FindAnyObjectByType<CardFlipper>();
        hintsRemaining = PlayerPrefs.GetInt("HintsRemaining", 3);
        UpdateHintText();
    }

    public void ActivateHint()
    {
        if (!isHintActive && CheckRemainingPairs() > 1 && hintsRemaining > 0)
        {
            hintsRemaining--;
            PlayerPrefs.SetInt("HintsRemaining", hintsRemaining);
            PlayerPrefs.Save();
            UpdateHintText();
            StartCoroutine(FlipCardsTemporarily());
        }
    }

    private IEnumerator FlipCardsTemporarily()
    {
        isHintActive = true;

        foreach (Transform child in lvlPanel.transform)
        {
            CardsController cardsController = child.GetComponent<CardsController>();
            if (cardsController != null && !child.name.Equals("FreeSpace") && !child.name.Equals("CardFounded"))
            {
                cardsController.FlipCard();
            }
        }

        yield return new WaitForSeconds(3f);

        foreach (Transform child in lvlPanel.transform)
        {
            CardsController cardsController = child.GetComponent<CardsController>();
            if (cardsController != null && !child.name.Equals("FreeSpace") && !child.name.Equals("CardFounded"))
            {
                cardsController.FlipCard();
            }
        }

        isHintActive = false;
    }

    public bool IsHintActive()
    {
        return isHintActive;
    }

    private int CheckRemainingPairs()
    {
        int pairCount = 0;

        foreach (Transform child in lvlPanel.transform)
        {
            CardsController cardsController = child.GetComponent<CardsController>();
            if (cardsController != null && !child.name.Equals("FreeSpace") && !cardsController.gameObject.name.Equals("CardFounded"))
            {
                pairCount++;
            }
        }

        return pairCount / 2;
    }

    private void UpdateHintText()
    {
        Hint3secText.text = "x" + hintsRemaining;
    }
}
