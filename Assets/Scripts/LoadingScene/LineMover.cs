using UnityEngine;

public class LineMover : MonoBehaviour
{
    public float speed = 5f;

    void Update()
    {
        transform.Translate(Vector3.left * speed * Time.deltaTime);
    }
}
