using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class StarsToContinue : MonoBehaviour
{
    [System.Serializable]
    public struct StarsRequirement
    {
        public TMP_Text starsCollectedText;
        public int needStars;
    }

    public StarsRequirement[] starsRequirements;

    void Start()
    {
        UpdateStarsCollectedInfo();
    }

    void OnEnable()
    {
        UpdateStarsCollectedInfo();
    }

    public void UpdateStarsCollectedInfo()
    {
        int collectedStars = CalculateCollectedStars();

        foreach (StarsRequirement starsRequirement in starsRequirements)
        {
            starsRequirement.starsCollectedText.text = $"{collectedStars}/{starsRequirement.needStars}";
        }
    }

    public void RefreshStarsCollectedInfo()
    {
        UpdateStarsCollectedInfo();
    }

    private int CalculateCollectedStars()
    {
        int collectedStars = 0;
        int levelsCount = transform.childCount;

        for (int i = 0; i < levelsCount; i++)
        {
            GameObject level = transform.GetChild(i).gameObject;
            Transform starsContainer = level.transform.Find("Stars");
            if (starsContainer == null)
            {
                continue;
            }

            Image[] stars = starsContainer.GetComponentsInChildren<Image>();

            foreach (Image star in stars)
            {
                if (star.color == Color.white)
                {
                    collectedStars++;
                }
            }
        }

        return collectedStars;
    }
}
