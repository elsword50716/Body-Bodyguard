using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class TentacleBlockController : MonoBehaviour
{
    public bool isOpen;
    [Range(0f, 200f)]
    public float detectRange;
    public Animator animator;
    public GameObject sprites;
    public CinemachineVirtualCamera virtualCamera;

    private Transform ship;

    private void Awake()
    {
        ship = GameObject.FindGameObjectWithTag("Ship").transform;
    }
    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        if (isOpen)
        {
            animator.SetBool("isOpen", isOpen);
            return;
        }

        if ((transform.position - ship.position).sqrMagnitude < detectRange * detectRange)
        {
            if (virtualCamera.Priority != 20)
                virtualCamera.Priority = 20;
        }
        else
        {
            if (virtualCamera.Priority != 0)
                virtualCamera.Priority = 0;
        }
    }

    public void DisableSprites()
    {
        sprites.SetActive(false);
    }

    public void DestroySelf()
    {
        Destroy(gameObject);
    }

    public void SetTimeScale(int scale)
    {
        Time.timeScale = scale;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(transform.position, detectRange);
    }
}
