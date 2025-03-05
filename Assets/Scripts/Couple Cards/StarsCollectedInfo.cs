using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class StarsCollectedInfo : MonoBehaviour
{
    public TMP_Text starsCollectedText;

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
        int totalStars = 0;
        int collectedStars = 0;

        int starsPerLevel = 3;

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

            totalStars += starsPerLevel;

            foreach (Image star in stars)
            {
                if (star.color == Color.white)
                {
                    collectedStars++;
                }
            }
        }

        starsCollectedText.text = $"{collectedStars}/{totalStars}";
    }

    public void RefreshStarsCollectedInfo()
    {
        UpdateStarsCollectedInfo();
    }
}
