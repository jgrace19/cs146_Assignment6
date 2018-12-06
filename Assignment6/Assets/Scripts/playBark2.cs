﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playBark2 : MonoBehaviour
{

    AudioSource m_AudioSource;
    AudioSource pebble_AudioSource;
    public GameObject AnimatorObject1;
    public GameObject AnimatorObject2;
    private Animator Animator1;
    private Animator Animator2;
    private Rigidbody2D pebble;
    public float pebblethrust;

    // Use this for initialization
    public void Start()
    {

        m_AudioSource = GetComponent<AudioSource>();
        Animator1 = AnimatorObject1.GetComponent<Animator>();
        Animator2 = AnimatorObject2.GetComponent<Animator>();
        pebble = GameObject.FindGameObjectWithTag("Pebble").GetComponent<Rigidbody2D>();
        pebble_AudioSource = GameObject.FindGameObjectWithTag("Pebble").GetComponent<AudioSource>();
        


    }

    public void playBarking()
    {
        m_AudioSource.Play();
        Animator1.Play("Bark3");
        Animator2.Play("Bark4");

        //GetComponent<PlayerScript>().isLockedforAnimations = false;


    }

    public void dropRocks()
    {
        pebble.isKinematic = false;
        pebble_AudioSource.Play();


    }


}