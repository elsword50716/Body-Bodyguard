using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletManager : MonoBehaviour
{
    public float poolMaxVolume;
    private void Update()
    {
        if (transform.childCount > poolMaxVolume)
        {
            Destroy(transform.GetChild(0).gameObject);
        }
    }
}
