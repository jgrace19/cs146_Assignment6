using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSoundEffect : MonoBehaviour
{

    public AudioClip walksound;
    public AudioSource source;
    private bool playedwalkonce = false;

    void Start()
    {
        source = GetComponent<AudioSource>();
    }

    void Update()
    {
        if (Input.GetAxis("Horizontal") != 0 && !playedwalkonce)
        {
            source.PlayOneShot(walksound);
            playedwalkonce = true;


        }
        if (Input.GetAxis("Horizontal") == 0)
        {
            playedwalkonce = false;
            source.Stop();
        }
    }
}
