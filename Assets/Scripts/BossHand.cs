using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossHand : MonoBehaviour
{
    public int id;
    public float maxHealth;
    public float currentHealth;
    public float deadCameraShackIntensity;
    public string explodeParticleTag;
    public string[] dropPickUpTag;
    public Material hitMaterial;
    public Animator bossAnimator;
    public EnemyAI enemyAI;

    private SpriteRenderer spriteRenderer;
    private Material originalMaterial;

    private void Awake()
    {
        currentHealth = maxHealth;
        spriteRenderer = GetComponent<SpriteRenderer>();
        originalMaterial = spriteRenderer.material;
    }

    private void Update()
    {
        if (currentHealth <= 0)
        {
            bossAnimator.SetTrigger("isDamaged");
            currentHealth = 0;
            enemyAI.GetDamaged(maxHealth);
            ObjectPooler.Instance.SpawnFromPool(explodeParticleTag, transform.position, null);
            CameraController.Instance.ShakeCamera(deadCameraShackIntensity, .2f, false);
            var randomIndex = Random.Range(0, dropPickUpTag.Length);
            ObjectPooler.Instance.SpawnFromPool(dropPickUpTag[randomIndex], transform.position, null);
            gameObject.SetActive(false);
        }
    }

    public void GetDamaged(float damage)
    {
        spriteRenderer.material = hitMaterial;
        Invoke("ResetMaterial", 0.1f);
        currentHealth -= damage;
    }

    private void ResetMaterial()
    {
        spriteRenderer.material = originalMaterial;
    }
}
