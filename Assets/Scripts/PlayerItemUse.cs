using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using FaceDir = PlayerFaceDir.FaceDir;

public class PlayerItemUse : MonoBehaviour
{
    private PlayerHealth _playerHealth;
    private PlayerFaceDir _playerFaceDir;
    [SerializeField] private Animator _swordAnimator;
    [SerializeField] private Transform _swordPivot;
    [SerializeField] private Collider2D _swordAtkTrigger;
    [SerializeField] private Transform _swordPivotWhileAnim;
    [SerializeField] private GameObject _shieldObj;

    public enum PlayerItemType
    {
        HealingPotion,
        LightningAtk,
        SwordAtk,
        ShieldMagic,
    }
    public PlayerItemType CurrentItem { get; set; }
    public static event Action<PlayerItemType> ItemSwapped;

    //public Dictionary<string, int> PlayerAttackStrength = new Dictionary<string, int>()
    //    {
    //        { "LightningAtkPower", 10 },
    //        { "SwordAtkPower", 15 },
    //    };
    public int GetAtkPower()
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
        InputSystem.actions.FindAction("SwapItem").started += PlayerSwappedItem;
        InputSystem.actions.FindAction("UseItem").started += PlayerHasUsedItem;
        InputSystem.actions.FindAction("UseItem").canceled += PlayerStoppedUsingItem;
    }
    private void OnDisable()
    {
        InputSystem.actions.FindAction("SwapItem").started -= PlayerSwappedItem;
        InputSystem.actions.FindAction("UseItem").started -= PlayerHasUsedItem;
        InputSystem.actions.FindAction("UseItem").canceled -= PlayerStoppedUsingItem;
    }

    private void Start()
    {
        _playerHealth = GetComponent<PlayerHealth>();
        _playerFaceDir = GetComponent<PlayerFaceDir>();
        _swordAtkTrigger.enabled = false;
        DeactivateShield();
    }

    private void PlayerSwappedItem(InputAction.CallbackContext ctx)
    {
        string prevItem = CurrentItem.ToString();
        switch (CurrentItem)
        {
            case PlayerItemType.HealingPotion: CurrentItem = PlayerItemType.LightningAtk; break;
            case PlayerItemType.LightningAtk: CurrentItem = PlayerItemType.SwordAtk; break;
            case PlayerItemType.SwordAtk: CurrentItem = PlayerItemType.ShieldMagic; break;
            case PlayerItemType.ShieldMagic: CurrentItem = PlayerItemType.HealingPotion; break;
            default: break;
        }
        string currItem = CurrentItem.ToString();
        Debug.Log($"item swapped: {prevItem} -> {currItem}");
        ItemSwapped?.Invoke(CurrentItem);
    }

    private void PlayerHasUsedItem(InputAction.CallbackContext ctx)
    {
        switch (CurrentItem)
        {
            case PlayerItemType.HealingPotion: UseHealingPotion(); break;
            case PlayerItemType.LightningAtk: DoLightningAttack(); break;
            case PlayerItemType.SwordAtk: DoSwordAttack(); break;
            case PlayerItemType.ShieldMagic: ActivateShield(); break;
            default: break;
        }
    }

    private void PlayerStoppedUsingItem(InputAction.CallbackContext ctx)
    {
        switch (CurrentItem)
        {
            case PlayerItemType.ShieldMagic: DeactivateShield(); break;
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
        if (_swordAnimator.GetBool("SwingNow")) return; // atk already in process

        Debug.Log("sword attack: " + GetAtkPower());

        Vector2 inputDirFloat = InputSystem.actions.FindAction("Move").ReadValue<Vector2>();
        int _playerDirH = (int)inputDirFloat.x;
        int _playerDirV = (int)inputDirFloat.y;

        // rotate 90 angles depending on player dir
        float rotAngle;
        FaceDir currFaceDir = _playerFaceDir.GetPlayerFaceDir();
        switch (currFaceDir)
        {
            case FaceDir.Up: rotAngle = 0; break;
            case FaceDir.Right: rotAngle = -90; break;
            case FaceDir.Down: rotAngle = 180; break;
            case FaceDir.Left: rotAngle = 90; break;
            default: rotAngle = 0; break;
        }
        Debug.Log(currFaceDir + ", " + rotAngle);
        _swordPivot.localRotation = Quaternion.Euler(_swordPivot.localRotation.x, _swordPivot.localRotation.y, rotAngle);

        // up-close attack
        _swordAtkTrigger.enabled = true;
        _swordAnimator.SetBool("SwingNow", true);
        _swordPivot.localPosition = _swordPivotWhileAnim.localPosition;
    }

    private void ActivateShield()
    {
        _shieldObj.SetActive(true);
        _playerHealth.TempInvincible = true;
    }

    private void DeactivateShield()
    {
        _shieldObj.SetActive(false);
        _playerHealth.TempInvincible = false;
    }
}