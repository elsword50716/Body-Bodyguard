﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnderwaterBomb : MonoBehaviour
{
    public LayerMask effectLayer;
    public Ship ship;
    public Animator animator;
    public float animationMaxSpeed;
    public float explosionCountDown;
    public float explosionRange;
    public float explosionShakeIntensity;
    public float explosionDuration;
    public float explosionForce;
    public string explosionParticleTag;
    public float damage;
    public float MaxHP;

    private float currentHP;
    private bool isDead;
    private float timer;

    private void OnEnable()
    {
        timer = explosionCountDown;
        isDead = false;
        currentHP = MaxHP;
    }

    private void Start()
    {
        ship = Ship.Instance;
        currentHP = MaxHP;
    }

    private void Update()
    {
        if (isDead)
        {
            animator.SetBool("isDead", isDead);
            if (timer > 0)
            {
                timer -= Time.deltaTime;
                var speed_temp = animator.GetFloat("Speed");
                animator.SetFloat("Speed", speed_temp + (animationMaxSpeed / explosionCountDown * Time.deltaTime));
            }
            else
                Explode();
        }
    }

    public void Explode()
    {
        ObjectPooler.Instance.SpawnFromPool(explosionParticleTag, transform.position, null);
        CameraController.Instance.ShakeCamera(explosionShakeIntensity, explosionDuration, true);

        var distance = (ship.transform.position - transform.position);
        if (distance.sqrMagnitude < explosionRange * explosionRange)
        {
            var rbody = ship.GetComponent<Rigidbody2D>();
            rbody.AddForce(distance.normalized * explosionForce * rbody.mass * (explosionForce / distance.magnitude));
            ship.GetDamaged(damage);
        }

        var targets = Physics2D.OverlapCircleAll(transform.position, explosionRange, effectLayer);

        for (int i = 0; i < targets.Length; i++)
        {
            if (targets[i].TryGetComponent<EnemyAI>(out var enemyAI))
            {
                enemyAI.GetDamagedByBomb(damage);
            }
            if (targets[i].TryGetComponent<EnemyLairAI>(out var enemyLairAI))
            {
                enemyLairAI.GetDamaged(damage);
            }
            if (targets[i].TryGetComponent<BasicBullet>(out var bullet))
            {
                bullet.ExplosionHandler(bullet.transform.position);
            }
            if (targets[i].TryGetComponent<UnderwaterBomb>(out var bomb))
            {
                if (bomb.transform == transform)
                    continue;

                Debug.Log("炸到其他水雷", bomb);
                // bomb.StartCoroutine(bomb.ExplodeDelay(0.05f));
                bomb.GetDamaged(damage);
                Debug.Log("start coroutine", bomb);
            }

        }

        Destroy(gameObject);
    }

    public IEnumerator ExplodeDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        Explode();
    }

    public void GetDamaged(float damage)
    {
        currentHP -= damage;
        if (currentHP <= 0)
        {
            isDead = true;
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("ShipShield"))
        {
            GetDamaged(other.relativeVelocity.magnitude);
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(transform.position, explosionRange);
    }
}
