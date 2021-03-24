using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Score : MonoBehaviour
{   
    public UIHandler scoreboard;
    private int score = 0;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void AddScore() {
        score += 1;
        if (score == 4)
            SceneManager.LoadScene("VictorySceneAnimals");

        if (scoreboard) {
            scoreboard.AddScore();
        }

    }
}
