using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawnController : MonoBehaviour
{
    [Range(0f, 1000f)]
    public float spawnAreaHeight;
    [Range(0f, 1000f)]
    public float spawnAreaWeight;
    public int maxEnemyNumber;
    public List<GameObject> enemyPrefabs = new List<GameObject>();

    private void Awake()
    {
        if (enemyPrefabs.Count == 0)
            return;

        Random.InitState(Random.Range(0, 50));

        for (int i = 0; i < maxEnemyNumber; i++)
        {
            var randomIndex = Random.Range(0, enemyPrefabs.Count - 1);
            var randomPosition = GetRandomPostion();
            GameObject enemy = Instantiate(enemyPrefabs[randomIndex], randomPosition, Quaternion.identity, transform);
            enemy.SetActive(false);
        }
    }

    private Vector3 GetRandomPostion()
    {
        return transform.position + new Vector3(Random.Range(-1f, 1f) * spawnAreaWeight, Random.Range(-1f, 1f) * spawnAreaHeight);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireCube(transform.position, new Vector3(spawnAreaWeight * 2, spawnAreaHeight * 2, 0f));
    }
}
