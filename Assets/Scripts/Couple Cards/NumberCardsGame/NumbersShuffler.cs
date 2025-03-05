using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NumbersShuffler : MonoBehaviour
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

        bool isCorrectOrder;
        do
        {
            Shuffle(cards);
            isCorrectOrder = true;

            for (int i = 0; i < cards.Count; i++)
            {
                if (cards[i].name != "Card_" + i)
                {
                    isCorrectOrder = false;
                    break;
                }
            }
        }
        while (isCorrectOrder);

        for (int i = 0; i < cards.Count; i++)
        {
            cards[i].SetSiblingIndex(i);
        }

        if (freeSpace != null)
        {
            freeSpace.SetSiblingIndex(cards.Count / 2);
        }
    }

    private void Shuffle(List<Transform> list)
    {
        for (int i = 0; i < list.Count; i++)
        {
            Transform temp = list[i];
            int randomIndex = Random.Range(i, list.Count);
            list[i] = list[randomIndex];
            list[randomIndex] = temp;
        }
    }
}
