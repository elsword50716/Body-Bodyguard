﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShipPartUpgradeController : MonoBehaviour
{
    public int partIndex;
    public PartType partType;
    public Ship ship;
    public ShooterController shooterController;
    public ShipController shipController;
    public ParticleSystem[] upgradeParticles;
    public ShipPartInfo[] partPrefabs;

    public enum PartType
    {
        shooter,
        laser,
        booster
    }


    private ShipData shipData;
    private int level_temp;

    private void OnValidate()
    {
        if (partType == PartType.booster)
            shipController = GetComponent<ShipController>();
        else
            shooterController = GetComponent<ShooterController>();
    }

    private void Awake()
    {
        shooterController = GetComponent<ShooterController>();
        ship = GameObject.FindGameObjectWithTag("Ship").GetComponent<Ship>();
        shipData = ship.shipData;
        level_temp = shipData.ShipPartLevel[partIndex];

        SetPartData();

        for (int i = 0; i < partPrefabs.Length; i++)
        {
            if(i == level_temp){
                partPrefabs[i].gameObject.SetActive(true);
                continue;
            }
            
            partPrefabs[i].gameObject.SetActive(false);
        }
    }

    private void Update()
    {
        RefreshShipData();
    }

    public void RefreshShipData()
    {
        shipData = ship.shipData;
        if (level_temp != shipData.ShipPartLevel[partIndex])
        {
            UpdatePart();
        }
    }

    public void UpdatePart()
    {
        partPrefabs[level_temp].gameObject.SetActive(false);
        level_temp = shipData.ShipPartLevel[partIndex];
        PlayUpgradeParticles();
        partPrefabs[level_temp].gameObject.SetActive(true);
        SetPartData();
        if (partType == PartType.shooter)
            shooterController.UpdateFirePoints();
    }

    private void SetPartData()
    {
        switch (partType)
        {
            case PartType.shooter:
                shooterController.shooterData = partPrefabs[level_temp].shooterData;
                break;
            case PartType.laser:
                shooterController.laserData = partPrefabs[level_temp].laserData;
                break;
            case PartType.booster:
                shipController.boosterData = partPrefabs[level_temp].boosterData;
                break;
        }
    }

    private void PlayUpgradeParticles()
    {
        if (partType == PartType.booster)
        {
            for (int i = 0; i < upgradeParticles.Length; i++)
            {
                upgradeParticles[i].transform.position = partPrefabs[level_temp].transform.GetChild(i).GetChild(1).position;
                upgradeParticles[i].Play();
            }
        }
        else
        {
            upgradeParticles[0].transform.position = partPrefabs[level_temp].transform.position;
            upgradeParticles[0].Play();
        }
    }
}
