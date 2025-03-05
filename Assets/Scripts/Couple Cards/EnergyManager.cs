using UnityEngine;
using UnityEngine.SceneManagement;

public class EnergyManager : MonoBehaviour
{
    private const string EnergyKey = "Energy";
    private const int maxEnergy = 30;
    private const string InfinityEnergyKey = "InfinityEnergy";

    private void Start()
    {
        InitializeEnergy();
    }

    private void InitializeEnergy()
    {
        if (!PlayerPrefs.HasKey(EnergyKey))
        {
            PlayerPrefs.SetInt(EnergyKey, maxEnergy);
            PlayerPrefs.Save();
        }
    }

    public void DeductEnergy()
    {
        if (PlayerPrefs.GetInt(InfinityEnergyKey, 0) == 1)
        {
            return;
        }

        int currentEnergy = PlayerPrefs.GetInt(EnergyKey, maxEnergy);

        if (currentEnergy > 0)
        {
            currentEnergy--;
            PlayerPrefs.SetInt(EnergyKey, currentEnergy);
            PlayerPrefs.Save();
            Debug.Log(currentEnergy);
        }
    }

    public void TryStartLevel()
    {
        int currentEnergy = PlayerPrefs.GetInt(EnergyKey, maxEnergy);

        if (currentEnergy == 0)
        {
            SceneManager.LoadScene("Menu");
        }
    }

    public void AddEnergy()
    {
        if (PlayerPrefs.GetInt(InfinityEnergyKey, 0) == 1)
        {
            return;
        }

        int currentEnergy = PlayerPrefs.GetInt(EnergyKey, maxEnergy);

        currentEnergy++;
        PlayerPrefs.SetInt(EnergyKey, currentEnergy);
        PlayerPrefs.Save();
        Debug.Log(currentEnergy);
    }
}
