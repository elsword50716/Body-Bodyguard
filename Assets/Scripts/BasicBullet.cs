using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicBullet : MonoBehaviour
{
    public bool isMissle;
    public string explosionParicleTag;
    public BulletData bulletData;

    private ObjectPooler objectPooler;
    private Rigidbody2D Rbody2D;
    private Vector2 explosionPosition;

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
        if (isMissle)
            return;

        if (other.gameObject.layer == 13 || other.gameObject.layer == 12 || other.CompareTag("Laser"))
        {
            if (other.CompareTag(gameObject.tag))
                return;

            if (other.TryGetComponent<Missle>(out var missle))
                missle.GetDamaged(bulletData.damage);

            ExplosionHandler(other.ClosestPoint(transform.position));
            gameObject.SetActive(false);
        }

        if (!other.CompareTag(bulletData.targetTag))
            return;

        if (other.TryGetComponent<EnemyAI>(out var enemy))
        {
            enemy.GetDamaged(bulletData.damage);

        }
        if (other.TryGetComponent<Ship>(out var ship))
        {
            ship.GetDamaged(bulletData.damage);

        }
        if (other.TryGetComponent<EnemyLairAI>(out var lair))
        {
            lair.GetDamaged(bulletData.damage);
        }

        ExplosionHandler(other.ClosestPoint(transform.position));
        gameObject.SetActive(false);

    }

    public void ExplosionHandler(Vector3 position)
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
