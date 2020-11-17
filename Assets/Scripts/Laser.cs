using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : MonoBehaviour
{
    public ContactFilter2D enemy;
    public float damagePerSec;

    private BoxCollider2D boxCollider2D;
    private List<Collider2D> enemyInLaser;

    private void Start()
    {
        boxCollider2D = GetComponent<BoxCollider2D>();
        enemyInLaser = new List<Collider2D>();
    }

    private void Update()
    {
        if (boxCollider2D.OverlapCollider(enemy, enemyInLaser) > 0)
        {
            foreach (var enemy in enemyInLaser)
            {
                if (enemy.GetComponent<EnemyAI>() != null)
                {
                    enemy.GetComponent<EnemyAI>().GetDamaged(damagePerSec * Time.deltaTime);
                }

                if (enemy.GetComponent<EnemyLairAI>() != null)
                {
                    enemy.GetComponent<EnemyLairAI>().GetDamaged(damagePerSec * Time.deltaTime);
                }
            }
        }
    }

    // private void OnTriggerStay2D(Collider2D other)
    // {
    //     if (other.GetComponent<EnemyAI>() == null)
    //         return;

    //     other.GetComponent<EnemyAI>().GetDamaged(damagePerSec * Time.deltaTime);

    // }
}
