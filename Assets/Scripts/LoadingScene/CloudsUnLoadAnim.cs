using System.Collections;
using UnityEngine;

public class CloudsUnLoadAnim : MonoBehaviour
{
    public GameObject[] cloudsLeftToRight;
    public GameObject[] cloudsRightToLeft;

    public float leftToRightSpeed = 2f;
    public float rightToLeftSpeed = 2f;

    public GameObject cloudsCanvas;

    public float targetXPositionLeftToRight = 0f;
    public float targetXPositionRightToLeft = 0f;

    private void OnEnable()
    {
        cloudsCanvas.SetActive(true);
        StartUnLoadCloudsAnimation();
    }

    public void StartUnLoadCloudsAnimation()
    {
        StartCoroutine(MoveCloudsTogether(cloudsLeftToRight, leftToRightSpeed, targetXPositionLeftToRight, true));
        StartCoroutine(MoveCloudsTogether(cloudsRightToLeft, rightToLeftSpeed, targetXPositionRightToLeft, false));
    }

    private IEnumerator MoveCloudsTogether(GameObject[] clouds, float speed, float targetXPosition, bool leftToRight)
    {
        yield return new WaitForSeconds(0.2f);
        bool moving = true;

        while (moving)
        {
            moving = false;

            foreach (GameObject cloud in clouds)
            {
                float direction = leftToRight ? 1f : -1f;
                float step = speed * Time.deltaTime * direction;

                cloud.transform.position += new Vector3(step, 0f, 0f);

                if ((leftToRight && cloud.transform.position.x < targetXPosition) ||
                    (!leftToRight && cloud.transform.position.x > targetXPosition))
                {
                    moving = true;
                }
                else
                {
                    cloud.transform.position = new Vector3(targetXPosition, cloud.transform.position.y, cloud.transform.position.z);
                }
            }

            yield return null;
        }

        cloudsCanvas.SetActive(false);
    }
}
