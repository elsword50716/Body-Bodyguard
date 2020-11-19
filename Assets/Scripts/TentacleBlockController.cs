using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class TentacleBlockController : MonoBehaviour
{
    public bool isOpen;
    public Animator animator;
    public GameObject spirtes;

    private void Start() {
        animator = GetComponent<Animator>();
    }

    private void Update() {
        if(isOpen)
            animator.SetBool("isOpen", isOpen);
    }

    public void DestroySprites(){
        Destroy(spirtes);
    }

    public void SetTimeScale(float scale){
        Time.timeScale = scale;
    }
}
