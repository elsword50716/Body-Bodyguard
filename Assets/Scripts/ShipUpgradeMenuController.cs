using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem.UI;
using UnityEngine.UI;

public class ShipUpgradeMenuController : MonoBehaviour
{
    public Ship ship;
    public Animator animator;
    public Button[] buttons;//上左中右下

    [SerializeField]private MultiplayerEventSystem P1_EventSystem;

    private void Awake()
    {
        if (ship == null)
            ship = GameObject.FindGameObjectWithTag("Ship").GetComponent<Ship>();
        
        // for (int i = 0; i < buttons.Length; i++)
        // {
        //     buttons[i].onClick.AddListener(delegate { OnUpgradeButtonClick(i); });
        // }

        buttons[0].onClick.AddListener(delegate { OnUpgradeButtonClick(0); });
        buttons[1].onClick.AddListener(delegate { OnUpgradeButtonClick(1); });
        buttons[2].onClick.AddListener(delegate { OnUpgradeButtonClick(2); });
        buttons[3].onClick.AddListener(delegate { OnUpgradeButtonClick(3); });
        buttons[4].onClick.AddListener(delegate { OnUpgradeButtonClick(4); });

    }

    private void Start()
    {
        //P1_EventSystem = ship.P1_EventSystem;
    }

    public void SetSelectButtons()
    {
        P1_EventSystem.firstSelectedGameObject = buttons[0].gameObject;
        P1_EventSystem.playerRoot = buttons[0].transform.parent.gameObject;
        P1_EventSystem.UpdateModules();
        P1_EventSystem.SetSelectedGameObject(buttons[0].gameObject);
    }

    public void OnUpgradeButtonClick(int i)
    {
        Debug.Log(ship.shipData.ShipPartLevel.Length);
        Debug.Log("i = " + i);
        ship.shipData.ShipPartLevel[i]++;
        ship.shipData.upgradeTimes++;
        ship.shipData.wrenchNumber = 0;
        GameSaveLoadManager.Instance.SaveData();
        animator.SetBool("isOpen", false);
    }

    public void SetTimeScale(float scale)
    {
        Time.timeScale = scale;
    }
}
