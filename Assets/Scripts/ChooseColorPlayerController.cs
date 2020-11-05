using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.UI;

public class ChooseColorPlayerController : MonoBehaviour
{
    public MultiplayerEventSystem multiplayerEventSystem;
    public PlayerInput playerInput;
    public GameObject[] pressToJoinPanels;
    public GameObject[] partPickerPanels;
    public GameObject[] colorPickerPanels;
    public float startPosition;
    public float positionMultiDelta;

    private int playerIndex;
    private bool isColorPickerPanelOpened_temp;

    private void Awake()
    {
        multiplayerEventSystem = GetComponentInChildren<MultiplayerEventSystem>();
        playerInput = GetComponent<PlayerInput>();
        GameDataManager.playerInputs.Add(playerInput);
        playerIndex = playerInput.playerIndex;
        multiplayerEventSystem.playerRoot = partPickerPanels[playerIndex];
        multiplayerEventSystem.firstSelectedGameObject = partPickerPanels[playerIndex].transform.GetChild(0).GetChild(0).GetChild(1).gameObject;
    }
    private void OnEnable()
    {
        transform.position = new Vector3(startPosition + positionMultiDelta * playerInput.playerIndex, -3.05f, 0f);
    }
    private void Start()
    {
        pressToJoinPanels[playerIndex].SetActive(false);
        partPickerPanels[playerIndex].SetActive(true);
        colorPickerPanels[playerIndex].SetActive(false);
        isColorPickerPanelOpened_temp = false;
    }

    private void Update()
    {
        bool isActived = colorPickerPanels[playerIndex].activeInHierarchy;
        if (isActived == isColorPickerPanelOpened_temp)
            return;

        isColorPickerPanelOpened_temp = isActived;

        if (isActived)
        {
            multiplayerEventSystem.playerRoot = colorPickerPanels[playerIndex];
            multiplayerEventSystem.firstSelectedGameObject = colorPickerPanels[playerIndex].transform.GetChild(0).GetChild(0).GetChild(1).gameObject;
            multiplayerEventSystem.SetSelectedGameObject(colorPickerPanels[playerIndex].transform.GetChild(0).GetChild(0).GetChild(1).gameObject);
            multiplayerEventSystem.UpdateModules();
        }
        else
        {
            multiplayerEventSystem.playerRoot = partPickerPanels[playerIndex];
            multiplayerEventSystem.firstSelectedGameObject = partPickerPanels[playerIndex].transform.GetChild(0).GetChild(0).GetChild(1).gameObject;
            multiplayerEventSystem.SetSelectedGameObject(partPickerPanels[playerIndex].transform.GetChild(0).GetChild(0).GetChild(1).gameObject);
            multiplayerEventSystem.UpdateModules();
        }
    }
}
