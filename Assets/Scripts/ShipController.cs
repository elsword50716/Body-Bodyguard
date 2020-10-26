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
    public float boostersAvoidShacking = 1.5f;
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
            addForce = m_AddForce.ReadValue<float>() == 1 ? true : false;
            ShipMove(m_ShipMove);
        }
        else
        {
            playerInput = null;
            m_ShipMove = null;
            m_AddForce = null;

            for (int i = 0; i < 4; i++)
            {
                boosters[i].GetChild(0).GetComponent<ParticleSystem>().Stop();
            }
        }




    }

    public void ShipMove(InputAction context)
    {
        moveInput = context.ReadValue<Vector2>();

        if (moveInput == Vector2.zero)
        {
            for (int i = 0; i < 4; i++)
            {
                boosters[i].GetChild(0).GetComponent<ParticleSystem>().Stop();
            }
            return;
        }

        var x = moveInput.x;
        var y = moveInput.y;

        if (x < 0)
        {
            if (y < 0)
            {
                BoostersControl(1);
            }
            else
            {
                BoostersControl(3);
            }
        }
        else
        {
            if (y < 0)
            {
                BoostersControl(0);
            }
            else
            {
                BoostersControl(2);
            }
        }

        //shipRbody.velocity += (moveInput * shipSpeed * Time.deltaTime);
    }

    public Vector2 GetMoveInput()
    {
        return moveInput;
    }

    private void BoostersControl(int theUsingOne)
    {
        for (int i = 0; i < boosters.Length; i++)
        {
            if (i == theUsingOne)
                continue;

            boosters[i].GetChild(0).GetComponent<ParticleSystem>().Stop();
        }

        float inputAngleDelta = Mathf.Acos(Vector2.Dot(boosters[theUsingOne].up, moveInput.normalized)) * Mathf.Rad2Deg;

        Debug.Log($"{theUsingOne} inputAngleDelta : " + inputAngleDelta);

        Vector2 boosterUp_temp = boosters[theUsingOne].up;

        if (inputAngleDelta > boostersAvoidShacking)
        {
            var degree = boostersRotateSpeed * Time.deltaTime;
            var RotatedX_N = (boosterUp_temp.x * Mathf.Cos(-degree * Mathf.Deg2Rad) - boosterUp_temp.y * Mathf.Sin(-degree * Mathf.Deg2Rad));
            var RotatedY_N = (boosterUp_temp.x * Mathf.Sin(-degree * Mathf.Deg2Rad) + boosterUp_temp.y * Mathf.Cos(-degree * Mathf.Deg2Rad));
            boosterUp_temp = new Vector2(RotatedX_N, RotatedY_N);

            var inputAngleDelta_temp = Mathf.Acos(Vector2.Dot(boosterUp_temp, moveInput.normalized)) * Mathf.Rad2Deg;

            if (inputAngleDelta_temp > inputAngleDelta)
            {
                boosters[theUsingOne].Rotate(new Vector3(0f, 0f, degree));
                Debug.Log("加角度");
            }
            else
            {
                boosters[theUsingOne].Rotate(new Vector3(0f, 0f, -degree));
                Debug.Log("減角度");
            }

        }
        else
        {
            boosters[theUsingOne].up = moveInput.normalized;
            if (theUsingOne == 0)
                boosters[theUsingOne].eulerAngles = new Vector3(0f, 0f, boosters[theUsingOne].eulerAngles.z);
            Debug.Log("貼其");
        }

        if (addForce)
        {
            boosters[theUsingOne].GetChild(0).GetComponent<ParticleSystem>().Play();
            shipRbody.velocity += ((Vector2)boosters[theUsingOne].up * shipSpeed * Time.deltaTime);
        }
        else
            boosters[theUsingOne].GetChild(0).GetComponent<ParticleSystem>().Stop();
    }




}
