﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour {
    public Rigidbody2D girlRb;
    public GameObject clock;
    [SerializeField]
    private float movementSpeed = 10;
    private bool facingRight;
    public Animator myAnimator;
    private bool isJumping;
    private bool isClimbing;
    public bool isGrounded;
    public float fallMultiplier = 1.05f;
    [SerializeField]
    public float ropeoffset;
    public float jumpForce;
    CircleCollider2D landingRopeRung;
    private float CLIMB_SPEED = .2f;


    // Use this for initialization
    void Start () {
        facingRight = true;
        isJumping = false;
        isGrounded = true;
        isClimbing = false;
        myAnimator = GetComponent<Animator>();
        	}
	
	// Update is called once per frame

    


    private void FixedUpdate()
    {

        if (isGrounded){
            myAnimator.SetLayerWeight(0, 1);
            myAnimator.SetLayerWeight(1, 0);
        }


        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");
        HandleHMovement(horizontal);
        HandleVMovement(vertical);
        Flip(horizontal);
        
    }

    private void HandleHMovement(float horizontal)
    {
        if (girlRb.position.y < -30)
        {
            FindObjectOfType<GameManager>().EndGame();
        }

        if (!isClimbing) {
            girlRb.velocity = new Vector2(horizontal * movementSpeed, girlRb.velocity.y);
        }

        myAnimator.SetFloat("speed", Mathf.Abs(horizontal));

        if (Input.GetKeyDown("space"))
        {
            //Jump off the rope 
            if (isClimbing)
            {
                girlRb.gravityScale = 2;
                isClimbing = false;
                SetClimbingAnimationLayer(false);
            }
            if (!isJumping) Jump();
        }

        if (girlRb.velocity.y < 0)
        {
            girlRb.velocity += Vector2.up * Physics2D.gravity.y * (fallMultiplier - 1);
            Fall();
        }

    }

    private void Flip(float horizontal)
    {
        if (horizontal > 0 && !facingRight || horizontal < 0 && facingRight)
        {
            facingRight = !facingRight;
            Vector3 scale = transform.localScale;
            scale.x *= -1;
            transform.localScale = scale;
        }
    }

    private void Jump()
    {
        isJumping = true;
        isGrounded = false;
        myAnimator.SetTrigger("Jump Into Air");
        //myAnimator.ResetTrigger("Land");
        girlRb.AddForce(jumpForce * Vector2.up, ForceMode2D.Impulse);
    }

    private void Fall()
    {
        myAnimator.SetBool  ("isFalling",true);
    }

    private void Land()
    {
        isJumping = false;
        isClimbing = false;
        isGrounded = true;
        myAnimator.SetTrigger("Land");
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (isGrounded == false & collision.collider.tag == "Ground")
        {
            Land();
        }
        if (collision.collider.tag == "Ghost" || collision.collider.tag == "ElevatorGhost")
        {
            myAnimator.SetTrigger("Dead");
            Destroy(clock);
            FindObjectOfType<GameManager>().EndGame();
        }

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<Collider2D>().tag == "Rope")
        {
            if(!isClimbing){
                girlRb.gravityScale = 0;
                landingRopeRung = collision.GetComponent<CircleCollider2D>();
                Vector3 ropePosition = new Vector3(collision.GetComponent<CircleCollider2D>().transform.position.x + ropeoffset, landingRopeRung.transform.position.y);
                girlRb.position = ropePosition;
                girlRb.velocity = Vector3.zero;
                girlRb.angularVelocity = 0;
                isClimbing = true;
            }
        }

    }

    private void HandleVMovement(float vertical)
    {
        if (Input.GetKey(KeyCode.W) && isClimbing==true)
        {
            Climb(CLIMB_SPEED, vertical);
        }

        if (Input.GetKey(KeyCode.S) && isClimbing==true)
        {
            Climb(-CLIMB_SPEED, vertical);
        }
    }

    private void SetClimbingAnimationLayer(bool climbing){
        if(climbing){
            myAnimator.SetLayerWeight(1, 1);
            myAnimator.SetLayerWeight(0, 0);
        }else{
            myAnimator.SetLayerWeight(1, 0);
            myAnimator.SetLayerWeight(0, 1);
        }
    }

    private void Climb(float climbSpeed, float vertical){
        SetClimbingAnimationLayer(true);
        Vector3 climbVector = new Vector3(landingRopeRung.transform.position.x + ropeoffset, girlRb.transform.position.y + climbSpeed);
        girlRb.position = climbVector;
        myAnimator.SetFloat("speed", Mathf.Abs(vertical));
        myAnimator.SetBool("Climb", true);
        isGrounded = false;
    }
}
