using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class showAnimation : MonoBehaviour {

    Animator m_Animator;

	// Use this for initialization
	void Start () {
        m_Animator = GetComponent<Animator>();
        StartCoroutine(Example());
    }

    IEnumerator Example()
    {
     
        yield return new WaitForSeconds(7);
        m_Animator.Play("Forward");
    }

    // Update is called once per frame

}
