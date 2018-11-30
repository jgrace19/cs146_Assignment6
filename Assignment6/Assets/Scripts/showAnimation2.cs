using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class showAnimation2 : MonoBehaviour {

    Animator m_Animator;

    // Use this for initialization
    void Start () {
        m_Animator = GameObject.Find("Jump").GetComponent<Animator>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("Triggered");
        m_Animator.Play("Tutorial Jump");
    }

}
