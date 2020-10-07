using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Ship : MonoBehaviour
{
    public Transform mainCamera;
    public Slider healthBar;
    public ShipData shipData;

    [SerializeField] private float currentHealth;

    private void Start()
    {
        currentHealth = shipData.maxHealth;
        healthBar.maxValue = shipData.maxHealth;
        mainCamera = Camera.main.transform;
    }

    private void Update()
    {
        mainCamera.position = new Vector3(transform.position.x, transform.position.y, -10);
        healthBar.value = currentHealth;

        if (currentHealth <= 0)
        {
            Dead();
        }
    }

    public void GetDamaged(float damage)
    {
        currentHealth -= damage;
    }

    private void Dead()
    {
        Debug.Log("Ship Dead!!!!!!!");
    }

}
