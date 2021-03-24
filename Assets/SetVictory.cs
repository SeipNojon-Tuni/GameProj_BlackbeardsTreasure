using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetVictory : MonoBehaviour
{   
    private GameObject globalVar;
    private GlobalVariables script;
    // Start is called before the first frame update
    void Start()
    {
        globalVar = GameObject.Find("GlobalVariables");
    }

    public void setVictoryAliens() {
        if(globalVar) {
            globalVar.GetComponent<GlobalVariables>().setVictory(2);
        }
    }

    public void setVictoryAnimals() {
        if(globalVar) {
            globalVar.GetComponent<GlobalVariables>().setVictory(1);
        }
    }
}
