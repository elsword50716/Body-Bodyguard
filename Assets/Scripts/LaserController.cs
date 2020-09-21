using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class LaserController : MonoBehaviour
{
    
    public Transform gunPivotPoint;
    public bool isGunHorizontal;
    public bool isShootting = false;
    public bool isOnControl = false;
    public GameObject bulletPrefab;
    public float gunMovingDegree = 1f;


    private float gunRotationZ = 0f;
    private Vector2 moveInput = Vector2.zero;
    private InputManager inputActions;


    void Start()
    {
        inputActions = new InputManager();
        var childCount = gunPivotPoint.childCount;
        
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




        if (isShootting)
        {
            bulletPrefab.SetActive(true);
        }else{
            bulletPrefab.SetActive(false);
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

    static float ClampAngle(float angle, float min, float max)
    {
        if (angle > 180)
        {
            angle -= 360;
        }
        return Mathf.Clamp(angle, min, max);
    }
}
