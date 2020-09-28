using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicBullet : MonoBehaviour
{
    public BulletData bulletData;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("BulletBound"))
        {
            Destroy(gameObject);
            return;
        }

        if (!other.CompareTag(bulletData.targetTag))
            return;

        if (other.GetComponent<EnemyAI>() == null)
        {

            Destroy(gameObject);
        }
        else
        {
            other.GetComponent<EnemyAI>().GetDamaged(bulletData.damagePoint);
            Destroy(gameObject);
        }
    }
}
