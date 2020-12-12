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
    public string[] eggTag;
    public int eggSpawnNumberPerTime;
    public float eggSpawnRate;
    public float eggSpawnTorque;
    [Header("雷射攻擊資料")]
    public GameObject[] laserPrefab;
    public ParticleSystem laserChargeParticle;
    public GameObject laserChargeBallParticle;
    public float laserChargeBallMaxSize;
    public float laserChargeBallToMaxSpeed;
    public float laserDelay;
    public float laserRotateSpeed;
    public float laserRotateLineLenght;
    [Header("尾獸玉攻擊資料")]
    public string[] laserBallTag;
    public float laserBallSpeed;
    public float laserBallTorqueSpeed;
    public int laserBallNumberPerTime;
    public float laserBallSpawnRate;
    public float laserBallMaxSize;
    public float laserBallToMaxSizeSpeed;
    public ParticleSystem laserBallChargeParicle;

    private EnemyAI enemyAI;
    private BulletManager shipBulletManager;
    private EnemyData enemyData;
    private float shipDistance;
    private int attackIndex;
    private bool isFinishAttck;
    private Vector3 targetPosion;
    private bool isFirst;
    private float laserRotateLineY;
    private Vector3 laserRotateLinePointA;
    private Vector3 laserRotateLinePointB;
    private Vector3 laserRotateLinePointC;
    private Vector3 laserRotateDir;
    private Vector3 laserRotateAddDir;
    private bool islaserRotateFromPointA;
    private int eggNumber;
    private int laserBallNumber;
    private GameObject laserBall_Temp;
    private GameObject egg_Temp;
    private Rigidbody2D laserBallRbody_Temp;
    private float timer;
    private int randomIndex;

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
        isFirst = true;
        eggNumber = 0;
        laserBallNumber = 0;

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
        {
            animator.SetBool("isFindShip", false);
            SetCameraPriority(false);
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

    public void Attack()
    {
        if (isFinishAttck)
        {
            attackIndex = Random.Range(1, 2);
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
            case 2:
                MouthLaser();
                break;
            case 3:
                MouthLaserBall();
                break;

        }
    }
    private void Rush()
    {
        if (isFirst)
        {
            targetPosion = ship.position;
            isFirst = false;
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
                isFirst = true;
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
            isFirst = true;
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
            randomIndex = Random.Range(0, eggTag.Length);
            egg_Temp = ObjectPooler.Instance.SpawnFromPool(eggTag[randomIndex], shootPoint.position, null);
            if (!egg_Temp.transform.GetChild(0).gameObject.activeSelf)
            {
                egg_Temp.SetActive(false);
                egg_Temp.SetActive(true);
            }
            Invoke("BirthEgg", 1f);
            timer = 0f;
        }
        else
            timer += Time.deltaTime;
    }

    private void BirthEgg()
    {
        //play sound here
        var eggRbody = egg_Temp.GetComponent<Rigidbody2D>();
        eggRbody.AddTorque(eggSpawnTorque);
        eggRbody.velocity = (ship.position - shootPoint.position).normalized * enemyData.BulletSpeed;
        eggNumber++;
    }

    private void MouthLaser()
    {
        if (isFirst)
        {
            //play sound here
            isFirst = false;
            laserRotateLineY = ship.position.y;
            islaserRotateFromPointA = (ship.position.x - shootPoint.position.x) > 0 ? true : false;
            laserRotateLinePointA = new Vector2(shootPoint.position.x - laserRotateLineLenght / 2, laserRotateLineY);
            laserRotateLinePointB = new Vector2(shootPoint.position.x + laserRotateLineLenght / 2, laserRotateLineY);
            if (islaserRotateFromPointA)
            {
                laserRotateLinePointC = laserRotateLinePointA;
                laserRotateDir = laserRotateLinePointA - shootPoint.position;
            }
            else
            {
                laserRotateLinePointC = laserRotateLinePointB;
                laserRotateDir = laserRotateLinePointB - shootPoint.position;
            }
            randomIndex = Random.Range(0, laserPrefab.Length);
            laserPrefab[randomIndex].transform.right = laserRotateDir.normalized;
            var startColor = laserPrefab[randomIndex].transform.GetChild(0).GetComponent<ParticleSystem>().main.startColor;
            var main = laserChargeParticle.main;
            main.startColor = startColor;
            laserChargeParticle.gameObject.SetActive(true);
            laserChargeBallParticle = laserPrefab[randomIndex].transform.GetChild(0).gameObject;
            laserChargeBallParticle.transform.localScale = new Vector3(0f, 0f, 1f);
            laserChargeBallParticle.SetActive(true);
        }

        if (timer > laserDelay)
        {
            laserChargeParticle.gameObject.SetActive(false);
            laserChargeBallParticle.transform.localScale = new Vector3(laserChargeBallMaxSize, laserChargeBallMaxSize, 1f);
            laserPrefab[randomIndex].transform.GetChild(1).gameObject.SetActive(true);
        }
        else
        {
            if (laserChargeBallParticle.transform.localScale.magnitude < new Vector3(laserChargeBallMaxSize, laserChargeBallMaxSize, 1f).magnitude)
            {
                var addSize = laserChargeBallToMaxSpeed * Time.deltaTime;
                laserChargeBallParticle.transform.localScale += new Vector3(addSize, addSize, 0f);
            }
            else
                laserChargeBallParticle.transform.localScale = new Vector3(laserChargeBallMaxSize, laserChargeBallMaxSize, 1f);
            timer += Time.deltaTime;
            return;
        }

        if (laserRotateLinePointC.x > laserRotateLinePointB.x || laserRotateLinePointC.x < laserRotateLinePointA.x)
        {
            timer = 0;
            laserChargeBallParticle.SetActive(false);
            laserPrefab[randomIndex].transform.GetChild(1).gameObject.SetActive(false);
            isFirst = true;
            isFinishAttck = true;
            enemyAI.SetState(EnemyAI.State.ChaseTarget);
            return;
        }

        laserRotateDir = laserRotateLinePointC - shootPoint.position;
        laserPrefab[randomIndex].transform.right = laserRotateDir.normalized;

        if (islaserRotateFromPointA)
            laserRotateLinePointC += (laserRotateLinePointB - laserRotateLinePointA) * Time.deltaTime * laserRotateSpeed;
        else
            laserRotateLinePointC += (laserRotateLinePointA - laserRotateLinePointB) * Time.deltaTime * laserRotateSpeed;


    }

    private void MouthLaserBall()
    {
        if (laserBallNumber == laserBallNumberPerTime)
        {
            isFirst = true;
            isFinishAttck = true;
            enemyAI.SetState(EnemyAI.State.ChaseTarget);
            timer = 0;
            laserBallNumber = 0;
            return;
        }

        if (timer > laserBallSpawnRate)
        {
            SpawnLaserBall();
        }
        else
            timer += Time.deltaTime;
    }

    private void SpawnLaserBall()
    {
        if (isFirst)
        {
            isFirst = false;
            randomIndex = Random.Range(0, laserBallTag.Length);
            laserBall_Temp = ObjectPooler.Instance.SpawnFromPool(laserBallTag[randomIndex], shootPoint.position, null);
            laserBall_Temp.GetComponent<BasicBullet>().isLaserBallChargeUp = true;
            laserBall_Temp.transform.localScale = new Vector3(0f, 0f, 1f);
            Debug.Log("<color=red>laserBall</color>", laserBall_Temp);
            laserBallRbody_Temp = laserBall_Temp.GetComponent<Rigidbody2D>();
            var particle = laserBall_Temp.GetComponentInChildren<ParticleSystem>();
            var startColor = particle.main.startColor;
            var main = laserBallChargeParicle.main;
            main.startColor = startColor;
            laserBallChargeParicle.gameObject.SetActive(true);
        }

        if (laserBall_Temp.transform.localScale.magnitude < new Vector3(laserBallMaxSize, laserBallMaxSize, 1).magnitude)
        {
            laserBallChargeParicle.gameObject.SetActive(true);
            laserBall_Temp.transform.position = shootPoint.position;
            laserBall_Temp.transform.localScale += new Vector3(Time.deltaTime * laserBallToMaxSizeSpeed, Time.deltaTime * laserBallToMaxSizeSpeed, 0);
        }
        else
        {
            isFirst = true;
            timer = 0;
            laserBallNumber++;
            laserBallChargeParicle.gameObject.SetActive(false);
            laserBall_Temp.transform.localScale = new Vector3(laserBallMaxSize, laserBallMaxSize, 1);
            laserBallRbody_Temp.AddTorque(laserBallTorqueSpeed);
            laserBallRbody_Temp.velocity = (ship.position - shootPoint.position).normalized * laserBallSpeed;
            laserBall_Temp.GetComponent<BasicBullet>().isLaserBallChargeUp = false;
        }



    }

    public void Dead()
    {
        cameraFollowPoint.position = transform.position;

    }


}
