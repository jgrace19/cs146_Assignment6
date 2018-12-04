using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playBark : MonoBehaviour {

    AudioSource m_AudioSource;
    public PlayerScript playerScript;
    public GameObject AnimatorObject1;
    public GameObject AnimatorObject2;
    private Animator Animator1;
    private Animator Animator2;

    // Use this for initialization
    public void Start()
    {
        
        m_AudioSource = GetComponent<AudioSource>();
        playerScript = GameObject.Find("Player").GetComponent<PlayerScript>();
        playerScript.isLockedforAnimations = true;
        Animator1 = AnimatorObject1.GetComponent<Animator>();
        Animator2 = AnimatorObject2.GetComponent<Animator>();

    }

    public void playBarking()
    {
        m_AudioSource.Play();
        Animator1.Play("Bark1");
        Animator2.Play("Bark2");
        playerScript.isLockedforAnimations = false;

        //GetComponent<PlayerScript>().isLockedforAnimations = false;


    }

    
}
