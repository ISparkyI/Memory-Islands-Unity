using UnityEngine;
using System.Collections;

public class SmallCoin : MonoBehaviour
{
    public float launchSpeed = 1f;
    public float fallSpeed = 1.5f;
    public float horizontalSpeedRange = 0.4f;
    public float destroyDistance = 1.4f; 

    private Vector3 initialPosition;
    private Vector3 velocity;

    private void Start()
    {
        initialPosition = transform.position;
        float randomHorizontalSpeed = Random.Range(-horizontalSpeedRange, horizontalSpeedRange);
        velocity = new Vector3(randomHorizontalSpeed, launchSpeed, 0);

        StartCoroutine(FallAndDestroy());
    }

    private IEnumerator FallAndDestroy()
    {
        while (Vector3.Distance(initialPosition, transform.position) < destroyDistance)
        {
            transform.position += velocity * Time.deltaTime;
            velocity.y -= fallSpeed * Time.deltaTime;
            yield return null;
        }

        Destroy(gameObject);
    }
}
