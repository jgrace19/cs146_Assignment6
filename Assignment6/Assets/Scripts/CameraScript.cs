using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour {

    public Material EffectMaterial;


    private void OnRenderImage(RenderTexture source, RenderTexture destination)
    {
        Debug.Log("rendered image");
       // Graphics.Blit(source, destination, EffectMaterial);
    }
}
