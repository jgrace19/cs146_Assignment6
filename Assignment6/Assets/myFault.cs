using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class myFault : MonoBehaviour {

    Animator myAnimator;

    // Use this for initialization
    void Start()
    {
        myAnimator = GameObject.Find("Fault").GetComponent<Animator>();

    }

    void playFault()
    {
        myAnimator.Play("S2Fault");
    }
}
