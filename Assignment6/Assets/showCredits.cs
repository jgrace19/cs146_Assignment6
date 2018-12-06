using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class showCredits : MonoBehaviour {

    public void credits()
    {
        SceneManager.LoadScene("CreditsScene");
    }
}
