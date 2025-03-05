using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Canvas))]
[RequireComponent(typeof(CanvasScaler))]
public class ResponsiveCanvasScaler : MonoBehaviour
{
    private CanvasScaler canvasScaler;

    public Vector2 referenceResolution = new Vector2(1080, 2160);
    public float maxMatchWidthOrHeight = 0.5f;

    private void Awake()
    {
        canvasScaler = GetComponent<CanvasScaler>();

        canvasScaler.uiScaleMode = CanvasScaler.ScaleMode.ScaleWithScreenSize;
        canvasScaler.referenceResolution = referenceResolution;
        canvasScaler.screenMatchMode = CanvasScaler.ScreenMatchMode.MatchWidthOrHeight;

        AdjustCanvasScaler();
    }

    private void AdjustCanvasScaler()
    {
        float aspectRatio = (float)Screen.width / (float)Screen.height;
        float match = maxMatchWidthOrHeight;

        if (aspectRatio > 1.0f)
        {
            match = maxMatchWidthOrHeight * (1f / aspectRatio);
        }
        else
        {
            match = maxMatchWidthOrHeight * aspectRatio;
        }

        canvasScaler.matchWidthOrHeight = Mathf.Clamp(match, 0.0f, maxMatchWidthOrHeight);
    }

    private void Update()
    {
        AdjustCanvasScaler();
    }
}
