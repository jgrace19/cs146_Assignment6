using System;
using UnityEngine;

public class Pickup : MonoBehaviour
{
    public AudioSource source;
    private bool playedmusiconce = false;
    public GameObject item;
    public GameObject cameralens;
    public GameObject tempParent;
    public Transform guide;
    bool carrying;
    public bool thrown = false;
    public float thrust = 100;
    public float range = 5;
    public GameObject[] ghosts;

    // Use this for initialization
    void Start()
    {
        source = GetComponent<AudioSource>();
        cameralens = GameObject.Find("Main Camera");
        item.GetComponent<Rigidbody2D>().gravityScale = 1;
        getAllGhosts(); // initialize ghosts array to both "elevator ghosts" and "regular ghosts"
        enableElevatorGhosts();
    }
    // Update is called once per frame
    void Update()
    {
        Debug.Log(thrown);
        if (carrying == false)
        {
            if (Input.GetKeyDown(KeyCode.K) && ((guide.transform.position - transform.position).sqrMagnitude < range * range))
            {
                pickup();
                source.volume = 0;
                cameralens.GetComponent<REDDOT_OldMovie_PostProcess>().enabled = false;
                carrying = true;
                enableGhosts();
            }
        }
        else if (carrying == true)
        {           
            if (Input.GetKeyDown(KeyCode.K))
            {
                drop();
                source.volume = 1;
                cameralens.GetComponent<REDDOT_OldMovie_PostProcess>().enabled = true;
                disableGhosts();
                carrying = false;
            }
        }
    }

    void enableElevatorGhosts() {
        GameObject[] elevatorGhosts = GameObject.FindGameObjectsWithTag("ElevatorGhost");
        foreach (GameObject ghost in elevatorGhosts)
        {
            GhostScript script = ghost.GetComponent<GhostScript>();
            script.StartMoving();
        }
    }

    void enableGhosts()
    {
        foreach (GameObject ghost in ghosts)
        {
            GhostScript script = ghost.GetComponent<GhostScript>();
            script.StartMoving();
        }
    }

    void disableGhosts()
    {
        foreach (GameObject ghost in ghosts)
        {
            GhostScript script = ghost.GetComponent<GhostScript>();
            script.StopMoving();
        }

    }

    void getAllGhosts()
    {
        GameObject[] regGhosts = GameObject.FindGameObjectsWithTag("Ghost");
        GameObject[] elevatorGhosts = GameObject.FindGameObjectsWithTag("ElevatorGhost");
        ghosts = new GameObject[regGhosts.Length + elevatorGhosts.Length];
        Array.Copy(regGhosts, ghosts, regGhosts.Length);
        Array.Copy(elevatorGhosts, 0, ghosts, regGhosts.Length, elevatorGhosts.Length);
    }

    void pickup()
    {
        item.GetComponent<Rigidbody2D>().gravityScale = 0;
        item.GetComponent<Rigidbody2D>().isKinematic = true;
        item.transform.parent = tempParent.transform;
        item.transform.localPosition = new Vector2(item.transform.localPosition.x, item.transform.localPosition.y + 1);
        thrown = false;
    }

    void drop()
    {
        item.GetComponent<Rigidbody2D>().isKinematic = false;
        item.GetComponent<Rigidbody2D>().gravityScale = 1;
        item.transform.parent = null;
        //item.transform.position.Set(guide.transform.position.x, guide.transform.position.y, guide.transform.position.z);
        //item.transform.localPosition = new Vector2(0, 0);
        float xForce = guide.GetComponent<PlayerScript>().IsFacingRight() ? 300 : -300;
        item.GetComponent<Rigidbody2D>().AddForce(new Vector2(xForce, 300f));
        thrown = true;
    }


}
