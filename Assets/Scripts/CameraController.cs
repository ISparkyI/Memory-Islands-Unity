using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class CameraController : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    public RectTransform canvasRectTransform;
    public float moveSpeed = 1f;
    public float minZoom = 10f;
    public float maxZoom = 20f;
    public Slider zoomSlider;
    public TextMeshProUGUI zoomText;

    private Vector3 dragOrigin;
    private Camera cam;
    private bool isDragging;

    private void Start()
    {
        cam = Camera.main;
        cam.orthographicSize = minZoom;

        if (zoomSlider != null)
        {
            zoomSlider.minValue = minZoom;
            zoomSlider.maxValue = maxZoom;
            zoomSlider.value = cam.orthographicSize;
            zoomSlider.onValueChanged.AddListener(OnZoomSliderChanged);
        }

        UpdateZoomText();
    }

    private void Update()
    {
        if (isDragging)
        {
            if (Input.GetMouseButton(0))
            {
                Vector3 currentWorldPosition = cam.ScreenToWorldPoint(Input.mousePosition);
                Vector3 direction = dragOrigin - currentWorldPosition;
                direction.z = 0;

                cam.transform.position += direction;
                ClampCamera();
            }
            else
            {
                isDragging = false;
            }
        }
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if (eventData.pointerCurrentRaycast.gameObject != null && eventData.pointerCurrentRaycast.gameObject.transform.IsChildOf(canvasRectTransform))
        {
            dragOrigin = cam.ScreenToWorldPoint(Input.mousePosition);
            isDragging = true;
        }
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        isDragging = false;
    }

    private void OnZoomSliderChanged(float value)
    {
        cam.orthographicSize = value;
        ClampCamera();
        UpdateZoomText();
    }

    private void UpdateZoomText()
    {
        if (zoomText != null)
        {
            zoomText.text = "x" + cam.orthographicSize.ToString("F1");
        }
    }

    private void ClampCamera()
    {
        Vector3 pos = cam.transform.position;

        float minX = canvasRectTransform.rect.xMin * canvasRectTransform.localScale.x;
        float maxX = canvasRectTransform.rect.xMax * canvasRectTransform.localScale.x;
        float minY = canvasRectTransform.rect.yMin * canvasRectTransform.localScale.y;
        float maxY = canvasRectTransform.rect.yMax * canvasRectTransform.localScale.y;

        pos.x = Mathf.Clamp(pos.x, minX, maxX);
        pos.y = Mathf.Clamp(pos.y, minY, maxY);

        cam.transform.position = pos;
    }
}
