using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndTrigger : MonoBehaviour {

    public AudioSource[] sources;

    private void Start()
    {
        sources = GetComponents<AudioSource>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        sources[0].Play();
        sources[1].Play();
        FindObjectOfType<GameManager>().CompleteLevel();
        GameObject MainCamera = GameObject.FindGameObjectWithTag("MainCamera");
        MainCamera.active = false;
    }
}
