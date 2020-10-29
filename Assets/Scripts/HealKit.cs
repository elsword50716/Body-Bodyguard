using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealKit : MonoBehaviour
{
    public float healPoint;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.GetComponentInParent<Ship>() != null)
        {
            var ship = other.GetComponentInParent<Ship>();
            ship.Heal(healPoint);
            Destroy(gameObject);
        }
    }
}
