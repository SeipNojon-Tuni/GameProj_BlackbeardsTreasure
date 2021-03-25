using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DisplayDialogue : MonoBehaviour
{   
    public GameObject robotUI;
    public GameObject animalUI;
    public GameObject globalVar;

    private int chosenSide = 1;

    public GameObject actorTinhead;
    public GameObject actorBeep;
    public GameObject nameTinhead;
    public GameObject nameBeep;

    public GameObject actorFluffpants;
    public GameObject actorDreadfur;
    public GameObject nameFluffpants;
    public GameObject nameDreadfur;

    public float dialogueTime = 2.5f;
    private bool running = false;

    // Start is called before the first frame update
    void Start()
    {
        robotUI.SetActive(false);
        animalUI.SetActive(false);
        globalVar = GameObject.Find("GlobalVariables");
        
        // Disable all actors by default.
        actorFluffpants.SetActive(false);
        actorDreadfur.SetActive(false);
        nameFluffpants.SetActive(false);
        nameDreadfur.SetActive(false);
        
        actorTinhead.SetActive(false);
        actorBeep.SetActive(false);
        nameTinhead.SetActive(false);
        nameBeep.SetActive(false);

        setDialogueActive();
    }

    public void setDialogueActive() {

        int side = 1;

        if(globalVar){ side = globalVar.GetComponent<GlobalVariables>().getVictory(); }
        else {Debug.Log("No GlobalVariables found!"); }

        if( side == 1) // Animals
        {
            animalUI.SetActive(true);
            chosenSide = 1;

            actorFluffpants.SetActive(true);
            actorDreadfur.SetActive(true);
            nameFluffpants.SetActive(true);
            nameDreadfur.SetActive(true);
        }
        else if (side == 2) // Robots
        {
            robotUI.SetActive(true);
            chosenSide = 2;

            actorTinhead.SetActive(true);
            actorBeep.SetActive(true);
            nameTinhead.SetActive(true);
            nameBeep.SetActive(true);
        }
    }

    public void displayDialogue(string state) 
    {
        // Another dialogue instance in progress. Forfeit this one.
        if(running) {
            return;
        }
        else {
            running = true;
            float weight = Random.Range(0.0f, 1.0f);
            string prefix = "";
            string actorName = "";

            if (chosenSide == 1 && weight >= 0.5) {
                prefix = "AnimalUI";
                actorName = "Cat";
            }
            else if (chosenSide == 1 && weight < 0.5) {
                prefix = "AnimalUI";
                actorName = "Dog";
            }
            else if (chosenSide == 2 && weight >= 0.5) {
                prefix = "RobotUI";
                actorName = "Beep";
            }
            else if (chosenSide == 2 && weight < 0.5) {
                prefix = "RobotUI";
                actorName = "Tin";
            }

            string path = prefix + "/" + state + "/" + state + actorName;
            
            StartCoroutine(displayText(path));
        }
    }

    IEnumerator displayText(string textName) {

        GameObject textElement = GameObject.Find(textName);
        
        if(textElement) { 
            textElement.SetActive(true);
            AudioSource auso = textElement.GetComponent<AudioSource>();

            if(auso) {
                auso.Play();
            }
            yield return new WaitForSeconds(dialogueTime);
            textElement.SetActive(false);
        }

        // Dialogue finished allow new one;
        running = false;
    }
}
