using UnityEngine;

public class ExperienceManager : MonoBehaviour
{
    private const string AllLevelsKey = "AllLevels";

    private void Start()
    {
        if (!PlayerPrefs.HasKey(AllLevelsKey))
        {
            PlayerPrefs.SetInt(AllLevelsKey, 0);
        }
    }

    public void AddExperience(int amount)
    {
        int currentExperience = PlayerPrefs.GetInt(AllLevelsKey, 0);
        currentExperience += amount;
        PlayerPrefs.SetInt(AllLevelsKey, currentExperience);
        PlayerPrefs.Save();
    }

    public void AddFiveExperience()
    {
        AddExperience(5);
    }

    public int GetCurrentExperience()
    {
        return PlayerPrefs.GetInt(AllLevelsKey, 0);
    }
}
