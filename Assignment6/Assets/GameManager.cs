using UnityEngine;
using UnityEngine.SceneManagement;//reload or start a new scene

public class GameManager : MonoBehaviour
{

    public float restartDelay = 1;
    public GameObject completeLevelUI;
    public GameObject horrorImageUI;
    public GameObject horrorImage2;
    private int rInt;

    // Use this for initialization
    void Start () {

        //Random r = new Random();
        rInt = Random.Range(0,11);

    }

    // Update is called once per frame
    //void Update () {

    //}

    bool gameHasEnded = false;//switch to ensure game ends once

    public void CompleteLevel()
    {
        completeLevelUI.SetActive(true);
    }

    public void EndGame()//ensure this is public
    {
        if (gameHasEnded == false)
        {
            gameHasEnded = true;
            Debug.Log("Game Over");
            Debug.Log(rInt);
            if (rInt > 7) {
                horrorImage2.SetActive(true);
            } else {
                horrorImageUI.SetActive(true);
            }

            Invoke("Restart", restartDelay);//invoke delays the launch of the Restart Method
        }
    }

    void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);//load the scene that's currently active = RELOAD
    }
}
