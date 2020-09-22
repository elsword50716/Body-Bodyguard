using System.Collections;
using System.Collections.Generic;
using UnityEngine;

static class GameDataManager
{

}

[System.Serializable]
public class EnemyData
{
    public int id;
    public string name;
    public float maxHealth;
    public float FireRate;
    public float moveSpeed;
    [Range(0f, 50f)]public float roamRange;
    [Range(0f, 50f)]public float detectShipRange;
    [Range(0f, 1f)]public float ClosestRangeToShip;
}
