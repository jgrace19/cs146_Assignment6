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
    private bool bouncing = false;
    private bool movingRight = true;
    Vector3 newRightPos;
    Vector3 newLeftPos;

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
            if (playerInElevator()) { // bounce
                if (!bouncing) {
                    bouncing = true;
                    float yPos = UnityEngine.Random.Range(-10.0f, 10.0f);
                    newRightPos = new Vector3(0.3f, yPos, 0);
                    newLeftPos = new Vector3(-9f, yPos, 0);
                }
                bounceAgainstElevator();
            } else { //follow player
                bouncing = false;
                float speed = Time.deltaTime / 4;
                transform.position = Vector3.Lerp(ghost.transform.position, player.position, speed);
            }
        }
    }

    private void bounceAgainstElevator()
    {
        float speed = Time.deltaTime;
        if (movingRight)  {
            transform.position = Vector3.Lerp(ghost.transform.position, newRightPos, speed);
            if  (transform.position.x >= newRightPos.x - 1) {
                switchDirections();
            }
        } else {
            transform.position = Vector3.Lerp(ghost.transform.position, newLeftPos, speed);
            if (transform.position.x <= newLeftPos.x + 1)
            {
                switchDirections();
            }
        }
    }

    bool playerInElevator() {
        //  return player.transform.position.x > -20 && player.transform.position.x < -13;
        return player.transform.position.x < -13;
    }

    void switchDirections() {
        movingRight = !movingRight;
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
