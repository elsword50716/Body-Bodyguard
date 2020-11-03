using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BasicBullet))]
public class Missle : MonoBehaviour
{
    public Transform target;
    public float rotateSpeed = 200f;

    private GameObject[] targetPool;
    private float timer = 0;
    private BulletData bulletData;
    private Rigidbody2D missleRbody;

    private void Start()
    {
        missleRbody = GetComponent<Rigidbody2D>();
        bulletData = GetComponent<BasicBullet>().bulletData;

        if (!string.IsNullOrEmpty(bulletData.targetTag))
        {
            targetPool = GameObject.FindGameObjectsWithTag(bulletData.targetTag);

            if (targetPool.Length == 0)
                return;
            target = targetPool[Random.Range(0, targetPool.Length)].transform;
        }
    }

    private void Update()
    {
        if (target == null)
            return;

        if (timer < bulletData.chasingDelay)
        {
            timer += Time.deltaTime;
            return;
        }

        if (target != null)
        {
            Vector2 dir = (Vector2)(target.position - transform.position).normalized;
            float rotateAmount = Vector3.Cross(dir, transform.up).z;
            missleRbody.angularVelocity = -rotateAmount * rotateSpeed;
            missleRbody.velocity = transform.up * bulletData.chasingSpeed;
        }

    }

}
