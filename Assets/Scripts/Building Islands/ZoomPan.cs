using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZoomPan : MonoBehaviour
{
    public RectTransform canvasRectTransform;
    public float zoomMin = 1f;
    public float zoomMax = 10f;

    private Vector3 touch;

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            touch = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        }

        if (Input.touchCount == 2)
        {
            Touch touchZero = Input.GetTouch(0);
            Touch touchOne = Input.GetTouch(1);

            Vector2 touchZeroLastPos = touchZero.position - touchZero.deltaPosition;
            Vector2 touchOneLastPos = touchOne.position - touchOne.deltaPosition;

            float disTouch = (touchZeroLastPos - touchOneLastPos).magnitude;
            float currentDistTouch = (touchZero.position - touchOne.position).magnitude;

            float difference = currentDistTouch - disTouch;

            zoom(difference * 0.01f);
        }

        if (Input.GetMouseButton(0))
        {
            Vector3 direction = touch - Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Camera.main.transform.position += direction;

            ClampCamera();
        }
    }

    private void zoom(float increment)
    {
        Camera.main.orthographicSize = Mathf.Clamp(Camera.main.orthographicSize - increment, zoomMin, zoomMax);
        ClampCamera();
    }

    private void ClampCamera()
    {
        Vector3 pos = Camera.main.transform.position;

        float minX = canvasRectTransform.rect.xMin * canvasRectTransform.localScale.x;
        float maxX = canvasRectTransform.rect.xMax * canvasRectTransform.localScale.x;
        float minY = canvasRectTransform.rect.yMin * canvasRectTransform.localScale.y;
        float maxY = canvasRectTransform.rect.yMax * canvasRectTransform.localScale.y;

        pos.x = Mathf.Clamp(pos.x, minX, maxX);
        pos.y = Mathf.Clamp(pos.y, minY, maxY);

        Camera.main.transform.position = pos;
    }
}
