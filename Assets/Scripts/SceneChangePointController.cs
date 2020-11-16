using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneChangePointController : MonoBehaviour
{
    public bool isSceneChanger;
    public Transform tempTransform;
    private int timer;

    private void Awake()
    {
        if(!isSceneChanger)
            tempTransform = transform.GetChild(0);
    }

    private void Update()
    {
        if(!isSceneChanger)
        {
            if (timer % 2 == 0)
                CameraController.Instance.ResetFollowTarget();
            else
                CameraController.Instance.ChangeFollowTarget(tempTransform);
        }


    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("Ship"))
            return;

        if(isSceneChanger){
            GetComponent<StateChange>().CrossScene();
            return;
        }
        tempTransform.position = other.transform.position;
        timer++;
    }
}
