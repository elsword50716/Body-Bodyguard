using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class SpawnPlayers : MonoBehaviour
{
    public GameObject playerPrefab;
    public Transform[] playerPositions;

    private bool isFound;
    private GameObject[] players;

    private void Awake()
    {
        if (GameDataManager.playerDatas.Count == 0)
            return;

        players = new GameObject[playerPositions.Length];

        Debug.Log(GameDataManager.playerDatas.Count);

        for (int i = 0; i < players.Length; i++)
        {
            GameObject player = Instantiate(playerPrefab, transform);
            players[i] = player;

        }

        for (int i = 0; i < players.Length; i++)
        {
            if (players[i].GetComponent<PlayerInput>().devices.Count == 0)
            {
                Debug.Log("destroy player with no user");
                Destroy(players[i]);
                continue;
            }
            var playerDeviceId = players[i].GetComponent<PlayerInput>().devices[0].deviceId;
            isFound = false;
            for (int j = 0; j < GameDataManager.playerDatas.Count; j++)
            {
                var id_temp = GameDataManager.playerDatas[j].deviceId;
                if (playerDeviceId == id_temp)
                {
                    players[i].GetComponent<PlayerController>().playerIndex = j;
                    players[i].transform.position = playerPositions[j].position;
                    isFound = true;
                    break;
                }
            }
            Debug.Log(isFound, players[i]);
            if (!isFound){
                Debug.Log("destroy player with no user/2");
                Destroy(players[i]);
            }

        }


    }
}
