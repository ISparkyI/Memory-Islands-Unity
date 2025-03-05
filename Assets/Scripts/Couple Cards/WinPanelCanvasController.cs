using UnityEngine;

public class WinPanelCanvasController : MonoBehaviour
{
    public WinStars winStars;
    public Radiance radiance;
    public WinPanelTime winPanelTime;
    public WinCoinAnim winCoinAnim;
    public CoinsResult coinsResult;
    public GameObject panelWithCards;
    public GameObject completedButton;
    public GameObject xButton;
    public GameObject slider;
    public GameObject timer;
    public GameObject exitButton;

    private void OnEnable()
    {
        ActivateComponents();
    }

    private void ActivateComponents()
    {
        exitButton.SetActive(false);
        timer.SetActive(false);
        slider.SetActive(false);
        completedButton.SetActive(false);
        xButton.SetActive(false);
        winStars.StartWinStars();
        radiance.StartRadiance();
        winPanelTime.StartWinPanelTime();
        winCoinAnim.StartWinCoinAnim();
        coinsResult.ResetCoinsResult();
    }

    public void PanelActivator()
    {
        panelWithCards.SetActive(true);
    }
}
