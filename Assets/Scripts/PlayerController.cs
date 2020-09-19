using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float walkSpeed = 5f;
    public float jumpPower = 0.03f;
    private float horizontalMove = 0f;
    private Rigidbody2D rbody2D;
    private Transform originalParent;
    private Transform shooterController_temp;

    private bool freezePosition = false;
    

    private PlayerInput playerInput;

    private void Awake()
    {
        playerInput = GetComponent<PlayerInput>();
        rbody2D = GetComponent<Rigidbody2D>();
        originalParent = transform.parent;
    }


    void Update()
    {
        if (freezePosition)
            return;

        /*if (canGoUp)
            transform.position += new Vector3(_moveAxis.x, _moveAxis.y, 0f) * walkSpeed * Time.deltaTime;
        else
            transform.position += new Vector3(_moveAxis.x, 0f, 0f) * walkSpeed * Time.deltaTime;*/

        if (horizontalMove > 0)
            transform.rotation = Quaternion.Euler(0f, 0f, 0f);
        if (horizontalMove < 0)
            transform.rotation = Quaternion.Euler(0f, 180f, 0f);

        rbody2D.velocity = new Vector2(horizontalMove * walkSpeed, rbody2D.velocity.y);
        
        
    }



    public void Move(InputAction.CallbackContext context)
    {
        horizontalMove = context.ReadValue<Vector2>().x;
        Debug.Log(horizontalMove);
    }

    public void Jump(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            rbody2D.AddForce(new Vector2(0f, jumpPower));
            if (transform.parent.CompareTag("ShooterController"))
            {
                ExitShooterController();
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
    }

    private void OnTriggerEnter2D(Collider2D other)
    {


        if (other.transform.CompareTag("ShooterController"))
        {
            EnterShooterController(other.transform);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {

    }

    private void ExitShooterController()
    {
        freezePosition = false;
        transform.parent = originalParent;
        shooterController_temp.GetComponent<PlayerInput>().enabled = false;
        shooterController_temp.GetComponent<ShooterController>().enabled = false;
        shooterController_temp.GetComponent<ShooterController>().isShootting = false;
    }

    private void EnterShooterController(Transform controller)
    {
        shooterController_temp = controller.transform;
        shooterController_temp.GetComponent<ShooterController>().enabled = true;
        shooterController_temp.GetComponent<PlayerInput>().enabled = true;
        freezePosition = true;
        transform.parent = shooterController_temp;
        rbody2D.velocity = Vector2.zero;
        transform.localPosition = Vector3.zero;
        transform.localEulerAngles = Vector3.zero;
    }

}
