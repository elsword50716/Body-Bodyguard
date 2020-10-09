using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicBullet : MonoBehaviour
{
    public BulletData bulletData;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.GetComponent<EnemyAI>() != null)
        {
            other.GetComponent<EnemyAI>().GetDamaged(bulletData.damage);
            Destroy(gameObject);

        }
        else if (other.GetComponent<Ship>() != null)
        {
            other.GetComponent<Ship>().GetDamaged(bulletData.damage);
            Destroy(gameObject);
        }
        else
            Destroy(gameObject);


    }
}
