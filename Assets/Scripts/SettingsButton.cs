using UnityEngine;

public class SettingsButton : MonoBehaviour
{
    public GameObject settingsBG;

    private bool isSettingsActive = false;

    public void ToggleSettings()
    {
        isSettingsActive = !isSettingsActive;
        settingsBG.SetActive(isSettingsActive);
    }
}
