using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldController : MonoBehaviour
{
    public bool isInvincible;
    public GameObject sprite;
    public CircleCollider2D circleCollider2D;
    public float damage;
    public string damageParticleTag;
    public GameObject invincibleParicle;
    public Color particleColor;

    private int counter;
    private float damageTemp;
    private bool isInvincibleTemp;

    private void Start()
    {
        if (sprite == null)
            sprite = transform.GetChild(0).gameObject;

        circleCollider2D = GetComponent<CircleCollider2D>();
        particleColor = transform.GetChild(0).GetChild(0).GetComponent<SpriteRenderer>().color;
        damageTemp = damage;
        isInvincibleTemp = isInvincible;
    }

    private void Update()
    {
        circleCollider2D.enabled = sprite.activeSelf;
        if (counter == 2)
            counter = 0;

        if (isInvincible == isInvincibleTemp)
            return;

        isInvincibleTemp = isInvincible;

        if (isInvincible)
        {
            damage *= 1000f;
            invincibleParicle.SetActive(true);
        }
        else
        {
            damage = damageTemp;
            invincibleParicle.SetActive(false);
        }

    }

    private void OnCollisionStay2D(Collision2D other)
    {
        if (damage == 0)
            return;

        if (other.transform.TryGetComponent<EnemyAI>(out var enemy))
        {
            counter++;
            if (!isInvincible && counter % 2 == 0)
            {
                var particle = ObjectPooler.Instance.SpawnFromPool(damageParticleTag, other.GetContact(0).point, null);
                var main = particle.GetComponent<ParticleSystem>().main;
                main.startColor = particleColor;
                var dir = (Vector2)other.transform.position - other.GetContact(0).point;
                particle.transform.up = dir.normalized;
            }
            enemy.GetDamaged(damage * Time.deltaTime);
        }
        if (other.transform.TryGetComponent<EnemyLairAI>(out var enemyLair))
        {
            counter++;
            if (!isInvincible && counter % 2 == 0)
            {
                var particle = ObjectPooler.Instance.SpawnFromPool(damageParticleTag, other.GetContact(0).point, null);
                var main = particle.GetComponent<ParticleSystem>().main;
                main.startColor = particleColor;
                var dir = (Vector2)other.transform.position - other.GetContact(0).point;
                particle.transform.up = dir.normalized;
            }
            enemy.GetDamaged(damage * Time.deltaTime);
        }
    }
}
