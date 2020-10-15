using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class ShooterController : MonoBehaviour
{
    public Transform gunPivotPoint;
    public bool isGunHorizontal;
    public bool isShootting = false;
    public bool isOnControl = false;
    public GameObject bulletPrefab;
    public float bulletDamage;
    public Transform bulletPool;
    public bool isLaser = false;
    public float BulletSpeed = 1f;
    public float FireRate = 0.2f;
    public Animator gunAnimator;


    public float gunMovingDegree = 1f;
    private float timer = 0f;
    private float gunRotationZ = 0f;
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

        for (int i = 0; i < firePoint.Length; i++)
        {
            firePoint[i] = gunPivotPoint.GetChild(i).GetChild(0);
        }
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

                //m_Fire.performed += context => isShootting = true;
                //m_Fire.canceled += context => isShootting = false;
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
            if (isShootting)
            {
                gunAnimator.SetBool("isShootting", true);
                //bulletPrefab.SetActive(true);
            }
            else
            {
                gunAnimator.SetBool("isShootting", false);
                bulletPrefab.SetActive(false);
            }
        }
        else
        {
            if (isShootting)
            {
                if (Time.time > timer)
                {
                    counter++;

                    if (counter == firePoint.Length)
                        counter = 0;

                    FireBullet(counter % firePoint.Length);

                    timer = Time.time + FireRate;

                }

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
        gunRotationZ = ClampAngle(gunPivotPoint.localEulerAngles.z, -60, 60);

        gunPivotPoint.localEulerAngles = new Vector3(0f, 0f, gunRotationZ);

        if (isGunHorizontal)
        {
            gunPivotPoint.Rotate(new Vector3(0f, 0f, -moveInput.x * gunMovingDegree * Time.deltaTime));
        }
        else
        {
            gunPivotPoint.Rotate(new Vector3(0f, 0f, -moveInput.y * gunMovingDegree * Time.deltaTime));
        }
    }

    private void FireBullet(int index)
    {
        var bullet = Instantiate(bulletPrefab, firePoint[index].position, Quaternion.identity, bulletPool);
        bullet.GetComponent<BasicBullet>().bulletData.targetTag = "Enemy";
        bullet.GetComponent<BasicBullet>().bulletData.damage = bulletDamage;
        var bulletRbody2D = bullet.GetComponent<Rigidbody2D>();
        bulletRbody2D.velocity = firePoint[index].up * BulletSpeed;
        bullet.transform.up = firePoint[index].up;
    }



    public static float ClampAngle(float angle, float min, float max)
    {
        if (angle > 180)
        {
            angle -= 360;
        }
        return Mathf.Clamp(angle, min, max);
    }
}
