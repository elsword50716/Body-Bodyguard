using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicBullet : MonoBehaviour
{
    public string explosionParicleTag;
    public BulletData bulletData;

    private ObjectPooler objectPooler;
    private Rigidbody2D Rbody2D;

    private void Start()
    {
        objectPooler = ObjectPooler.Instance;
        Rbody2D = GetComponent<Rigidbody2D>();
    }

    private void LateUpdate()
    {
        if (Rbody2D.velocity == Vector2.zero)
            gameObject.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.layer == 13 || other.gameObject.layer == 12 || other.CompareTag("Laser"))
        {
            if (other.CompareTag(gameObject.tag))
                return;
            ExplosionHandler();
            gameObject.SetActive(false);
        }

        if (!other.CompareTag(bulletData.targetTag))
            return;

        if (other.GetComponent<EnemyAI>() != null)
        {
            other.GetComponent<EnemyAI>().GetDamaged(bulletData.damage);

        }
        if (other.GetComponent<Ship>() != null)
        {
            other.GetComponent<Ship>().GetDamaged(bulletData.damage);

        }
        if (other.GetComponent<EnemyLairAI>() != null)
        {
            other.GetComponent<EnemyLairAI>().GetDamaged(bulletData.damage);
        }

        ExplosionHandler();
        gameObject.SetActive(false);

    }

    private void ExplosionHandler()
    {
        if (!string.IsNullOrEmpty(explosionParicleTag))
        {
            var particle = objectPooler.SpawnFromPool(explosionParicleTag, transform.position, null).GetComponent<ParticleSystem>();
            var particleMain = particle.main;
            particleMain.startColor = GetComponentInChildren<SpriteRenderer>().color;
            particle.Play();

        }
    }
}
