using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public float MaxHealth = 5;
    public float CurrentHealth { get; set; }
    public bool TempInvincible { get; set; }

    [SerializeField] private UIPlayerHearts _uiPlayerHearts;

    public static event Action OnDead;
    private bool deadCalled = false;

    public static bool IsDead { get; private set; }

    private void Start()
    {
        ResetHealth();
    }

    public void ResetHealth()
    {
        CurrentHealth = MaxHealth;
        deadCalled = false;
        IsDead = false;
        healthChangedAction();
    }

    public void ReduceHealth(float amount)
    {
        if (CurrentHealth <= 0) { CurrentHealth = 0; return; }

        CurrentHealth -= amount;
        if (CurrentHealth <= 0)
        {
            CurrentHealth = 0;
            healthChangedAction();

            if (!deadCalled)
            {
                deadCalled = true;
                IsDead = true;
                OnDead?.Invoke();
            }
            return;
        }

        healthChangedAction();
    }

    public void RestoreHealth(float amount)
    {
        CurrentHealth += amount;
        if (CurrentHealth > MaxHealth) ResetHealth();
        healthChangedAction();
    }

    private void healthChangedAction()
    {
        int heartCnt = Mathf.RoundToInt(CurrentHealth / 0.5f);
        _uiPlayerHearts.UpdateHeartsUI(heartCnt);
        Debug.Log($"player health: {CurrentHealth}, player heartCnt: {heartCnt}");
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

