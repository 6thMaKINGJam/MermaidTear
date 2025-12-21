using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class KrakenAttack : MonoBehaviour
{
    private Transform _player;
    private EnemyHealth _krakenHealth;

    [SerializeField] private Transform _aimTarget;
    //[SerializeField] private Transform[] _poisons;
    [SerializeField] private GameObject _poison1;
    [SerializeField] private GameObject _poison2;
    [SerializeField] private float _atkInterval = 1.5f;
    [SerializeField] private Animator _poison1Anim;
    [SerializeField] private Animator _poison2Anim;

    public static event Action StartKrakenAttack;
    private bool _atkIsStarted;
    private bool _aimTargetIsActive;

    private Coroutine _poisonAtkCoroutine;

    private void OnEnable()
    {
        StartKrakenAttack += KrakenMove_StartKrakenAttack;
    }
    private void OnDisable()
    {
        StartKrakenAttack -= KrakenMove_StartKrakenAttack;
    }

    private void FixedUpdate()
    {
        //if (_atkIsStarted && !_aimTarget.gameObject.activeSelf)
            //_aimTarget.gameObject.SetActive(true);

        aimTargetFollowPlayer();

        if (_poisonAtkCoroutine != null)
            if (_krakenHealth.CurrentHealth <= 0)
            {
                StopCoroutine(_poisonAtkCoroutine);
                SceneManager.LoadScene("Level 3");
            }
    }

    private void Start()
    {
        _atkIsStarted = false;
        _player = GameObject.FindWithTag("Player").transform;
        _krakenHealth = GetComponent<EnemyHealth>();
        setActivePoisons(false);
        currInt = 2;
        _aimTarget.gameObject.SetActive(false);
        _aimTargetIsActive = false;
    }

    public static void InvokeStartKrakenAttack()
    {
        StartKrakenAttack?.Invoke();
    }

    private void aimTargetFollowPlayer()
    {
        if (_atkIsStarted)
            _aimTarget.position = new Vector3(_player.position.x, _player.position.y, _aimTarget.position.z);
    }

    private void KrakenMove_StartKrakenAttack()
    {
        Debug.Log("Kraken attack started");
        _atkIsStarted = true;
        _aimTargetIsActive = true;
        _aimTarget.gameObject.SetActive(true);

        _poisonAtkCoroutine = StartCoroutine(atkWithInterval());
    }

    private void setActivePoisons(bool active)
    {
        _poison1.SetActive(active);
        _poison2.SetActive(active);
    }

    private int currInt = 1;
    private IEnumerator atkWithInterval()
    {
        while (true)
        {
            yield return new WaitForSeconds(_atkInterval);

            // do the attack
            if (currInt == 2)
            {
                currInt = 1;
                _poison1.SetActive(true);
                _poison1.transform.position = new Vector3(_player.position.x, _player.position.y, _aimTarget.position.z);
                _poison1Anim.SetBool("activated", true);
            }
            else
            {
                currInt = 2;
                _poison2.SetActive(true);
                _poison2.transform.position = new Vector3(_player.position.x, _player.position.y, _aimTarget.position.z);
                _poison2Anim.SetBool("activated", true);
            }
        }
    }
}
