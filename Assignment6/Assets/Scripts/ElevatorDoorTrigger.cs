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
        float startTime = Time.time;
        Vector3 newPos = new Vector3(AnimatorObject.transform.position.x, -39.1f, AnimatorObject.transform.position.z);
        float distCovered = (Time.time - startTime);
        float journeyLength = Vector3.Distance(AnimatorObject.transform.position, newPos);
        AnimatorObject.transform.position = Vector3.Lerp(AnimatorObject.transform.position, newPos, 1);
       // myAnimator.SetTrigger("DoorClose");
        source.Play();
    }
}
