using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem.UI;
using UnityEngine.UI;
using TMPro;

public class Ship : MonoBehaviour
{
    public Transform MinimapCamera;
    public Transform shipInside;
    public Slider healthBar;
    public TextMeshProUGUI wrenchText;
    public Animator wrenchAnimator;
    public Animator shipDamageEffectAnimator;
    public Animator shipUpgradeAnimator;
    public Animator shipDeadAnimator;
    public ParticleSystem shipHealParticle;
    public ParticleSystem shipDeadParticle;
    public MultiplayerEventSystem P1_EventSystem;
    public SpawnPlayers spawnPlayers;
    public ShipData shipData;

    [SerializeField] private float currentHealth;
    private Rigidbody2D rbody2D;
    private float maxHealth_temp;

    private void Start()
    {
        rbody2D = GetComponent<Rigidbody2D>();
        currentHealth = shipData.maxHealth;
        healthBar.maxValue = shipData.maxHealth;
        maxHealth_temp = shipData.maxHealth;
        RefreshWrenchUI();
    }

    private void Update()
    {
        MinimapCamera.position = new Vector3(transform.position.x, transform.position.y, -10);
        shipInside.position = transform.position;
        healthBar.value = currentHealth;
        RefreshWrenchUI();

        if (maxHealth_temp != shipData.maxHealth)
        {
            healthBar.maxValue = shipData.maxHealth;
            maxHealth_temp = shipData.maxHealth;
            currentHealth = maxHealth_temp;
        }

        if (currentHealth <= 0)
        {
            currentHealth = 0f;
            Dead();
        }
    }

    public void GetDamaged(float damage)
    {
        shipDamageEffectAnimator.SetTrigger("isShipHit");
        //CameraController.Instance.ShakeCamera(damage, .1f);
        currentHealth -= damage;
    }

    public void Dead()
    {
        //Debug.Log("Ship Dead!!!!!!!");
        if (!shipDeadParticle.isPlaying)
        {
            shipDeadParticle.gameObject.SetActive(true);
            shipDeadParticle.Play();
            shipDeadAnimator.SetBool("isDead", true);
            if (GameDataManager.playerDatas.Count > 0)
            {
                spawnPlayers.ResetPlayersPosition();
            }
        }
    }

    public void Heal(float healPoint)
    {
        shipHealParticle.Play();
        if (currentHealth + healPoint >= maxHealth_temp)
            currentHealth = maxHealth_temp;
        else
            currentHealth += healPoint;
    }

    public float GetCurrentHP()
    {
        return currentHealth;
    }

    public void SetCurrentHP(float HP)
    {
        currentHealth = HP;
    }

    public void GetWrench(int number)
    {
        wrenchAnimator.SetTrigger("isGetWrench");
        shipData.wrenchNumber += number;
    }

    public void RefreshWrenchUI()
    {
        wrenchText.SetText($"{shipData.wrenchNumber}/{15 + shipData.upgradeTimes * 5}");
        if (shipData.wrenchNumber >= 15 + shipData.upgradeTimes * 5 && !shipUpgradeAnimator.GetBool("isOpen"))
        {
            shipUpgradeAnimator.SetBool("isOpen", true);
            shipUpgradeAnimator.transform.position = transform.position;
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        Debug.Log("ship Hit!!");

        if (other.gameObject.layer != 13)
            return;

        Debug.Log("ship Damage: " + other.relativeVelocity.magnitude);

        if (other.relativeVelocity.magnitude > 5f)
            GetDamaged(other.relativeVelocity.magnitude * (shipData.maxHealth / 500f));

    }

}
