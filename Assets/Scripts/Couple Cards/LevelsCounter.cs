using UnityEngine;
using TMPro;

public class LevelsCounter : MonoBehaviour
{
    public GameObject content;
    public TMP_Text totalLevelsText;
    private int currentLevel = 1;

    void Start()
    {
        UpdateLevelsCount();
    }

    void UpdateLevelsCount()
    {
        if (content == null || totalLevelsText == null)
        {
            Debug.LogError("Content або TotalLevelsText не призначені.");
            return;
        }

        int levelsCount = 0;
        foreach (Transform child in content.transform)
        {
            if (child.CompareTag("LVL"))
            {
                levelsCount++;
            }
        }

        totalLevelsText.text = currentLevel + "/" + levelsCount;
    }

    public void RefreshLevelsCount()
    {
        UpdateLevelsCount();
    }

    public void SetCurrentLevel(int level)
    {
        currentLevel = level;
        UpdateLevelsCount();
    }
}
