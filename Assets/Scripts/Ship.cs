using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Ship : MonoBehaviour
{
    public Transform mainCamera;
    public Transform shipInside;
    public Slider healthBar;
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
        mainCamera = Camera.main.transform;

    }

    private void Update()
    {
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
            Dead();
        }
    }

    public void GetDamaged(float damage)
    {
        currentHealth -= damage;
    }

    public void Dead()
    {
        Debug.Log("Ship Dead!!!!!!!");
    }

    public void Heal(float healPoint)
    {
        currentHealth += healPoint;
    }

    public float GetCurrentHP(){
        return currentHealth;
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        Debug.Log("ship Hit!!");

        if (other.gameObject.layer != 13)
            return;

        Debug.Log("ship Damage: " + other.relativeVelocity.magnitude);

        GetDamaged(other.relativeVelocity.magnitude);

    }

}
