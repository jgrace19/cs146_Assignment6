using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatformScript : MonoBehaviour {
    private int RIGHT = 1;
    private int LEFT = -1;
    private float moveSpeed = .2f;

    [SerializeField] 
    public float XMax;

    [SerializeField]
    public float XMin;

    private int direction;
    public Transform platform;

    public bool platformMoving;

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

    public void setPlatformMoving(bool moving){
        platformMoving = moving;
    }

    public int getDirection(){
        return direction;
    }

}
