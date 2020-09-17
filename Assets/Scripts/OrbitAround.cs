using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrbitAround : MonoBehaviour
{
    public Transform center, bulletPrefab;
    public float speed, timeRange;
    [Range(0, 100)] public float bulletSpeed;
    [Range(-90, 90)] public float speed2;

    private float timer;

    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        transform.RotateAround(center.transform.position, Vector3.forward, speed * Time.fixedDeltaTime);
        Debug.DrawLine(transform.position, transform.position + transform.up.normalized, Color.red);


        if (timer < timeRange)
        {
            timer += Time.fixedDeltaTime;
        }
        else
        {
            Rigidbody2D bulletRbody = Instantiate(bulletPrefab.GetComponent<Rigidbody2D>(), transform.position + transform.up.normalized, Quaternion.identity);

            bulletRbody.velocity = transform.up.normalized * bulletSpeed;
            timer = 0;
        }

        transform.rotation = Quaternion.AngleAxis(speed2, Vector3.forward);

        

    }
}
