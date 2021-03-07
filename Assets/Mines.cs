using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mines : MonoBehaviour
{
    private Camera cam;
    public Rigidbody pallo;
    public float vel_mult = 160;
    public float reload_time;
    public AudioClip sound;
    public AudioSource auso;  

    private float diffx;
    private float diffy;
    private float x;
    private float y;
    private float kulma;
    private float distance;
    private bool reloading;

    void Start()
    { cam =Camera.main;

    }
    void Update()
    {
    // Secondary fire - barrels
    if (Input.GetButtonDown("Fire2"))
        {   
            if (!reloading) {
                auso.clip = sound;
                reloading = true;
                auso.Play(0);
        
                for (int i = 0; i < 5; i++) {

                    float time = i*0.2f;

                    Invoke("ExecuteAfterTime", time);
                }
                // Set weapons reloaded after delay.
                Invoke("SetReloaded", reload_time);
            }
        }
    }
    void ExecuteAfterTime()
    {   
        float z_off = Random.Range(-10.0f, 10.0f);

        Rigidbody clone;
        Vector3 pos = transform.position -15*transform.forward;

        clone = Instantiate(pallo, pos, new Quaternion(0, 0 , 0, 1));
        clone.AddRelativeForce(-transform.forward*vel_mult);

    }
    void SetReloaded() {
        reloading = false;
    }
}