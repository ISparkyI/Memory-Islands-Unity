using Newtonsoft.Json;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LocalizationManager : MonoBehaviour
{
    public static LocalizationManager Instance;

    public TextAsset[] localizationFiles;

    private Dictionary<string, string> localizedText;
    private string missingTextString = "N";

    private List<string> supportedLanguages = new List<string> { "en", "ru", "ua" };
    private int currentLanguageIndex;

    private const string LanguagePrefKey = "SelectedLanguage";

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            SetDefaultLanguageIfFirstLaunch();
            LoadLocalization(supportedLanguages[currentLanguageIndex]);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void SetDefaultLanguageIfFirstLaunch()
    {
        if (!PlayerPrefs.HasKey(LanguagePrefKey))
        {
            switch (Application.systemLanguage)
            {
                case SystemLanguage.Ukrainian:
                    PlayerPrefs.SetString(LanguagePrefKey, "ua");
                    break;
                case SystemLanguage.Russian:
                    PlayerPrefs.SetString(LanguagePrefKey, "ru");
                    break;
                default:
                    PlayerPrefs.SetString(LanguagePrefKey, "en");
                    break;
            }
            PlayerPrefs.Save();
        }
        LoadLanguageFromPrefs();
    }

    private void LoadLanguageFromPrefs()
    {
        string savedLanguage = PlayerPrefs.GetString(LanguagePrefKey, "en");
        currentLanguageIndex = supportedLanguages.IndexOf(savedLanguage);
        if (currentLanguageIndex < 0)
        {
            currentLanguageIndex = 0;
        }
    }

    public void LoadLocalization(string language)
    {
        int index = supportedLanguages.IndexOf(language);
        if (index >= 0 && index < localizationFiles.Length)
        {
            TextAsset localizationFile = localizationFiles[index];

            if (localizationFile != null)
            {
                try
                {
                    LocalizationData localizationData = JsonConvert.DeserializeObject<LocalizationData>(localizationFile.text);
                    if (localizationData != null)
                    {
                        localizedText = localizationData.ToDictionary();
                    }
                }
                catch (System.Exception ex)
                {
                    Debug.LogError("Failed to deserialize localization data: " + ex.Message);
                }
            }
        }
    }

    public string GetLocalizedValue(string key)
    {
        if (localizedText != null && localizedText.ContainsKey(key))
        {
            return localizedText[key];
        }
        else
        {
            return missingTextString;
        }
    }

    public void ChangeLanguageToNext()
    {
        currentLanguageIndex = (currentLanguageIndex + 1) % supportedLanguages.Count;
        string newLanguage = supportedLanguages[currentLanguageIndex];
        PlayerPrefs.SetString(LanguagePrefKey, newLanguage);
        PlayerPrefs.Save();
        LoadLocalization(newLanguage);
        UpdateLocalizedTexts();
    }

    private void UpdateLocalizedTexts()
    {
        LocalizedText[] localizedTexts = FindObjectsByType<LocalizedText>(FindObjectsSortMode.None);
        foreach (LocalizedText localizedText in localizedTexts)
        {
            localizedText.UpdateText();
        }
    }

    public void SetupChangeLanguageButton(Button button)
    {
        if (button != null)
        {
            button.onClick.AddListener(ChangeLanguageToNext);
        }
    }
}

[System.Serializable]
public class LocalizationData
{
    public Dictionary<string, string> HomeMenu;
    public Dictionary<string, string> UpgradesMenu;
    public Dictionary<string, string> ShopMenu;
    public Dictionary<string, string> SettingsMenu;
    public Dictionary<string, string> SelectAvatarMenu;
    public Dictionary<string, string> WinPanelMenu;
    public Dictionary<string, string> MyIslandMenu;

    public Dictionary<string, string> ToDictionary()
    {
        var dict = new Dictionary<string, string>();

        AddMenuToDictionary(dict, "HomeMenu", HomeMenu);
        AddMenuToDictionary(dict, "UpgradesMenu", UpgradesMenu);
        AddMenuToDictionary(dict, "ShopMenu", ShopMenu);
        AddMenuToDictionary(dict, "SettingsMenu", SettingsMenu);
        AddMenuToDictionary(dict, "SelectAvatarMenu", SelectAvatarMenu);
        AddMenuToDictionary(dict, "WinPanelMenu", WinPanelMenu);
        AddMenuToDictionary(dict, "MyIslandMenu", MyIslandMenu);

        return dict;
    }

    private void AddMenuToDictionary(Dictionary<string, string> dict, string menuName, Dictionary<string, string> menu)
    {
        if (menu != null)
        {
            foreach (var item in menu)
            {
                dict.Add($"{menuName}.{item.Key}", item.Value);
            }
        }
    }
}
