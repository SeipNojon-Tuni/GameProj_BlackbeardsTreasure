using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EnemyScore : MonoBehaviour
{   
    // Enemy Score is ship specific
    private int score = 0;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {

    }
    public void resetScore() {
        score = 0;
    }
 
    public void AddScore() {
        score += 1;
    }
    public int getScore() {
        return score;
    }
}
