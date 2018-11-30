using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndTriggerTutorial : MonoBehaviour {

    Animator m_Animator;

    // Use this for initialization
    void Start()
    {
        m_Animator = GameObject.Find("Panel - End").GetComponent<Animator>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("Triggered");
        m_Animator.Play("Tutorial End");
    }
}
