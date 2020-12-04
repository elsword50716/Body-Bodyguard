using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldPack : MonoBehaviour
{
    [Range(0f, 1f)]
    public float healPercent;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.GetComponentInParent<Ship>() != null && !other.CompareTag("Laser"))
        {
            var ship = other.GetComponentInParent<Ship>();
            ship.ShieldHeal(healPercent * ship.sheildData.maxShieldHP);
            gameObject.SetActive(false);
        }
    }
}
