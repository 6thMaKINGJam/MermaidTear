using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHurtBlink : MonoBehaviour
{
    [SerializeField] private float _hurtBlinkSeconds = 0.14f;
    [SerializeField] private Color _hurtColor = Color.red;
    private Material _playerMat;
    private PlayerHealth _playerHealth;

    private void Start()
    {
        _playerMat = GetComponent<SpriteRenderer>().material;
        _playerHealth = GetComponent<PlayerHealth>();
    }

    public IEnumerator HurtBlink()
    {
        Color prevColor = _playerMat.GetColor("_BaseColor");
        
        _playerHealth.TempInvincible = true;
        for (int i = 0; i < 3; ++i)
        {
            _playerMat.SetColor("_BaseColor", _hurtColor);
            yield return new WaitForSeconds(_hurtBlinkSeconds);
            _playerMat.SetColor("_BaseColor", prevColor);
            yield return new WaitForSeconds(_hurtBlinkSeconds);
        }
        _playerHealth.TempInvincible = false;
    }
}
