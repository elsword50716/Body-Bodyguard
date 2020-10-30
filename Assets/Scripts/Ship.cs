﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Ship : MonoBehaviour
{
    public Transform mainCamera;
    public float m_PixelsPerUnit = 20f;
    public Transform shipInside;
    public Slider healthBar;
    public Animator shipDamageEffectAnimator;
    public ParticleSystem shipHealParticle;
    public ShipData shipData;

    [SerializeField] private float currentHealth;
    private Rigidbody2D rbody2D;
    private float maxHealth_temp;

    private void Start()
    {
        rbody2D = GetComponent<Rigidbody2D>();
        currentHealth = shipData.maxHealth;
        healthBar.maxValue = shipData.maxHealth;
        maxHealth_temp = shipData.maxHealth;
        //mainCamera = Camera.main.transform;

    }

    private void Update()
    {
        //mainCamera.position = new Vector3(Round(transform.position.x), Round(transform.position.y), -10);
        mainCamera.position = new Vector3(transform.position.x, transform.position.y, -10);
        shipInside.position = transform.position;
        healthBar.value = currentHealth;

        if (maxHealth_temp != shipData.maxHealth)
        {
            healthBar.maxValue = shipData.maxHealth;
            maxHealth_temp = shipData.maxHealth;
            currentHealth = maxHealth_temp;
        }

        if (currentHealth <= 0)
        {
            currentHealth = 0f;
            Dead();
        }
    }

    float Round(float x)
    {
        return Mathf.Round(x * m_PixelsPerUnit) / m_PixelsPerUnit;
    }

    public void GetDamaged(float damage)
    {
        shipDamageEffectAnimator.SetTrigger("isShipHit");
        currentHealth -= damage;
    }

    public void Dead()
    {
        //Debug.Log("Ship Dead!!!!!!!");
    }

    public void Heal(float healPoint)
    {
        shipHealParticle.Play();
        currentHealth += healPoint;
    }

    public float GetCurrentHP()
    {
        return currentHealth;
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        Debug.Log("ship Hit!!");

        if (other.gameObject.layer != 13)
            return;

        Debug.Log("ship Damage: " + other.relativeVelocity.magnitude);

        if (other.relativeVelocity.magnitude > 5f)
            GetDamaged(other.relativeVelocity.magnitude * (shipData.maxHealth / 100f));

    }

}
