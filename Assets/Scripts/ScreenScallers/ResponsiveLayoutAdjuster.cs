using UnityEngine;

public class ResponsiveLayoutAdjuster : MonoBehaviour
{
    public float marginMultiplier = 1f;
    public float spacingMultiplier = -1.2f;
    public float scaleMultiplier = 0.7f;

    private void Start()
    {
        AdjustLayout();
    }

    private void AdjustLayout()
    {
        float aspectRatio = (float)Screen.width / (float)Screen.height;

        bool isTabletAspectRatio = IsTabletAspectRatio(aspectRatio);

        if (isTabletAspectRatio)
        {
            RectTransform canvasRectTransform = GetComponent<RectTransform>();

            float newPadding = canvasRectTransform.rect.width * marginMultiplier;

            foreach (RectTransform child in transform)
            {

                child.anchoredPosition = new Vector2(child.anchoredPosition.x + newPadding, child.anchoredPosition.y);

                Vector3 originalScale = child.localScale;
                child.localScale = originalScale * scaleMultiplier;
            }
        }
    }

    private bool IsTabletAspectRatio(float aspectRatio)
    {
        float[] tabletAspectRatios = { 0.75f, 0.8f, 1.25f, 1.33f };

        foreach (float tabletRatio in tabletAspectRatios)
        {
            if (Mathf.Abs(aspectRatio - tabletRatio) < 0.1f)
            {
                return true;
            }
        }

        return false;
    }
}
