using System.Collections;
using System.Collections.Generic;
using UnityEngine;

static class GameDataManager
{

}

[System.Serializable]
public struct EnemyData
{
    public int id;
    public string name;
    public float maxHealth;
    public float FireRate;
    public float BulletSpeed;
    public float attackDamage;
    public float moveSpeed;
    public float inAttackRangeMoveSpeed;
    [Tooltip("Yellow Sphere")] [Range(0f, 50f)] public float roamRange;
    [Tooltip("White Sphere")] [Range(0f, 50f)] public float detectShipRange;
    [Tooltip("Blue Sphere")] [Range(0f, 100f)] public float ClosestDistanceToShip;
}

[System.Serializable]
public struct BulletData
{
    public int id;
    public string name;
    public float startSpeed;
    public float damage;
    public float chasingDelay;
    public float chasingSpeed;
    public string targetTag;
}

[System.Serializable]
public struct ShipData
{
    public int id;
    public string name;
    public float maxHealth;
    public float NormalSpeed;
    public float BoostSpeed;
    public float ShootterRotateAngle;
}
