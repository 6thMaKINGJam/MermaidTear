using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerSwapItem : MonoBehaviour
{
    private void OnEnable()
    {
        InputSystem.actions.FindAction("SwapItem").started += PlayerSwappedItem;
    }
    private void OnDisable()
    {
        InputSystem.actions.FindAction("SwapItem").started -= PlayerSwappedItem;
    }

    private void PlayerSwappedItem(InputAction.CallbackContext ctx)
    {
        
    }
}
