using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private int _playerDirH;
    private int _playerDirV;
    [SerializeField] private float _moveSpeed = 5;

    //private Rigidbody2D _rb;

    private void Start()
    {
        //_rb = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        MovePlayer();
    }

    private void MovePlayer()
    {
        Vector2 inputDirFloat = InputReader.Input.FindAction("Move").ReadValue<Vector2>();
        _playerDirH = (int)inputDirFloat.x;
        _playerDirV = (int)inputDirFloat.y;
        //Debug.Log(inputDirFloat);

        transform.position += _moveSpeed * Time.fixedDeltaTime * new Vector3(_playerDirH, _playerDirV, 0);
    }
}
