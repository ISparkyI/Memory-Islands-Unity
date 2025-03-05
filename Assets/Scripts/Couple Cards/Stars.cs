using UnityEngine;
using UnityEngine.UI;

public class Stars : MonoBehaviour
{
    public Timer timer;
    public Image star1;
    public Image star2;
    public Image star3;

    private bool star1Activated = false;
    private bool star2Activated = false;
    private bool star3Activated = false;

    private void Update()
    {
        float totalTime = timer.totalTime;

        float star1Time = totalTime * 0.25f;
        float star2Time = totalTime * 0.5f;
        float star3Time = totalTime * 0.9f;

        if (!star1Activated && timer.timePassed >= star1Time)
        {
            SetStarStatus(star1, true);
            star1Activated = true;
        }
        if (!star2Activated && timer.timePassed >= star2Time)
        {
            SetStarStatus(star2, true);
            star2Activated = true;
        }
        if (!star3Activated && timer.timePassed >= star3Time)
        {
            SetStarStatus(star3, true);
            star3Activated = true;
        }
    }

    private void SetStarStatus(Image star, bool isGrey)
    {
        if (isGrey)
        {
            star.color = Color.gray;
        }
        else
        {
            star.color = Color.white;
        }
    }

    public void ResetStars()
    {
        SetStarStatus(star1, false);
        SetStarStatus(star2, false);
        SetStarStatus(star3, false);

        star1Activated = false;
        star2Activated = false;
        star3Activated = false;
    }

    public int GetActivatedStarsCount()
    {
        int count = 3;
        if (star1Activated) count--;
        if (star2Activated) count--;
        if (star3Activated) count--;
        return count;
    }
}
