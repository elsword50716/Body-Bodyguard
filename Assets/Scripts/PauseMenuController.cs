using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.UI;
using TMPro;

public class PauseMenuController : MonoBehaviour
{
    public GameObject main;
    public GameObject mainFirstSelected;
    public GameObject settings;
    public GameObject settingsFirstSelected;
    public TextMeshProUGUI pauseText;
    public bool isPaused;

    private MultiplayerEventSystem multiplayerEvent;
    private int index;

    private void Start()
    {
        main.SetActive(false);
        settings.SetActive(false);
    }

    public void Pause(int playerIndex, MultiplayerEventSystem multiplayerEventSystem)
    {
        index = playerIndex;
        multiplayerEvent = multiplayerEventSystem;
        pauseText.SetText($"{index}P Pause");
        multiplayerEvent.playerRoot = main;
        multiplayerEvent.firstSelectedGameObject = mainFirstSelected;
        multiplayerEvent.UpdateModules();
        multiplayerEvent.SetSelectedGameObject(mainFirstSelected);
        main.SetActive(true);
        Time.timeScale = 0f;
        isPaused = true;
    }

    public void Resume(int playerIndex)
    {
        if (playerIndex != index)
            return;
        multiplayerEvent.playerRoot = null;
        multiplayerEvent.firstSelectedGameObject = null;
        main.SetActive(false);
        Time.timeScale = 1f;
        isPaused = false;
    }
    public void ResumeBT()
    {
        multiplayerEvent.playerRoot = null;
        multiplayerEvent.firstSelectedGameObject = null;
        main.SetActive(false);
        Time.timeScale = 1f;
        isPaused = false;
    }

    public void OnSettingsBTClick()
    {
        settings.SetActive(true);
        main.SetActive(false);
        multiplayerEvent.playerRoot = settings;
        multiplayerEvent.firstSelectedGameObject = settingsFirstSelected;
        multiplayerEvent.UpdateModules();
        multiplayerEvent.SetSelectedGameObject(settingsFirstSelected);
    }

    public void OnBackBTClick()
    {
        settings.SetActive(false);
        main.SetActive(true);
        multiplayerEvent.playerRoot = main;
        multiplayerEvent.firstSelectedGameObject = mainFirstSelected;
        multiplayerEvent.UpdateModules();
        multiplayerEvent.SetSelectedGameObject(mainFirstSelected);
    }
}
