using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class HealKit : MonoBehaviour
{
    [Range(0f, 1f)]
    public float healPercent;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.GetComponentInParent<Ship>() != null && !other.CompareTag("Laser"))
        {
            var ship = other.GetComponentInParent<Ship>();
            ship.Heal(healPercent * ship.shipData.maxHealth);
            Destroy(gameObject);
        }
    }
}
