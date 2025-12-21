using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KrakenPoisonDissipate : MonoBehaviour
{
    // make kraken poison disappear after its animation end

    [SerializeField] private GameObject _poison;
    [SerializeField] private Animator _poisonAnim;

    public void DisablePoison()
    {
        _poison.SetActive(false);
        _poisonAnim.SetBool("activated", false);
    }
}
