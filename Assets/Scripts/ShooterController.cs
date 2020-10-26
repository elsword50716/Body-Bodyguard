using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class ShooterController : MonoBehaviour
{
    public Transform gunPivotPoint;
    public float gunMaxRotationRange = 60f;
    public bool isGunHorizontal;
    public bool isShootting = false;
    public bool isOnControl = false;
    public GameObject bulletPrefab;
    public float bulletDamage;
    public Transform bulletPool;
    public bool isLaser = false;
    public float laserChargeSpeedMulti = 1f;
    public float laserConsumeSpeedMulti = 1f;
    public Transform laserBatterySprite;
    public Animator laserBatteryThunderAnimator;
    public float BulletSpeed = 1f;
    public float FireRate = 0.2f;
    public Animator gunAnimator;


    public float gunMovingDegree = 1f;
    private float timer = 0f;
    private float gunRotationZ = 0f;
    private float laserCurrentAmount;
    private int counter = 0;
    private Vector2 moveInput = Vector2.zero;
    private Transform[] firePoint;
    private PlayerInput playerInput;
    private InputAction m_GunMove;
    private InputAction m_Fire;


    private void Start()
    {

        var childCount = gunPivotPoint.childCount;
        firePoint = new Transform[childCount];
        if(gunPivotPoint.GetComponentInChildren<Animator>() != null)
            gunAnimator = gunPivotPoint.GetComponentInChildren<Animator>();

        for (int i = 0; i < firePoint.Length; i++)
        {
            firePoint[i] = gunPivotPoint.GetChild(i).GetChild(0);
        }

        laserCurrentAmount = 100f;
    }



    private void Update()
    {
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
                    gunAnimator.SetBool("isShootting", true);
                    bulletPrefab.SetActive(true);
                    laserCurrentAmount -= laserConsumeSpeedMulti *Time.deltaTime;
                    laserBatteryThunderAnimator.SetBool("isCharging", false);
                }
                else
                {
                    gunAnimator.SetBool("isShootting", false);
                    bulletPrefab.SetActive(false);
                    laserCurrentAmount = 0;
                    laserBatteryThunderAnimator.SetBool("isCharging", false);
                }
            }
            else
            {
                gunAnimator.SetBool("isShootting", false);
                bulletPrefab.SetActive(false);
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
                if(gunAnimator !=null)
                    gunAnimator.SetFloat("ShootRate", 1/(FireRate * 10));
                if (Time.time > timer)
                {
                    counter++;

                    if (counter == firePoint.Length)
                        counter = 0;

                    FireBullet(counter % firePoint.Length);

                    timer = Time.time + FireRate;

                }

            }else{
                if(gunAnimator !=null)
                    gunAnimator.SetFloat("ShootRate", 0);
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

        gunRotationZ = gunPivotPoint.localEulerAngles.z;
        gunRotationZ = gunRotationZ > 180f ? gunRotationZ - 360f : gunRotationZ;

        var moveDegree = isGunHorizontal ? -moveInput.x * gunMovingDegree * Time.deltaTime : -moveInput.y * gunMovingDegree * Time.deltaTime;

        if (Mathf.Abs(gunRotationZ + moveDegree) < gunMaxRotationRange)
            gunPivotPoint.Rotate(new Vector3(0f, 0f, moveDegree));
        else{
            if(Mathf.Abs(gunRotationZ) != gunMaxRotationRange)
                gunPivotPoint.localEulerAngles = new Vector3(0f, 0f, gunRotationZ > 0 ? gunMaxRotationRange : -gunMaxRotationRange);
        }

    }

    private void FireBullet(int index)
    {
        var particle = firePoint[index].GetComponentInChildren<ParticleSystem>();
        var particleMain = particle.main;
        var color = bulletPrefab.GetComponentInChildren<SpriteRenderer>().color;
        ParticleSystem.MinMaxGradient grad = new ParticleSystem.MinMaxGradient(color, Color.white);
        particleMain.startColor = grad;
        particle.Play();
        var bullet = Instantiate(bulletPrefab, firePoint[index].position, Quaternion.identity, bulletPool);
        bullet.GetComponent<BasicBullet>().bulletData.targetTag = "Enemy";
        bullet.GetComponent<BasicBullet>().bulletData.damage = bulletDamage;
        var bulletRbody2D = bullet.GetComponent<Rigidbody2D>();
        bulletRbody2D.velocity = firePoint[index].up * BulletSpeed;
        bullet.transform.up = firePoint[index].up;
    }
}
