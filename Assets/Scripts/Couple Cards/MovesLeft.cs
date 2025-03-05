using UnityEngine;
using TMPro;

public class MovesLeft : MonoBehaviour
{
    [SerializeField] private TMP_Text movesLeftText;
    [SerializeField] private GameObject reloadPanel;

    public void CheckMovesLeft()
    {
        int movesLeft;
        if (int.TryParse(movesLeftText.text, out movesLeft))
        {
            if (movesLeft == -1)
            {
                ActivateReloadPanel();
            }
        }
    }

    private void ActivateReloadPanel()
    {
        if (reloadPanel != null)
        {
            reloadPanel.SetActive(true);
        }
    }

    public void DecreaseMovesLeft()
    {
        int movesLeft;
        if (int.TryParse(movesLeftText.text, out movesLeft))
        {
            movesLeft--;
            movesLeftText.text = movesLeft.ToString();
            CheckMovesLeft();
        }
    }
}
