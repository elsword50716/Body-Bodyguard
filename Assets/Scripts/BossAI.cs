using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Cinemachine;

[RequireComponent(typeof(EnemyAI))]
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
    public TentacleBlockController tentacle;
    public Slider bossHealthBar;
    public Animator animator;
    public string bulletTag;
    public string deadExplosionTag;
    [Header("衝撞攻擊資料")]
    public float rushDamper;
    public GameObject rushParticle;
    [Header("產卵攻擊資料")]
    public string eggTag;
    public int eggSpawnNumberPerTime;
    public float eggSpawnRate;
    public float eggSpawnTorque;
    [Header("雷射攻擊資料")]
    public GameObject laserPrefab;
    public float laserRotateSpeed;
    public float laserRotateLineLenght;
    [Header("尾獸玉攻擊資料")]
    public string laserBallTag;
    public int laserBallNumberPerTime;
    public float laserBallSpawnRate;

    private EnemyAI enemyAI;
    private BulletManager shipBulletManager;
    private EnemyData enemyData;
    private float shipDistance;
    private int attackIndex;
    private bool isFinishAttck;
    private Vector3 targetPosion;
    private bool isFirstRush;
    private bool isFirstLaser;
    private float laserRotateLineY;
    private Vector3 laserRotateLinePointA;
    private Vector3 laserRotateLinePointB;
    private Vector3 laserRotateLinePointC;
    private Vector3 laserRotateDir;
    private Vector3 laserRotateAddDir;
    private bool islaserRotateFromPointA;
    private int eggNumber;
    private int laserBallNumber;
    private float timer;

    private void OnValidate()
    {
        enemyAI = GetComponent<EnemyAI>();
        enemyAI.isBoss = true;
    }
    private void Awake()
    {
        ship = GameObject.FindGameObjectWithTag("Ship").transform;
        shipBulletManager = ship.GetComponentInChildren<BulletManager>();
        enemyAI = GetComponent<EnemyAI>();
        enemyData = enemyAI.enemyData;
    }

    private void OnEnable()
    {
        isFinishAttck = true;
        isFirstRush = true;
        isFirstLaser = true;
        eggNumber = 0;

    }

    private void Start()
    {

    }

    private void Update()
    {
        shipDistance = (ship.position - transform.position).sqrMagnitude;
        if (shipDistance < enemyData.detectShipRange * enemyData.detectShipRange)
        {
            animator.SetBool("isFindShip", true);
            EyeFollowShip();
            SetCameraPriority(true);
            SetCameraFollowPoint();
        }
        else
            animator.SetBool("isFindShip", false);
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

    public void Attack()
    {
        Debug.Log("Boss Attck");
        if (isFinishAttck)
        {
            attackIndex = Random.Range(0, 2);
            isFinishAttck = false;
        }
        switch (attackIndex)
        {
            case 0:
                Rush();
                break;
            case 1:
                ShootEgg();
                break;
            case 3:
                MouthLaser();
                break;
            case 4:
                MouthLaserBall();
                break;

        }
    }
    private void Rush()
    {
        if (isFirstRush)
        {
            targetPosion = ship.position;
            isFirstRush = false;
        }
        if ((targetPosion - transform.position).sqrMagnitude > rushDamper * rushDamper)
        {
            rushParticle.SetActive(true);
            List<Collider2D> collider2Ds = new List<Collider2D>();
            if (enemyAI.Rbody2D.GetContacts(enemyAI.shipLayer, collider2Ds) > 0)
            {
                //damage the ship
                enemyAI.DamageShip(collider2Ds[0]);
                rushParticle.SetActive(false);
                isFirstRush = true;
                isFinishAttck = true;
                enemyAI.SetState(EnemyAI.State.ChaseTarget);
                return;
            }
            enemyAI.MoveTo(targetPosion, true, true);
            Debug.DrawLine(transform.position, targetPosion, Color.blue);
        }
        else
        {
            rushParticle.SetActive(false);
            isFirstRush = true;
            isFinishAttck = true;
            enemyAI.SetState(EnemyAI.State.ChaseTarget);
        }
    }
    private void ShootEgg()
    {
        if (eggNumber == eggSpawnNumberPerTime)
        {
            isFinishAttck = true;
            enemyAI.SetState(EnemyAI.State.ChaseTarget);
            timer = 0;
            eggNumber = 0;
            return;
        }

        if (timer > eggSpawnRate)
        {
            BirthEgg();
            timer = 0f;
        }
        else
            timer += Time.deltaTime;
    }

    private void BirthEgg()
    {
        //play sound here
        var egg = ObjectPooler.Instance.SpawnFromPool(eggTag, shootPoint.position, null);
        egg.SetActive(false);
        egg.SetActive(true);
        var eggRbody = egg.GetComponent<Rigidbody2D>();
        eggRbody.AddTorque(eggSpawnTorque);
        eggRbody.velocity = (ship.position - shootPoint.position).normalized * enemyData.BulletSpeed;
        eggNumber++;
    }

    private void MouthLaser()
    {
        if (isFirstLaser)
        {
            //play sound here
            isFirstLaser = false;
            laserRotateLineY = ship.position.y;
            islaserRotateFromPointA = (ship.position.x - shootPoint.position.x) > 0 ? true : false;
            laserRotateLinePointA = new Vector2(shootPoint.position.x - laserRotateLineLenght / 2, laserRotateLineY);
            laserRotateLinePointB = new Vector2(shootPoint.position.x + laserRotateLineLenght / 2, laserRotateLineY);
            if (islaserRotateFromPointA)
            {
                laserRotateLinePointC = Vector2.Lerp(laserRotateLinePointA, laserRotateLinePointB, Time.deltaTime) * laserRotateSpeed;
                laserRotateDir = laserRotateLinePointA - shootPoint.position;
            }
            else
            {
                laserRotateLinePointC = Vector2.Lerp(laserRotateLinePointB, laserRotateLinePointA, Time.deltaTime) * laserRotateSpeed;
                laserRotateDir = laserRotateLinePointB - shootPoint.position;
            }
            laserPrefab.transform.right = laserRotateDir.normalized;
            laserPrefab.SetActive(true);
        }

        if (laserRotateLinePointC.x > laserRotateLinePointB.x || laserRotateLinePointC.x < laserRotateLinePointA.x)
        {
            laserPrefab.SetActive(false);
            isFirstLaser = true;
            isFinishAttck = true;
            enemyAI.SetState(EnemyAI.State.ChaseTarget);
            return;
        }

        laserRotateDir = laserRotateLinePointC - shootPoint.position;
        laserPrefab.transform.right = laserRotateDir.normalized;

        if (islaserRotateFromPointA)
            laserRotateLinePointC = Vector2.Lerp(laserRotateLinePointC, laserRotateLinePointB, Time.deltaTime) * laserRotateSpeed;
        else
            laserRotateLinePointC = Vector2.Lerp(laserRotateLinePointC, laserRotateLinePointA, Time.deltaTime) * laserRotateSpeed;


    }

    private void MouthLaserBall()
    {

    }

    public void Dead()
    {

    }


}
