using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShipUpgradeMenuController : MonoBehaviour
{
    public Ship ship;
    public Button[] buttons;//上左中右下

    private void Awake() {
        if(ship == null)
            ship = GameObject.FindGameObjectWithTag("Ship").GetComponent<Ship>();
    }
}
