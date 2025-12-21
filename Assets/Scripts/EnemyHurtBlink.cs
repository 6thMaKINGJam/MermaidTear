using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHurtBlink : MonoBehaviour
{
    [SerializeField] private float _hurtBlinkSeconds = 0.14f;
    [SerializeField] private Color _hurtColor = Color.red;
    private Material _enemyMat;
    private EnemyHealth _enemyHealth;

    private void Start()
    {
        _enemyMat = GetComponent<SpriteRenderer>().material;
        _enemyHealth = GetComponent<EnemyHealth>();
    }

    public IEnumerator HurtBlink()
    {
        Color prevColor = _enemyMat.GetColor("_BaseColor");

        _enemyHealth.TempInvincible = true;
        //for (int i = 0; i < 3; ++i)
        //{
            _enemyMat.SetColor("_BaseColor", _hurtColor);
            yield return new WaitForSeconds(_hurtBlinkSeconds);
            _enemyMat.SetColor("_BaseColor", prevColor);
            yield return new WaitForSeconds(_hurtBlinkSeconds);
        //}
        _enemyHealth.TempInvincible = false;
    }
}
