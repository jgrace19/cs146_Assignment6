using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class finalTrigger : MonoBehaviour {

    Animator myAnimator;
    PlayerScript playerScript;
    Animator playerAnimator;


	// Use this for initialization
	void Start () {
		myAnimator = GameObject.Find("Luna").GetComponent<Animator>();
        playerScript = GameObject.Find("Player").GetComponent<PlayerScript>();
        playerAnimator = GameObject.Find("Player").GetComponent<Animator>();

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        playerAnimator.Play("Idle");
        
        playerScript.isLockedforAnimations = true;
        myAnimator.Play("S2Luna");
    }
}
