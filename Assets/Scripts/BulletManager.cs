using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletManager : MonoBehaviour
{
    public float poolMaxVolume;
    public bool isShip;
    public float poolMaxRadious;
    private void Update()
    {
        if (transform.childCount > poolMaxVolume)
        {
            Destroy(transform.GetChild(0).gameObject);
        }

        if(isShip){
            for (int i = 0; i < transform.childCount; i++)
            {
                if(Vector3.Distance(transform.GetChild(i).position, transform.position) > poolMaxRadious)
                    Destroy(transform.GetChild(i).gameObject);
            }            
            
        }
    }

    private void OnDrawGizmosSelected() {
        Gizmos.DrawWireSphere(transform.position, poolMaxRadious);
    }
}
