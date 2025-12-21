using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    [SerializeField] private int _maxHealth = 10;
    [SerializeField] private float _dyingDur = 0.8f;
    public int CurrentHealth;
    public bool TempInvincible { get; set; }

    private void Start()
    {
        ResetHealth();
    }

    public void ResetHealth()
    {
        CurrentHealth = _maxHealth;
        healthChangedAction();
    }

    public void ReduceHealth(int amount)
    {
        if (CurrentHealth <= 0) return;
        CurrentHealth -= amount;
        if (CurrentHealth <= 0) StartCoroutine(ScheduleDie(_dyingDur));
        healthChangedAction();
    }

    public void RestoreHealth(int amount)
    {
        CurrentHealth += amount;
        if (CurrentHealth > _maxHealth) ResetHealth();
        healthChangedAction();
    }

    public IEnumerator ScheduleDie(float dyingDur)
    {
        yield return new WaitForSeconds(dyingDur);
        gameObject.SetActive(false);
        Debug.Log("setactivefalse");
    }

    private void healthChangedAction()
    {
        Debug.Log($"enemy \"name\" health: {CurrentHealth}");
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
