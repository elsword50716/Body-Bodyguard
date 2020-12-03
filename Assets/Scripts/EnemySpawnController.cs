using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawnController : MonoBehaviour
{
    public bool isContainTurret;
    public int turretId;
    public float rotateAngle;
    [Range(0f, 1000f)]
    public float spawnAreaHeight;
    [Range(0f, 1000f)]
    public float spawnAreaWeight;
    public int maxEnemyNumber;
    public List<GameObject> enemyPrefabs = new List<GameObject>();

    private void Awake()
    {
        //SpawnEnemies();
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

    public void SpawnEnemies()
    {
        if (enemyPrefabs.Count == 0)
            return;

        Random.InitState(Random.Range(0, 50));

        for (int i = 0; i < maxEnemyNumber; i++)
        {
            var randomIndex = Random.Range(0, enemyPrefabs.Count);
            var randomPosition = GetRandomPostion();
            GameObject enemy = Instantiate(enemyPrefabs[randomIndex], randomPosition, Quaternion.identity, transform);
            if (isContainTurret)
            {
                if (randomIndex == turretId)
                {
                    var turretEnemyAi = enemy.GetComponent<EnemyAI>();
                    enemy.transform.GetChild(0).rotation = Quaternion.Euler(0, 0, rotateAngle);
                    turretEnemyAi.SetStartPosition(transform.position);
                    turretEnemyAi.enemyData.roamRange = spawnAreaHeight == 0 ? spawnAreaWeight : spawnAreaHeight;
                }
            }
            enemy.SetActive(false);
        }
    }

    public void ClearAllEnemy()
    {
        foreach (Transform enemy in transform)
        {
            Destroy(enemy.gameObject);
        }
    }
}
