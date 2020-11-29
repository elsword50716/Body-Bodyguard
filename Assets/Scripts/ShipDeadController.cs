using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem.UI;

public class ShipDeadController : MonoBehaviour
{
    public Ship ship;
    public MultiplayerEventSystem P1_EventSystem;
    public Animator animator;
    public Button restartBT;

    private void Awake()
    {
        restartBT.onClick.AddListener(delegate { RestartToSavePoint(); });
    }

    public void SetP1SelectedBT()
    {
        P1_EventSystem = ship.P1_EventSystem;
        P1_EventSystem.SetSelectedGameObject(restartBT.gameObject);
    }

    public void SetTimeScale(int scale)
    {
        Time.timeScale = scale;
    }

    public void RestartToSavePoint()
    {
        Debug.Log("Restart!!");
        if (GameDataManager.playerDatas.Count == 0)
            return;

        GameSaveLoadManager.Instance.LoadData();
        animator.SetBool("isDead", false);

    }


}
