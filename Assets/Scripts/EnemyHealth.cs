using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    [SerializeField] private int _maxHealth = 10;
    private int _currentHealth;

    private void Start()
    {
        ResetHealth();
    }

    public void ResetHealth()
    {
        _currentHealth = _maxHealth;
        Debug.Log("enemy \"" + name + "\" health: " + _currentHealth);
    }

    public void ReduceHealth(int amount)
    {
        _currentHealth -= amount;
        Debug.Log("enemy \"" + name + "\" health: " + _currentHealth);
    }

    public void RestoreHealth(int amount)
    {
        _currentHealth += amount;
        if (_currentHealth > _maxHealth) ResetHealth();
        Debug.Log("enemy \"" + name + "\" health: " + _currentHealth);
    }
}
