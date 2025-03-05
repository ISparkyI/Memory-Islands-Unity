using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class TVScreenCardsChecker : MonoBehaviour
{
    public TVScreenCard screenCard;
    public GameObject panel;

    public AudioSource tvCardSound;

    private void Start()
    {
    }

    public void OnCardClick(TVCardsController card)
    {
        if (screenCard == null || panel == null)
            return;

        Image screenCardImage = screenCard.GetComponent<Image>();

        Image cardImage = card.GetComponent<Image>();

        if (screenCardImage.sprite == cardImage.sprite)
        {
            screenCard.StopChangingCards();
            StartCoroutine(AnimateCardShrinkAndChangeCard(card));

            if (tvCardSound != null)
            {
                tvCardSound.Play();
            }
        }
    }

    private IEnumerator AnimateCardShrinkAndChangeCard(TVCardsController card)
    {
        card.gameObject.name = "CardFounded";

        float delay = 0.3f;
        yield return new WaitForSeconds(delay);

        screenCard.SetRandomCardImage();
        screenCard.StartChangingCards();

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
}
