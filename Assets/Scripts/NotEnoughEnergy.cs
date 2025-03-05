using System.Collections;
using UnityEngine;

public class NotEnoughEnergy : MonoBehaviour
{
    public GameObject windowIfNotEnoughEnergy;
    public GameObject windowIfNotEnoughCoins;
    public GameObject windowIfNotEnoughRubins;
    private const string EnergyKey = "Energy";
    private const int maxEnergy = 30;

    private const string ShowCoinsErrorKey = "ShowCoinsError";
    private const string ShowRubinsErrorKey = "ShowRubinsError";

    void Start()
    {
        CloseAllWindows();
        StartCoroutine(WaitOneSecond());
    }

    public void IfNotEnoughEnergy()
    {
        int currentEnergy = PlayerPrefs.GetInt(EnergyKey, maxEnergy);

        if (currentEnergy == 0)
        {
            windowIfNotEnoughEnergy.SetActive(true);
        }
    }

    public void IfNotEnoughCoins()
    {
        if (PlayerPrefs.GetInt(ShowCoinsErrorKey, 0) == 1)
        {
            windowIfNotEnoughCoins.SetActive(true);
            PlayerPrefs.SetInt(ShowCoinsErrorKey, 0);
        }
    }

    public void IfNotEnoughRubins()
    {
        if (PlayerPrefs.GetInt(ShowRubinsErrorKey, 0) == 1)
        {
            windowIfNotEnoughRubins.SetActive(true);
            PlayerPrefs.SetInt(ShowRubinsErrorKey, 0);
        }
    }

    public void CloseAllWindows()
    {
        windowIfNotEnoughEnergy.SetActive(false);
        windowIfNotEnoughCoins.SetActive(false);
        windowIfNotEnoughRubins.SetActive(false);
    }

    public IEnumerator WaitOneSecond()
    {
        yield return new WaitForSeconds(0.3f);
        IfNotEnoughEnergy();
        IfNotEnoughCoins();
        IfNotEnoughRubins();
    }
}
