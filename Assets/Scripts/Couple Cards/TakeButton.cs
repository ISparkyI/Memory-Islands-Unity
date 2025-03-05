using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class TakeButton : MonoBehaviour
{
    public TMP_Text coinsResult;
    public GameObject WinPanel;
    public GameObject takeButton;
    public TMP_Text levelText;
    public Transform lvlContentPanel;

    public GameObject screenCard;
    public GameObject LVLSPanelCanvas;
    public GameObject TVLVLSPanelCanvas;
    public GameObject numLVLSPanelCanvas;
    public GameObject tv;
    public GameObject hints;
    public GameObject numbers;

    public int[] levelsToChangeSkybox;
    public ChangeSkybox changeSkybox;

    private const string EnergyKey = "Energy";
    private const int maxEnergy = 30;
    private const string CoinsKey = "Coins";

    private const string Every8LvlsAdKey = "8lvlad";
    public AdsManager adsManager;

    private void Start()
    {
        UpdateLevelText();
    }

    public void OnTakeButtonClick()
    {
        int currentEnergy = PlayerPrefs.GetInt(EnergyKey, maxEnergy);

        if (currentEnergy == 1)
        {
            SceneManager.LoadScene("Menu");
        }

        if (coinsResult != null)
        {
            if (int.TryParse(coinsResult.text, out int coinsToAdd))
            {
                int currentCoins = PlayerPrefs.GetInt(CoinsKey, 0);
                currentCoins += coinsToAdd;
                PlayerPrefs.SetInt(CoinsKey, currentCoins);
                PlayerPrefs.Save();
            }
        }

        WinPanel.SetActive(false);

        int savedCurrentLevel = PlayerPrefs.GetInt("CurrentLevel");
        int currentLevel = savedCurrentLevel + 1;

        if (ShouldChangeSkybox(currentLevel))
        {
            if (changeSkybox != null)
            {
                changeSkybox.ChangeToNextSkybox();
            }
        }
        else
        {
            LoadNextLevel();
        }

        int adCounter = PlayerPrefs.GetInt(Every8LvlsAdKey, 0) + 1;
        PlayerPrefs.SetInt(Every8LvlsAdKey, adCounter);
        PlayerPrefs.Save();

        if (adCounter % 8 == 0)
        {
            adsManager.ShowInterstitialAd();
        }
    }

    private void LoadNextLevel()
    {
        if (lvlContentPanel != null)
        {
            int savedCurrentLevel = PlayerPrefs.GetInt("CurrentLevel");
            int nextLevel = savedCurrentLevel + 1;

            for (int i = 0; i < lvlContentPanel.childCount; i++)
            {
                Transform child = lvlContentPanel.GetChild(i);
                if (child.CompareTag("LVL"))
                {
                    int childLevel = 0;
                    TMP_Text childLevelText = child.GetComponentInChildren<TMP_Text>();
                    if (childLevelText != null && int.TryParse(childLevelText.text, out childLevel))
                    {
                        if (childLevel == nextLevel)
                        {
                            Component levelManager = child.GetComponent<LvlManager>() as Component ?? child.GetComponent<TVLvlsManager>() as Component ?? child.GetComponent<NumbersLvlManager>() as Component;
                            if (levelManager != null)
                            {
                                if (levelManager is LvlManager lvlManager)
                                {
                                    lvlManager.OnLevelButtonClick();
                                    LVLSPanelCanvas.SetActive(true);
                                    TVLVLSPanelCanvas.SetActive(false);
                                    hints.SetActive(true);
                                    tv.SetActive(false);
                                    numbers.SetActive(false);
                                }
                                else if (levelManager is TVLvlsManager tvLvlManager)
                                {
                                    screenCard.SetActive(false);
                                    TVLVLSPanelCanvas.SetActive(false);
                                    tvLvlManager.OnLevelButtonClick();
                                    LVLSPanelCanvas.SetActive(false);
                                    TVLVLSPanelCanvas.SetActive(true);
                                    hints.SetActive(false);
                                    tv.SetActive(true);
                                    numbers.SetActive(false);
                                    screenCard.SetActive(true);
                                }
                                else if (levelManager is NumbersLvlManager numbersLvlManager)
                                {
                                    numLVLSPanelCanvas.SetActive(false);
                                    TVLVLSPanelCanvas.SetActive(false);
                                    numbersLvlManager.OnLevelButtonClick();
                                    numbers.SetActive(true);
                                    numLVLSPanelCanvas.SetActive(true);
                                    LVLSPanelCanvas.SetActive(false);
                                    hints.SetActive(false);
                                    tv.SetActive(false);
                                }
                                PlayerPrefs.SetInt("CurrentLevel", nextLevel);
                                PlayerPrefs.Save();

                                UpdateLevelText();
                                return;
                            }
                        }
                    }
                }
            }
        }
    }

    private bool ShouldChangeSkybox(int level)
    {
        foreach (int changeLevel in levelsToChangeSkybox)
        {
            if (level == changeLevel)
            {
                return true;
            }
        }
        return false;
    }

    private void UpdateLevelText()
    {
        if (levelText != null)
        {
            int currentLevel = PlayerPrefs.GetInt("CurrentLevel");
            string language = GetCurrentLanguage();
            string levelPrefix = GetClaimText(language);
            levelText.text = $"{levelPrefix} : {currentLevel}";
        }
    }

    private string GetCurrentLanguage()
    {
        return PlayerPrefs.GetString("SelectedLanguage", "en");
    }

    private string GetClaimText(string language)
    {
        switch (language)
        {
            case "en":
                return "LVL";
            case "ua":
                return "Рівень";
            case "ru":
                return "Уровень";
            default:
                return "LVL";
        }
    }
}
