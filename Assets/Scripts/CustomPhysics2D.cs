using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomPhysics2D : MonoBehaviour
{

    void Start()
    {
        Physics2D.IgnoreLayerCollision(9, 8);
        Physics2D.IgnoreLayerCollision(9, 9);
        Physics2D.IgnoreLayerCollision(10, 8);
        Physics2D.IgnoreLayerCollision(11, 12);
        Physics2D.IgnoreLayerCollision(11, 11);
        Physics2D.IgnoreLayerCollision(9, 13);
    }


    void Update()
    {

    }
}
