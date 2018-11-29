using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour {
    public bool isLockedforAnimations = false;
    public Rigidbody2D girlRb;
    public GameObject clock;
    [SerializeField]
    private float movementSpeed = 10;
    private bool facingRight;
    public Animator myAnimator;
    private bool isJumping;
    private bool isClimbing;
    public bool isGrounded;
    private bool isFalling;
    public float fallMultiplier = 1.05f;
    [SerializeField]
    public float ropeoffset;
    public float jumpForce;
    CircleCollider2D landingRopeRung;
    private float CLIMB_SPEED = .2f;
    private float platformSpeed = .1f;
    private bool onMovingPlatform = false;
    private Collider2D movingPlatform;

    // Use this for initialization
    void Start () {
        facingRight = true;
        isJumping = false;
        isGrounded = true;
        isClimbing = false;
        isFalling = false;
        myAnimator = GetComponent<Animator>();
        	}
	
	// Update is called once per frame


    public bool IsFacingRight() {
        return this.facingRight;
    }


    private void FixedUpdate()
    {
        if (!isLockedforAnimations)
        {

            if (isGrounded)
            {
                myAnimator.SetLayerWeight(0, 1);
                myAnimator.SetLayerWeight(1, 0);
            }


            float horizontal = Input.GetAxis("Horizontal");
            float vertical = Input.GetAxis("Vertical");
            HandleHMovement(horizontal);
            HandleVMovement(vertical);
            Flip(horizontal);
        }
    }

    private void HandleHMovement(float horizontal)
    {
        Debug.Log(onMovingPlatform);

        if (girlRb.position.y < -30)
        {
            FindObjectOfType<GameManager>().EndGame();
        }

        if (!isClimbing)
        {
            girlRb.velocity = new Vector2(horizontal * movementSpeed, girlRb.velocity.y);
        }

        myAnimator.SetFloat("speed", Mathf.Abs(horizontal));

        if (Input.GetKeyDown("space"))
        {
            //Jump off the rope 
            if (isClimbing)
            {
                JumpOffRope();
            }
            if (!isJumping)
            {
                Jump();
            }
        }

        if (girlRb.velocity.y < 0)
        {
            girlRb.velocity += Vector2.up * Physics2D.gravity.y * (fallMultiplier - 1);
            Fall();
        }

        if (onMovingPlatform)
        {
            MovingPlatformScript movingPlatformScript = movingPlatform.GetComponent<MovingPlatformScript>();
            if(movingPlatformScript.platformMoving)
            {
                int direction = movingPlatformScript.getDirection();

                Vector3 girlMovingPlatformPos = new Vector3(girlRb.transform.position.x + (platformSpeed * direction), girlRb.transform.position.y);
                girlRb.position = girlMovingPlatformPos;
            }

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
        onMovingPlatform = false;
        isJumping = true;
        isGrounded = false;
        myAnimator.SetTrigger("Jump Into Air");
        //myAnimator.ResetTrigger("Land");
        girlRb.AddForce(jumpForce * Vector2.up, ForceMode2D.Impulse);
    }

    private void JumpOffRope() {
        girlRb.gravityScale = 2;
        isClimbing = false;
        SetClimbingAnimationLayer(false);
        isJumping = true;
        isGrounded = false;
        isFalling = true;
        myAnimator.SetTrigger("Jump Into Air");
        Vector2 force;
        if (facingRight) {
            force = new Vector2(50, 0);
        } else {
            force = new Vector2(-50, 0);
        }
        girlRb.AddForce(force, ForceMode2D.Impulse);
    }

    private void Fall()
    {
        myAnimator.SetBool  ("isFalling",true);
        isFalling = true;
    }

    private void Land()
    {
        Debug.Log("landed");
        isJumping = false;
        isClimbing = false;
        isGrounded = true;
        isFalling = false;
        myAnimator.SetTrigger("Land");
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (isGrounded == false & collision.collider.tag == "Ground" || collision.collider.tag == "movingPlatform")
        {
            Land();
        }
        if (collision.collider.tag == "Ghost" || collision.collider.tag == "ElevatorGhost" || collision.collider.tag == "upDownGhost" )
        {
            myAnimator.SetTrigger("Dead");
            Destroy(clock);
            FindObjectOfType<GameManager>().EndGame();
        }

        if(collision.collider.tag == "movingPlatform"){
            onMovingPlatform = true;
            movingPlatform = collision.collider;
        }else{
            onMovingPlatform = false;
            movingPlatform = null;
        }

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<Collider2D>().tag == "Rope")
        {
            if(!isClimbing && !isFalling){
                girlRb.gravityScale = 0;
                landingRopeRung = collision.GetComponent<CircleCollider2D>();
                Vector3 ropePosition = new Vector3(collision.GetComponent<CircleCollider2D>().transform.position.x + ropeoffset, landingRopeRung.transform.position.y);
                girlRb.position = ropePosition;
                girlRb.velocity = Vector3.zero;
                girlRb.angularVelocity = 0;
                isClimbing = true;
            }
        } else if (collision.GetComponent<Collider2D>().tag == "EndClimb") {

            JumpOffRope();
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
        isFalling = false;
    }

    public bool getIsClimbing(){
        return isClimbing;
    }
}
