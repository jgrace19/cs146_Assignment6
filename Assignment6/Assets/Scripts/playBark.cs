using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playBark : MonoBehaviour {

    AudioSource m_AudioSource;
    public PlayerScript playerScript;

    // Use this for initialization
   public void Start()
    {
        
        m_AudioSource = GetComponent<AudioSource>();
        playerScript = GameObject.Find("Player").GetComponent<PlayerScript>();
        playerScript.isLockedforAnimations = true;
        //Random r = new Random();
        //rInt = Random.Range(0, 11);

    }

    public void playBarking()
    {
        m_AudioSource.Play();
        playerScript.isLockedforAnimations = false;

        //GetComponent<PlayerScript>().isLockedforAnimations = false;


    }

    
}
