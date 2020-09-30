using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    public bool isVirus;
    public float virusAttackMoveSpeed;
    public Transform Ship;
    public ContactFilter2D shipLayer;
    public float currentHealth;
    public float avoidShaking = 1f;
    public float NextRoamingPositionDelay = 1f;
    public Transform BulletPrefab;

    [Header("敵人資料")]
    public EnemyData enemyData = new EnemyData();


    private enum State
    {
        Roaming,
        ChaseTarget,
        Attacking,
        Dead,
    }
    private Vector3 startingPosotion;
    private Vector3 roamPosotion;
    private Vector3 targetPosotion;
    private bool isReachTarget = false;
    private State state;
    private float nextShootTimer = 0f;
    private float nextMoveTimer = 0f;
    private Transform bulletPool;
    private Animator animator;


    private void Awake()
    {
        Ship = GameObject.FindGameObjectWithTag("Ship").transform;
        if (!isVirus)
            bulletPool = GetComponentInChildren<BulletManager>().transform;
        animator = GetComponentInChildren<Animator>();
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

        switch (state)
        {

            default:

            case State.Roaming:
                Debug.DrawLine(transform.position, roamPosotion, Color.green);
                Roaming();
                FindTarget();




                break;

            case State.ChaseTarget:
                if (Vector3.Distance(transform.position, Ship.position) > enemyData.detectShipRange)
                    state = State.Roaming;

                if (Vector3.Distance(transform.position, Ship.position) < enemyData.ClosestDistanceToShip - avoidShaking)
                {

                    //move back
                    MoveTo(Ship.position, false, false);
                }
                else if (Vector3.Distance(transform.position, Ship.position) > enemyData.ClosestDistanceToShip + avoidShaking)
                {
                    //move toward
                    MoveTo(Ship.position, true, false);
                }
                else
                {
                    //attcak
                    transform.RotateAround(Ship.position, Vector3.forward, enemyData.inAttackRangeMoveSpeed * Time.deltaTime);

                    transform.rotation = Quaternion.identity;

                    if (isVirus)
                    {
                        var spriteUpDir = (Ship.position - transform.position).normalized;
                        GetComponentInChildren<SpriteRenderer>().transform.up = spriteUpDir;
                    }

                    if (nextShootTimer > enemyData.FireRate)
                    {
                        //Attack();
                        state = State.Attacking;
                        nextShootTimer = 0;
                    }
                    else
                        nextShootTimer += Time.deltaTime;
                }

                Debug.DrawLine(transform.position, Ship.position, Color.red);

                break;

            case State.Attacking:
                Attack();
                break;

            case State.Dead:
                animator.SetBool("isDead", true);
                break;
        }

        if (currentHealth <= 0)
        {
            state = State.Dead;
        }

    }

    private Vector3 GetRoamingPostion()
    {
        return startingPosotion + new Vector3(Random.Range(-1f, 1f), Random.Range(-1f, 1f)) * enemyData.roamRange;
    }

    private void MoveTo(Vector3 targetPosition, bool isApproaching, bool isAttacking)
    {
        var speedMultiply = isAttacking ? virusAttackMoveSpeed : enemyData.moveSpeed;

        if (isApproaching)
        {
            if (isVirus)
            {
                var spriteUpDir = (targetPosition - transform.position).normalized;
                GetComponentInChildren<SpriteRenderer>().transform.up = spriteUpDir;
            }
            transform.Translate((targetPosition - transform.position).normalized * speedMultiply * Time.deltaTime);
        }
        else
            transform.Translate((transform.position - targetPosition).normalized * speedMultiply * Time.deltaTime);
    }

    private void FindTarget()
    {
        if (Vector3.Distance(transform.position, Ship.position) < enemyData.detectShipRange)
        {
            state = State.ChaseTarget;


        }
        else
        {
            state = State.Roaming;
        }
    }

    private void Roaming()
    {
        if (Vector3.Distance(transform.position, roamPosotion) <= 0.5f)
        {
            if (nextMoveTimer > NextRoamingPositionDelay)
                roamPosotion = GetRoamingPostion();
            else
                nextMoveTimer += Time.deltaTime;

        }
        else
        {
            MoveTo(roamPosotion, true, false);
            nextMoveTimer = 0;
        }
    }

    private void Attack()
    {
        if (isVirus)
        {
            List<Collider2D> collider2Ds = new List<Collider2D>();
            if (transform.GetComponent<Rigidbody2D>().GetContacts(shipLayer, collider2Ds) > 0)
            {
                //damage the ship
                DamageShip();
                state = State.ChaseTarget;
            }
            else
            {
                MoveTo(Ship.position, true, true);
                Debug.DrawLine(transform.position, Ship.position, Color.blue);
            }

        }
        else
        {
            var bullet = Instantiate(BulletPrefab, transform.position, Quaternion.identity, bulletPool);
            var bulletRbody = bullet.GetComponent<Rigidbody2D>();
            bulletRbody.velocity = (Ship.position - transform.position).normalized * enemyData.BulletSpeed;
            bullet.transform.up = bulletRbody.velocity.normalized;
            bullet.GetComponent<BasicBullet>().bulletData.targetTag = "Ship";
            bullet.GetComponent<BasicBullet>().bulletData.damage = enemyData.attackDamage;
            state = State.ChaseTarget;
        }

    }

    public void GetDamaged(float damage)
    {
        currentHealth -= damage;
    }

    private void DamageShip()
    {
        Debug.Log("hit Ship/Virus");
    }



    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(transform.position, enemyData.detectShipRange);
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, enemyData.roamRange);
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, enemyData.ClosestDistanceToShip);

    }
}
