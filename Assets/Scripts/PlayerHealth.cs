using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public float MaxHealth = 5;
    public float CurrentHealth { get; set; }
    public bool TempInvincible { get; set; }

    [SerializeField] private UIPlayerHearts _uiPlayerHearts;

    private void Start()
    {
        ResetHealth();
    }

    public void ResetHealth()
    {
        CurrentHealth = MaxHealth;
        Debug.Log("player health: " + CurrentHealth);
    }

    public void ReduceHealth(float amount)
    {
        CurrentHealth -= amount;
        if (CurrentHealth < 0) CurrentHealth = 0;

        int heartCnt = Mathf.RoundToInt(CurrentHealth / 0.5f);
        Debug.Log(heartCnt);
        _uiPlayerHearts.UpdateHeartsUI(heartCnt);

        Debug.Log("player health: " + CurrentHealth);
    }

    public void RestoreHealth(float amount)
    {
        CurrentHealth += amount;
        if (CurrentHealth > MaxHealth) ResetHealth();

        Debug.Log("player health: " + CurrentHealth);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.layer == Layers.EnemyLayer && !TempInvincible)
        {
            ReduceHealth(other.GetComponent<EnemyAttack>().AtkPower);
            StartCoroutine(GetComponent<PlayerHurtBlink>().HurtBlink());
        }
    }
}
