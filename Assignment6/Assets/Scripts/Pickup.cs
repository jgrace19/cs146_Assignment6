﻿using System;
using UnityEngine;

public class Pickup : MonoBehaviour
{
    public AudioSource source;

    public AudioSource clockSource;
    public AudioClip clockClip;

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
    float timeLeft;
    Vector3 originalPosition;
    Rigidbody2D pillar;
   // Vector2 stopforce = (0, 0);

    // Use this for initialization
    void Start()
    {
        clockSource.clip = clockClip;
        originalPosition = item.transform.position;
        source = GetComponent<AudioSource>();
        cameralens = GameObject.Find("Main Camera");
        item.GetComponent<Rigidbody2D>().gravityScale = 1;
        getAllGhosts(); // initialize ghosts array to both "elevator ghosts" and "regular ghosts"
        enableElevatorGhosts();
        timeLeft = 10.0f;
        
    }
    // Update is called once per frame
    void Update()
    {
        if (carrying == false)
        {
            if (Input.GetKeyDown(KeyCode.K) && ((guide.transform.position - transform.position).sqrMagnitude < range * range))
            {
                pickup();
                pauseTime();
                carrying = true;
            }
        }
        else if (carrying == true)
        {
            timeLeft -= Time.deltaTime;
            Color lerpedColor = Color.Lerp(Color.white, Color.clear, 1-(.1f*timeLeft));
            item.GetComponent<SpriteRenderer>().color = lerpedColor;
            if (Input.GetKeyDown(KeyCode.K) || timeLeft < 0)
            {
                drop();
                resumeTime();
                carrying = false;
            }
            if (timeLeft < 0) {
                respawn();
            }
        }
    }

    void respawn() {
        timeLeft = 10.0f;
        item.transform.position = originalPosition;
        item.GetComponent<SpriteRenderer>().color = Color.white;
    }

    void pauseTime() {
        source.volume = 1;
        clockSource.Play();
        cameralens.GetComponent<REDDOT_OldMovie_PostProcess>().enabled = true;
        GameObject pillerObj = GameObject.Find("Pillar");
        if (pillerObj != null) {
            pillar = pillerObj.GetComponent<Rigidbody2D>();
            Destroy(pillar);
        }
        //pillar.constraints = RigidbodyConstraints2D.FreezePosition;
        //pillar.constraints = RigidbodyConstraints2D.FreezeRotation;
        //pillar.isKinematic = true;
        GameObject[] platforms;
        platforms = GameObject.FindGameObjectsWithTag("movingPlatform");
        foreach (GameObject platform in platforms) {
            platform.GetComponent<MovingPlatformScript>().setPlatformMoving(false);
        }
        disableGhosts();
    }

    void resumeTime() {
        source.volume = 0;
        cameralens.GetComponent<REDDOT_OldMovie_PostProcess>().enabled = false;
        GameObject[] platforms;
        platforms = GameObject.FindGameObjectsWithTag("movingPlatform");
        foreach (GameObject platform in platforms)
        {
            platform.GetComponent<MovingPlatformScript>().setPlatformMoving(true);
        }
        enableGhosts();
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
            if (script != null) script.StartMoving();
        }
        //special hacky fix for up-down ghosts :/
        GameObject[] upDownGhosts = GameObject.FindGameObjectsWithTag("upDownGhost");
        foreach(GameObject ghost in upDownGhosts){
            upDownGhost script = ghost.GetComponent<upDownGhost>();
            if (script != null) script.upDownGhostsMoving = true;
        }
        //special hacky fix for square ghosts :/
        GameObject[] squareGhosts = GameObject.FindGameObjectsWithTag("squareGhost");
        foreach (GameObject ghost in squareGhosts)
        {
            squareGhost script = ghost.GetComponent<squareGhost>();
            if (script != null) script.squareGhostMoving = true;
        }

        GameObject upDownDoor = GameObject.FindGameObjectWithTag("upDownDoor");
        try {
            upDownDoor doorScript = upDownDoor.GetComponent<upDownDoor>();
            if (doorScript != null) doorScript.upDownDoorMoving = true;
        } catch {
        }

    }

    void disableGhosts()
    {
        foreach (GameObject ghost in ghosts)
        {
            GhostScript script = ghost.GetComponent<GhostScript>();
            if (script != null) script.StopMoving();
        }
        //special hacky fix for up-down ghosts :/
        GameObject[] upDownGhosts = GameObject.FindGameObjectsWithTag("upDownGhost");
        foreach (GameObject ghost in upDownGhosts)
        {
            upDownGhost script = ghost.GetComponent<upDownGhost>();
            if (script != null) script.upDownGhostsMoving = false;
        }
        //special hacky fix for up-down ghosts :/
        GameObject[] squareGhosts = GameObject.FindGameObjectsWithTag("squareGhost");
        foreach (GameObject ghost in squareGhosts)
        {
            squareGhost script = ghost.GetComponent<squareGhost>();
            if (script != null) script.squareGhostMoving = false;
        }

        GameObject upDownDoor = GameObject.FindGameObjectWithTag("upDownDoor");
        if (upDownDoor != null) {
            upDownDoor doorScript = upDownDoor.GetComponent<upDownDoor>();
            if (doorScript != null) doorScript.upDownDoorMoving = false;
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
        item.GetComponent<SpriteRenderer>().color = Color.white;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform == guide) {
            Physics2D.IgnoreCollision(guide.GetComponent<PolygonCollider2D>(), item.GetComponent<Collider2D>());
        }
    }


}
