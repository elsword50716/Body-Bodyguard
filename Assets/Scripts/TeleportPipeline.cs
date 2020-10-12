using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class TeleportPipeline : MonoBehaviour
{
    public GameObject buttonHint;
    public Transform theOtherSide;
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


    void Start()
    {
        isPortalOpen = false;
        isSomeoneOnIt = false;
        isTeleporting = false;
    }


    void Update()
    {
        if (player == null)
            return;
        
        if(theOtherSide.GetComponent<TeleportPipeline>().player != null)
            return;
        
        if (m_Interaction != null && m_Interaction.triggered && fade == 1f)
        {
            isTeleporting = true;
        }


        if (isSomeoneOnIt && !isTeleporting)
        {
            if (fade < 1f)
            {
                player.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezePositionY;
                fade += Time.deltaTime * transportingSpeed;
                playerMaterial.SetFloat("_Fade", fade);
            }
            else
            {
                fade = 1f;
                playerMaterial.SetFloat("_Fade", fade);
                player.GetComponent<PlayerController>().fade = fade;
                player.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeRotation;
                player.GetComponent<PlayerController>().SetPlayerFreezed(false);
            }

        }

        if (isTeleporting)
        {
            player.GetComponent<PlayerController>().SetPlayerFreezed(true);
            player.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezePositionY;

            if (fade <= 1f && fade > 0f)
            {
                fade -= Time.deltaTime * transportingSpeed;
                playerMaterial.SetFloat("_Fade", fade);
            }

            if (fade <= 0f)
            {
                fade = 0f;
                playerMaterial.SetFloat("_Fade", fade);
                player.GetComponent<PlayerController>().fade = fade;
                player.position = theOtherSide.position;
                fade = 0;
            }
        }

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
        fade = player.GetComponent<PlayerController>().fade;
        playerMaterial = player.GetComponentInChildren<SpriteRenderer>().material;

        m_Interaction = playerInput.actions["Interactions"];

    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.transform != player || player == null)
            return;

        isPortalOpen = false;
        isSomeoneOnIt = false;
        isTeleporting = false;
        buttonHint.SetActive(false);

        player = null;
        playerInput = null;
        playerMaterial = null;
        m_Interaction = null;
    }

    public void Teleport()
    {
        theOtherSide.GetComponent<TeleportPipeline>().isPortalOpen = false;
        player.position = theOtherSide.position;
    }
}
