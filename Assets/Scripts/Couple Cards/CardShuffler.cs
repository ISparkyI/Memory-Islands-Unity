using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CardShuffler : MonoBehaviour
{
    public GameObject lvlPanel;

    public void ShuffleCards()
    {
        StartCoroutine(ShuffleWithDelay());
    }

    private IEnumerator ShuffleWithDelay()
    {
        yield return null;

        int childCount = lvlPanel.transform.childCount;
        Transform freeSpace = null;
        List<Transform> cards = new List<Transform>();

        for (int i = 0; i < childCount; i++)
        {
            Transform child = lvlPanel.transform.GetChild(i);
            if (child.name == "FreeSpace")
            {
                freeSpace = child;
            }
            else
            {
                cards.Add(child);
            }
        }

        for (int i = 0; i < cards.Count; i++)
        {
            Transform temp = cards[i];
            int randomIndex = Random.Range(i, cards.Count);
            cards[i] = cards[randomIndex];
            cards[randomIndex] = temp;
        }

        for (int i = 0; i < cards.Count; i++)
        {
            cards[i].SetSiblingIndex(i);
        }

        if (freeSpace != null)
        {
            freeSpace.SetSiblingIndex(cards.Count / 2);
        }
    }
}
