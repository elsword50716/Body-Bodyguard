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

        for (int i = 0; i < GameDataManager.playerDatas.Count; i++)
        {
            var player = Instantiate(playerPrefab, playerPositions[i].position, Quaternion.identity, transform);
            player.GetComponent<PlayerController>().playerIndex = i;
            var playerUser = GetComponent<PlayerInput>().user;
            playerUser = GameDataManager.playerDatas[i].input.user;
        }
    }
}
