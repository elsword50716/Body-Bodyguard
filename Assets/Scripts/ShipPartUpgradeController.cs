using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShipPartUpgradeController : MonoBehaviour
{
    public int partIndex;
    public ShipData shipData;

    private void Awake() {
        shipData = GameObject.FindGameObjectWithTag("Ship").GetComponent<Ship>().shipData;
    }
}
