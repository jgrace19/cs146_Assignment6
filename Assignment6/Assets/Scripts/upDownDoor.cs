using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class upDownDoor : MonoBehaviour
{
    int UP = 1;
    int DOWN = -1;

    public float yMax;
    public float yMin;
    private int direction = 1;
    private float moveSpeed = 2f;
    public GameObject door;
    public bool upDownDoorMoving;

    // Use this for initialization
    void Start()
    {
        upDownDoorMoving = true;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (upDownDoorMoving)
        {
            moveUpAndDown();
        }
    }


    void moveUpAndDown()
    {
        if (door.transform.position.y < yMin && direction == DOWN)
        {
            direction = UP;
        }
        if (door.transform.position.y > yMax && direction == UP)
        {
            direction = DOWN;
        }
        door.transform.position = new Vector2(door.transform.position.x, door.transform.position.y + moveSpeed * direction);
    }
}

