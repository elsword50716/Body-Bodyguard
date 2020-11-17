using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class LevelGoalManager : MonoBehaviour
{
    public string goalName;
    public TextMeshProUGUI goalText;

    private void Update() {
        var lairCurrentNumber = GameDataManager.lairCurrentNumber;
        var lairTotalNumber = GameDataManager.lairTotalNumber;

        goalText.SetText($"{lairCurrentNumber}/{lairTotalNumber}");
        
        if(lairCurrentNumber == lairTotalNumber)
            goalText.color = Color.green;
    }
}
