using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class ShipController : MonoBehaviour
{
    public Transform shipLight;
    public Rigidbody2D shipRbody;
    public Transform ship;
    public ShipData shipData;
    public bool isOnControl = false, addForce = true;
    public float shipSpeed = 100f;


    private Vector2 moveInput = Vector2.zero;
    private PlayerInput playerInput;
    private InputAction m_ShipMove;

    private void Awake()
    {
        shipData = ship.GetComponent<Ship>().shipData;
        shipSpeed = shipData.NormalSpeed;
        shipRbody = ship.GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        if (isOnControl)
        {
            if (playerInput == null)
            {
                playerInput = GetComponentInChildren<PlayerInput>();
                m_ShipMove = playerInput.actions["Move"];
            }
            ShipMove(m_ShipMove);
        }
        else
        {
            playerInput = null;
            m_ShipMove = null;
        }

        shipData = ship.GetComponent<Ship>().shipData;

        if (shipSpeed != shipData.NormalSpeed)
        {
            shipSpeed = shipData.NormalSpeed;
        }

        /*if (addForce)
            shipRbody.velocity = moveInput * shipSpeed;
        else
            shipRbody.MovePosition(new Vector2(shipRbody.transform.position.x + moveInput.x, shipRbody.transform.position.y + moveInput.y));*/

    }

    public void ShipMove(InputAction context)
    {
        if (!isOnControl)
        {
            moveInput = Vector2.zero;
            return;
        }

        moveInput = context.ReadValue<Vector2>();
        //ship.Translate(new Vector3(moveInput.x * shipSpeed * Time.deltaTime, moveInput.y * shipSpeed * Time.deltaTime, 0f));
        //shipRbody.AddForce(moveInput * shipSpeed * shipRbody.mass);
        shipRbody.velocity += (moveInput * shipSpeed * Time.deltaTime);
        shipLight.up = moveInput.normalized;
    }


}
