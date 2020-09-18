using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class ShooterController : MonoBehaviour
{
    public Transform gunPivotPoint;
    public bool isGunHorizontal;
    public bool isShootting = false;

    public GameObject bulletPrefab;
    public float BulletSpeed = 1f;
    public float FireRate = 0.2f;

    public float gunMovingDegree = 1f;
    private float timer = 0f;
    private int counter = 0;
    private Vector2 moveInput = Vector2.zero;
    private Transform[] firePoint;


    void Start()
    {
        var childCount = gunPivotPoint.childCount;
        firePoint = new Transform[childCount];
        
        for (int i = 0; i < firePoint.Length; i++)
        {
            firePoint[i] = gunPivotPoint.GetChild(i).GetChild(0);
        }
    }



    void Update()
    {

        if (Mathf.Abs(gunPivotPoint.rotation.z) < 90f)
        {
            if (isGunHorizontal)
            {
                gunPivotPoint.Rotate(new Vector3(0f, 0f, -moveInput.x * gunMovingDegree * Time.deltaTime));
            }
            else
            {
                gunPivotPoint.Rotate(new Vector3(0f, 0f, -moveInput.y * gunMovingDegree * Time.deltaTime));
            }

        }

        if (isShootting)
        {
            timer += Time.deltaTime;

            if(timer > FireRate){
                counter++;
                timer = 0;

                if(counter == firePoint.Length)
                counter = 0;

                FireBullet(counter % firePoint.Length);
            }

            


            
        }
    }

    public void GunMove(InputAction.CallbackContext context)
    {
        moveInput = context.ReadValue<Vector2>();
    }

    public void Attack(InputAction.CallbackContext context)
    {
        if (context.performed)
            isShootting = true;
        if (context.canceled)
            isShootting = false;
    }

    private void FireBullet(int index)
    {
        var bullet = Instantiate(bulletPrefab, firePoint[index].position, Quaternion.identity);
        var bulletRbody2D = bullet.GetComponent<Rigidbody2D>();
        bulletRbody2D.velocity = firePoint[index].up * BulletSpeed;
        bullet.transform.up = firePoint[index].up;
    }
}
