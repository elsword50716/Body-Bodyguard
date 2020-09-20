using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public Transform groudCheck;
    public float walkSpeed = 5f;
    public LayerMask shipGround;
    public float jumpPower = 0.03f;
    public float jumpTimeRange = 0.5f;
    [Range(0, 1)] public float groundCheckRadious = 0.02f;


    private float horizontalMove = 0f;
    private Rigidbody2D rbody2D;
    private Transform originalParent;
    private Transform Controller_temp;
    private bool freezePosition = false;
    [SerializeField] private bool canJump = true;
    private PlayerInput playerInput;

    private void Awake()
    {
        playerInput = GetComponent<PlayerInput>();
        rbody2D = GetComponent<Rigidbody2D>();
        originalParent = transform.parent;
    }


    void Update()
    {

        Collider2D[] collider2Ds = Physics2D.OverlapCircleAll(groudCheck.position, groundCheckRadious, shipGround);
        if (collider2Ds.Length > 0)
            canJump = true;
        else
            canJump = false;


        if (freezePosition)
            return;

        if (horizontalMove > 0)
            transform.rotation = Quaternion.Euler(0f, 0f, 0f);
        if (horizontalMove < 0)
            transform.rotation = Quaternion.Euler(0f, 180f, 0f);

        rbody2D.velocity = new Vector2(horizontalMove * walkSpeed, rbody2D.velocity.y);


    }



    public void Move(InputAction.CallbackContext context)
    {
        horizontalMove = context.ReadValue<Vector2>().x;
        Debug.Log("WASD: " + context.ReadValue<Vector2>());
    }

    public void Jump(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            if (canJump)
            {
                rbody2D.AddForce(new Vector2(0f, jumpPower));

            }

            if (transform.parent.CompareTag("ShooterController"))
            {
                ExitShooterController();
            }

            if (transform.parent.CompareTag("ShipMoveController"))
            {
                ExitShipMoveController();
            }
        }


    }

    public void Interactions(InputAction.CallbackContext context)
    {

    }
    public void Back(InputAction.CallbackContext context)
    {
        if (!context.performed)
            return;
        Debug.Log("Back");

        if (transform.parent.CompareTag("ShooterController"))
        {
            ExitShooterController();
        }

        if (transform.parent.CompareTag("ShipMoveController"))
        {
            ExitShipMoveController();
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.transform.CompareTag("ShooterController"))
        {
            Controller_temp = other.transform;
            EnterShooterController();
        }

        if (other.transform.CompareTag("ShipMoveController"))
        {
            Controller_temp = other.transform;
            EnterShipMoveController();
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(groudCheck.position, groundCheckRadious);

    }

    private void ExitShooterController()
    {
        freezePosition = false;
        transform.parent = originalParent;
        Controller_temp.GetComponent<ShooterController>().enabled = false;
        Controller_temp.GetComponent<ShooterController>().isOnControl = false;
        Controller_temp.GetComponent<ShooterController>().isShootting = false;
    }

    private void EnterShooterController()
    {
        Controller_temp.GetComponent<ShooterController>().enabled = true;
        Controller_temp.GetComponent<ShooterController>().isOnControl = true;
        freezePosition = true;
        transform.parent = Controller_temp;
        rbody2D.velocity = Vector2.zero;
        transform.localPosition = Vector3.zero;
        transform.localEulerAngles = Vector3.zero;
    }

    private void ExitShipMoveController()
    {
        freezePosition = false;
        transform.parent = originalParent;
        Controller_temp.GetComponent<ShipController>().enabled = false;
        Controller_temp.GetComponent<ShipController>().isOnControl = false;
    }

    private void EnterShipMoveController()
    {
        Controller_temp.GetComponent<ShipController>().enabled = true;
        Controller_temp.GetComponent<ShipController>().isOnControl = true;
        freezePosition = true;
        transform.parent = Controller_temp;
        rbody2D.velocity = Vector2.zero;
        transform.localPosition = Vector3.zero;
        transform.localEulerAngles = Vector3.zero;
    }
}
