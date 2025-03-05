using UnityEngine;

public class HintManager : MonoBehaviour
{
    [SerializeField] private GameObject hint3sec;
    [SerializeField] private GameObject hint50;
    [SerializeField] private GameObject hint1PairOfCards;

    private void OnEnable()
    {
        int currentLevel = PlayerPrefs.GetInt("CurrentLevel", 1);
        ActivateHints(currentLevel);
    }

    public void ActivateHints(int level)
    {
        hint3sec.SetActive(false);
        hint50.SetActive(false);
        hint1PairOfCards.SetActive(false);

        if (level >= 1 && level <= 4)
        {
            hint3sec.SetActive(true);
        }
        else if (level >= 5 && level <= 8)
        {
            hint3sec.SetActive(true);
            hint50.SetActive(true);
        }
        else if (level > 8)
        {
            hint3sec.SetActive(true);
            hint50.SetActive(true);
            hint1PairOfCards.SetActive(true);
        }
    }
}
