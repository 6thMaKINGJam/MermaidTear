using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    [SerializeField] private int _maxHealth = 10;
    private int _currentHealth;
    public bool TempInvincible { get; set; }

    private void Start()
    {
        ResetHealth();
    }

    public void ResetHealth()
    {
        _currentHealth = _maxHealth;
        healthChangedAction();
    }

    public void ReduceHealth(int amount)
    {
        _currentHealth -= amount;
        healthChangedAction();
    }

    public void RestoreHealth(int amount)
    {
        _currentHealth += amount;
        if (_currentHealth > _maxHealth) ResetHealth();
        healthChangedAction();
    }

    private void healthChangedAction()
    {
        Debug.Log($"enemy \"name\" health: {_currentHealth}");
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.layer == Layers.PlayerAtkLayer && !TempInvincible)
        {
            ReduceHealth(GameObject.FindWithTag("Player").GetComponent<PlayerItemUse>().GetAtkPower());
            StartCoroutine(GetComponent<EnemyHurtBlink>().HurtBlink());
        }
    }
}
