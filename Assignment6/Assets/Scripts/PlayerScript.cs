using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour {
    public Rigidbody2D girlRb;
    [SerializeField]
    private float movementSpeed;
    private bool facingRight;
    public Animator myAnimator;
    private bool isJumping;
    private bool isClimbing;
    private bool isGrounded;
    public float fallMultiplier = 1.05f;
    [SerializeField]
    
    public float jumpForce;
    
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
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");
        HandleHMovement(horizontal);
        HandleVMovement(vertical);
        Flip(horizontal);
        
    }

    private void HandleHMovement(float horizontal)
    {
        girlRb.velocity = new Vector2(horizontal * movementSpeed, girlRb.velocity.y);

        myAnimator.SetFloat("speed", Mathf.Abs(horizontal));

        if(Input.GetKeyDown("space"))
        {
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
        if(horizontal > 0 && !facingRight || horizontal < 0 && facingRight)
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
        Debug.Log(collision.collider.tag);
        if (isGrounded == false & collision.collider.tag == "Ground")
        {
            Land();
        }
        if (collision.collider.tag == "Rope")
        {
            isClimbing = true;
        }
    }

    private void HandleVMovement(float vertical)
    {
        if (Input.GetKey(KeyCode.W) && isClimbing == true)
        {
            girlRb.velocity = new Vector2(girlRb.velocity.x, vertical * movementSpeed);
            myAnimator.SetFloat("speed", Mathf.Abs(vertical));
            //Physics2D.gravity = Vector2.zero;
            myAnimator.SetBool("Climb", true);
            isGrounded = false;
        }
    }
}
