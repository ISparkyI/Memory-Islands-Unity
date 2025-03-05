using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class LvlsListController : MonoBehaviour
{
    [System.Serializable]
    public struct LevelRequirement
    {
        public int level;
        public int requiredStars;
        public int previousLevel;
    }

    public GameObject[] levels;
    public LevelsCounter levelsCounter;
    public GameObject tv;
    public GameObject hints;
    public GameObject numbers;
    public GameObject tvLVLSPanelCanvas;
    public GameObject lVLSPanelCanvas;
    public GameObject sortNumberslVLSPanelCanvas;
    public TMP_Text starsCollected;
    public GameObject winPanel;

    public LevelRequirement[] levelRequirements;

    private void Start()
    {
        UpdateStarsVisuals();
        CheckLevelCompletion();
    }
    private void OnEnable()
    {
        UpdateStarsVisuals();
        CheckLevelCompletion();
    }

    public void RefreshLevels()
    {
        UpdateStarsVisuals();
        CheckLevelCompletion();
    }

    public void UpdateStarsVisuals()
    {
        for (int i = 0; i < levels.Length; i++)
        {
            GameObject level = levels[i];

            Transform starsContainer = level.transform.Find("Stars");
            if (starsContainer == null)
            {
                continue;
            }

            Image[] stars = starsContainer.GetComponentsInChildren<Image>();

            string levelName = level.name;
            string[] splitName = levelName.Split(' ');
            if (splitName.Length < 2 || splitName[0] != "LVL")
            {
                continue;
            }

            if (!int.TryParse(splitName[1], out int levelNumber))
            {
                continue;
            }

            int starsToShow = PlayerPrefs.GetInt("StarsLevel_" + levelNumber, 0);

            for (int j = 0; j < stars.Length; j++)
            {
                if (j < starsToShow)
                {
                    stars[j].color = Color.white;
                }
                else
                {
                    stars[j].color = Color.gray;
                }
            }
        }
    }

    public void CheckLevelCompletion()
    {
        int lastUnlockedLevel = 1;

        for (int i = 0; i < levels.Length; i++)
        {
            GameObject level = levels[i];

            Transform starsContainer = level.transform.Find("Stars");
            if (starsContainer == null)
            {
                continue;
            }

            Image[] stars = starsContainer.GetComponentsInChildren<Image>();

            Button levelButton = level.GetComponent<Button>();
            if (levelButton != null)
            {
                string levelName = level.name;
                string[] splitName = levelName.Split(' ');
                if (splitName.Length < 2 || splitName[0] != "LVL")
                {
                    continue;
                }

                if (!int.TryParse(splitName[1], out int levelNumber))
                {
                    continue;
                }

                LevelRequirement requirement = FindRequirementForLevel(levelNumber);
                if (requirement.level == 0)
                {
                    Debug.LogError($"Level requirement not found for level {levelNumber}");
                    continue;
                }

                bool previousLevelPassed = IsLevelUnlocked(requirement.previousLevel);
                bool hasEnoughStars = GetStarsCollected() >= requirement.requiredStars;

                Color buttonColor = (previousLevelPassed && hasEnoughStars) ? Color.white : Color.gray;
                levelButton.image.color = buttonColor;
                levelButton.interactable = (previousLevelPassed && hasEnoughStars);

                Text buttonText = levelButton.GetComponentInChildren<Text>();
                if (buttonText != null)
                {
                    buttonText.color = buttonColor;
                }

                if (i == 0)
                {
                    levelButton.interactable = true;
                    buttonColor = Color.white;
                    levelButton.image.color = buttonColor;
                    if (buttonText != null)
                    {
                        buttonText.color = buttonColor;
                    }
                    continue;
                }

                if (previousLevelPassed && hasEnoughStars)
                {
                    lastUnlockedLevel = levelNumber;
                }
            }

            GameObject lockObject = level.transform.Find("Lock")?.gameObject;
            if (lockObject != null)
            {
                string levelName = level.name;
                string[] splitName = levelName.Split(' ');
                if (splitName.Length < 2 || splitName[0] != "LVL")
                {
                    continue;
                }

                if (!int.TryParse(splitName[1], out int levelNumber))
                {
                    continue;
                }

                LevelRequirement requirement = FindRequirementForLevel(levelNumber);
                if (requirement.level == 0)
                {
                    Debug.LogError($"Level requirement not found for level {levelNumber}");
                    continue;
                }

                bool previousLevelPassed = IsLevelUnlocked(requirement.previousLevel);
                bool hasEnoughStars = GetStarsCollected() >= requirement.requiredStars;

                lockObject.SetActive(!(previousLevelPassed && hasEnoughStars));
            }
        }

        if (levelsCounter != null)
        {
            levelsCounter.SetCurrentLevel(lastUnlockedLevel);
        }
    }

    private LevelRequirement FindRequirementForLevel(int level)
    {
        foreach (var requirement in levelRequirements)
        {
            if (requirement.level == level)
            {
                return requirement;
            }
        }
        return new LevelRequirement();
    }

    private bool IsLevelUnlocked(int level)
    {
        return PlayerPrefs.GetInt("StarsLevel_" + level, 0) > 0;
    }

    private int GetStarsCollected()
    {
        if (starsCollected != null)
        {
            string[] starsData = starsCollected.text.Split('/');
            if (starsData.Length >= 1 && int.TryParse(starsData[0].Trim(), out int starsCollectedValue))
            {
                return starsCollectedValue;
            }
        }
        return 0;
    }

    public void ShowHints()
    {
        winPanel.SetActive(false);
        tvLVLSPanelCanvas.SetActive(false);
        lVLSPanelCanvas.SetActive(true);
        sortNumberslVLSPanelCanvas.SetActive(false);
        hints.SetActive(true);
        tv.SetActive(false);
    }

    public void ShowTV()
    {
        winPanel.SetActive(false);
        tvLVLSPanelCanvas.SetActive(true);
        lVLSPanelCanvas.SetActive(false);
        sortNumberslVLSPanelCanvas.SetActive(false);
        hints.SetActive(false);
        tv.SetActive(true);
    }

    public void ShowNumbers()
    {
        winPanel.SetActive(false);
        sortNumberslVLSPanelCanvas.SetActive(true);
        tvLVLSPanelCanvas.SetActive(false);
        lVLSPanelCanvas.SetActive(false);
        hints.SetActive(false);
        tv.SetActive(false);
        numbers.SetActive(true);
    }
}
