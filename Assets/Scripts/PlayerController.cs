using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.UI;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public int playerIndex;
    public Transform originalParent;
    public Transform respwanPoint;
    public Transform groudCheck;
    public float walkSpeed = 5f;
    public LayerMask shipGround;
    public float jumpPower = 0.03f;
    public float jumpTimeRange = 0.5f;
    [Range(0, 1)] public float groundCheckRadious = 0.02f;
    public Animator animator;
    public Animator teleportAnimator;
    public float fade = 1f;
    public float fadeSpeed = 1f;
    public Transform teleportOtherSide;
    public PauseMenuController pauseMenuController;
    public MultiplayerEventSystem multiplayerEventSystem;

    private float horizontalMove = 0f;
    private Rigidbody2D rbody2D;
    private Transform Controller_temp;
    private bool freezePosition = false;
    [SerializeField] private bool canJump = true;
    private PlayerInput playerInput;
    private InputAction m_Move;

    const float gravityScale = 9.8f;

    private void Awake()
    {
        originalParent = transform.parent;
        respwanPoint = originalParent;
        playerInput = GetComponent<PlayerInput>();
        //playerIndex = playerInput.user.index;
        // var playerUser = playerInput.user;
        // playerUser = GameDataManager.playerDatas[playerIndex].input.user;
        m_Move = playerInput.actions["Move"];
        rbody2D = GetComponent<Rigidbody2D>();
        rbody2D.gravityScale = 1f;
        pauseMenuController = GameObject.FindGameObjectWithTag("PauseMenu").GetComponent<PauseMenuController>();
        multiplayerEventSystem = GetComponentInChildren<MultiplayerEventSystem>();
    }


    void Update()
    {
        Collider2D[] collider2Ds = Physics2D.OverlapCircleAll(groudCheck.position, groundCheckRadious, shipGround);
        if (collider2Ds.Length > 0)
            canJump = true;
        else
            canJump = false;

        if (freezePosition)
        {
            animator.SetBool("isWalking", false);
            return;
        }

        Move(m_Move);

    }

    public void SetPlayerFreezed(int onOrOff)
    {
        bool set = onOrOff == 1 ? true : false;
        freezePosition = set;
        if (set)
        {
            rbody2D.velocity = Vector2.zero;
            rbody2D.constraints = RigidbodyConstraints2D.FreezePositionY | RigidbodyConstraints2D.FreezeRotation;
        }
        else
            rbody2D.constraints = RigidbodyConstraints2D.FreezeRotation;
    }

    public void Move(InputAction context)
    {
        horizontalMove = Mathf.Abs(context.ReadValue<Vector2>().y) > 0.25f ? 0 : context.ReadValue<Vector2>().x;

        if (horizontalMove == 0)
            animator.SetBool("isWalking", false);
        else
            animator.SetBool("isWalking", true);


        if (horizontalMove > 0)
        {
            horizontalMove = 1;
            transform.rotation = Quaternion.Euler(0f, 0f, 0f);
        }
        if (horizontalMove < 0)
        {
            horizontalMove = -1;
            transform.rotation = Quaternion.Euler(0f, 180f, 0f);
        }

        rbody2D.velocity = new Vector2(horizontalMove * walkSpeed, rbody2D.velocity.y);
    }

    public void Jump(InputAction.CallbackContext context)
    {
        if (Time.timeScale == 0f)
            return;
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

    public void Pause(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            bool isPaused = pauseMenuController.isPaused;
            if (isPaused)
                pauseMenuController.Resume(playerIndex + 1);
            else
                pauseMenuController.Pause(playerIndex + 1, multiplayerEventSystem);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {


        if (other.transform.CompareTag("ShooterController"))
        {
            if (other.transform.childCount > 1)
                return;
            Controller_temp = other.transform;
            EnterShooterController();
        }

        if (other.transform.CompareTag("ShipMoveController"))
        {
            if (other.transform.childCount > 1)
                return;
            Controller_temp = other.transform;
            EnterShipMoveController();
        }
    }


    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(groudCheck.position, groundCheckRadious);

    }

    private void ExitShooterController()
    {
        animator.SetBool("isOnControl", false);
        freezePosition = false;
        transform.parent = originalParent;
        rbody2D.gravityScale = 1;
        Controller_temp.GetComponent<ShooterController>().isOnControl = false;
        Controller_temp.GetComponent<ShooterController>().isShootting = false;
    }

    private void EnterShooterController()
    {
        animator.SetBool("isOnControl", true);
        Controller_temp.GetComponent<ShooterController>().isOnControl = true;
        freezePosition = true;
        transform.parent = Controller_temp;
        rbody2D.velocity = Vector2.zero;
        rbody2D.gravityScale = 0;
        transform.localPosition = new Vector3(0.08f, 0.35f, 0f);
        transform.localEulerAngles = Vector3.zero;
    }

    private void ExitShipMoveController()
    {
        animator.SetBool("isOnControl", false);
        freezePosition = false;
        transform.parent = originalParent;
        rbody2D.gravityScale = 1;
        Controller_temp.GetComponent<ShipController>().isOnControl = false;
    }

    private void EnterShipMoveController()
    {
        animator.SetBool("isOnControl", true);
        Controller_temp.GetComponent<ShipController>().isOnControl = true;
        freezePosition = true;
        transform.parent = Controller_temp;
        rbody2D.velocity = Vector2.zero;
        rbody2D.gravityScale = 0;
        transform.localPosition = new Vector3(0.08f, 0.35f, 0f);
        transform.localEulerAngles = Vector3.zero;
    }

    public void TeleportToOtherSide()
    {
        transform.position = teleportOtherSide.position;
    }

    public void PlayTeleportSound()
    {
        SoundManager.Instance.PlaySoundOneShot(SoundManager.SoundType.TeleportPipelineSound);
    }
}
