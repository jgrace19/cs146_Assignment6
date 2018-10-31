using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class CreditScript : MonoBehaviour {

    public GameObject creditUI;
	// Use this for initialization
	void Start () {
        Invoke("StartGame", 3);
	}

    void StartGame() {
        creditUI.SetActive(false);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
