using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossEyeFollow : MonoBehaviour
{
    public Transform ship;
    public Transform eye;
    public float eyeMoveLength;

    private void Awake()
    {
        ship = GameObject.FindGameObjectWithTag("Ship").transform;
        eye = transform.GetChild(0);
    }

    private void Update()
    {
        var dir = (ship.position - transform.position).normalized;
        eye.position = transform.position + dir * eyeMoveLength;
    }
}
