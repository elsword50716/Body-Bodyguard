using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class EnemyLairAI : MonoBehaviour
{
    public bool isDead;
    public EnemyLairData enemyLairData;
    public Transform virusPool;
    public int maxEnemyNumber;
    public List<GameObject> enemyPrefabs;
    [Range(0f, 1000f)]
    public float spawnAreaHeight;
    [Range(0f, 1000f)]
    public float spawnAreaWeight;
    public CinemachineVirtualCamera virtualCamera;
    public Animator CameraAnimator;

    [SerializeField] private float currentHealth;
    private List<GameObject> enemyList;
    private Transform ship;
    private float spawnTimer = 0f;
    private int virusIndex = 0;
    private bool isFirstDead = true;

    private void Awake()
    {
        if (enemyPrefabs.Count == 0)
            return;

        GameDataManager.lairTotalNumber++;
        enemyList = new List<GameObject>();

        Random.InitState(Random.Range(0, 50));

        for (int i = 0; i < maxEnemyNumber; i++)
        {
            var randomIndex = Random.Range(0, enemyPrefabs.Count - 1);
            var randomPosition = GetRandomPostion();
            GameObject enemy = Instantiate(enemyPrefabs[randomIndex], transform.position, Quaternion.identity, virusPool);
            enemy.GetComponent<EnemyAI>().SetStartPosition(randomPosition);
            enemy.SetActive(false);
            enemyList.Add(enemy);
        }

        ship = GameObject.FindGameObjectWithTag("Ship").transform;
    }

    private void Start()
    {
        if(isDead){
            gameObject.SetActive(false);
        }
        currentHealth = enemyLairData.maxHealth;
    }

    private void Update()
    {
        if (currentHealth <= 0)
        {
            currentHealth = 0;
            Dead();
            return;
        }

        if ((transform.position - ship.position).sqrMagnitude < enemyLairData.detectShipRange * enemyLairData.detectShipRange)
        {
            if (virtualCamera.Priority != 20)
                virtualCamera.Priority = 20;
            if (spawnTimer >= enemyLairData.SpawnRate)
            {
                ReleaseVirus();
                spawnTimer = 0f;
            }
            else
            {
                spawnTimer += Time.deltaTime;
            }
        }
        else
        {
            spawnTimer = 0f;
            if (virtualCamera.Priority != 0)
                virtualCamera.Priority = 0;
        }
    }

    private Vector3 GetRandomPostion()
    {
        return virusPool.position + new Vector3(Random.Range(-1f, 1f) * spawnAreaWeight, Random.Range(-1f, 1f) * spawnAreaHeight);
    }

    public void GetDamaged(float damage)
    {
        currentHealth -= damage;
        if (currentHealth <= 0f)
            Dead();
    }

    private void ReleaseVirus()
    {
        if (virusIndex > enemyList.Count - 1)
            return;

        enemyList[virusIndex].SetActive(true);
        virusIndex++;
    }

    private void Dead()
    {
        if (isFirstDead)
        {
            //GameDataManager.lairCurrentNumber++;
            isDead = true;
            StartCoroutine(AddLairCurrentNumber());
            CameraAnimator.SetBool("isDead", true);
            isFirstDead = false;
            return;
        }


    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(transform.position, enemyLairData.detectShipRange);
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireCube(virusPool.position, new Vector3(spawnAreaWeight * 2, spawnAreaHeight * 2, 0f));
    }

    IEnumerator AddLairCurrentNumber()
    {
        yield return new WaitForSeconds(110f / 60f);
        foreach (Transform sprite in transform)
        {
            sprite.gameObject.SetActive(false);
        }
        yield return new WaitForSeconds((202f - 110f) / 60f);
        GameDataManager.lairCurrentNumber++;
        yield return new WaitForSeconds(2f);
        Destroy(virtualCamera.gameObject);
        Destroy(gameObject);
    }
}
