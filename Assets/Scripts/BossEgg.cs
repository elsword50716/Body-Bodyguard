using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossEgg : MonoBehaviour
{
    public float explodeDelay;
    public float explodeForce;
    public Animator eggAnimator;
    public float animationMaxSpeed;
    public EnemySpawnController enemySpawnController;
    public Rigidbody2D rbody;
    public GameObject explosionParticle;

    private float explodeTimer;
    private List<GameObject> enemyList;

    private void Awake()
    {
        enemySpawnController.SpawnEnemies();
        enemyList = enemySpawnController.GetEnemyList();
    }

    private void OnEnable()
    {
        explodeTimer = explodeDelay;
        rbody.isKinematic = false;
        eggAnimator.SetFloat("Speed", 1f);
        eggAnimator.SetBool("startBirth", false);
        eggAnimator.SetBool("explode", false);
    }

    private void Update()
    {
        if (explodeTimer < explodeDelay / 3)
        {
            eggAnimator.SetBool("startBirth", true);
            var speed_temp = eggAnimator.GetFloat("Speed");
            eggAnimator.SetFloat("Speed", speed_temp + (animationMaxSpeed / (explodeDelay / 3) * Time.deltaTime));
        }
        if (explodeTimer > 0)
        {
            explodeTimer -= Time.deltaTime;
        }
        else
        {
            eggAnimator.SetBool("explode", true);
            explodeTimer = 0;
        }
    }

    public void Explode()
    {
        explosionParticle.SetActive(true);
        for (int i = 0; i < enemyList.Count; i++)
        {
            if (enemyList[i] == null)
                enemyList.RemoveAt(i);
            enemyList[i].GetComponent<EnemyAI>().SetStartPosition(enemySpawnController.GetRandomPostion());
            enemyList[i].transform.position = transform.position;
            enemyList[i].SetActive(true);
        }
        rbody.velocity = Vector2.zero;
        rbody.isKinematic = true;
        this.enabled = false;
    }
}
