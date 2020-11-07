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
    public Animator animator;
    public bool isPaused;

    private MultiplayerEventSystem multiplayerEvent;
    private int index;

    private void Start()
    {
        main.SetActive(false);
        settings.SetActive(false);
    }
    private void Update()
    {
        if (multiplayerEvent != null)
            multiplayerEvent.UpdateModules();
    }

    public void Pause(int playerIndex, MultiplayerEventSystem multiplayerEventSystem)
    {
        index = playerIndex;
        multiplayerEvent = multiplayerEventSystem;
        pauseText.SetText($"{index}P Pause");
        // multiplayerEvent.playerRoot = main;
        // multiplayerEvent.firstSelectedGameObject = mainFirstSelected;
        // multiplayerEvent.UpdateModules();
        // multiplayerEvent.SetSelectedGameObject(mainFirstSelected);
        // main.SetActive(true);
        // Time.timeScale = 0f;
        isPaused = true;
        animator.SetBool("isPaused", isPaused);
    }

    public void Resume(int playerIndex)
    {
        if (playerIndex != index)
            return;
        multiplayerEvent.playerRoot = null;
        multiplayerEvent.firstSelectedGameObject = null;
        // main.SetActive(false);
        // Time.timeScale = 1f;
        isPaused = false;
        animator.SetBool("isPaused", isPaused);
    }
    public void ResumeBT()
    {
        multiplayerEvent.playerRoot = null;
        multiplayerEvent.firstSelectedGameObject = null;
        // main.SetActive(false);
        // Time.timeScale = 1f;
        isPaused = false;
        animator.SetBool("isPaused", isPaused);
    }

    public void OnSettingsBTClick()
    {
        animator.SetBool("isSettingOpen", true);
        //settings.SetActive(true);
        // main.SetActive(false);
        // multiplayerEvent.playerRoot = settings;
        // multiplayerEvent.firstSelectedGameObject = settingsFirstSelected;
        // multiplayerEvent.UpdateModules();
        // multiplayerEvent.SetSelectedGameObject(settingsFirstSelected);
    }

    public void OnBackBTClick()
    {
        animator.SetBool("isSettingOpen", false);
        // settings.SetActive(false);
        //main.SetActive(true);
        // multiplayerEvent.playerRoot = main;
        // multiplayerEvent.firstSelectedGameObject = mainFirstSelected;
        // multiplayerEvent.UpdateModules();
        // multiplayerEvent.SetSelectedGameObject(mainFirstSelected);
    }

    public void ChangeTimeScale(int scale)
    {
        Time.timeScale = scale;
    }

    public void ChangeEventSystemSelected(int index)
    {
        if (index == 1)
        {
            multiplayerEvent.playerRoot = settings;
            multiplayerEvent.firstSelectedGameObject = settingsFirstSelected;
            multiplayerEvent.UpdateModules();
            multiplayerEvent.SetSelectedGameObject(settingsFirstSelected);
        }

        if (index == 0)
        {
            multiplayerEvent.playerRoot = main;
            multiplayerEvent.firstSelectedGameObject = mainFirstSelected;
            multiplayerEvent.UpdateModules();
            multiplayerEvent.SetSelectedGameObject(mainFirstSelected);
        }
    }
}
