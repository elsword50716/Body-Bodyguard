using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Cinemachine;
using Pathfinding;

[RequireComponent(typeof(AIPath))]
[RequireComponent(typeof(AIDestinationSetter))]
public class BossAI : MonoBehaviour
{
    public Transform ship;
    public Transform shootPoint;
    public Transform eggPool;
    public Transform cameraFollowPoint;
    public float cameraLerpValue;
    public Transform eye;
    public Transform eyeMask;
    public float eyeMoveLength;
    public CinemachineVirtualCamera bossCamera;
    public Slider bossHealthBar;
    public Animator animator;
    public LayerMask obstaclesLayer;
    public float avoidShaking = 1f;
    public EnemyData enemyData;
    public string[] bulletTag;
    public string[] DropingPoolTag;
    public string deadExplosionTag;
    public float NextRoamingPositionDelay = 1f;

    public enum State
    {
        Roaming,
        ChaseTarget,
        Attacking,
    }

    private Vector3 startingPosotion;
    private BulletManager shipBulletManager;
    private Vector3 roamPosotion;
    private Transform targetPosition;
    private GameObject ChasingPoint;
    private State state;
    private float nextShootTimer = 0f;
    private float nextMoveTimer = 0f;
    private Rigidbody2D Rbody2D;
    private AIPath aIPath;
    private AIDestinationSetter destinationSetter;
    private float originalEndReachedDistance;
    private bool alreadySetStartPosition = false;
    private bool isKillByBomb;
    [SerializeField] private float currentHealth;


    private void Awake()
    {
        ship = GameObject.FindGameObjectWithTag("Ship").transform;
        Rbody2D = GetComponent<Rigidbody2D>();
        shipBulletManager = ship.GetComponentInChildren<BulletManager>();
        destinationSetter = GetComponent<AIDestinationSetter>();
        aIPath = GetComponent<AIPath>();
        ChasingPoint = new GameObject("ChasingPoint");
        ChasingPoint.transform.parent = transform;
        targetPosition = ChasingPoint.transform;
        state = State.Roaming;
    }
    private void Start()
    {
        aIPath.canSearch = true;
        aIPath.canMove = true;
        aIPath.radius = enemyData.aiRadius;
        currentHealth = enemyData.maxHealth;
        if (!alreadySetStartPosition)
            startingPosotion = transform.position;
        roamPosotion = GetRoamingPostion();
        destinationSetter.target = targetPosition;
        originalEndReachedDistance = aIPath.endReachedDistance;

    }

    private void Update()
    {
        if (currentHealth <= 0)
        {
            currentHealth = 0;
            Dead();
        }

        aIPath.maxSpeed = enemyData.moveSpeed;

        switch (state)
        {

            default:

            case State.Roaming:
                Debug.DrawLine(transform.position, roamPosotion, Color.green);
                animator.SetBool("isFindShip", false);
                SetCameraPriority(false);
                Roaming();
                FindTarget();

                break;

            case State.ChaseTarget:
                animator.SetBool("isFindShip", true);
                SetCameraPriority(true);
                EyeFollowShip();
                SetCameraFollowPoint();
                if ((transform.position - ship.position).sqrMagnitude > enemyData.detectShipRange * enemyData.detectShipRange)
                    state = State.Roaming;

                targetPosition.position = ship.position + (transform.position - ship.position).normalized * enemyData.ClosestDistanceToShip;

                Debug.DrawLine(transform.position, ship.position, Color.red);

                if ((transform.position - ship.position).sqrMagnitude >= (enemyData.ClosestDistanceToShip + avoidShaking) * (enemyData.ClosestDistanceToShip + avoidShaking) ||
                    (transform.position - ship.position).sqrMagnitude <= (enemyData.ClosestDistanceToShip - avoidShaking) * (enemyData.ClosestDistanceToShip - avoidShaking))
                {
                    aIPath.canSearch = true;
                    aIPath.canMove = true;
                    return;
                }
                else
                {
                    aIPath.canSearch = false;
                    aIPath.canMove = false;

                }

                if (IsObstaclesBetween())
                    return;


                transform.RotateAround(ship.position, Vector3.forward, enemyData.inAttackRangeMoveSpeed * Time.deltaTime);
                transform.rotation = Quaternion.identity;

                if (nextShootTimer > enemyData.FireRate)
                {
                    //Attack();
                    state = State.Attacking;
                    nextShootTimer = 0;
                }
                else
                    nextShootTimer += Time.deltaTime;


                break;

            case State.Attacking:
                Attack();
                break;
        }
    }

    private void EyeFollowShip()
    {
        var dir = (ship.position - eyeMask.position).normalized;
        eye.position = eyeMask.position + dir * eyeMoveLength;
    }

    private void SetCameraPriority(bool isOn)
    {
        bossCamera.Priority = isOn ? 25 : 0;
        shipBulletManager.poolMaxRadious = isOn ? 100f : 60f;
    }

    private void SetCameraFollowPoint()
    {
        cameraFollowPoint.position = Vector2.Lerp(transform.position, ship.position, cameraLerpValue);
    }

