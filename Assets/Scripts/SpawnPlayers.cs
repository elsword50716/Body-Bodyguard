using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class SpawnPlayers : MonoBehaviour
{
    public GameObject playerPrefab;
    public Transform[] playerPositions;

    private void Awake()
    {
        if (GameDataManager.playerDatas.Count == 0)
            return;

        Debug.Log(GameDataManager.playerDatas.Count);

        for (int i = 0; i < GameDataManager.playerDatas.Count; i++)
        {
            var player = Instantiate(playerPrefab, transform);
            var playerDeviceId = player.GetComponent<PlayerInput>().devices[0].deviceId;
            for (int j = 0; j < GameDataManager.playerDatas.Count; j++)
            {
                var id_temp = GameDataManager.playerDatas[j].deviceId;
                if (playerDeviceId == id_temp)
                {
                    player.GetComponent<PlayerController>().playerIndex = j;
                    player.transform.position = playerPositions[j].position;
                }
            }

            // var player = Instantiate(playerPrefab, playerPositions[i].position, Quaternion.identity, transform);
            // player.GetComponent<PlayerController>().playerIndex = i;
            // var playerInput = player.GetComponent<PlayerInput>();
            // Debug.Log(playerInput.devices[0].deviceId, player.gameObject);
            // Debug.Log(playerInput.devices[0].name, player.gameObject);
        }


    }
}
