using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour {
    public AudioSource auso;
    public AudioClip setClip;

    public void PlayGame ()
    {   
        StartCoroutine(startAfterDelay());
    }

    public void QuitGame ()
    {
        Debug.Log("QUIT!");
        Application.Quit();
    }

    public void StartCredits ()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 2);
    }

    private IEnumerator startAfterDelay() {

        // Wait the parrot sound.
        if(setClip) {
            yield return new WaitForSeconds(setClip.length + 0.2f);
        }

        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }


}
