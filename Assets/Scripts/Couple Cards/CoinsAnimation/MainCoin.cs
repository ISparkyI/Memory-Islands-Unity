using UnityEngine;
using System.Collections;

public class MainCoin : MonoBehaviour
{
    public GameObject smallCoinPrefab;
    public float spawnInterval = 0.1f;
    public float verticalOffset = 0.5f;

    public void StartSpawningCoins()
    {
        StartCoroutine(SpawnSmallCoins());
    }

    private IEnumerator SpawnSmallCoins()
    {
        while (true)
        {
            SpawnSmallCoin();
            yield return new WaitForSeconds(spawnInterval);
        }
    }

    private void SpawnSmallCoin()
    {
        Vector3 spawnPosition = transform.position + new Vector3(0, verticalOffset, 0);
        GameObject smallCoin = Instantiate(smallCoinPrefab, spawnPosition, Quaternion.identity, transform);
        smallCoin.transform.localScale = transform.localScale * Random.Range(0.3f, 0.5f);

        if (smallCoin.GetComponent<SmallCoin>() == null)
        {
            smallCoin.AddComponent<SmallCoin>();
        }
    }
}
