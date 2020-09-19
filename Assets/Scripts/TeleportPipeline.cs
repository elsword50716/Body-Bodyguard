using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeleportPipeline : MonoBehaviour
{
    public Transform theOtherSide;

    public bool isPortalOpen = true;

    private Transform player;

    void Start()
    {

    }


    void Update()
    {

    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(!isPortalOpen)
            return;
        
        if (!other.CompareTag("Player"))
            return;

        player = other.transform;

        Teleport();

    }

    private void OnTriggerExit2D(Collider2D other) {
        if (!other.CompareTag("Player"))
            return;

        isPortalOpen = true;
    }

    public void Teleport(){
        theOtherSide.GetComponent<TeleportPipeline>().isPortalOpen = false;
        player.position = theOtherSide.position;
    }
}
