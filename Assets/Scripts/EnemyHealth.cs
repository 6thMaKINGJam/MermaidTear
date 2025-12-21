using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    [SerializeField] private int _maxHealth = 10;
    [SerializeField] private float _dyingDur = 0.8f;
    public int CurrentHealth;
    public bool TempInvincible { get; set; }

    public event Action OnDead;
    private bool deadCalled = false;

    private void Start()
    {
        ResetHealth();
    }

    public void ResetHealth()
    {
        CurrentHealth = _maxHealth;
        deadCalled = false;
        healthChangedAction();
    }

    public void ReduceHealth(int amount)
    {
        if (CurrentHealth <= 0) return;

        CurrentHealth -= amount;

        if (CurrentHealth <= 0)
        {
            CurrentHealth = 0;
            healthChangedAction();

            // ✅ 여기서 바로 부를 수도 있지만, 너는 죽는 연출시간(_dyingDur)이 있으니
            // 실제로 꺼지기 직전에 부르는 게 더 자연스러움 -> ScheduleDie에서 Invoke
            StartCoroutine(ScheduleDie(_dyingDur));
            return;
        }

        healthChangedAction();
    }

    public IEnumerator ScheduleDie(float dyingDur)
    {
        yield return new WaitForSeconds(dyingDur);

        if (!deadCalled)
        {
            deadCalled = true;
            OnDead?.Invoke();
        }

        gameObject.SetActive(false);
        Debug.Log("setactivefalse");
    }

    private void healthChangedAction()
    {
        Debug.Log($"enemy health: {CurrentHealth}");
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
