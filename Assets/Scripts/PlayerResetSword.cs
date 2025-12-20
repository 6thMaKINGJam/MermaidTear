using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerResetSword : MonoBehaviour
{
    [SerializeField] private Animator _swordAnimator;
    [SerializeField] private Transform _swordPivot;
    [SerializeField] private Transform _swordPivotWhileAnim;
    private Transform _swordPivotDefault;

    private void Start()
    {
        _swordPivotDefault = _swordPivot;
    }

    public void ResetSword()
    {
        _swordAnimator.SetBool("SwingNow", false);
        _swordPivot = _swordPivotDefault;
    }
}
