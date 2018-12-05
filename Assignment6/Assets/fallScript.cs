using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class fallScript : MonoBehaviour {

    public float force = 300;
    PlayerScript playerScript;
    AudioSource m_Source;


    // Use this for initialization
    void Start()
    {
        playerScript = GameObject.Find("Player").GetComponent<PlayerScript>();
        m_Source = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    public void Fall() {
        playerScript.isLockedforAnimations = false;
        m_Source.Play();
        Rigidbody2D rB = gameObject.GetComponent<Rigidbody2D>();
        //Debug.Log("force applied");
        rB.AddForce(Vector2.left * force);
	}
}
