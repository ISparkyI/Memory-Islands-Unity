using UnityEngine;

public class GameController : MonoBehaviour
{
    public Stars starsScript;
    public WinStars winStarsScript;
    public GameObject winPanel;
    public GameObject moreButtons;

    public ChangeSkybox changeSkybox;

    private void OnEnable()
    {
        if (winPanel != null && winPanel.activeSelf)
        {
            winPanel.SetActive(false);
        }

        if (moreButtons != null && moreButtons.activeSelf)
        {
            changeSkybox.ToggleMoreButtons();
        }
    }

    void Update()
    {
        if (winPanel.activeSelf && starsScript != null && winStarsScript != null)
        {
            int activatedStars = starsScript.GetActivatedStarsCount();
            winStarsScript.SetStarsToShow(activatedStars);
        }
    }
}
