using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSoundEffect : MonoBehaviour
{

    public AudioClip walksound;
    public AudioClip climbSound;
    public AudioSource source;
    private bool playedwalkonce = false;
    private bool playedClimbOnce = false;
    public PlayerScript playerScript;
    private bool isClimbing;

    void Start()
    {
        GameObject Player = GameObject.Find("Player");
        playerScript = Player.GetComponent<PlayerScript>();
        source = GetComponent<AudioSource>();
        isClimbing = GetComponent<PlayerScript>().getIsClimbing();
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
