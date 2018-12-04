using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSoundEffect : MonoBehaviour
{

    public AudioClip walksound;
    public AudioClip climbSound;
    public AudioClip jumpSound;
    public AudioSource[] sources;
    public AudioSource walkSource;
    public AudioSource jumpSource;
    private bool playedwalkonce = false;
    private bool playedjumponce = false;
   // private bool playedClimbOnce = false;
    public PlayerScript playerScript;
   // private bool isClimbing;
    private bool isJumping;

    void Start()
    {
        GameObject Player = GameObject.Find("Player");
        playerScript = Player.GetComponent<PlayerScript>();
        sources = GetComponents<AudioSource>();
        walkSource = sources[0];
        jumpSource = sources[1];
        //isClimbing = GetComponent<PlayerScript>().getIsClimbing();
    }

    void Update()
    {
        
        if (Input.GetAxis("Horizontal") != 0 && !playedwalkonce && playerScript.isGrounded == true)
        {
            walkSource.PlayOneShot(walksound);
            playedwalkonce = true;
        }

        if (Input.GetAxis("Horizontal") == 0 || playerScript.isGrounded == false)
        {
            playedwalkonce = false;
            walkSource.Stop(); 
        }


        if (Input.GetKeyDown("space") && !playedjumponce)
        {

            jumpSource.PlayOneShot(jumpSound);
            playedjumponce = true;
        }

        if (playerScript.isGrounded == true)
        {
            playedjumponce = false;
        }

       
    }
}
