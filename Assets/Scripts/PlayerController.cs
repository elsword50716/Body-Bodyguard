using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float walkSpeed = 5f;
    public float jumpPower = 0.03f;

    private float horizontalMove = 0f;
    private Vector2 _moveAxis;
    private Rigidbody2D rbody2D;
    private Transform originalParent;
    private Transform shooterController_temp;
    private bool canGoUp = false;
    private bool onSeat = false;


    private void Awake()
    {
        rbody2D = GetComponent<Rigidbody2D>();
        originalParent = transform.parent;
    }


    void Update()
    {
        if(onSeat)
            return;

        if (canGoUp)
            transform.position += new Vector3(_moveAxis.x, _moveAxis.y, 0f) * walkSpeed * Time.deltaTime;
        else
            transform.position += new Vector3(_moveAxis.x, 0f, 0f) * walkSpeed * Time.deltaTime;

    }



    public void Move(InputAction.CallbackContext context)
    {
        if(onSeat)
            return;

        horizontalMove = Mathf.Abs(context.ReadValue<Vector2>().x);
        _moveAxis = context.ReadValue<Vector2>();
        Debug.Log(_moveAxis);

        if (onSeat)
            return;

        if (context.ReadValue<Vector2>().x > 0)
            transform.rotation = Quaternion.Euler(0f, 0f, 0f);
        if (context.ReadValue<Vector2>().x < 0)
            transform.rotation = Quaternion.Euler(0f, 180f, 0f);



    }

    public void Jump(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            rbody2D.AddForce(new Vector2(0f, jumpPower));
            Debug.Log("ship add force");
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
        if (transform.parent.CompareTag("Seats"))
        {
            ExitSeat();
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.transform.parent.CompareTag("Stairs"))
        {
            rbody2D.gravityScale = 0;
            canGoUp = true;
        }

        if (other.transform.CompareTag("Seats"))
        {
            shooterController_temp = other.transform.parent;
            shooterController_temp.GetComponent<ShooterController>().enabled = true;
            shooterController_temp.GetComponent<PlayerInput>().enabled = true;
            onSeat = true;
            transform.parent = other.transform;
            transform.localPosition = Vector3.zero;
            transform.localEulerAngles = Vector3.zero;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.transform.parent.CompareTag("Stairs"))
        {
            rbody2D.gravityScale = 1;
            canGoUp = false;
        }
    }

    private void ExitSeat()
    {
        onSeat = false;
        transform.parent = originalParent;
        _moveAxis = Vector2.zero;
        shooterController_temp.GetComponent<ShooterController>().enabled = false;
        shooterController_temp.GetComponent<ShooterController>().isShootting = false;
        shooterController_temp.GetComponent<PlayerInput>().enabled = false;
    }

}
