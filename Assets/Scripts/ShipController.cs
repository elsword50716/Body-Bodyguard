using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class ShipController : MonoBehaviour
{
    public Rigidbody2D shipRbody;
    public bool isOnControl = false, addForce = true;
    public float shipSpeed = 100f;

    private InputManager inputActions;
    private Vector2 moveInput = Vector2.zero;


    void Start()
    {
        inputActions = new InputManager();
    }

    void Update()
    {
        if (isOnControl)
        {
            inputActions.OnControlParts.Move.performed += ShipMove;
            inputActions.OnControlParts.Enable();

        }
        else
        {
            inputActions.OnControlParts.Move.performed -= ShipMove;
            inputActions.OnControlParts.Disable();
        }

        if (addForce)
            shipRbody.velocity = moveInput * shipSpeed;
        else
            shipRbody.MovePosition(new Vector2(shipRbody.transform.position.x + moveInput.x, shipRbody.transform.position.y + moveInput.y));

    }

    private void ShipMove(InputAction.CallbackContext context)
    {
        if (!isOnControl)
        {
            moveInput = Vector2.zero;
            return;
        }

        moveInput = context.ReadValue<Vector2>();
    }


}
