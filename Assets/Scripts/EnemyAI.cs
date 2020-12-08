using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using Pathfinding;

[RequireComponent(typeof(AIPath))]
[RequireComponent(typeof(AIDestinationSetter))]
public class EnemyAI : MonoBehaviour
{
    public bool isTurret;
    public bool isVirus;
    public float virusAttackMoveSpeed;
    public Transform Ship;
    public ContactFilter2D shipLayer;
    public float avoidShaking = 1f;
    public float NextRoamingPositionDelay = 1f;
    public LayerMask obstaclesLayer;
    public string[] DropingPoolTag;
    public string bulletPoolTag;
    public Transform bulletPool;
    [SerializeField] private float currentHealth;
    public string deadExplosionTag;
    public Material hitEffectMaterial;


    [Header("敵人資料")]
    public EnemyData enemyData = new EnemyData();


    private enum State
    {
        Roaming,
        ChaseTarget,
        Attacking,
    }
    private Vector3 startingPosotion;
    private Vector3 roamPosotion;
    private Transform targetPosition;
    private GameObject ChasingPoint;
    private State state;
    private float nextShootTimer = 0f;
    private float nextMoveTimer = 0f;

    private Animator animator;
    private Rigidbody2D Rbody2D;
    private AIPath aIPath;
    private AIDestinationSetter destinationSetter;
    private float originalEndReachedDistance;
    private ObjectPooler objectPooler;
    private Transform enemySprite;
    private bool alreadySetStartPosition = false;
    private bool isKillByBomb;
    private Material originalMaterial;
    private SpriteRenderer[] spriteRenderer;

    private void OnValidate()
    {
        startingPosotion = transform.position;
        if (enemySprite == null)
            enemySprite = transform.GetChild(0);
    }

    private void Awake()
    {
        Ship = GameObject.FindGameObjectWithTag("Ship").transform;

        destinationSetter = GetComponent<AIDestinationSetter>();
        aIPath = GetComponent<AIPath>();

        animator = GetComponentInChildren<Animator>();

        Rbody2D = GetComponent<Rigidbody2D>();
        ChasingPoint = new GameObject("ChasingPoint");
        ChasingPoint.transform.parent = transform;
        targetPosition = ChasingPoint.transform;
        //targetPosition = Instantiate(ChasingPoint.transform, Vector2.zero, Quaternion.identity, transform);
        state = State.Roaming;
        enemySprite = transform.GetChild(0);
        spriteRenderer = enemySprite.GetComponentsInChildren<SpriteRenderer>();
        originalMaterial = spriteRenderer[0].material;
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
        objectPooler = ObjectPooler.Instance;
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


                Roaming();
                FindTarget();




                break;

            case State.ChaseTarget:
                if ((transform.position - Ship.position).sqrMagnitude > enemyData.detectShipRange * enemyData.detectShipRange)
                    state = State.Roaming;

                targetPosition.position = Ship.position + (transform.position - Ship.position).normalized * enemyData.ClosestDistanceToShip;

                Debug.DrawLine(transform.position, Ship.position, Color.red);

                if ((transform.position - Ship.position).sqrMagnitude >= (enemyData.ClosestDistanceToShip + avoidShaking) * (enemyData.ClosestDistanceToShip + avoidShaking) ||
                    (transform.position - Ship.position).sqrMagnitude <= (enemyData.ClosestDistanceToShip - avoidShaking) * (enemyData.ClosestDistanceToShip - avoidShaking))
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

                if (isVirus)
                    enemySprite.up = aIPath.desiredVelocity;

                if (IsObstaclesBetween())
                    return;


                transform.RotateAround(Ship.position, Vector3.forward, enemyData.inAttackRangeMoveSpeed * Time.deltaTime);
                transform.rotation = Quaternion.identity;

                //attcak
                if (isVirus)
                {
                    var spriteUpDir = (Ship.position - transform.position).normalized;
                    enemySprite.up = spriteUpDir;
                }

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

    private Vector3 GetRoamingPostion()
    {
        if (!isTurret)
            return startingPosotion + new Vector3(Random.Range(-1f, 1f) * enemyData.roamRange, Random.Range(-1f, 1f) * enemyData.roamRange);
        else
            return startingPosotion + enemySprite.right * enemyData.roamRange * Random.Range(-1f, 1f);
    }

    private void MoveTo(Vector3 targetPosition, bool isApproaching, bool isAttacking)
    {
        var speedMultiply = isAttacking ? virusAttackMoveSpeed : enemyData.moveSpeed;

        if (isApproaching)
        {
            if (isVirus)
            {
                var spriteUpDir = (targetPosition - transform.position).normalized;
                enemySprite.up = spriteUpDir;
            }
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
        if ((transform.position - Ship.position).sqrMagnitude < enemyData.detectShipRange * enemyData.detectShipRange)
        {
            if (IsObstaclesBetween())
                return;

            Rbody2D.velocity = Vector2.zero;
            if (alreadySetStartPosition)
            {
                enemyData.moveSpeed /= 3f;
                alreadySetStartPosition = false;
            }
            if (isTurret)
                state = State.Attacking;
            else
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

            if (isVirus)
                enemySprite.up = aIPath.desiredVelocity;

            targetPosition.position = roamPosotion;
            //MoveTo(roamPosotion, true, false);
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
                DamageShip(collider2Ds[0]);
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
            if (isTurret)
            {
                if (nextShootTimer > enemyData.FireRate)
                {
                    FireBullet();
                    nextShootTimer = 0;
                }
                else
                    nextShootTimer += Time.deltaTime;
            }
            else
                FireBullet();
        }

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

        var deadExplosion = objectPooler.SpawnFromPool(deadExplosionTag, transform.position, null).GetComponent<ParticleSystem>();

        if (isVirus || isTurret)
        {
            SetDeadExplotionParticleColor(deadExplosion);
        }

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
                    objectPooler.SpawnFromPool(DropingPoolTag[index], transform.position, null);
            }
        }
        Destroy(gameObject);
    }

