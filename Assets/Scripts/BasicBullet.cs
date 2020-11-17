using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicBullet : MonoBehaviour
{
    public string explosionParicleTag;
    public BulletData bulletData;

    private ObjectPooler objectPooler;

    private void Start()
    {
        objectPooler = ObjectPooler.Instance;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.layer == 13 || other.gameObject.layer == 12)
        {
            ExplosionHandler();
            gameObject.SetActive(false);
        }

        if (!other.CompareTag(bulletData.targetTag))
            return;

        if (other.GetComponent<EnemyAI>() != null)
        {
            other.GetComponent<EnemyAI>().GetDamaged(bulletData.damage);
            ExplosionHandler();
            gameObject.SetActive(false);

        }
        if (other.GetComponent<Ship>() != null)
        {
            other.GetComponent<Ship>().GetDamaged(bulletData.damage);
            ExplosionHandler();
            gameObject.SetActive(false);
        }
        if(other.GetComponent<EnemyLairAI>() != null){
            other.GetComponent<EnemyLairAI>().GetDamaged(bulletData.damage);
            ExplosionHandler();
            gameObject.SetActive(false);
        }


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
