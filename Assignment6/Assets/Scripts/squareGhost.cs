using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class squareGhost : MonoBehaviour {
    private int LEFT = -1;
    private int RIGHT = 1;
    private int UP = 1;
    private int DOWN = -1;
    private float moveSpeed = .2f;

    public float xMax;
    public float yMax;
    public float xMin;
    public float yMin;
    public int xDirection;
    public int yDirection;
    public bool movingSideways;
    public GameObject ghost;
    public bool squareGhostMoving;

    // Use this for initialization
    void Start () {
        xDirection = RIGHT;
        movingSideways = true;
        moveSpeed = Random.Range(.1f, .4f);
        squareGhostMoving = true;
	}
	
	// Update is called once per frame
	void FixedUpdate () {
        if(squareGhostMoving){
            move();
        }
	}

    private void move()
    {
        if (movingSideways)
        {
            if (ghost.transform.position.x > xMax && xDirection == RIGHT)
            {
                yDirection = UP;
                movingSideways = false;
            }
            else if (ghost.transform.position.x < xMin && xDirection == LEFT)
            {
                yDirection = DOWN;
                movingSideways = false;
            }
            ghost.transform.position = new Vector2(ghost.transform.position.x + moveSpeed * xDirection, ghost.transform.position.y);
        }
        else
        {
            if (ghost.transform.position.y > yMax && yDirection == UP)
            {
                xDirection = LEFT;
                movingSideways = true;
            }
            else if (ghost.transform.position.y < yMin && yDirection == DOWN)
            {
                xDirection = RIGHT;
                movingSideways = true;
            }
            ghost.transform.position = new Vector2(ghost.transform.position.x, ghost.transform.position.y + moveSpeed * yDirection);
        }
    }
}
