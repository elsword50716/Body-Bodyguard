﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomPhysics2D : MonoBehaviour
{
    
    void Start()
    {
        Physics2D.IgnoreLayerCollision(9, 8);
    }

    
    void Update()
    {
        
    }
}
