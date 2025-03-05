using System.Collections;
using UnityEngine;

public class ParticlesGenerator : MonoBehaviour
{
    public GameObject[] particlePrefabs;
    public float particleLifetime = 2f;
    public float particleMoveUpDistance = 1f;
    public float particleMoveSpeed = 0.5f;
    public Transform particlesBorder;
    public int maxParticles = 40;
    public float totalDuration = 3f;

    private int currentParticlesCount = 0;
    private bool isAnimating = false;
    private bool generationStopped = false; 

    public void StartParticlesGeneration()
    {
        if (!isAnimating)
        {
            StartCoroutine(GenerateParticlesRoutine());
        }
    }

    private IEnumerator GenerateParticlesRoutine()
    {
        float elapsedTime = 0f;
        isAnimating = true;
        generationStopped = false;

        while (elapsedTime < totalDuration)
        {
            if (currentParticlesCount < maxParticles)
            {
                GenerateParticle();
            }
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        generationStopped = true;
        isAnimating = false;
    }

    public void GenerateParticle()
    {
        if (particlePrefabs.Length > 0 && !generationStopped)
        {
            int randomIndex = Random.Range(0, particlePrefabs.Length);
            GameObject particlePrefab = particlePrefabs[randomIndex];

            if (particlePrefab != null && particlesBorder != null)
            {
                GameObject particle = Instantiate(particlePrefab, particlesBorder);
                particle.transform.position = GetRandomPositionWithinBorders();
                currentParticlesCount++;

                StartCoroutine(MoveAndDestroyParticles(particle));
            }
        }
    }

    private IEnumerator MoveAndDestroyParticles(GameObject particle)
    {
        Vector3 startPosition = particle.transform.localPosition;
        Vector3 targetPosition = startPosition + Vector3.up * particleMoveUpDistance;

        float elapsedTime = 0f;

        while (elapsedTime < particleLifetime)
        {
            particle.transform.localPosition = Vector3.Lerp(startPosition, targetPosition, elapsedTime / particleLifetime);
            elapsedTime += Time.deltaTime * particleMoveSpeed;
            yield return null;
        }

        Destroy(particle);
        currentParticlesCount--;

        if (!generationStopped)
        {
            GenerateParticle();
        }
    }

    private Vector3 GetRandomPositionWithinBorders()
    {
        if (particlesBorder != null)
        {
            RectTransform borderRect = particlesBorder.GetComponent<RectTransform>();

            if (borderRect != null)
            {
                Vector2 randomPosition = new Vector2(
                    Random.Range(borderRect.rect.xMin, borderRect.rect.xMax),
                    Random.Range(borderRect.rect.yMin, borderRect.rect.yMax)
                );
                return particlesBorder.TransformPoint(randomPosition);
            }
        }
        return Vector3.zero;
    }
}
