using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZoomToKrakenBounds : MonoBehaviour
{
    [SerializeField] private CinemachineVirtualCamera _krakenVirtualCam;

    private bool _krakenZoomComplete;

    private void Start()
    {
        _krakenVirtualCam.gameObject.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (_krakenZoomComplete) return;

        if (other.CompareTag("Player"))
        {
            _krakenVirtualCam.gameObject.SetActive(true);
            _krakenZoomComplete = true;
        }
    }
}
