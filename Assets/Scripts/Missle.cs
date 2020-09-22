using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Missle : MonoBehaviour
{

    public float chasingDelay = 0.5f;
    public float chasingSpeed = 10f;
    private Transform enemy;
    private float timer = 0;

    private Rigidbody2D missleRbody;
    private void Start()
    {
        enemy = GameObject.FindGameObjectWithTag("Enemy").transform;
        missleRbody = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        if(timer < chasingDelay){
            timer+=Time.deltaTime;
            return;
        }
        transform.up = missleRbody.velocity = (enemy.position - transform.position).normalized * chasingSpeed;
        
    }
    
    private void OnTriggerEnter2D(Collider2D other) {
        if(!other.CompareTag("Enemy"))
            return;
        
        Destroy(gameObject);
    }
    
}
