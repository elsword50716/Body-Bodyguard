﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class ShooterController : MonoBehaviour
{
    public bool isShootting = false;
    public bool isOnControl = false;
    public ShooterData shooterData;
    public bool isLaser = false;
    public GameObject laserPrefab;
    public float laserChargeSpeedMulti = 1f;
    public float laserConsumeSpeedMulti = 1f;
    public Transform laserBatterySprite;
    public Animator laserBatteryThunderAnimator;

    private float timer = 0f;
    private float gunRotationZ = 0f;
    private float laserCurrentAmount;
    private int counter = 0;
    private Vector2 moveInput = Vector2.zero;
    private Transform[] firePoint;
    private PlayerInput playerInput;
    private InputAction m_GunMove;
    private InputAction m_Fire;
    private ObjectPooler objectPooler;

    private void Start()
    {
        if(!isLaser)
            UpdateFirePoints();

        laserCurrentAmount = 100f;
        objectPooler = ObjectPooler.Instance;
    }



    private void Update()
    {
        if (Time.timeScale == 0f)
            return;

        if (isOnControl)
        {
            if (playerInput == null)
            {
                playerInput = GetComponentInChildren<PlayerInput>();
                m_GunMove = playerInput.actions["Move"];
                m_Fire = playerInput.actions["Attack"];
            }
            GunMove(m_GunMove);
            isShootting = m_Fire.ReadValue<float>() == 1 ? true : false;
        }
        else
        {
            playerInput = null;
            m_Fire = null;
        }


        if (isLaser)
        {
            if (laserBatterySprite != null)
                laserBatterySprite.localScale = new Vector3(laserCurrentAmount / 100f, 1f, 1f);
            if (isShootting)
            {
                if (laserCurrentAmount > 0)
                {
                    SoundManager.Instance.PlaySound(SoundManager.SoundType.laserFire);
                    shooterData.gunAnimator.SetBool("isShootting", true);
                    laserPrefab.SetActive(true);
                    laserCurrentAmount -= laserConsumeSpeedMulti * Time.deltaTime;
                    laserBatteryThunderAnimator.SetBool("isCharging", false);
                }
                else
                {
                    SoundManager.Instance.StopPlaySound(SoundManager.SoundType.laserFire);
                    shooterData.gunAnimator.SetBool("isShootting", false);
                    laserPrefab.SetActive(false);
                    laserCurrentAmount = 0;
                    laserBatteryThunderAnimator.SetBool("isCharging", false);
                }
            }
            else
            {
                SoundManager.Instance.StopPlaySound(SoundManager.SoundType.laserFire);
                shooterData.gunAnimator.SetBool("isShootting", false);
                laserPrefab.SetActive(false);
                if (laserCurrentAmount < 100f)
                {
                    laserCurrentAmount += laserChargeSpeedMulti * Time.deltaTime;
                    laserBatteryThunderAnimator.SetBool("isCharging", true);
                }
                else
                {
                    laserCurrentAmount = 100f;
                    laserBatteryThunderAnimator.SetBool("isCharging", false);
                }
            }
        }
        else
        {
            if (isShootting)
            {
                if (shooterData.gunAnimator != null)
                    shooterData.gunAnimator.SetFloat("ShootRate", 1 / (shooterData.fireRate * 10));
                if (Time.time > timer)
                {
                    counter++;

                    if (counter == firePoint.Length)
                        counter = 0;

                    FireBullet(counter % firePoint.Length);

                    timer = Time.time + shooterData.fireRate;

                }

            }
            else
            {
                if (shooterData.gunAnimator != null)
                    shooterData.gunAnimator.SetFloat("ShootRate", 0);
            }






        }
    }




    public void GunMove(InputAction context)
    {
        if (!isOnControl)
        {
            moveInput = Vector2.zero;
            return;
        }

        moveInput = context.ReadValue<Vector2>();

        gunRotationZ = shooterData.gunPivotPoint.localEulerAngles.z;
        gunRotationZ = gunRotationZ > 180f ? gunRotationZ - 360f : gunRotationZ;

        var moveDegree = shooterData.isGunHorizontal ? -moveInput.x * shooterData.gunMovingDegreePerSec * Time.deltaTime : -moveInput.y * shooterData.gunMovingDegreePerSec * Time.deltaTime;

        if (Mathf.Abs(gunRotationZ + moveDegree) < shooterData.gunMaxRotationRange)
            shooterData.gunPivotPoint.Rotate(new Vector3(0f, 0f, moveDegree));
        else
        {
            if (Mathf.Abs(gunRotationZ) != shooterData.gunMaxRotationRange)
                shooterData.gunPivotPoint.localEulerAngles = new Vector3(0f, 0f, gunRotationZ > 0 ? shooterData.gunMaxRotationRange : -shooterData.gunMaxRotationRange);
        }

    }

    private void FireBullet(int index)
    {
        var bullet = objectPooler.SpawnFromPool(shooterData.bulletTag, firePoint[index].position, shooterData.bulletPool);
        var particle = firePoint[index].GetComponentInChildren<ParticleSystem>();
        var particleMain = particle.main;
        var color = bullet.GetComponentInChildren<SpriteRenderer>().color;
        ParticleSystem.MinMaxGradient grad = new ParticleSystem.MinMaxGradient(color, Color.white);
        particleMain.startColor = grad;
        particle.Play();
        SoundManager.Instance.PlaySoundOneShot(SoundManager.SoundType.shoooterFire);
        bullet.GetComponent<BasicBullet>().bulletData.targetTag = "Enemy";
        bullet.GetComponent<BasicBullet>().bulletData.damage = shooterData.bulletDamage;
        var bulletRbody2D = bullet.GetComponent<Rigidbody2D>();
        bulletRbody2D.velocity = firePoint[index].up * shooterData.bulletSpeed;
        bullet.transform.up = firePoint[index].up;
    }

    public void UpdateFirePoints()
    {
        var childCount = shooterData.gunPivotPoint.childCount;
        firePoint = new Transform[childCount];

        for (int i = 0; i < firePoint.Length; i++)
        {
            firePoint[i] = shooterData.gunPivotPoint.GetChild(i).GetChild(0);
        }
    }
}
