using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class whatHappened : MonoBehaviour {

    Animator myAnimator;


    // Use this for initialization
    void Start()
    {
        myAnimator = GameObject.Find("What").GetComponent<Animator>();

    }

    void playWhat()
    {
        myAnimator.Play("S2How");
    }
}
