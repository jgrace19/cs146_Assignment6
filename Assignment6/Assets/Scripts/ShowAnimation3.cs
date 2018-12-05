using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowAnimation3 : MonoBehaviour
{

    Animator m_Animator;

    // Use this for initialization
    void Start()
    {
        m_Animator = GameObject.Find("Down").GetComponent<Animator>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        m_Animator.Play("Down There");
    }

}
