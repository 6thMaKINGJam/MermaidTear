using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IntroPanelController : MonoBehaviour
{
    public GameObject firstimage;
    public GameObject startbutton;
    public GameObject subtitlepanel;

    public AudioSource firstaudiosource;
    public AudioSource secondaudiosource;
    //public AudioClip panelsound;
    
    public void showpanel()
    {
        firstaudiosource.Stop();
        firstimage.SetActive(false);
        startbutton.SetActive(false);

        subtitlepanel.SetActive(true);
        
        //AudioSource.PlayClipAtPoint(panelsound, Vector3.zero);
        secondaudiosource.Play();
    }
    
}
