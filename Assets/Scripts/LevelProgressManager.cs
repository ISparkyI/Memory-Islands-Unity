using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class LevelProgressManager : MonoBehaviour
{
    [SerializeField] private RectTransform lvlBG;
    [SerializeField] private TMP_Text accountLevelText;
    private const string AllLevelsKey = "AllLevels";
    private const int experiencePerLevel = 25;
    private const float maxWidth = 200f;
    private const float initialWidth = 5f;
    private const float initialXPosition = -52f; 

    private void OnEnable()
    {
        UpdateLevelProgress();
    }

    private void Start()
    {
        InitializeLvlBG();
        UpdateLevelProgress();
    }

    private void InitializeLvlBG()
    {
        lvlBG.sizeDelta = new Vector2(initialWidth, lvlBG.sizeDelta.y);
        lvlBG.pivot = new Vector2(0, 0.5f);
        lvlBG.anchoredPosition = new Vector2(initialXPosition, lvlBG.anchoredPosition.y); 
    }

    private void UpdateLevelProgress()
    {
        int currentExperience = PlayerPrefs.GetInt(AllLevelsKey, 0);
        int level = CalculateLevel(currentExperience);
        float progress = CalculateProgress(currentExperience, level);

        accountLevelText.text = "" + level;

        float newWidth = Mathf.Lerp(initialWidth, maxWidth, progress);
        lvlBG.sizeDelta = new Vector2(newWidth, lvlBG.sizeDelta.y);

        lvlBG.anchoredPosition = new Vector2(initialXPosition, lvlBG.anchoredPosition.y);
    }

    private int CalculateLevel(int experience)
    {
        int level = 1;
        int requiredExperience = experiencePerLevel;

        while (experience >= requiredExperience)
        {
            experience -= requiredExperience;
            level++;
            requiredExperience += experiencePerLevel;
        }

        return level;
    }

    private float CalculateProgress(int experience, int level)
    {
        int previousLevelExperience = 0;
        for (int i = 1; i < level; i++)
        {
            previousLevelExperience += i * experiencePerLevel;
        }

        int currentLevelExperience = experience - previousLevelExperience;
        int nextLevelExperience = level * experiencePerLevel;

        return (float)currentLevelExperience / nextLevelExperience;
    }
}
