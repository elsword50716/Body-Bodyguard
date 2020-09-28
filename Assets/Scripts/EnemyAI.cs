using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    public bool isVirus;
    public Transform Ship;

    public float currentHealth;
    public float avoidShaking = 1f;
    public float NextRoamingPositionDelay = 1f;
    public Transform BulletPrefab;

    public EnemyData enemyData = new EnemyData();


    private enum State
    {
        Roaming,
        ChaseTarget,
    }
    private Vector3 startingPosotion;
    private Vector3 roamPosotion;
    private Vector3 targetPosotion;
    private bool isReachTarget = false;
    private State state;
    private float nextShootTimer = 0f;
    private float nextMoveTimer = 0f;


    private void Awake()
    {
        Ship = GameObject.FindGameObjectWithTag("Ship").transform;
        state = State.Roaming;
    }

    private void Start()
    {
        currentHealth = enemyData.maxHealth;
        startingPosotion = transform.position;
        roamPosotion = GetRoamingPostion();
    }

    private void Update()
    {
        FindTarget();

        switch (state)
        {

            default:

            case State.Roaming:
                Debug.DrawLine(transform.position, roamPosotion, Color.green);

                if (Vector3.Distance(transform.position, roamPosotion) <= 0.5f)
                {
                    if (nextMoveTimer > NextRoamingPositionDelay)
                        roamPosotion = GetRoamingPostion();
                    else
                        nextMoveTimer += Time.deltaTime;

                }
                else
                {
                    MoveTo(roamPosotion, true);
                    nextMoveTimer = 0;
                }



                break;

            case State.ChaseTarget:
                if (Vector3.Distance(transform.position, Ship.position) < enemyData.ClosestDistanceToShip - avoidShaking)
                {
                    //move back
                    MoveTo(Ship.position, false);
                }
                else if (Vector3.Distance(transform.position, Ship.position) > enemyData.ClosestDistanceToShip + avoidShaking)
                {
                    //move toward
                    MoveTo(Ship.position, true);
                }
                else
                {
                    //attcak
                    transform.RotateAround(Ship.position, Vector3.forward, enemyData.inAttackRangeMoveSpeed * Time.deltaTime);

                    if (!isVirus)
                        transform.rotation = Quaternion.identity;

                    if (Time.time > nextShootTimer)
                    {
                        Attack();
                        nextShootTimer = Time.time + enemyData.FireRate;
                    }
                }

                Debug.DrawLine(transform.position, Ship.position, Color.red);

                break;
        }

        if (currentHealth <= 0)
        {
            Death();
        }

    }

    private Vector3 GetRoamingPostion()
    {
        return startingPosotion + new Vector3(Random.Range(-1f, 1f), Random.Range(-1f, 1f)) * enemyData.roamRange;
    }

    private void MoveTo(Vector3 targetPosition, bool isApproaching)
    {
        if (isApproaching)
            transform.Translate((targetPosition - transform.position).normalized * enemyData.moveSpeed * Time.deltaTime);
        else
            transform.Translate((transform.position - targetPosition).normalized * enemyData.moveSpeed * Time.deltaTime);
    }

    private void FindTarget()
    {
        if (Vector3.Distance(transform.position, Ship.position) < enemyData.detectShipRange)
        {
            state = State.ChaseTarget;

            //Vector3.Lerp(transform.position, Ship.position, enemyData.ClosestRangeToShip);
        }
        else
        {
            state = State.Roaming;
        }
    }

    private void Attack()
    {
        if (isVirus)
        {

        }
        else
        {
            var bullet = Instantiate(BulletPrefab, transform.position, Quaternion.identity);
            var bulletRbody = bullet.GetComponent<Rigidbody2D>();
            bulletRbody.velocity = (Ship.position - transform.position).normalized * enemyData.BulletSpeed;
            bullet.transform.up = bulletRbody.velocity.normalized;
            bullet.GetComponent<BasicBullet>().bulletData.targetTag = "Ship";

        }
    }

    public void GetDamaged(float damage)
    {
        currentHealth -= damage;
    }

    public void Death()
    {
        Destroy(gameObject);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(transform.position, enemyData.detectShipRange);


    }
}
