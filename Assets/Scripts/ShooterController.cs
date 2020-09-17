using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class ShooterController : MonoBehaviour
{
    public Transform shootter;
    public bool isGunHorizontal;
    private InputManager inputActions;
    private Vector2 moveInput = Vector2.zero;

    private void Awake()
    {
        inputActions = new InputManager();
    }

    private void OnEnable()
    {
        inputActions.OnControlParts.Move.performed += GunMove;
        inputActions.OnControlParts.Move.Enable();

    }

    private void OnDisable()
    {
        inputActions.OnControlParts.Move.performed -= GunMove;
        inputActions.OnControlParts.Move.Disable();
    }

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    private void GunMove(InputAction.CallbackContext context)
    {
        moveInput = context.ReadValue<Vector2>();
    }
}
