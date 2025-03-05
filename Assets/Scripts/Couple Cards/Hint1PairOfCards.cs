using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Hint1PairOfCards : MonoBehaviour
{
    public GameObject lvlPanel;
    public TMP_Text Hint1PairCardsText;
    private int hintsRemaining;
    private bool isProcessing;

    private void Start()
    {
        hintsRemaining = PlayerPrefs.GetInt("Hints1PairRemaining", 3);
        UpdateHintText();
    }

    public void Remove1PairOfCards()
    {
        if (!isProcessing && hintsRemaining > 0 && CheckRemainingPairs() > 1)
        {
            hintsRemaining--;
            PlayerPrefs.SetInt("Hints1PairRemaining", hintsRemaining);
            PlayerPrefs.Save();
            UpdateHintText();
            StartCoroutine(RemovePair());
        }
    }

    private IEnumerator RemovePair()
    {
        isProcessing = true;
        List<Transform> remainingPairs = GetRemainingPairs();

        if (remainingPairs.Count >= 2)
        {
            CardsController card1 = remainingPairs[0].GetComponent<CardsController>();
            CardsController card2 = GetPairOfCard(card1).GetComponent<CardsController>();

            StartCoroutine(FlipCardAnimation(card1));
            StartCoroutine(FlipCardAnimation(card2));

            yield return new WaitForSeconds(0.2f);

            StartCoroutine(AnimateCardShrink(card1));
            StartCoroutine(AnimateCardShrink(card2));

            yield return new WaitForSeconds(0.5f);

            isProcessing = false;
        }
    }

    private List<Transform> GetRemainingPairs()
    {
        List<Transform> remainingPairs = new List<Transform>();
        HashSet<int> processedPairs = new HashSet<int>();

        foreach (Transform child in lvlPanel.transform)
        {
            CardsController cardsController = child.GetComponent<CardsController>();
            if (cardsController != null && !child.name.Equals("FreeSpace") && !cardsController.gameObject.name.Equals("CardFounded"))
            {
                string[] nameParts = child.name.Split('_');
                if (nameParts.Length >= 3)
                {
                    int pairIndex;
                    if (int.TryParse(nameParts[1], out pairIndex))
                    {
                        if (!processedPairs.Contains(pairIndex))
                        {
                            processedPairs.Add(pairIndex);
                            remainingPairs.Add(child);
                        }
                    }
                }
            }
        }

        return remainingPairs;
    }

    private CardsController GetPairOfCard(CardsController card)
    {
        string cardName = card.gameObject.name;
        string[] nameParts = cardName.Split('_');
        if (nameParts.Length < 3)
            return null;

        string pairName = nameParts[0] + "_" + nameParts[1];
        foreach (Transform child in lvlPanel.transform)
        {
            CardsController otherCard = child.GetComponent<CardsController>();
            if (otherCard != null && child.gameObject != card.gameObject && child.gameObject.name.StartsWith(pairName))
            {
                return otherCard;
            }
        }

        return null;
    }

    private int CheckRemainingPairs()
    {
        HashSet<int> remainingPairs = new HashSet<int>();

        foreach (Transform child in lvlPanel.transform)
        {
            CardsController cardsController = child.GetComponent<CardsController>();
            if (cardsController != null && !child.name.Equals("FreeSpace") && !cardsController.gameObject.name.Equals("CardFounded"))
            {
                string[] nameParts = child.name.Split('_');
                if (nameParts.Length >= 3)
                {
                    int pairIndex;
                    if (int.TryParse(nameParts[1], out pairIndex))
                    {
                        remainingPairs.Add(pairIndex);
                    }
                }
            }
        }

        return remainingPairs.Count;
    }

    private IEnumerator FlipCardAnimation(CardsController card)
    {
        float duration = 0.2f;
        float timer = 0f;
        Vector3 originalScale = card.transform.localScale;

        while (timer < duration)
        {
            float scale = Mathf.Lerp(1f, 0f, Mathf.PingPong(timer * 2, 1f));
            card.transform.localScale = new Vector3(originalScale.x * scale, originalScale.y, originalScale.z);
            timer += Time.deltaTime;
            yield return null;
        }

        card.FlipCardToBack();

        card.transform.localScale = originalScale;
    }

    private IEnumerator AnimateCardShrink(CardsController card)
    {
        float delay = 0.25f;
        yield return new WaitForSeconds(delay);

        float duration = 0.25f; 
        float timer = 0f;
        Vector3 originalScale = card.transform.localScale;

        while (timer < duration)
        {
            float scale = Mathf.Lerp(1f, 0f, timer / duration);
            card.transform.localScale = originalScale * scale;

            SetAlphaRecursively(card.transform, 1f - (timer / duration));

            timer += Time.deltaTime;
            yield return null;
        }

        SetAlphaRecursively(card.transform, 0f);
        card.gameObject.name = "CardFounded";
    }

    private void SetAlphaRecursively(Transform parent, float alpha)
    {
        Image image = parent.GetComponent<Image>();
        if (image != null)
        {
            Color color = image.color;
            color.a = alpha;
            image.color = color;
        }

        foreach (Transform child in parent)
        {
            SetAlphaRecursively(child, alpha);
        }
    }

    private void UpdateHintText()
    {
        Hint1PairCardsText.text = "x" + hintsRemaining;
    }
}
