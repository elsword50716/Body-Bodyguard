using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class TeleportPipeline : MonoBehaviour
{
    public ContactFilter2D playerLayer;
    public GameObject buttonHint;
    public Transform theOtherSide;
    public SpriteRenderer Light;
    public float transportingSpeed = 1f;
    public bool isPortalOpen = false;
    public bool isSomeoneOnIt = false;
    public bool isTeleporting = false;
    public bool isOnOtherSide = false;

    private Transform player;
    private Transform player_temp;
    private PlayerInput playerInput;
    private Material playerMaterial;
    private InputAction m_Interaction;
    private float fade;
    private Animator playerAnimator;
    private Vector2 overlapPosition;
    private Vector2 overlapSize;
    private List<Collider2D> overLapResult;


    void Start()
    {
        isPortalOpen = false;
        isSomeoneOnIt = false;
        isTeleporting = false;
        Light = transform.GetChild(2).GetComponent<SpriteRenderer>();
        var boxCollider2D = GetComponent<BoxCollider2D>();
        overlapPosition = boxCollider2D.offset;
        overlapSize = boxCollider2D.size;
        overLapResult = new List<Collider2D>();
        Physics2D.queriesHitTriggers = true;
    }


    void Update()
    {

        if (Physics2D.OverlapBox(overlapPosition, overlapSize, 0f, playerLayer, overLapResult) > 0)
        {
            foreach (var player in overLapResult)
            {
                Debug.Log("player on it!!", player.gameObject);
            }
        }
        if (player == null)
        {
            if (Light != null)
                Light.color = Color.green;

            return;
        }

        if (theOtherSide.GetComponent<TeleportPipeline>().player != null)
        {
            if (Light != null)
                Light.color = Color.red;
            return;
        }

        if (Light != null)
            Light.color = Color.green;

        if (m_Interaction != null && playerAnimator != null && m_Interaction.triggered)
        {
            // isTeleporting = true;
            playerAnimator.SetTrigger("isTeleport");
        }


        // if (isSomeoneOnIt && !isTeleporting)
        // {
        //     if (fade < 1f)
        //     {
        //         player.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezePositionY | RigidbodyConstraints2D.FreezeRotation;
        //         fade += Time.deltaTime * transportingSpeed;
        //         playerMaterial.SetFloat("_Fade", fade);
        //     }
        //     else
        //     {
        //         fade = 1f;
        //         playerMaterial.SetFloat("_Fade", fade);
        //         player.GetComponent<PlayerController>().fade = fade;
        //         player.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeRotation;
        //         player.GetComponent<PlayerController>().SetPlayerFreezed(0);
        //     }

        // }

        // if (isTeleporting)
        // {
        //     player.GetComponent<PlayerController>().SetPlayerFreezed(1);
        //     player.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezePositionY | RigidbodyConstraints2D.FreezeRotation;

        //     if (fade <= 1f && fade > 0f)
        //     {
        //         fade -= Time.deltaTime * transportingSpeed;
        //         playerMaterial.SetFloat("_Fade", fade);
        //     }

        //     if (fade <= 0f)
        //     {
        //         fade = 0f;
        //         playerMaterial.SetFloat("_Fade", fade);
        //         player.GetComponent<PlayerController>().fade = fade;
        //         player.position = theOtherSide.position;
        //         fade = 0;
        //     }
        // }

    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (isSomeoneOnIt)
            return;

        if (!other.CompareTag("Player"))
            return;

        isPortalOpen = true;
        isSomeoneOnIt = true;
        buttonHint.SetActive(true);

        player = other.transform;
        playerInput = player.GetComponent<PlayerInput>();
        playerAnimator = player.GetComponent<Animator>();
        // fade = player.GetComponent<PlayerController>().fade;
        player.GetComponent<PlayerController>().teleportOtherSide = theOtherSide;
        // playerMaterial = player.GetComponentInChildren<SpriteRenderer>().material;

        m_Interaction = playerInput.actions["Interactions"];

    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.transform != player || player == null)
            return;

        if (Light != null)
            Light.color = Color.green;

        isPortalOpen = false;
        isSomeoneOnIt = false;
        isTeleporting = false;
        buttonHint.SetActive(false);

        player = null;
        playerInput = null;
        playerAnimator = null;
        // playerMaterial = null;
        m_Interaction = null;
    }
}
