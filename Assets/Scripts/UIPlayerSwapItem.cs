using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using PlayerItemType = PlayerItemUse.PlayerItemType;

public class UIPlayerSwapItem : MonoBehaviour
{
    [Header("Item Slot (UI Image)")]
    private Image _uiItemSlot;

    [SerializeField] private Sprite _uiHealingPotion;
    [SerializeField] private Sprite _uiLightningMagic;
    [SerializeField] private Sprite _uiSword;
    [SerializeField] private Sprite _uiShield;

    [Header("Item Slot (Text)")]
    [SerializeField] private TMP_Text _textItemSlot;
    [SerializeField] private string _textHealingPotion = "회복 포션";
    [SerializeField] private string _textLightningMagic = "라이트닝 마법 스크롤";
    [SerializeField] private string _textSword = "검";
    [SerializeField] private string _textShield = "방어 마법 스크롤";

    private void Start()
    {
        _uiItemSlot = GetComponent<Image>();
        PlayerSwappedItem(PlayerItemType.HealingPotion);
    }

    private void OnEnable()
    {
        PlayerItemUse.ItemSwapped += PlayerSwappedItem;
    }

    private void OnDisable()
    {
        PlayerItemUse.ItemSwapped -= PlayerSwappedItem;
    }

    private void PlayerSwappedItem(PlayerItemType currentItem)
    {
        switch (currentItem)
        {
            case PlayerItemType.HealingPotion:  changeItemUI(_uiHealingPotion, _textHealingPotion); break;
            case PlayerItemType.LightningAtk:   changeItemUI(_uiLightningMagic, _textLightningMagic); break;
            case PlayerItemType.SwordAtk:       changeItemUI(_uiSword, _textSword); break;
            case PlayerItemType.ShieldMagic:    changeItemUI(_uiShield, _textShield); break;
            default: break;
        }
    }

    private void changeItemUI(Sprite sprite, string text)
    {
        _uiItemSlot.sprite = sprite;
        _textItemSlot.text = text;
    }
}
