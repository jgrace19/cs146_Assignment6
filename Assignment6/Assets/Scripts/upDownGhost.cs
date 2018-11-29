using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class upDownGhost : MonoBehaviour {
    int UP = 1;
    int DOWN = -1;

    public float yMax;
    public float yMin;
    private int direction = 1;
    private float moveSpeed = 2f;
    public GameObject ghost;
    public bool upDownGhostsMoving;

	// Use this for initialization
	void Start () {
        upDownGhostsMoving = true;
	}

    // Update is called once per frame
    void FixedUpdate()
    {
        if(upDownGhostsMoving){
            moveUpAndDown();
        }
    }


    void moveUpAndDown(){
        if (ghost.transform.position.y < yMin && direction == DOWN)
        {
            direction = UP;
        }
        if (ghost.transform.position.y > yMax && direction == UP)
        {
            direction = DOWN;
        }
        ghost.transform.position = new Vector2(ghost.transform.position.x, ghost.transform.position.y + moveSpeed * direction);
    }
}

