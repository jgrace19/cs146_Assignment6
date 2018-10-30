using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostScript : MonoBehaviour {

    //public Shader testShader = Resources.Load("Shaders/TestShader") as Shader;
    public Shader testShader;
    public Transform player;
    public GameObject ghost;
    public Camera camera;

    // Use this for initialization
    void Start () {
        camera.SetReplacementShader(testShader, "RenderType");
    }

    private void Awake()
    {
        var boxCollider = ghost.GetComponent<BoxCollider2D>();
        boxCollider.isTrigger = true;

    }

    // Update is called once per frame
    void Update()
    {
        float speed = Time.deltaTime / 4;
        transform.position = Vector3.Lerp(ghost.transform.position, player.position, speed);
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        Debug.Log("collision");
        if (col.transform == player) {
            Debug.Log("hit player");
        }
    }
}
