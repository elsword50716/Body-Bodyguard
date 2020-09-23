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
    public float BulletSpeed;
    public float moveSpeed;
    [Range(0f, 50f)]public float roamRange;
    [Range(0f, 50f)]public float detectShipRange;
    [Range(0f, 100f)]public float ClosestDistanceToShip;
}

[System.Serializable]
public class BulletData{
    public int id;
    public string name;
    public float startSpeed;
    public float damagePoint;
    public float chasingDelay;
    public float chasingSpeed;
}
