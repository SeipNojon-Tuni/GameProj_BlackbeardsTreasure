using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIHandler : MonoBehaviour
{   
    public Text health_display;
    public GameObject currentTarget;
    public Text score_display;
    private int score;
    public Image primary;
    public Image secondary;

    public Sprite cannon;
    public Sprite barrage;
    public Sprite fireBarrage;
    public Sprite barrel;

    // Start is called before the first frame update
    void Start()
    {
        primary.sprite = barrage;
        secondary.sprite = fireBarrage;

        primary.enabled = true;
        secondary.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {   

        // Set health display.
        if(health_display && currentTarget) {
            health_display.text = currentTarget.GetComponent<ShipHealth>().getCurrentHealth().ToString();
        }

        // Set score
        score_display.text = score.ToString();
    }

    // Set tracked player and current weapon sprite accordingly.
    public void setCurrentTarget(GameObject target) {
        currentTarget = target;
        if(currentTarget.name == "ManOWarPlayer1" || currentTarget.name == "ManOWarPlayer2") {
            primary.sprite = barrage;
            secondary.sprite = fireBarrage;
        }
        else {
            primary.sprite = cannon;
            secondary.sprite = barrel;
        }

    }

    public void changeWeapon(string weapon) {
        if(weapon == "Primary") {
            primary.enabled = true;
            secondary.enabled = false;
        } 
        else {
            primary.enabled = false;
            secondary.enabled = true;
        }
    }

    public void AddScore() {
        score += 1;
    }

}