    private Vector3 GetRoamingPostion()
    {
        return startingPosotion + new Vector3(enemyData.roamRange * Random.Range(-1f, 1f), enemyData.roamRange * Random.Range(-1f, 1f));
    }

    private void MoveTo(Vector3 targetPosition, bool isApproaching, bool isAttacking)
    {
        var speedMultiply = isAttacking ? enemyData.inAttackRangeMoveSpeed : enemyData.moveSpeed;

        if (isApproaching)
        {
            Rbody2D.velocity = (targetPosition - transform.position).normalized * speedMultiply;
            //transform.Translate((targetPosition - transform.position).normalized * speedMultiply * Time.deltaTime);
        }
        else
        {
            Rbody2D.velocity = (transform.position - targetPosition).normalized * speedMultiply;
            //transform.Translate((transform.position - targetPosition).normalized * speedMultiply * Time.deltaTime);
        }
    }

    private void FindTarget()
    {
        if ((transform.position - ship.position).sqrMagnitude < enemyData.detectShipRange * enemyData.detectShipRange)
        {
            if (IsObstaclesBetween())
                return;

            Rbody2D.velocity = Vector2.zero;
            if (alreadySetStartPosition)
            {
                enemyData.moveSpeed /= 3f;
                alreadySetStartPosition = false;
            }
            state = State.ChaseTarget;
        }
        else
        {
            state = State.Roaming;
        }
    }

    private void Roaming()
    {
        var dir = roamPosotion - transform.position;


        if (aIPath.reachedEndOfPath)
        {
            if (nextMoveTimer > NextRoamingPositionDelay)
            {
                if (alreadySetStartPosition)
                {
                    enemyData.moveSpeed /= 3f;
                    alreadySetStartPosition = false;
                }
                roamPosotion = GetRoamingPostion();
                aIPath.endReachedDistance = 0;
                nextMoveTimer = 0;
            }
            else
                nextMoveTimer += Time.deltaTime;
        }
        else
        {
            if (dir.magnitude < 0.5f)
                aIPath.endReachedDistance = originalEndReachedDistance;

            targetPosition.position = roamPosotion;
            //MoveTo(roamPosotion, true, false);
            nextMoveTimer = 0;
        }
    }

    private void Attack()
    {
        Debug.Log("Boss Attck");
        state = State.ChaseTarget;
    }

    private void Dead()
    {
        Rbody2D.constraints = RigidbodyConstraints2D.FreezeAll;

        // if (!isVirus)
        // {
        //     foreach (Transform bullet in bulletPool)
        //     {
        //         bullet.transform.parent = objectPooler.transform;
        //         Debug.Log("Bullet BAck");
        //         //bullet.gameObject.SetActive(false);
        //     }
        //     if (bulletPool.childCount > 0)
        //         return;
        // }

        var deadExplosion = ObjectPooler.Instance.SpawnFromPool(deadExplosionTag, transform.position, null).GetComponent<ParticleSystem>();

        deadExplosion.Play();
        if (!isKillByBomb)
        {
            CameraController.Instance.ShakeCamera(10f, .1f, false);
        }
        if (DropingPoolTag.Length != 0)
        {
            if (Random.Range(0f, 1f) <= enemyData.dropProbability)
            {
                var index = Random.Range(0, DropingPoolTag.Length);
                if (!string.IsNullOrEmpty(DropingPoolTag[index]))
                    ObjectPooler.Instance.SpawnFromPool(DropingPoolTag[index], transform.position, null);
            }
        }
        Destroy(gameObject);
    }

    private bool IsObstaclesBetween()
    {
        var toShipDir = ship.position - transform.position;

        RaycastHit2D hit2D = Physics2D.Raycast(transform.position, toShipDir.normalized, toShipDir.magnitude, obstaclesLayer);

        return hit2D.collider != null ? true : false;
    }

    public void GetDamaged(float damage)
    {
        currentHealth -= damage;
        if (currentHealth <= 0f)
        {
            isKillByBomb = false;
            Dead();
        }
    }

    private void DamageShip(Collider2D collider)
    {
        if (collider.TryGetComponent<ShieldController>(out var shield))
        {
            if (!shield.isInvincible)
                collider.GetComponentInParent<Ship>().GetDamaged(enemyData.attackDamage);
        }
        else
            collider.GetComponentInParent<Ship>().GetDamaged(enemyData.attackDamage);
    }

    public void SetStartPosition(Vector3 position)
    {
        startingPosotion = position;
        enemyData.moveSpeed *= 3f;
        alreadySetStartPosition = true;
    }

    public void GetDamagedByBomb(float damage)
    {
        currentHealth -= damage;
        if (currentHealth <= 0f)
        {
            isKillByBomb = true;
            Dead();
        }
    }

    private void OnDrawGizmosSelected()
    {

        Gizmos.DrawWireSphere(transform.position, enemyData.detectShipRange);
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireCube(startingPosotion, new Vector3(enemyData.roamRange, enemyData.roamRange, 0f));
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, enemyData.ClosestDistanceToShip);
    }


}
