using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnderwaterBomb : MonoBehaviour
{
    public LayerMask effectLayer;
    public float explosionRange;
    public string explosionParticleTag;
    public float damage;
    public float MaxHP;

    private float currentHP;

    private void Start()
    {
        currentHP = MaxHP;
    }

    private void Update()
    {
        if (currentHP <= 0f)
            Explode();
    }

    public void Explode()
    {
        var targets = Physics2D.OverlapCircleAll(transform.position, explosionRange, effectLayer);

        foreach (var target in targets)
        {
            if (target.TryGetComponent<Ship>(out var ship))
            {
                ship.GetDamaged(damage);
            }
            if (target.TryGetComponent<EnemyAI>(out var enemyAI))
            {
                enemyAI.GetDamaged(damage);
            }
            if (target.TryGetComponent<EnemyLairAI>(out var enemyLairAI))
            {
                enemyLairAI.GetDamaged(damage);
            }
            if (target.TryGetComponent<UnderwaterBomb>(out var bomb))
            {
                bomb.Explode();
            }

        }

        ObjectPooler.Instance.SpawnFromPool(explosionParticleTag, transform.position, null);
        gameObject.SetActive(false);
    }

    public void GetDamaged(float damage)
    {
        currentHP -= damage;
        if (currentHP <= 0)
        {
            Explode();
        }
    }
}
