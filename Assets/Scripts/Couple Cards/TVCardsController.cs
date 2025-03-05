using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections;

public class TVCardsController : MonoBehaviour, IPointerClickHandler
{
    public Sprite frontSideCard;
    public Sprite backSideCard;

    public TVScreenCardsChecker screenCardsChecker;

    private Image image;
    public bool isFrontSide = true;
    private TVCardFlipper cardFlipper;

    public MovesLeft movesLeft;

    public AudioSource flipCardSound;
    public AudioSource flipCardBackSound;

    private void Start()
    {
        image = GetComponent<Image>();
        frontSideCard = image.sprite;
        image.sprite = frontSideCard;

        cardFlipper = FindAnyObjectByType<TVCardFlipper>();
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (screenCardsChecker != null)
        {
            TVCardsController cardsController = GetComponent<TVCardsController>();
            if (cardsController != null)
            {
                screenCardsChecker.OnCardClick(cardsController);
            }
        }
        StartCoroutine(FlipDeley());
        movesLeft.DecreaseMovesLeft();
    }

    public void FlipCard()
    {
        if (isFrontSide)
        {
            image.sprite = backSideCard;
            if (flipCardBackSound != null)
            {
                flipCardBackSound.Play();
            }
        }
        else
        {
            image.sprite = frontSideCard;
            if (flipCardSound != null)
            {
                flipCardSound.Play();
            }
        }

        isFrontSide = !isFrontSide;
    }

    private IEnumerator FlipDeley()
    {
        yield return new WaitForSeconds(0.15f);
        FlipCard();
    }
}
