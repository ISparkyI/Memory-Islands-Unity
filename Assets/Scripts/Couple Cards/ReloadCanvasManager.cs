using UnityEngine;
using TMPro;

public class ReloadCanvasManager : MonoBehaviour
{
    [SerializeField] private TMP_Text timerText;
    [SerializeField] private GameObject reloadPanelCanvas;

    private bool isReloadPanelActive = false;

    private void Update()
    {
        CheckTimer();
    }

    private void CheckTimer()
    {
        if (!isReloadPanelActive && timerText.text == "00:00")
        {
            ActivateReloadPanel();
        }
    }

    private void ActivateReloadPanel()
    {
        reloadPanelCanvas.SetActive(true);
        isReloadPanelActive = true;
    }

    public void DeactivateReloadPanel()
    {
        reloadPanelCanvas.SetActive(false);
        isReloadPanelActive = false;
    }
}
