using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserGun : MonoBehaviour
{
    public GameObject laserPrefab;
    public float damagePerSec;

    private float timer = 0f;
    private float damagePreSec_temp = 0f;

    private bool canDamageEnemy = false;

    private void Start() {
        damagePreSec_temp = damagePerSec -1;
    }

    private void Update()
    {
        if(damagePreSec_temp != damagePerSec){
            damagePreSec_temp = damagePerSec;
            laserPrefab.GetComponent<Laser>().damagePerSec = damagePerSec;

        }
    }

    public void FireLaser()
    {
        if (laserPrefab.activeSelf)
            return;

        laserPrefab.SetActive(true);
    }

    
}
