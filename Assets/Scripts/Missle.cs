using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BasicBullet))]
public class Missle : MonoBehaviour
{
    public Transform target;

    
    private float timer = 0;
    private BulletData bulletData;
    private Rigidbody2D missleRbody;

    private void Start()
    {
        bulletData = GetComponent<BasicBullet>().bulletData;
        missleRbody = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        if (timer < bulletData.chasingDelay)
        {
            timer += Time.deltaTime;
            return;
        }
        transform.up = missleRbody.velocity = (target.position - transform.position).normalized * bulletData.chasingSpeed;

    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("hit something/Trigger");

        if (other.transform != target)
            return;

        Destroy(gameObject);
    }

}
