using System;
using UnityEngine;
using UnityEngine.UI;

public class UIPlayerHearts : MonoBehaviour
{
    private PlayerHealth _playerHealth;

    //[SerializeField] private GameObject _playerHeartsParent;
    [SerializeField] private Image[] _playerHearts;

    private void Start()
    {
        _playerHealth = GetComponent<PlayerHealth>();
        //_playerHearts = _playerHeartsParent.GetComponentsInChildren<Image>();
    }

    public void UpdateHeartsUI(int currentHeartCnt)
    {
        for (int i = 0; i < currentHeartCnt; ++i)
        {
            _playerHearts[i].enabled = true;
            //Debug.Log($"_playerHearts[{i}].enabled = true;");
        }
        for (int i = currentHeartCnt; i < _playerHearts.Length; ++i)
            _playerHearts[i].enabled = false;
    }
}
