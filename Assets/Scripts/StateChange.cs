using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StateChange : MonoBehaviour
{
    public string nextSceneName;
    public int nextLevel_id;
    public int nextState_id;

    int Current_Level_id;
    int Current_State_id;

    AsyncOperation async;
    float progressValue = 0;

    List<int> tempList;

    public void ToNextState()
    {
        Current_Level_id = GameDataManager.stateDatas.Current_Level_id;
        Current_State_id = GameDataManager.stateDatas.Current_State_id;

        tempList = new List<int>
        {
            Current_Level_id,
            Current_State_id
        };

        GameDataManager.stateDatas.Current_State_id++;

        GameDataManager.stateDatas.LevelAndStateHistory.Add(tempList);

    }

    public void ToAnyState()
    {
        Current_Level_id = GameDataManager.stateDatas.Current_Level_id;
        Current_State_id = GameDataManager.stateDatas.Current_State_id;

        tempList = new List<int>
        {
            Current_Level_id,
            Current_State_id
        };

        GameDataManager.stateDatas.Current_Level_id = nextLevel_id;
        GameDataManager.stateDatas.Current_State_id = nextState_id;

        GameDataManager.stateDatas.LevelAndStateHistory.Add(tempList);
    }

    public void CrossScene()
    {
        if (string.IsNullOrEmpty(nextSceneName))
            return;

        GameDataManager.stateDatas.Current_Level_id = nextLevel_id;
        GameDataManager.stateDatas.Current_State_id = nextState_id;

        GameDataManager.nextSceneName = nextSceneName;

        SceneManager.LoadScene("LoadingScene", LoadSceneMode.Additive);//把loadingScene加進來
    }

    public void ToPreviousState()
    {
        if (GameDataManager.stateDatas.LevelAndStateHistory.Count == 0)
            return;

        int lastindex = GameDataManager.stateDatas.LevelAndStateHistory.Count - 1;

        GameDataManager.stateDatas.Current_Level_id = GameDataManager.stateDatas.LevelAndStateHistory[lastindex][0];
        GameDataManager.stateDatas.Current_State_id = GameDataManager.stateDatas.LevelAndStateHistory[lastindex][1];
        GameDataManager.stateDatas.LevelAndStateHistory.RemoveAt(lastindex);
    }

    
}