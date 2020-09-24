using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    public Transform Ship;

    public float currentHealth;
    public float avoidShaking = 1f;
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
    private float nextShoot = 0f;


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
                MoveTo(roamPosotion, true);
                Debug.DrawLine(transform.position, roamPosotion, Color.green);
                float reachedPositionDistance = 1f;
                if (Vector3.Distance(transform.position, roamPosotion) < reachedPositionDistance)
                {
                    roamPosotion = GetRoamingPostion();
                }



                break;

            case State.ChaseTarget:
                if (Vector3.Distance(transform.position, Ship.position) < enemyData.ClosestDistanceToShip - avoidShaking)
                {
                    //move back
                    MoveTo(Ship.position, false);
                }
                else if (Vector3.Distance(transform.position, Ship.position) > enemyData.ClosestDistanceToShip + avoidShaking)
                    MoveTo(Ship.position, true);
                else
                {
                    //attcak
                    transform.RotateAround(Ship.position, Vector3.forward, enemyData.moveSpeed * Time.deltaTime);
                    transform.rotation = Quaternion.identity;

                    if (Time.time > nextShoot)
                    {
                        Attack();
                        nextShoot = Time.time + enemyData.FireRate;
                    }
                }

                Debug.DrawLine(transform.position, Ship.position, Color.red);

                break;
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

        var bullet = Instantiate(BulletPrefab, transform.position, Quaternion.identity);
        var bulletRbody = bullet.GetComponent<Rigidbody2D>();
        bulletRbody.velocity = (Ship.position - transform.position).normalized * enemyData.BulletSpeed;
        bullet.transform.up = bulletRbody.velocity.normalized;
        bullet.GetComponent<BasicBullet>().bulletData.targetTag = "Ship";
    }

    public void GetDamaged(float damage){
        currentHealth -= damage;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(transform.position, enemyData.detectShipRange);


    }
}
