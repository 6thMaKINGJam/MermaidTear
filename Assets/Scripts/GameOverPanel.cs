using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOverPanel : MonoBehaviour
{
    [SerializeField] private GameObject _gameOverPanel;

    private void OnEnable()
    {
        PlayerHealth.OnDead += PlayerIsDead;
    }
    private void OnDisable()
    {
        PlayerHealth.OnDead -= PlayerIsDead;
    }

    private void Start()
    {
        _gameOverPanel.SetActive(false);
    }

    private void PlayerIsDead()
    {
        _gameOverPanel.SetActive(true);
    }
}
