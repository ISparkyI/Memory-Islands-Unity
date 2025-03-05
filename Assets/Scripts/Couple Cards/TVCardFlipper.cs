using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class TVCardFlipper : MonoBehaviour
{
    public GameObject lvlPanel;

    public void OnEnable()
    {
        StartCoroutine(StartFlipAllCardsWithDelay());
    }

    public IEnumerator StartFlipAllCardsWithDelay()
    {
        float startDelay = 0.1f;
        yield return new WaitForSeconds(startDelay);

        FlipAllCards();
    }

    public void FlipAllCards()
    {
        foreach (Transform child in lvlPanel.transform)
        {
            TVCardsController cardsController = child.GetComponent<TVCardsController>();
            if (cardsController != null && !child.name.Equals("FreeSpace"))
            {
                cardsController.FlipCard();
            }
        }
    }
}
