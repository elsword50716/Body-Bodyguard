using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldPack : MonoBehaviour
{
    public bool isPhoenix;
    [Range(0f, 1f)]
    public float healPercent;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.GetComponentInParent<Ship>() != null && !other.CompareTag("Laser"))
        {
            var ship = other.GetComponentInParent<Ship>();
            ship.ShieldHeal(healPercent * ship.sheildData.maxShieldHP);
            if (isPhoenix)
            {
                if (ship.sheildData.shieldSprite.transform.parent.TryGetComponent<ShieldController>(out var shield))
                {
                    shield.isInvincible = true;
                    shield.invincibleParicleUI.Play();
                }
            }
            gameObject.SetActive(false);
        }
    }
}
