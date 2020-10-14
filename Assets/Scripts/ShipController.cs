using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class ShipController : MonoBehaviour
{
    public Rigidbody2D shipRbody;
    public Transform ship;
    public ShipData shipData;
    public bool isOnControl = false, addForce = false;
    public float shipSpeed = 100f;
    public Transform[] boosters;

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
        shipData = ship.GetComponent<Ship>().shipData;

        if (shipSpeed != shipData.NormalSpeed)
        {
            shipSpeed = shipData.NormalSpeed;
        }

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




    }

    public void ShipMove(InputAction context)
    {
        if (!isOnControl)
        {
            moveInput = Vector2.zero;
            return;
        }

        moveInput = context.ReadValue<Vector2>();

        float angle = Mathf.Acos(Vector2.Dot(Vector2.up, moveInput.normalized));

        Debug.Log("angle : " + angle + "//" + angle * Mathf.Rad2Deg);

        // if (addForce == false)
        // {
        //     for (int i = 0; i < 4; i++)
        //     {
        //         boosters[i].GetChild(0).gameObject.SetActive(false);
        //     }
        //     return;
        // }

        // var x = moveInput.x;
        // var y = moveInput.y;

        // float angle;

        // if (x < 0)
        // {
        //     if (y < 0)
        //     {

        //         boosters[1].GetChild(0).gameObject.SetActive(true);
        //         boosters[1].up = moveInput.normalized;

        //     }
        //     else
        //     {
        //         boosters[3].GetChild(0).gameObject.SetActive(true);
        //         boosters[3].up = moveInput.normalized;
        //     }
        // }
        // else
        // {
        //     if (y < 0)
        //     {
        //         boosters[0].GetChild(0).gameObject.SetActive(true);
        //         boosters[0].up = moveInput.normalized;

        //     }
        //     else
        //     {

        //         boosters[2].GetChild(0).gameObject.SetActive(true);
        //         boosters[2].up = moveInput.normalized;

        //     }
        // }

        //shipRbody.velocity += (moveInput * shipSpeed * Time.deltaTime);
    }

    public Vector2 GetMoveInput()
    {
        return moveInput;
    }


}
