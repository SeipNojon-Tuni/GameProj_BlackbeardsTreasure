﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ReturnButton : MonoBehaviour {
    public void Return ()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 2);
        Score.score = 0;
    }

}
