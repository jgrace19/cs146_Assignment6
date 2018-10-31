using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flash : MonoBehaviour {

    public float intensity = 10;
    public Shader shader;
    public Material material;
    private bool flash = false;

    // Creates a private material used to the effect
    void Awake()
    {
        material = new Material(shader);
    }

    public void StartFlash() {
        flash = true;
    }

    // Postprocess the image
    void OnRenderImage(RenderTexture source, RenderTexture destination)
    {
        Debug.Log("on render image");
        if (!flash)
        {
            Graphics.Blit(source, destination);
            return;
        }
        material.SetFloat("_flash", intensity);
        Graphics.Blit(source, destination, material);
        flash = false;
    }
}
