using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class TeleportPipeline : MonoBehaviour
{
    public Transform theOtherSide;

    public float transportingSpeed = 1f;
    public bool isPortalOpen = false;
    public bool isSomeoneOnIt = false;
    public bool isTransporting = false;
    public bool isOnOtherSide = false;

    private Transform player;
    private PlayerInput playerInput;
    private Material playerMaterial;
    private InputAction m_Interaction;
    private float fade = 1f;


    void Start()
    {
        isPortalOpen = false;
        isSomeoneOnIt = false;
        isTransporting = false;
    }


    void Update()
    {


        if (m_Interaction.triggered)
        {
            isTransporting = true;
        }

        if (player == null)
            return;

        if (isTransporting)
        {
            player.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezePositionX;

            if (fade <= 1f && fade > 0f && isOnOtherSide == false)
            {
                fade -= Time.deltaTime * transportingSpeed;
                playerMaterial.SetFloat("_Fade", fade);
            }

            if (fade <= 0f && isOnOtherSide == false)
            {
                fade = 0f;
                playerMaterial.SetFloat("_Fade", fade);
                player.position = theOtherSide.position;
                isOnOtherSide = true;
            }

            if (isOnOtherSide)
            {
                if (fade < 1f)
                {
                    fade += Time.deltaTime * transportingSpeed;
                    playerMaterial.SetFloat("_Fade", fade);
                }
                else if (fade >= 1f)
                {
                    fade = 1f;
                    playerMaterial.SetFloat("_Fade", fade);
                    isOnOtherSide = false;
                    isTransporting = false;
                }
            }

            return;
        }

        player.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeRotation;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (isSomeoneOnIt)
            return;

        if (!other.CompareTag("Player"))
            return;

        isPortalOpen = true;
        isSomeoneOnIt = true;

        player = other.transform;
        playerInput = player.GetComponent<PlayerInput>();
        playerMaterial = player.GetComponentInChildren<SpriteRenderer>().material;

        m_Interaction = playerInput.actions["Interactions"];

    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.transform != player || player == null)
            return;

        isPortalOpen = false;
        isSomeoneOnIt = false;

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
