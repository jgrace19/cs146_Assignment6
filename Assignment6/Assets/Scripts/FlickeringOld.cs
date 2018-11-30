using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlickeringOld : MonoBehaviour {

    REDDOT_OldMovie_PostProcess postprocess;
    public float minWait;
    public float maxWait;

    // Use this for initialization
    void Start()
    {
        postprocess = GetComponent<REDDOT_OldMovie_PostProcess>();
        StartCoroutine(Flashing());

    }

    IEnumerator Flashing()
    {
        while (true)
        {
            yield return new WaitForSeconds(Random.Range(minWait, maxWait));
            postprocess.enabled = !postprocess.enabled;
        }
    }
}
