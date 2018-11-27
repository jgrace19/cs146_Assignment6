using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElevatorDoorTrigger : MonoBehaviour
{
    private Animator myAnimator;
    public GameObject AnimatorObject;
    public AudioSource source;
    public AnimationState state;
    private Pickup pickup;

    // Use this for initialization
    void Start()
    {
        myAnimator = AnimatorObject.GetComponent<Animator>();
        source = GetComponent<AudioSource>();
        pickup = GetComponent<Pickup>();
    }

    void DoorTime()
    {
        if(pickup.thrown == true)
        {
            state.speed = 0;
        } else
        {
            state.speed = 1;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        myAnimator.SetTrigger("DoorClose");
        source.Play();
    }
}
