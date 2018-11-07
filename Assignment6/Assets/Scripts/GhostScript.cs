using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostScript : MonoBehaviour {

    //public Shader testShader = Resources.Load("Shaders/TestShader") as Shader;
    public Shader testShader;
    public Transform player;
    public GameObject ghost;
    public Flash flashScript;

    private bool isEnabled = false;
    private bool movingRight = true;
    Vector3 newPos;

    // Use this for initialization
    void Start () {
    }

    private void Awake()
    {
        var boxCollider = ghost.GetComponent<BoxCollider2D>();
        boxCollider.isTrigger = true;
    }

    public void StartMoving() {
        isEnabled = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (isEnabled)
        {
            if (ghost.transform.position.x > -6) {
                float speed = Time.deltaTime / 4;
                transform.position = Vector3.Lerp(ghost.transform.position, player.position, speed);
            }
        }
    }

    private void bounceAgainstElevator()
    {
        float speed = Time.deltaTime / 4;
        if (movingRight)  {
            transform.position = Vector3.Lerp(ghost.transform.position, newPos, speed);
            if  (transform.position == newPos) {
                switchDirections();
            }
            movingRight = false;
        } else {
            movingRight = true;
        }
    }

    void switchDirections() {
        movingRight = !movingRight;
        float yPos = UnityEngine.Random.Range(-10.0f, 10.0f);
         newPos = new Vector3(6.95f, yPos, 0);
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        Debug.Log("collision");
        if (col.transform == player) {
            Debug.Log("hit player");
          // flashScript.StartFlash();
        }
    }
}
