using System;
using UnityEngine;
using UnityEngine.UI;

public class UIPlayerHearts : MonoBehaviour
{
    private PlayerHealth _playerHealth;

    //[SerializeField] private GameObject _playerHeartsParent;
    [SerializeField] private Image[] _playerHearts;
    //private Image[] _reverseOrderHearts;

    private void Start()
    {
        _playerHealth = GetComponent<PlayerHealth>();
        //_playerHearts = _playerHeartsParent.GetComponentsInChildren<Image>();

        //_reverseOrderHearts = _playerHearts;
        //Array.Reverse(_reverseOrderHearts);
    }

    public void UpdateHeartsUI(int currentHeartCnt)
    {
        //

    }
}
