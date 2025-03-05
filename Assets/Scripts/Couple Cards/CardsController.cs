using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections;

public class CardsController : MonoBehaviour, IPointerClickHandler
{
    public Sprite frontSideCard;
    public Sprite backSideCard;

    private Image image;
    public bool isFrontSide = true;
    private CardFlipper cardFlipper;

    public MovesLeft movesLeft;

    private void Start()
    {
        image = GetComponent<Image>();
        frontSideCard = image.sprite;
        image.sprite = frontSideCard;
        cardFlipper = FindAnyObjectByType<CardFlipper>();
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (cardFlipper.CanFlip())
        {
            StartCoroutine(FlipDeley());
            movesLeft.DecreaseMovesLeft();
        }
    }

    public void FlipCard()
    {
        if (image == null) return;

        if (isFrontSide)
        {
            image.sprite = backSideCard;
            cardFlipper.IncreaseFlippedCount();
            cardFlipper.AddFlippedCard(this);
        }
        else
        {
            image.sprite = frontSideCard;
            cardFlipper.DecreaseFlippedCount();
            cardFlipper.RemoveFlippedCard(this);
        }

        isFrontSide = !isFrontSide;

        cardFlipper.CheckMatchedCards();
    }

    private IEnumerator FlipDeley()
    {
        yield return new WaitForSeconds(0.15f);
        FlipCard();
    }

    public void FlipCardToBack()
    {
        if (isFrontSide)
        {
            image.sprite = backSideCard;
            isFrontSide = false;
        }
    }

    public void FlipCardToFront()
    {
        if (!isFrontSide)
        {
            image.sprite = frontSideCard;
            isFrontSide = true;
        }
    }
}
