﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class ShipController : MonoBehaviour
{
    //public Rigidbody2D shipRbody;
    public Transform ship;
    public bool isOnControl = false, addForce = true;
    public float shipSpeed = 100f;

    private InputManager inputActions;
    private Vector2 moveInput = Vector2.zero;


    private void Start()
    {
        inputActions = new InputManager();
    }

    private void Update()
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

        ship.Translate(new Vector3(moveInput.x * shipSpeed * Time.deltaTime, moveInput.y * shipSpeed * Time.deltaTime, 0f));

        /*if (addForce)
            shipRbody.velocity = moveInput * shipSpeed;
        else
            shipRbody.MovePosition(new Vector2(shipRbody.transform.position.x + moveInput.x, shipRbody.transform.position.y + moveInput.y));*/

    }

    public void ShipMove(InputAction.CallbackContext context)
    {
        if (!isOnControl)
        {
            moveInput = Vector2.zero;
            return;
        }

        moveInput = context.ReadValue<Vector2>();
    }


}
