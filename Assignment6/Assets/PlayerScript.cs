using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour {
    public Rigidbody2D girlRb;

    [SerializeField]
    private float movementSpeed;

    private bool facingRight;
    private Animator myAnimator;
    private bool jump;
    [SerializeField]
    private float jumpForce;

	// Use this for initialization
	void Start () {
        facingRight = true;
        myAnimator = GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update () {
        HandleInput();
	}

    private void FixedUpdate()
    {
        float horizontal = Input.GetAxis("Horizontal");
        HandleMovement(horizontal);
        Flip(horizontal);
        ResetValues();
    }

    private void HandleMovement(float horizontal)
    {
        girlRb.velocity = new Vector2(horizontal * movementSpeed, girlRb.velocity.y);

        myAnimator.SetFloat("speed", Mathf.Abs(horizontal));

        if(jump)
        {
            girlRb.AddForce(new Vector2(0, jumpForce));
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

    private void HandleInput()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            jump = true;
        }
    }

    private void ResetValues()
    {
        jump = false;
    }
}
