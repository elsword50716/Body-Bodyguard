using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : MonoBehaviour
{
    public float damagePerSec;

    private float timer = 0f;

    [SerializeField] private bool canDamageEnemy = false;

    private void OnDisable()
    {
        timer = 0f;
        canDamageEnemy = false;
    }

    private void Update()
    {
        Attack();
    }


    private void Attack()
    {
        if (Time.time > timer)
        {
            canDamageEnemy = true;
            timer = Time.time + 1f;

            Debug.Log("Laser Fire!!");
        }
        else
        {


        }
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        Debug.Log("hit!!");
        if (!other.CompareTag("Enemy"))
            return;

        /*if (!canDamageEnemy)
            return;*/

        other.GetComponent<EnemyAI>().GetDamaged(damagePerSec * Time.deltaTime);
        canDamageEnemy = false;
        Debug.Log("Laser hit!!");

    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        //Debug.Log("Enter hit!!");

    }
}
