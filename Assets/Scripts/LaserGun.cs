using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserGun : MonoBehaviour
{
    public GameObject bulletPrefab;

    public void FireLaser()
    {
        if(bulletPrefab.activeSelf)
            return;

        bulletPrefab.SetActive(true);
    }
}
