
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickup : MonoBehaviour
{

    public GameObject item;
    public GameObject tempParent;
    public Transform guide;
    bool carrying;
    public float range = 5;
    public GameObject[] ghosts;

    // Use this for initialization
    void Start()
    {
        item.GetComponent<Rigidbody2D>().gravityScale = 1;
    }
    // Update is called once per frame
    void Update()
    {
        if (carrying == false)
        {
            if (Input.GetKeyDown(KeyCode.K) && ((guide.transform.position - transform.position).sqrMagnitude < range * range))
            {
                pickup();
                carrying = true;
                enableGhosts();
            }
        }
        else if (carrying == true)
        {
            if (Input.GetKeyDown(KeyCode.K))
            {
                drop();
                carrying = false;
            }
        }
    }

    void enableGhosts() {
        ghosts = GameObject.FindGameObjectsWithTag("Ghost");
        foreach (GameObject ghost in ghosts)
        {
            GhostScript script = ghost.GetComponent<GhostScript>();
            script.StartMoving();
        }
    }

    void pickup()
    {
        item.GetComponent<Rigidbody2D>().gravityScale = 0;
        item.GetComponent<Rigidbody2D>().isKinematic = true;
        item.transform.parent = tempParent.transform;
        item.transform.localPosition = new Vector2(item.transform.localPosition.x, item.transform.localPosition.y + 1);
        //item.transform.localPosition.Set(item.transform.localPosition.x, 3);
            }

    void drop()
    {

        item.GetComponent<Rigidbody2D>().gravityScale = 1;
        item.GetComponent<Rigidbody2D>().isKinematic = false;
        item.transform.parent = null;
        item.transform.position.Set(guide.transform.position.x, guide.transform.position.y + 30, guide.transform.position.z);

    }
}

