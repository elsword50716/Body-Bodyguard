using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.IO;

public class GameSaveLoadManager : MonoBehaviour
{
    public bool isInMainMenu;
    public Transform ship;
    public Transform enemyLairs;
    public Transform enemySpawnPoints;
    public static GameSaveLoadManager Instance;

    private List<EnemyLairAI> enemyLairAIs;
    private List<EnemySpawnController> enemySpawnControllers;

    private void Awake() {
        if(isInMainMenu)
            return;

        Instance = this;

        ship = GameObject.FindGameObjectWithTag("Ship").transform;

        enemyLairAIs = new List<EnemyLairAI>();
        enemySpawnControllers = new List<EnemySpawnController>();
        foreach (Transform enemyLair in enemyLairs)
        {
            enemyLairAIs.Add(enemyLair.GetComponentInChildren<EnemyLairAI>());
        }
        foreach (Transform enemySpawnPoint in enemySpawnPoints)
        {
            var enemySpawnController = enemySpawnPoint.GetComponent<EnemySpawnController>();
            enemySpawnController.ClearAllEnemy();
            enemySpawnController.SpawnEnemies();
            enemySpawnControllers.Add(enemySpawnController);
        }

        if (File.Exists(Application.persistentDataPath + "/Save.json"))
            LoadData();
        else
            SaveData();
    }

    public void SaveData(){
        var saveData = new GameSaveData();
        saveData.LevelName = SceneManager.GetActiveScene().name;
        saveData.shipData = ship.GetComponent<Ship>().shipData;
        saveData.shipData.shipPosition = ship.position;
        saveData.lairCurrentNumber = GameDataManager.lairCurrentNumber;
        saveData.lairTotalNumber = GameDataManager.lairTotalNumber;
        saveData.LairIsDead = new bool[enemyLairAIs.Count];
        for (int i = 0; i < enemyLairAIs.Count; i++)
        {
            saveData.LairIsDead[i] = enemyLairAIs[i].isDead;
        }

        string jsonData = JsonUtility.ToJson(saveData, true);
        File.WriteAllText(Application.persistentDataPath + "/Save.json", jsonData);
    }

    public void LoadData(){
        if (!File.Exists(Application.persistentDataPath + "/Save.json")){
            Debug.Log("GameSave doesn't exist!!!");
            return;
        }
        var saveData = new GameSaveData();
        saveData = JsonUtility.FromJson<GameSaveData>(File.ReadAllText(Application.persistentDataPath + "/Save.json"));
        ship.GetComponent<Ship>().shipData = saveData.shipData;
        ship.position = saveData.shipData.shipPosition;

        for (int i = 0; i < saveData.LairIsDead.Length; i++)
        {
            enemyLairAIs[i].isDead = saveData.LairIsDead[i];
        }

        GameDataManager.lairCurrentNumber = saveData.lairCurrentNumber;
        GameDataManager.lairTotalNumber = saveData.lairTotalNumber;

        foreach (var enemySpawnController in enemySpawnControllers)
        {
            if(enemySpawnController.transform.childCount < enemySpawnController.maxEnemyNumber){
                enemySpawnController.ClearAllEnemy();
                enemySpawnController.SpawnEnemies();
            }
        }
    }

    public void SetSceneName(){
        if(GetComponent<StateChange>() == null)
            return;
        
        var stateChange = GetComponent<StateChange>();
        stateChange.nextSceneName = JsonUtility.FromJson<GameSaveData>(File.ReadAllText(Application.persistentDataPath + "/Save.json")).LevelName;
        stateChange.CrossScene();
    }
}
