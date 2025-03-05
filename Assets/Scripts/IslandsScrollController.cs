using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;

[System.Serializable]
public class IslandLevel
{
    public Image island;
    public int requiredLevel;
    public TMP_Text levelText;
}

public class IslandsScrollController : MonoBehaviour
{
    public IslandLevel[] islands;
    public TMP_Text accountLevelText;

    private int selectedItemIndex = 0;

    private void Start()
    {
        selectedItemIndex = PlayerPrefs.GetInt("SelectedIsland", 0);
        CheckIslandLevels();
        HighlightSelectedIsland();

        AssignIslandClickHandlers();
    }

    private void AssignIslandClickHandlers()
    {
        for (int i = 0; i < islands.Length; i++)
        {
            int index = i;
            EventTrigger trigger = islands[i].island.gameObject.AddComponent<EventTrigger>();

            EventTrigger.Entry entry = new EventTrigger.Entry();
            entry.eventID = EventTriggerType.PointerClick;
            entry.callback.AddListener((eventData) => { OnIslandClick(index); });
            trigger.triggers.Add(entry);
        }
    }

    private void OnIslandClick(int index)
    {
        int accountLevel = int.Parse(accountLevelText.text);

        if (accountLevel >= islands[index].requiredLevel)
        {
            selectedItemIndex = index;
            PlayerPrefs.SetInt("SelectedIsland", selectedItemIndex);

            HighlightSelectedIsland();
        }
    }

    private void HighlightSelectedIsland()
    {
        foreach (var islandLevel in islands)
        {
            var outline = islandLevel.island.GetComponent<Outline>();
            if (outline != null)
            {
                outline.enabled = false;
            }
        }

        if (selectedItemIndex >= 0 && selectedItemIndex < islands.Length)
        {
            var outline = islands[selectedItemIndex].island.GetComponent<Outline>();
            if (outline != null)
            {
                outline.enabled = true;
            }
        }
    }

    private void CheckIslandLevels()
    {
        int accountLevel = int.Parse(accountLevelText.text);

        for (int i = 0; i < islands.Length; i++)
        {
            if (accountLevel < islands[i].requiredLevel)
            {
                islands[i].island.color = Color.gray;

                var trigger = islands[i].island.GetComponent<EventTrigger>();
                if (trigger != null)
                {
                    trigger.enabled = false;
                }

                if (islands[i].levelText != null)
                {
                    islands[i].levelText.gameObject.SetActive(true);
                }
            }
            else
            {
                if (islands[i].levelText != null)
                {
                    islands[i].levelText.gameObject.SetActive(false);
                }
            }
        }
    }

    public int GetSelectedItemIndex()
    {
        return selectedItemIndex;
    }
}
