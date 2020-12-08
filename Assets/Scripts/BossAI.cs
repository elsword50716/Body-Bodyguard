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
    public Slider bossHealthBar;
    public Animator animator;
    public string bulletTag;
    public string EggTag;
    public string[] DropingPoolTag;
    public string deadExplosionTag;
    private float rushDamper;
    private GameObject rushParticle;
    public float eggSpawnOnceNumber;

    private EnemyAI enemyAI;
    private BulletManager shipBulletManager;
    private EnemyData enemyData;
    private float shipDistance;
    private int attackIndex;
    private bool isFinishAttck;
    private Vector3 targetPosion;
    private bool isFirstRush;


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
    }

    private void Start()
    {

    }

    private void Update()
    {
        shipDistance = (ship.position - transform.position).sqrMagnitude;
        if (shipDistance < enemyData.detectShipRange * enemyData.detectShipRange)
        {
            EyeFollowShip();
            SetCameraPriority(true);
            SetCameraFollowPoint();
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
        Debug.Log("Boss Attck");
        if (isFinishAttck)
        {
            attackIndex = Random.Range(0, 4);
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
            targetPosion = ship.position;
        if ((targetPosion - transform.position).sqrMagnitude > rushDamper * rushDamper)
        {
            rushParticle.SetActive(true);
            List<Collider2D> collider2Ds = new List<Collider2D>();
            if (enemyAI.Rbody2D.GetContacts(enemyAI.shipLayer, collider2Ds) > 0)
            {
                //damage the ship
                enemyAI.DamageShip(collider2Ds[0]);
                isFinishAttck = true;
                rushParticle.SetActive(false);
                enemyAI.SetState(EnemyAI.State.ChaseTarget);
                return;
            }
            enemyAI.MoveTo(targetPosion, true, true);
            Debug.DrawLine(transform.position, targetPosion, Color.blue);
        }
        else
        {
            rushParticle.SetActive(false);
            isFinishAttck = true;
            enemyAI.SetState(EnemyAI.State.ChaseTarget);
        }
    }
    private void ShootEgg()
    {

    }

    private void MouthLaser()
    {

    }

    private void MouthLaserBall()
    {

    }

    public void Dead()
    {

    }


}
