using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMove : MonoBehaviour
{
    [SerializeField] private float _moveSpeed = 2.5f;

    [SerializeField] private Transform _positionA;
    [SerializeField] private Transform _positionB;
    private bool currTarget;
    private Vector3 getCurrTarget() => currTarget ? _positionB.position : _positionA.position;

    private void FixedUpdate()
    {
        MoveEnemy();
    }

    private void MoveEnemy()
    {
        transform.position = Vector3.MoveTowards(transform.position, getCurrTarget(), _moveSpeed * Time.fixedDeltaTime);

        if (Vector3.Distance(transform.position, getCurrTarget()) < Mathf.Epsilon * 3)
            currTarget = !currTarget; // swap current target
    }
}
