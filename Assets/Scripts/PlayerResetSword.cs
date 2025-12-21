using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerResetSword : MonoBehaviour
{
    [SerializeField] private Animator _swordAnimator;
    [SerializeField] private Transform _swordPivot;
    [SerializeField] private Transform _swordPivotWhileAnim;
    [SerializeField] private Collider2D _swordAtkTrigger;
    private Vector3 _swordPivotDefaultPos;
    private Quaternion _swordPivotDefaultRot;

    private void Start()
    {
        _swordPivotDefaultPos = _swordPivot.localPosition;
        _swordPivotDefaultRot = _swordPivot.localRotation;
    }

    public void ResetSword()
    {
        _swordAnimator.SetBool("SwingNow", false);
        _swordPivot.localPosition = _swordPivotDefaultPos;
        _swordPivot.localRotation = _swordPivotDefaultRot;
        _swordAtkTrigger.enabled = false;
    }
}
