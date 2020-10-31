using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicBullet : MonoBehaviour
{
    public ParticleSystem explosionParicle;
    public BulletData bulletData;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.layer == 13)
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
        else if (other.GetComponent<Ship>() != null)
        {
            other.GetComponent<Ship>().GetDamaged(bulletData.damage);
            ExplosionHandler();
            gameObject.SetActive(false);
        }


    }

    private void ExplosionHandler()
    {
        if (explosionParicle != null)
        {
            var particle = Instantiate(explosionParicle, transform.position, Quaternion.identity);
            var particleMain = particle.main;
            particleMain.startColor = GetComponentInChildren<SpriteRenderer>().color;
            particle.transform.up = transform.up * -1;
            particle.Play();

        }
    }
}
