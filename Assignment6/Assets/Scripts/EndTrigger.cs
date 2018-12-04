using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndTrigger : MonoBehaviour {

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("Triggered");
        FindObjectOfType<GameManager>().CompleteLevel();
        GameObject MainCamera = GameObject.FindGameObjectWithTag("MainCamera");
        MainCamera.active = false;

    }
}
