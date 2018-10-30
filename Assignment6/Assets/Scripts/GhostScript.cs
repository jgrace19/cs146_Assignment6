using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostScript : MonoBehaviour {

    //public Shader testShader = Resources.Load("Shaders/TestShader") as Shader;
    public Shader testShader;
    public Transform player;
    public Transform ghost;

    // Use this for initialization
    void Start () {
        GetComponent<Camera>().SetReplacementShader(testShader, "RenderType");

    }

    // Update is called once per frame
    void Update()
    {
        float speed = Time.deltaTime / 4;
        transform.position = Vector3.Lerp(ghost.position, player.position, speed);
    }
}
