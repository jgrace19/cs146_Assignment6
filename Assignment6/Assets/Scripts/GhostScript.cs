using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostScript : MonoBehaviour
{

    //public Shader testShader = Resources.Load("Shaders/TestShader") as Shader;
    public Shader testShader;
    public Transform player;
    public GameObject ghost;
    public Flash flashScript;

    private bool isEnabled = false;
    private bool bouncing = false;
    private bool movingRight = true;
    private bool isElevatorGhost;
    Vector3 newRightPos;
    Vector3 newLeftPos;
    Shader movingShader;
    Shader stoppedShader;
    Renderer rend;

    // Use this for initialization
    void Start()
    {
        isElevatorGhost = (ghost.tag == "ElevatorGhost");
        rend = GetComponent<Renderer>();
        movingShader = Shader.Find("Custom/ConcentricCircles");
        stoppedShader = Shader.Find("Custom/PausedConcentricCircles");
    }

    public void StartMoving()
    {
        isEnabled = true;
        if (movingShader != null) {
            rend.material.shader = movingShader;

        }
    }

    public void StopMoving()
    {
        isEnabled = false;
        rend.material.shader = stoppedShader;

    }

    // Update is called once per frame
    void Update()
    {
        if (isEnabled)
        {
            if (playerInElevator()) // bounce
            {
                ghostPlayerInElevator();
            }
            else if (!isElevatorGhost) //follow player, but don't want elevator ghosts to follow player unless in elevator
            {
                bouncing = false;
                followPlayer();
            }
        }
    }

    void ghostPlayerInElevator()
    {
        if (isElevatorGhost)
        {
            Debug.Log("elevator ghost");
            followPlayer();
        }
        else
        {
            if (!bouncing)
            {
                bouncing = true; // Choose new random location to go to to "bounce"
                float yPos = UnityEngine.Random.Range(-10.0f, 10.0f);
                newRightPos = new Vector3(0.3f, yPos, 0);
                newLeftPos = new Vector3(-9f, yPos, 0);
            }
            bounceAgainstElevator();
        }
    }

    private void followPlayer()
    {
        float speed = Time.deltaTime / 2;
        transform.position = Vector3.Lerp(ghost.transform.position, player.position, speed);
    }

    private void bounceAgainstElevator()
    {
        float speed = Time.deltaTime;
        if (movingRight)
        {
            transform.position = Vector3.Lerp(ghost.transform.position, newRightPos, speed);
            if (transform.position.x >= newRightPos.x - 1)
            {
                switchDirections();
            }
        }
        else
        {
            transform.position = Vector3.Lerp(ghost.transform.position, newLeftPos, speed);
            if (transform.position.x <= newLeftPos.x + 1)
            {
                switchDirections();
            }
        }
    }

    bool playerInElevator()
    { // move to player eventually
        //  return player.transform.position.x > -20 && player.transform.position.x < -13;
        return player.transform.position.x < -13;
    }

    void switchDirections()
    {
        movingRight = !movingRight;
    }
}

