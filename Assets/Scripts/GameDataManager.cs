using System.Collections;
using System.Collections.Generic;
using UnityEngine;

static class GameDataManager
{
    public static StateDatas stateDatas = new StateDatas();
    public static string nextSceneName;
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


[System.Serializable]
public struct StateInfo
{
    public string Comment;
    public int State_id;
    public GameObject State;
}

[System.Serializable]
public class LevelInfo
{
    public string Comment;
    public int Level_id;
    public GameObject Level;
    public StateInfo[] stateInfo;
}

public class StateDatas
{
    public int Current_State_id = 0;
    public int Current_Level_id = 0;

    public List<List<int>> LevelAndStateHistory = new List<List<int>>();
    
}