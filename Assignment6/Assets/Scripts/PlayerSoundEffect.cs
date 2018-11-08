using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSoundEffect : MonoBehaviour
{

    public AudioClip walksound;
    public AudioSource source;
    private bool playedwalkonce = false;
    public PlayerScript playerScript;

    void Start()
    {
        GameObject Player = GameObject.Find("Player");
        playerScript = Player.GetComponent<PlayerScript>();
        source = GetComponent<AudioSource>();
    }

    void Update()
    {
        if (Input.GetAxis("Horizontal") != 0 && !playedwalkonce && playerScript.isGrounded == true)
        {
            source.PlayOneShot(walksound);
            playedwalkonce = true;


        }
        if (Input.GetAxis("Horizontal") == 0 || playerScript.isGrounded == false)
        {
            playedwalkonce = false;
            source.Stop();
        }
    }
}
