using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlickeringLight : MonoBehaviour {

    Light light;
    public float minWait;
    public float maxWait;

	// Use this for initialization
	void Start () {
        light = GetComponent<Light>();
        StartCoroutine(Flashing());
		
	}
	
	IEnumerator Flashing()
    {
        while (true)
        {
            yield return new WaitForSeconds(Random.Range(minWait, maxWait));
            light.enabled = !light.enabled;
        }
    }
		
}

