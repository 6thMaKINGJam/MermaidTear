using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    private int _playerDirH;
    private int _playerDirV;
    [SerializeField] private float _moveSpeed = 2;
    [SerializeField] private float _animSpeed = 4;

    //private Rigidbody2D _rb;
    private Animator _anim;

    private void Start()
    {
        //_rb = GetComponent<Rigidbody2D>();
        _anim = GetComponent<Animator>();
    }

    private void FixedUpdate()
    {
        MovePlayer();
    }

    private void MovePlayer()
    {
        Vector2 inputDirFloat = InputSystem.actions.FindAction("Move").ReadValue<Vector2>();
        _playerDirH = (int)inputDirFloat.x;
        _playerDirV = (int)inputDirFloat.y;
        //Debug.Log(inputDirFloat);

        if (_anim.GetInteger("PlayerDirH") != _playerDirH)
            _anim.SetInteger("PlayerDirH", _playerDirH);
        if (_anim.GetInteger("PlayerDirV") != _playerDirV)
            _anim.SetInteger("PlayerDirV", _playerDirV);
        _anim.speed = _animSpeed;

        transform.position += _moveSpeed * Time.fixedDeltaTime * new Vector3(_playerDirH, _playerDirV, 0);
    }
}
