using UnityEngine;
using UnityEngine.UI;

public class ImageScroller : MonoBehaviour
{
    public float speedY = 0.1f;
    public float speedX = 0.05f;
    private RawImage rawImage;
    private Rect uvRect;

    void Start()
    {
        rawImage = GetComponent<RawImage>();
        if (rawImage != null)
        {
            uvRect = rawImage.uvRect;
        }
    }

    void Update()
    {
        if (rawImage != null)
        {
            uvRect.y += speedY * Time.deltaTime;
            uvRect.x += speedX * Time.deltaTime;

            rawImage.uvRect = uvRect;
        }
    }
}
