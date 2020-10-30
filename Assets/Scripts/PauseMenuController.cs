using UnityEngine;
using TMPro;

public class PauseMenuController : MonoBehaviour
{
    public GameObject main;
    public GameObject settings;
    public TextMeshProUGUI pauseText;
    public bool isPaused;

    private void Start()
    {
        main.SetActive(false);
        settings.SetActive(false);
    }

    public void Pause(int playerIndex){
        pauseText.SetText($"{playerIndex}P Pause");
        main.SetActive(true);
        Time.timeScale = 0f;
        isPaused = true;
    }

    public void Resume(){
        main.SetActive(false);
        Time.timeScale = 1f;
        isPaused = false;
    }

    public void OnSettingsBTClick(){
        settings.SetActive(true);
    }

    public void OnBackBTClick(){
        settings.SetActive(false);
    }
}
