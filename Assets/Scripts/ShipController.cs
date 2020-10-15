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
    public float boostersRotateSpeed = 1f;
    public Transform[] boosters;

    private Vector2 moveInput = Vector2.zero;
    private PlayerInput playerInput;
    private InputAction m_ShipMove;
    private InputAction m_AddForce;

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
                m_AddForce = playerInput.actions["Attack"];
            }
            ShipMove(m_ShipMove);
            addForce = m_AddForce.ReadValue<float>() == 1 ? true : false;
        }
        else
        {
            playerInput = null;
            m_ShipMove = null;
            m_AddForce = null;

            for (int i = 0; i < 4; i++)
            {
                boosters[i].GetChild(0).gameObject.SetActive(false);
            }
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

        float inputAngle = Mathf.Round(Mathf.Acos(Vector2.Dot(Vector2.up, moveInput.normalized)));

        Debug.Log("angle : " + inputAngle + "//" + inputAngle * Mathf.Rad2Deg);

        if (moveInput == Vector2.zero)
        {
            for (int i = 0; i < 4; i++)
            {
                boosters[i].GetChild(0).gameObject.SetActive(false);
            }
            return;
        }

        var x = moveInput.x;
        var y = moveInput.y;

        if (x < 0)
        {
            if (y < 0)
            {
                boosters[0].GetChild(0).gameObject.SetActive(false);
                boosters[2].GetChild(0).gameObject.SetActive(false);
                boosters[3].GetChild(0).gameObject.SetActive(false);

                var boosterAngle = boosters[1].eulerAngles.z > 180 ? boosters[1].eulerAngles.z - 360 : boosters[1].eulerAngles.z;
                boosterAngle = Mathf.Round(boosterAngle);
                if(boosterAngle > inputAngle + 0.5f){
                    boosters[1].Rotate(new Vector3(0f, 0f, -boostersRotateSpeed * Time.deltaTime));
                }else if(boosterAngle < inputAngle - 0.5f){
                    boosters[1].Rotate(new Vector3(0f, 0f, boostersRotateSpeed * Time.deltaTime));
                }
                //boosters[1].up = moveInput.normalized;
                boosters[1].GetChild(0).gameObject.SetActive(addForce);
                if (addForce)
                    shipRbody.velocity += (moveInput * shipSpeed * Time.deltaTime);
            }
            else
            {
                boosters[0].GetChild(0).gameObject.SetActive(false);
                boosters[1].GetChild(0).gameObject.SetActive(false);
                boosters[2].GetChild(0).gameObject.SetActive(false);

                boosters[3].GetChild(0).gameObject.SetActive(addForce);
                boosters[3].up = moveInput.normalized;
            }
        }
        else
        {
            if (y < 0)
            {
                boosters[1].GetChild(0).gameObject.SetActive(false);
                boosters[2].GetChild(0).gameObject.SetActive(false);
                boosters[3].GetChild(0).gameObject.SetActive(false);

                boosters[0].GetChild(0).gameObject.SetActive(addForce);
                boosters[0].up = moveInput.normalized;
            }
            else
            {
                boosters[0].GetChild(0).gameObject.SetActive(false);
                boosters[1].GetChild(0).gameObject.SetActive(false);
                boosters[3].GetChild(0).gameObject.SetActive(false);

                boosters[2].GetChild(0).gameObject.SetActive(addForce);
                boosters[2].up = moveInput.normalized;
            }
        }

        //shipRbody.velocity += (moveInput * shipSpeed * Time.deltaTime);
    }

    public Vector2 GetMoveInput()
    {
        return moveInput;
    }




}
