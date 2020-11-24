using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShipPartUpgradeController : MonoBehaviour
{
    public int partIndex;
    public Ship ship;
    public ShooterController shooterController;
    public ParticleSystem upgradeParticle;
    public ShooterInfo[] shooterPrefabs;
    private ShipData shipData;
    private int level_temp;

    private void Awake() {
        shooterController = GetComponent<ShooterController>();
        ship = GameObject.FindGameObjectWithTag("Ship").GetComponent<Ship>();
        shipData = ship.shipData;
        level_temp = shipData.ShipPartLevel[partIndex];
        shooterController.shooterData = shooterPrefabs[level_temp].shooterData;
        shooterPrefabs[0].gameObject.SetActive(true);
        for (int i = 1; i < shooterPrefabs.Length; i++)
        {
            shooterPrefabs[i].gameObject.SetActive(false);
        }
    }

    private void Update() {
        RefreshShipData();
    }

    public void RefreshShipData(){
        shipData = ship.shipData;
        if(level_temp != shipData.ShipPartLevel[partIndex]){
            UpdateGun();
        }
    }

    public void UpdateGun(){
        shooterPrefabs[level_temp].gameObject.SetActive(false);
        level_temp = shipData.ShipPartLevel[partIndex];
        upgradeParticle.transform.position = shooterPrefabs[level_temp].transform.position;
        upgradeParticle.Play();
        shooterPrefabs[level_temp].gameObject.SetActive(true);
        shooterController.shooterData = shooterPrefabs[level_temp].shooterData;
        shooterController.UpdateFirePoints();
    }
}
