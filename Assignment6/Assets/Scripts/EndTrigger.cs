using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndTrigger : MonoBehaviour {

    public AudioSource[] sources;
    Animator myAnimator;

    void Start()
    {
        myAnimator = GameObject.Find("Panel - S1End").GetComponent<Animator>();

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("triggered");
        myAnimator.Play("S1End");
    }
}
