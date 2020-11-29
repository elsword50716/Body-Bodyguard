using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class ShipController : MonoBehaviour
{
    public bool isOnControl = false;
    public bool isMapUnlocked;
    public bool addForce = false;
    public Rigidbody2D shipRbody;
    public Transform ship;
    public Animator mapAnimator;
    public float boostersAvoidShacking = 1.5f;
    public BoosterData boosterData;

    private Vector2 moveInput = Vector2.zero;
    private PlayerInput playerInput;
    private InputAction m_ShipMove;
    private InputAction m_AddForce;
    private InputAction m_OpenMap;
    private bool isMapOpen = false;

    private void Awake()
    {
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
                m_AddForce = playerInput.actions["Attack"];
                m_OpenMap = playerInput.actions["Interactions"];
            }
            addForce = m_AddForce.ReadValue<float>() == 1 ? true : false;

            if (m_OpenMap.triggered && isMapUnlocked)
            {
                isMapOpen = mapAnimator.GetBool("isOpen");
                mapAnimator.SetBool("isOpen", isMapOpen ? false : true);
            }


            ShipMove(m_ShipMove);
        }
        else
        {
            playerInput = null;
            m_ShipMove = null;
            m_AddForce = null;

            if (mapAnimator.GetBool("isOpen"))
            {
                mapAnimator.SetBool("isOpen", false);
            }

            for (int i = 0; i < 4; i++)
            {
                boosterData.boosters[i].GetChild(0).GetComponent<ParticleSystem>().Stop();
            }
        }




    }

    public void ShipMove(InputAction context)
    {
        moveInput = context.ReadValue<Vector2>();

        if (moveInput == Vector2.zero)
        {
            SoundManager.Instance.StopPlaySound(SoundManager.SoundType.booster);
            for (int i = 0; i < 4; i++)
            {
                boosterData.boosters[i].GetChild(0).GetComponent<ParticleSystem>().Stop();
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
        for (int i = 0; i < boosterData.boosters.Length; i++)
        {
            if (i == theUsingOne)
                continue;

            boosterData.boosters[i].GetChild(0).GetComponent<ParticleSystem>().Stop();
        }

        float inputAngleDelta = Mathf.Acos(Vector2.Dot(boosterData.boosters[theUsingOne].up, moveInput.normalized)) * Mathf.Rad2Deg;

        Debug.Log($"{theUsingOne} inputAngleDelta : " + inputAngleDelta);

        Vector2 boosterUp_temp = boosterData.boosters[theUsingOne].up;

        if (inputAngleDelta > boostersAvoidShacking)
        {
            var degree = boosterData.boostersRotateSpeed * Time.deltaTime;
            var RotatedX_N = (boosterUp_temp.x * Mathf.Cos(-degree * Mathf.Deg2Rad) - boosterUp_temp.y * Mathf.Sin(-degree * Mathf.Deg2Rad));
            var RotatedY_N = (boosterUp_temp.x * Mathf.Sin(-degree * Mathf.Deg2Rad) + boosterUp_temp.y * Mathf.Cos(-degree * Mathf.Deg2Rad));
            boosterUp_temp = new Vector2(RotatedX_N, RotatedY_N);

            var inputAngleDelta_temp = Mathf.Acos(Vector2.Dot(boosterUp_temp, moveInput.normalized)) * Mathf.Rad2Deg;

            if (inputAngleDelta_temp > inputAngleDelta)
            {
                boosterData.boosters[theUsingOne].Rotate(new Vector3(0f, 0f, degree));
                Debug.Log("加角度");
            }
            else
            {
                boosterData.boosters[theUsingOne].Rotate(new Vector3(0f, 0f, -degree));
                Debug.Log("減角度");
            }

        }
        else
        {
            boosterData.boosters[theUsingOne].up = moveInput.normalized;
            if (theUsingOne == 0)
                boosterData.boosters[theUsingOne].eulerAngles = new Vector3(0f, 0f, boosterData.boosters[theUsingOne].eulerAngles.z);
            Debug.Log("貼其");
        }

        if (addForce)
        {
            SoundManager.Instance.PlaySoundLoop(SoundManager.SoundType.booster, false);
            boosterData.boosters[theUsingOne].GetChild(0).GetComponent<ParticleSystem>().Play();
            shipRbody.velocity += ((Vector2)boosterData.boosters[theUsingOne].up * boosterData.shipSpeed * Time.deltaTime);
        }
        else
        {
            SoundManager.Instance.StopPlaySound(SoundManager.SoundType.booster);
            boosterData.boosters[theUsingOne].GetChild(0).GetComponent<ParticleSystem>().Stop();
        }
    }




}
