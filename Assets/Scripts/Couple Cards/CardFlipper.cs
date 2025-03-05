using UnityEngine;
using System.Collections.Generic;
using System.Collections;
using UnityEngine.UI;

public class CardFlipper : MonoBehaviour
{
    private int flippedCardsCount = 0;
    private List<CardsController> flippedCards = new List<CardsController>();
    private bool delayActive = false;

    public GameObject LvlPanel;
    public GameObject WinPanel;
    public Hint3Sec hint3Sec;

    public AudioSource flipCardSound;
    public AudioSource flipCardBackSound;

    public bool CanFlip()
    {
        return flippedCardsCount < 2 && !delayActive && (hint3Sec == null || !hint3Sec.IsHintActive());
    }

    public void IncreaseFlippedCount()
    {
        flippedCardsCount++;
    }

    public void DecreaseFlippedCount()
    {
        flippedCardsCount--;
    }

    public void AddFlippedCard(CardsController card)
    {
        flippedCards.Add(card);
        if (flipCardSound != null)
        {
            flipCardSound.Play();
        }
    }

    public void RemoveFlippedCard(CardsController card)
    {
        flippedCards.Remove(card);

        if (flipCardBackSound != null)
        {
            flipCardBackSound.Play();
        }
    }

    public void CheckMatchedCards()
    {
        if (flippedCards.Count == 2 && (hint3Sec == null || !hint3Sec.IsHintActive()))
        {
            Sprite sprite1 = flippedCards[0].isFrontSide ? flippedCards[0].frontSideCard : flippedCards[0].backSideCard;
            Sprite sprite2 = flippedCards[1].isFrontSide ? flippedCards[1].frontSideCard : flippedCards[1].backSideCard;

            if (sprite1 == sprite2)
            {
                StartCoroutine(AnimateCardShrink(flippedCards[0]));
                StartCoroutine(AnimateCardShrink(flippedCards[1]));

                flippedCards[0].gameObject.name = "CardFounded";
                flippedCards[1].gameObject.name = "CardFounded";

                flippedCards.Clear();
                flippedCardsCount = 0;

                StartCoroutine(ActivateWinPanelWithDelay());
            }
            else
            {
                if (flipCardBackSound != null)
                {
                    flipCardBackSound.Play();
                }

                StartCoroutine(FlipCardsBackAfterDelay(flippedCards[0], flippedCards[1]));

                flippedCards.Clear();
                flippedCardsCount = 0;
            }
        }
    }

    public IEnumerator AnimateCardShrink(CardsController card)
    {
        float delay = 0.3f;
        yield return new WaitForSeconds(delay);

        float duration = 0.5f;
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

    private IEnumerator FlipCardsBackAfterDelay(CardsController card1, CardsController card2)
    {
        delayActive = true;

        yield return new WaitForSeconds(0.7f);

        if (hint3Sec == null || !hint3Sec.IsHintActive())
        {
            card1.FlipCard();
            card2.FlipCard();
        }

        delayActive = false;
    }

    private IEnumerator ActivateWinPanelWithDelay()
    {
        float delay = 0f;
        yield return new WaitForSeconds(delay);

        foreach (Transform child in LvlPanel.transform)
        {
            if (child.gameObject.name != "CardFounded" && child.gameObject.name != "FreeSpace")
            {
                yield break;
            }
        }

        WinPanel.SetActive(true);
    }

    public void ResetFlippedCards()
    {
        flippedCardsCount = 0;
        flippedCards.Clear();
    }

}
