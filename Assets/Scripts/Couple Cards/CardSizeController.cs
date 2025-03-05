using UnityEngine;
using UnityEngine.UI;
using System;
using System.Collections;

[System.Serializable]
public class CardSizeSettings
{
    public int pairCount;
    public Vector2 cellSize;
    public Vector2 spacing;
    public int invisibleCardCount;
    public float fromSpacingX;
}

public class CardSizeController : MonoBehaviour
{
    [SerializeField] private CardSizeSettings[] cardSizeSettings;
    [SerializeField] private GridLayoutGroup gridLayoutGroup;
    [SerializeField] private GameObject cardPrefab;

    [SerializeField] private float animationDuration = 0.5f;
    private Vector2 originalSpacing;
    private float animationStartSpacingX;

    public event Action OnCardSizeUpdated;

    public void UpdateCardSize()
    {
        StartCoroutine(DelayedUpdateCardSize());
    }

    private IEnumerator DelayedUpdateCardSize()
    {
        yield return null;

        int pairCount = transform.childCount / 2;
        bool addedInvisibleCard = false;

        foreach (CardSizeSettings settings in cardSizeSettings)
        {
            if (settings.pairCount == pairCount)
            {
                if (gridLayoutGroup != null)
                {
                    originalSpacing = settings.spacing;

                    gridLayoutGroup.cellSize = settings.cellSize;
                    animationStartSpacingX = settings.fromSpacingX;

                    gridLayoutGroup.spacing = new Vector2(animationStartSpacingX, originalSpacing.y);
                }

                if (settings.invisibleCardCount > 0 && !addedInvisibleCard)
                {
                    AddInvisibleCards(settings.invisibleCardCount);
                    addedInvisibleCard = true;
                }
                OnCardSizeUpdated?.Invoke();

                AnimateGridSpacing();
                yield break;
            }
        }
    }

    private void AddInvisibleCards(int count)
    {
        foreach (Transform child in transform)
        {
            if (child.name == "FreeSpace")
            {
                Destroy(child.gameObject);
            }
        }

        int middleIndex = transform.childCount / 2;

        for (int i = 0; i < count; i++)
        {
            GameObject invisibleCard = Instantiate(cardPrefab, transform);
            invisibleCard.name = "FreeSpace";
            Image cardImage = invisibleCard.GetComponent<Image>();
            if (cardImage != null)
            {
                cardImage.color = new Color(0, 0, 0, 0);
            }
            Button cardButton = invisibleCard.GetComponent<Button>();
            if (cardButton != null)
            {
                cardButton.interactable = false;
            }

            invisibleCard.transform.SetSiblingIndex(middleIndex);
        }
    }

    private void AnimateGridSpacing()
    {
        if (gridLayoutGroup != null)
        {
            StartCoroutine(AnimateSpacing());
        }
    }

    private IEnumerator AnimateSpacing()
    {
        float elapsedTime = 0f;

        Vector2 startSpacing = new Vector2(animationStartSpacingX, originalSpacing.y);
        Vector2 endSpacing = originalSpacing;

        while (elapsedTime < animationDuration)
        {
            elapsedTime += Time.deltaTime;
            float t = Mathf.Clamp01(elapsedTime / animationDuration);

            float newSpacingX = Mathf.Lerp(startSpacing.x, endSpacing.x, t);
            float newSpacingY = Mathf.Lerp(startSpacing.y, endSpacing.y, t);

            gridLayoutGroup.spacing = new Vector2(newSpacingX, newSpacingY);
            yield return null;
        }

        gridLayoutGroup.spacing = endSpacing;
    }
}
