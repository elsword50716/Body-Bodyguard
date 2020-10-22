using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : MonoBehaviour
{
    public float damagePerSec;

    private void OnTriggerStay2D(Collider2D other)
    {
        if (!other.CompareTag("Enemy"))
            return;

        other.GetComponent<EnemyAI>().GetDamaged(damagePerSec * Time.deltaTime);

    }
}
