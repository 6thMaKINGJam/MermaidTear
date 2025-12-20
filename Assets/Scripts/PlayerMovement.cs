using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private int _playerDirH;
    private int _playerDirV;

    private Rigidbody2D _rb;

    private void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        MovePlayer();
    }

    private void MovePlayer()
    {
        Vector2 inputDir = InputReader.Input.FindAction("Move").ReadValue<Vector2>();
        _playerDirH = (int)inputDir.x;
        _playerDirV = (int)inputDir.y;

        Debug.Log(inputDir);


    }
}
