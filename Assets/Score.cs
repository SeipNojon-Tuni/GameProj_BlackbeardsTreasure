using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Score : MonoBehaviour
{   
    public UIHandler scoreboard;
    public GameObject globalVar;
    private static int score = 0;
    // Start is called before the first frame update
    void Start()
    {
        globalVar = GameObject.Find("GlobalVariables");
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void AddScore() {
        score += 1;
        if (score >= 4)
            if(globalVar) {
                if(globalVar.GetComponent<GlobalVariables>().getVictory() == 1) {
                    SceneManager.LoadScene("VictorySceneAnimals");
                    score = 0;
                }
                else {
                    SceneManager.LoadScene("VictorySceneRobots");
                    score = 0;
                }
            }
            // When running solely Level scene jump to Animal victory so no error occures.
            else {
                SceneManager.LoadScene("VictorySceneAnimals");
            }

        if (scoreboard) {
            scoreboard.AddScore();
        }

    }
}
