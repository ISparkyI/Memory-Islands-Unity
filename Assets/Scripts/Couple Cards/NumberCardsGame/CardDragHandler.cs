using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections;

public class CardDragHandler : MonoBehaviour, IPointerDownHandler, IDragHandler, IPointerUpHandler
{
    private RectTransform panelRectTransform;
    private RectTransform selectedCard;
    private Canvas selectedCardCanvas;
    private Vector2 offset;
    private Vector2 originalPosition;
    private int originalSortingOrder;
    private int originalIndex;
    private Vector3 originalScale;
    private RectTransform hoveredCard;

    public GameObject winPanel;
    public MovesLeft movesLeft;
    public float scaleFactor = 1.1f;
    public float animationDuration = 0.2f;

    public AudioSource slidesound;
    void Start()
    {
        panelRectTransform = GetComponent<RectTransform>();
        winPanel.SetActive(false);
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        RectTransform clickedObject = eventData.pointerEnter.GetComponent<RectTransform>();
        if (clickedObject != null && clickedObject.IsChildOf(panelRectTransform))
        {
            if (clickedObject.parent == panelRectTransform)
            {
                selectedCard = clickedObject;
                selectedCardCanvas = selectedCard.GetComponent<Canvas>();

                if (selectedCardCanvas == null)
                {
                    selectedCardCanvas = selectedCard.gameObject.AddComponent<Canvas>();
                    selectedCardCanvas.overrideSorting = true;
                }

                originalPosition = selectedCard.anchoredPosition;
                originalSortingOrder = selectedCardCanvas.sortingOrder;
                originalIndex = selectedCard.GetSiblingIndex();
                originalScale = selectedCard.localScale;

                selectedCardCanvas.sortingOrder = 100;
                selectedCard.localScale *= scaleFactor;

                Vector2 localPoint;
                RectTransformUtility.ScreenPointToLocalPointInRectangle(panelRectTransform, eventData.position, eventData.pressEventCamera, out localPoint);
                offset = selectedCard.anchoredPosition - localPoint;
            }
            else
            {
                selectedCard = null;
            }
        }
        else
        {
            selectedCard = null;
        }
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (selectedCard != null)
        {
            Vector2 localPoint;
            RectTransformUtility.ScreenPointToLocalPointInRectangle(panelRectTransform, eventData.position, eventData.pressEventCamera, out localPoint);

            Vector2 newPosition = localPoint + offset;
            selectedCard.anchoredPosition = newPosition;

            RectTransform hovered = eventData.pointerEnter?.GetComponent<RectTransform>();
            if (hovered != null && hovered.IsChildOf(panelRectTransform) && hovered != selectedCard)
            {
                if (hoveredCard != hovered)
                {
                    hoveredCard = hovered;
                }
            }
            else
            {
                hoveredCard = null;
            }
        }
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        if (selectedCard != null)
        {
            RectTransform targetCard = eventData.pointerEnter?.GetComponent<RectTransform>();
            if (targetCard != null && targetCard.IsChildOf(panelRectTransform) && targetCard.parent == panelRectTransform && targetCard != selectedCard)
            {
                StartCoroutine(AnimateCardSwap(selectedCard, targetCard));
                if (slidesound != null)
                {
                    slidesound.Play();
                }
            }
            else
            {
                StartCoroutine(ReturnCardToOriginalPosition(selectedCard));
            }

            selectedCardCanvas.sortingOrder = originalSortingOrder;
            selectedCard.localScale = originalScale;
            Destroy(selectedCardCanvas);

            selectedCard = null;
            selectedCardCanvas = null;

            if (movesLeft != null)
            {
                movesLeft.DecreaseMovesLeft();
            }
        }
    }

    private void CheckLevelCompletion()
    {
        for (int i = 0; i < panelRectTransform.childCount; i++)
        {
            RectTransform child = panelRectTransform.GetChild(i) as RectTransform;
            if (child != null && child.name != "Card_" + i)
            {
                return;
            }
        }

        winPanel.SetActive(true);
    }

    private IEnumerator AnimateCardSwap(RectTransform card1, RectTransform card2)
    {
        yield return StartCoroutine(MoveCardToPosition(card1, card2.anchoredPosition));
        yield return StartCoroutine(MoveCardToPosition(card2, originalPosition));

        int originalIndex = card1.GetSiblingIndex();
        card1.SetSiblingIndex(card2.GetSiblingIndex());
        card2.SetSiblingIndex(originalIndex);

        CheckLevelCompletion();
    }

    private IEnumerator MoveCardToPosition(RectTransform card, Vector2 targetPosition)
    {
        float elapsedTime = 0f;
        Vector2 startingPosition = card.anchoredPosition;

        while (elapsedTime < animationDuration)
        {
            card.anchoredPosition = Vector2.Lerp(startingPosition, targetPosition, elapsedTime / animationDuration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        card.anchoredPosition = targetPosition;
    }

    private IEnumerator ReturnCardToOriginalPosition(RectTransform card)
    {
        float elapsedTime = 0f;
        Vector2 startingPosition = card.anchoredPosition;

        while (elapsedTime < animationDuration)
        {
            card.anchoredPosition = Vector2.Lerp(startingPosition, originalPosition, elapsedTime / animationDuration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        card.anchoredPosition = originalPosition;
    }
}
