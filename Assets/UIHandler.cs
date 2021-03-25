using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIHandler : MonoBehaviour
{   
    public Text health_display;
    public GameObject currentTarget;
    public Text score_display;
    public Text weaponRdyText;

    private int score;
    public Image primary;
    public Image secondary;

    public Sprite cannon;
    public Sprite barrage;
    public Sprite fireBarrage;
    public Sprite barrel;

    public AudioSource auso;
    public AudioClip reloadSound;
    public AudioClip chWeapon;
    
    private Image activeWeapon;
    private Color refColor;

    // Start is called before the first frame update
    void Start()
    {
        primary.sprite = barrage;
        secondary.sprite = fireBarrage;

        primary.enabled = true;
        secondary.enabled = false;

        weaponRdyText.enabled = false;

        activeWeapon = primary;
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
            
            activeWeapon = primary;
        } 
        else {
            primary.enabled = false;
            secondary.enabled = true;
            
            activeWeapon = secondary;
        }

        if(auso) {
            auso.clip = chWeapon;
            auso.Play();
        }
    }

    public void reloadCycle(float time) {

        // Enable corresponding empty image.
        if(activeWeapon == primary) {
            //primaryEmpty.enabled = true;
            refColor = primary.color;
        }
        else {
            //secondaryEmpty.enabled = true;
            refColor = secondary.color;
        }
        
        StartCoroutine(imageLerp(activeWeapon, time));
        
    }

    IEnumerator imageLerp(Image lastActive, float time) {

        for (float ft = 0; ft < time; ft += 0.2f) {
            float current = ft / time;
            lastActive.color = new Color(Mathf.Lerp(0.95f, refColor.r, current), Mathf.Lerp(0.95f, refColor.b, current),  Mathf.Lerp(0.95f, refColor.g, current), Mathf.Lerp(0.5f, 1.0f, current));
            yield return new WaitForSeconds(.2f);
        }

        // Play reloaded sound
        auso.clip = reloadSound;
        auso.Play();

        StartCoroutine(displayReady());
    }


    IEnumerator displayReady() {
        weaponRdyText.enabled = true;
        yield return new WaitForSeconds(1.0f);
        weaponRdyText.enabled = false;
    }

    public void AddScore() {
        score += 1;
    }

}
