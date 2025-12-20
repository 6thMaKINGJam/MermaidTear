using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerItemUse : MonoBehaviour
{
    private PlayerHealth _playerHealth;
    [SerializeField] private Animator _swordAnimator;
    [SerializeField] private Transform _swordPivot;
    [SerializeField] private Transform _swordPivotWhileAnim;

    public enum PlayerItemType
    {
        HealingPotion,
        LightningAtk,
        SwordAtk,
    }
    public PlayerItemType CurrentItem { get; set; }

    //public Dictionary<string, int> PlayerAttackStrength = new Dictionary<string, int>()
    //    {
    //        { "LightningAtkPower", 10 },
    //        { "SwordAtkPower", 15 },
    //    };
    private int GetAtkPower()
    {
        switch (CurrentItem)
        {
            case PlayerItemType.LightningAtk: return 10;
            case PlayerItemType.SwordAtk: return 15;
            default: return 0;
        }
    }

    private void OnEnable()
    {
        InputSystem.actions.FindAction("UseItem").started += PlayerHasUsedItem;
        InputSystem.actions.FindAction("SwapItem").started += PlayerSwappedItem;
    }
    private void OnDisable()
    {
        InputSystem.actions.FindAction("UseItem").started -= PlayerHasUsedItem;
        InputSystem.actions.FindAction("SwapItem").started -= PlayerSwappedItem;
    }

    private void Start()
    {
        _playerHealth = GetComponent<PlayerHealth>();
    }

    private void PlayerSwappedItem(InputAction.CallbackContext ctx)
    {
        string prevItem = CurrentItem.ToString();
        switch (CurrentItem)
        {
            case PlayerItemType.HealingPotion: CurrentItem = PlayerItemType.LightningAtk; break;
            case PlayerItemType.LightningAtk: CurrentItem = PlayerItemType.SwordAtk; break;
            case PlayerItemType.SwordAtk: CurrentItem = PlayerItemType.HealingPotion; break;
            default: break;
        }
        string currItem = CurrentItem.ToString();
        Debug.Log($"item swapped: {prevItem} -> {currItem}");
    }

    private void PlayerHasUsedItem(InputAction.CallbackContext ctx)
    {
        switch (CurrentItem)
        {
            case PlayerItemType.HealingPotion: UseHealingPotion(); break;
            case PlayerItemType.LightningAtk: DoLightningAttack(); break;
            case PlayerItemType.SwordAtk: DoSwordAttack(); break;
            default: break;
        }
    }

    private void UseHealingPotion()
    {
        float healAmount = 3;
        _playerHealth.RestoreHealth(healAmount);
    }

    private void DoLightningAttack()
    {
        Debug.Log("lightning attack: " + GetAtkPower());
        // AoE attack
    }

    private void DoSwordAttack()
    {
        Debug.Log("sword attack: " + GetAtkPower());
        // up-close attack
        _swordAnimator.SetBool("SwingNow", true);
        _swordPivot = _swordPivotWhileAnim;
        _swordPivot.Rotate()
        // rotate 90 angles depending on player dir
    }
}