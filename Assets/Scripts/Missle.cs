using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BasicBullet))]
public class Missle : MonoBehaviour
{
    public LayerMask layerMask;
    public Transform target;
    public float missleHP;
    public float rotateSpeed = 200f;
    [Range(0f, 200f)]
    public float ExplosionRange;
    public float DetectRange;
    public SpriteRenderer aimSprite;
    public Animation aimAnimation;

    private List<GameObject> targetPool;
    private float timer = 0;
    private BulletData bulletData;
    private Rigidbody2D missleRbody;
    [SerializeField] private AnimationClip aimLoop;
    private Keyframe[] keyframes;
    private float scale;
    private Transform ship;
    private BasicBullet basicBullet;
    private float currentHP;

    private void Awake()
    {
        ship = GameObject.FindGameObjectWithTag("Ship").transform;
        basicBullet = GetComponent<BasicBullet>();
        missleRbody = GetComponent<Rigidbody2D>();
        basicBullet.isMissle = true;
    }

    private void OnValidate()
    {
        ship = GameObject.FindGameObjectWithTag("Ship").transform;
    }

    private void OnEnable()
    {
        ResetMissle();
    }

    private void Start()
    {
        ResetMissle();
    }

    private void Update()
    {
        if (target == null)
        {
            FindTarget();
            if (target == null)
            {
                aimSprite.gameObject.SetActive(false);
                return;
            }
        }

        aimSprite.transform.position = target.position;
        aimSprite.transform.localRotation = Quaternion.Inverse(transform.rotation);

        if (!aimAnimation.IsPlaying(aimLoop.name))
        {
            aimAnimation.Play(aimLoop.name);
        }

        if (timer < bulletData.chasingDelay)
        {
            timer += Time.deltaTime;
            return;
        }

        Vector2 dir = (Vector2)(target.position - transform.position).normalized;
        float rotateAmount = Vector3.Cross(dir, transform.up).z;
        missleRbody.angularVelocity = -rotateAmount * rotateSpeed;
        missleRbody.velocity = transform.up * bulletData.chasingSpeed;


    }

    public void GetDamaged(float damage)
    {
        currentHP -= damage;
        if (currentHP <= 0f)
        {
            AreaExplosion(transform.position);
        }
    }

    private void ResetMissle()
    {
        bulletData = basicBullet.bulletData;
        aimSprite.gameObject.SetActive(false);
        layerMask = basicBullet.bulletData.targetLayer;
        currentHP = missleHP;

        FindTarget();
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(transform.position, ExplosionRange);
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(ship.position, DetectRange);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.TryGetComponent<Missle>(out var missle) && !other.gameObject.CompareTag(gameObject.tag))
            missle.GetDamaged(bulletData.damage);

        if (other.transform != target)
            return;

        AreaExplosion(other.ClosestPoint(transform.position));

    }

    private void AreaExplosion(Vector3 Position)
    {
        var targets = Physics2D.OverlapCircleAll(transform.position, ExplosionRange, layerMask);
        foreach (var target in targets)
        {
            if (target.TryGetComponent<EnemyAI>(out var enemy))
                enemy.GetDamaged(bulletData.damage);

            if (target.TryGetComponent<Ship>(out var ship))
                ship.GetDamaged(bulletData.damage);

            if (target.TryGetComponent<EnemyLairAI>(out var lair))
                lair.GetDamaged(bulletData.damage);
        }
        basicBullet.ExplosionHandler(Position);
        gameObject.SetActive(false);
    }

    private void FindTarget()
    {
        if (!string.IsNullOrEmpty(bulletData.targetTag))
        {
            var targets = Physics2D.OverlapCircleAll(ship.position, DetectRange, layerMask);

            if (targets.Length == 0)
                return;
            target = targets[Random.Range(0, targets.Length)].transform;

            if (bulletData.targetTag != "Ship")
            {
                if (target.TryGetComponent<EnemyAI>(out var enemy))
                {
                    scale = enemy.enemyData.aiRadius / 1.45f;
                    aimSprite.transform.localScale = new Vector3(scale, scale, 1f);
                }
                if (target.TryGetComponent<EnemyLairAI>(out var lair))
                {
                    scale = 20f / 1.45f;
                    aimSprite.transform.localScale = new Vector3(scale, scale, 1f);
                }
            }

            aimLoop = new AnimationClip();
            aimLoop.legacy = true;
            keyframes = new Keyframe[3];
            keyframes[0] = new Keyframe(0f, scale * 1.2f);
            keyframes[1] = new Keyframe(0.5f, scale * 1f);
            keyframes[2] = new Keyframe(1f, scale * 1.2f);
            var curve = new AnimationCurve(keyframes);
            aimLoop.SetCurve("Aim_Sprite", typeof(Transform), "localScale.x", curve);
            aimLoop.SetCurve("Aim_Sprite", typeof(Transform), "localScale.y", curve);

            aimAnimation.AddClip(aimLoop, aimLoop.name);
            aimSprite.transform.position = target.position;
            aimSprite.gameObject.SetActive(true);

        }
    }
}
