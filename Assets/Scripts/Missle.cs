using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BasicBullet))]
public class Missle : MonoBehaviour
{
    public LayerMask layerMask;
    public Transform target;
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
    [SerializeField]private AnimationClip aimStart;
    [SerializeField]private AnimationClip aimLoop;
    private Keyframe[] keyframes;
    private float scale;
    private Transform ship;

    private void OnValidate() {
        ship = GameObject.FindGameObjectWithTag("Ship").transform;
    }

    private void Start()
    {
        missleRbody = GetComponent<Rigidbody2D>();
        bulletData = GetComponent<BasicBullet>().bulletData;
        //layerMask = GameObject.FindGameObjectWithTag(bulletData.targetTag).layer;
        aimSprite.gameObject.SetActive(false);

        if (!string.IsNullOrEmpty(bulletData.targetTag))
        {
            var targets = Physics2D.OverlapCircleAll(transform.position, DetectRange, layerMask);
            // var temp = GameObject.FindGameObjectsWithTag(bulletData.targetTag);
            // targetPool = new List<GameObject>(temp);

            // foreach (var target in targetPool)
            // {
            //     if(!target.activeSelf)
            //         targetPool.Remove(target);
            // }

            // if (targetPool.Count == 0)
            //     return;
            // target = targetPool[Random.Range(0, targetPool.Count)].transform;

            if(targets.Length == 0)
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
            
            aimStart.legacy = true;
            keyframes = new Keyframe[2];
            keyframes[0] = new Keyframe(0f, scale * 1.2f);
            keyframes[1] = new Keyframe(0.5f, scale * 1f);
            var curve = new AnimationCurve(keyframes);
            aimStart.SetCurve("Aim_Sprite", typeof(Transform), "localScale.x", curve);
            aimStart.SetCurve("Aim_Sprite", typeof(Transform), "localScale.y", curve);

            aimLoop.legacy = true;
            keyframes = new Keyframe[3];
            keyframes[0] = new Keyframe(0f, scale * 1.2f);
            keyframes[1] = new Keyframe(0.5f, scale * 1f);
            keyframes[2] = new Keyframe(1f, scale * 1f);
            curve = new AnimationCurve(keyframes);
            aimLoop.SetCurve("Aim_Sprite", typeof(Transform), "localScale.x", curve);
            aimLoop.SetCurve("Aim_Sprite", typeof(Transform), "localScale.y", curve);

            aimAnimation.AddClip(aimStart, aimStart.name);
            aimAnimation.AddClip(aimLoop, aimLoop.name);

            aimSprite.transform.position = target.position;
            aimSprite.gameObject.SetActive(true);
            aimAnimation.Play(aimStart.name);
        }


    }

    private void Update()
    {
        if (target == null)
            return;

        aimSprite.transform.position = target.position;
        aimSprite.transform.localRotation = Quaternion.Inverse(transform.rotation);

        if(!aimAnimation.IsPlaying(aimStart.name) && !aimAnimation.IsPlaying(aimLoop.name)){
            aimAnimation.Play(aimLoop.name);
        }

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

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(transform.position, ExplosionRange);
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(ship.position, DetectRange);
    }
}
