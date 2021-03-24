using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class VictoryScreen : MonoBehaviour
{

    public void ReturnToMenu()
    {
        SceneManager.LoadScene("MainMenu (Test");
    }

    public void QuitGame()
    {
        Debug.Log("Quit");
        Application.Quit();
    }



}
