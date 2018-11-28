using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatformScript : MonoBehaviour {
    private int RIGHT = 1;
    private int LEFT = -1;
    private float moveSpeed = .1f;

    [SerializeField] 
    private float XMax;

    [SerializeField]
    private float XMin;

    private int direction;
    public Transform platform;

    private bool platformMoving;

    // Use this for initialization
    void Start () {
        direction = LEFT;
        platformMoving = true;
	}

    // Update is called once per frame
    void FixedUpdate()
    {
        if(platformMoving){
            movePlatform();
        }
    }


    void movePlatform(){
        if (platform.position.x < XMin && direction == LEFT)
        {
            direction = RIGHT;
        }
        if (platform.position.x > XMax && direction == RIGHT)
        {
            direction = LEFT;
        }
        platform.position = new Vector2(platform.position.x + moveSpeed * direction, platform.position.y);
    }

    void setPlatformMoving(bool moving){
        platformMoving = moving;
    }

    public int getDirection(){
        return direction;
    }

}