    private bool IsObstaclesBetween()
    {
        var toShipDir = Ship.position - transform.position;

        RaycastHit2D hit2D = Physics2D.Raycast(transform.position, toShipDir.normalized, toShipDir.magnitude, obstaclesLayer);

        return hit2D.collider != null ? true : false;
    }

    private void SetDeadExplotionParticleColor(ParticleSystem particle)
    {
        Color color = transform.GetChild(1).GetComponent<SpriteRenderer>().color;
        ParticleSystem.MinMaxGradient grad = new ParticleSystem.MinMaxGradient(color, Color.black);
        var particleMain = particle.main;
        particleMain.startColor = grad;
    }

    public void GetDamaged(float damage)
    {
        currentHealth -= damage;
        for (int i = 0; i < spriteRenderer.Length; i++)
        {
            spriteRenderer[i].material = hitEffectMaterial;
        }
        Invoke("ResetMaterial", 0.1f);
        if (currentHealth <= 0f)
        {
            isKillByBomb = false;
            Dead();
        }

    }

    private void ResetMaterial()
    {
        for (int i = 0; i < spriteRenderer.Length; i++)
        {
            spriteRenderer[i].material = originalMaterial;
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

    private void FireBullet()
    {
        var bullet = objectPooler.SpawnFromPool(bulletPoolTag, transform.position, null);
        var bulletRbody = bullet.GetComponent<Rigidbody2D>();
        bulletRbody.velocity = (Ship.position - transform.position).normalized * enemyData.BulletSpeed;
        bullet.transform.up = bulletRbody.velocity.normalized;
        bullet.GetComponent<BasicBullet>().bulletData.targetTag = "Ship";
        bullet.GetComponent<BasicBullet>().bulletData.damage = enemyData.attackDamage;
        if (isTurret)
            state = State.Roaming;
        else
            state = State.ChaseTarget;
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
        if (isTurret)
            Gizmos.DrawWireCube(startingPosotion, enemySprite.right * enemyData.roamRange * 2);
        else
        {
            Gizmos.DrawWireCube(startingPosotion, new Vector3(enemyData.roamRange, enemyData.roamRange, 0f));
            Gizmos.color = Color.blue;
            Gizmos.DrawWireSphere(transform.position, enemyData.ClosestDistanceToShip);
        }

    }


}
