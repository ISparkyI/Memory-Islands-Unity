using UnityEngine;

public class ChangeSkybox : MonoBehaviour
{
    public GameObject CoupleCardsBar;
    public GameObject LevelsList;
    public GameObject LvlsPanel;
    public GameObject tvLvlsPanel;
    public GameObject numLvlsPanel;


    public GameObject MoreButtons;
    private bool isButtonsActive = true;

    public Material[] skyboxMaterials;
    private int currentSkyboxIndex = 0;

    void OnEnable()
    {
        RenderSettings.skybox = skyboxMaterials[currentSkyboxIndex];
        LevelsList.SetActive(true);
        CoupleCardsBar.SetActive(false);
        MoreButtons.SetActive(false);
    }

    public void ChangeToNextSkybox()
    {
        LvlsPanel.SetActive(false);
        tvLvlsPanel.SetActive(false);
        numLvlsPanel.SetActive(false);
        LevelsList.SetActive(true);
        CoupleCardsBar.SetActive(false);
        currentSkyboxIndex++;

        if (currentSkyboxIndex >= skyboxMaterials.Length)
        {
            currentSkyboxIndex = 0;
        }

        RenderSettings.skybox = skyboxMaterials[currentSkyboxIndex];
    }

    public void ChangeToPreviousSkybox()
    {
        ToggleMoreButtons();
        LvlsPanel.SetActive(true);
        tvLvlsPanel.SetActive(true);
        numLvlsPanel.SetActive(true);
        LevelsList.SetActive(false);
        CoupleCardsBar.SetActive(true);
        currentSkyboxIndex--;

        if (currentSkyboxIndex < 0)
        {
            currentSkyboxIndex = skyboxMaterials.Length - 1;
        }

        RenderSettings.skybox = skyboxMaterials[currentSkyboxIndex];
    }

    public void ToggleMoreButtons()
    {
        isButtonsActive = !isButtonsActive;
        MoreButtons.SetActive(isButtonsActive);
    }
}
