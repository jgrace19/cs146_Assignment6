using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostScript : MonoBehaviour {

    //public Shader testShader = Resources.Load("Shaders/TestShader") as Shader;
    public Shader testShader;

    // Use this for initialization
    void Start () {
        GetComponent<Camera>().SetReplacementShader(testShader, "RenderType");

    }

    // Update is called once per frame
    void Update () {
		
	}
}
