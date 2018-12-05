using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playScream : MonoBehaviour {

    AudioSource mySource;
    Animator myAnimator;


    // Use this for initialization
    void Start()
    {
        mySource = GetComponent<AudioSource>();
        myAnimator = GameObject.Find("Panel - End").GetComponent<Animator>();

    }

    void scream()
    {
        mySource.Play();
        myAnimator.Play("End");
    }
}
