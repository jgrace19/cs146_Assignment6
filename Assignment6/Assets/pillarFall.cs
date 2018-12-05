using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pillarFall : MonoBehaviour {

	void triggerFall () {
        fallScript fallScript = FindObjectOfType<fallScript>();
        fallScript.Fall();
    }
	

}
