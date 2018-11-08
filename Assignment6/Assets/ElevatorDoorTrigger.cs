using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElevatorDoorTrigger : MonoBehaviour
{
    private Animator myAnimator;
    public GameObject AnimatorObject;
    public AudioSource source; 

    // Use this for initialization
    void Start()
    {
        myAnimator = AnimatorObject.GetComponent<Animator>();
        source = GetComponent<AudioSource>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("Triggered");
        myAnimator.SetTrigger("DoorClose");
        source.Play();
    }
}
