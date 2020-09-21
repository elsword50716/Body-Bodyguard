﻿using System.Collections;
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
    public bool isLaser = false;
    public float BulletSpeed = 1f;
    public float FireRate = 0.2f;


    public float gunMovingDegree = 1f;
    private float timer = 0f;
    private float gunRotationZ = 0f;
    private int counter = 0;
    private Vector2 moveInput = Vector2.zero;
    private Transform[] firePoint;

    private InputManager inputActions;


    void Start()
    {
        inputActions = new InputManager();
        var childCount = gunPivotPoint.childCount;
        firePoint = new Transform[childCount];

        for (int i = 0; i < firePoint.Length; i++)
        {
            firePoint[i] = gunPivotPoint.GetChild(i).GetChild(0);
        }
    }



    void Update()
    {

        if (isOnControl)
        {
            inputActions.OnControlParts.Move.performed += GunMove;
            inputActions.OnControlParts.Attack.performed += context => isShootting = true;
            inputActions.OnControlParts.Attack.canceled += context => isShootting = false;
            inputActions.OnControlParts.Enable();

        }
        else
        {
            inputActions.OnControlParts.Move.performed -= GunMove;
            inputActions.OnControlParts.Attack.performed -= context => isShootting = true;
            inputActions.OnControlParts.Attack.canceled -= context => isShootting = false;
            inputActions.OnControlParts.Disable();

        }

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


        if (isLaser)
        {
            if (isShootting)
            {
                bulletPrefab.SetActive(true);
            }
            else
            {
                bulletPrefab.SetActive(false);
            }
        }
        else
        {
            if (isShootting)
            {
                timer += Time.deltaTime;

                if (timer > FireRate)
                {
                    counter++;
                    timer = 0;

                    if (counter == firePoint.Length)
                        counter = 0;

                    FireBullet(counter % firePoint.Length);
                }

            }






        }
    }

    private void GunMove(InputAction.CallbackContext context)
    {
        if (!isOnControl)
        {
            moveInput = Vector2.zero;
            return;
        }

        moveInput = context.ReadValue<Vector2>();
    }

    private void FireBullet(int index)
    {
        var bullet = Instantiate(bulletPrefab, firePoint[index].position, Quaternion.identity);
        var bulletRbody2D = bullet.GetComponent<Rigidbody2D>();
        bulletRbody2D.velocity = firePoint[index].up * BulletSpeed;
        bullet.transform.up = firePoint[index].up;
    }

    static float ClampAngle(float angle, float min, float max)
    {
        if (angle > 180)
        {
            angle -= 360;
        }
        return Mathf.Clamp(angle, min, max);
    }
}
