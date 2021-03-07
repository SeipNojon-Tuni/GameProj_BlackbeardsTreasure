using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Barrage : MonoBehaviour
{
    private Camera cam;
    public Rigidbody pallo;
    public float vel_mult = 5000;
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
    private ParticleSystem smoke_left;
    private ParticleSystem smoke_right;

    void Start()
    { cam =Camera.main;

        reloading = false;

        // Get cannon fire particles.
        ParticleSystem[] smokes = GetComponentsInChildren<ParticleSystem>();

        foreach(var system in smokes) {

            if (system.gameObject.name == "Smoke_left") {
                smoke_left = system;
            }
            else if(system.gameObject.name == "Smoke_right") {
                smoke_right = system;
            }
        }

    }
    void Update()
    {
    Vector2 mousePos = new Vector2();
    // Primary fire
    if (Input.GetButtonDown("Fire1"))
        {   
            if (!reloading) {
        
                for (int i = 0; i < 10; i++) {

                    auso.clip = sound;
                    reloading = true;
                    auso.Play(0);

                    mousePos.x = Input.mousePosition.x;
                    mousePos.y = Input.mousePosition.y;
                    Vector3 screenPos = cam.WorldToScreenPoint(transform.position);
                    diffx=mousePos.x-screenPos.x;
                    diffy=mousePos.y-screenPos.y;
                    //distance=Mathf.Sqrt(Mathf.Pow(diffy,2)+Mathf.Pow(diffx,2));
                    kulma=Mathf.Atan(diffy/diffx);
                    if(diffx<0){kulma=kulma-Mathf.PI;};
                    x=Mathf.Cos((kulma));
                    y=Mathf.Sin((kulma));
                    
                    float time = i*0.2f;

                    
                    // Positive x for right side, negative for left.
                    if (x > 0 ) {
                        smoke_right.Play();                         
                    }
                    else {
                        smoke_left.Play();
                    }

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
        Vector3 offset = new Vector3 (0, 0, z_off);
        Vector3 pos = transform.position + offset;

        clone = Instantiate(pallo, pos, new Quaternion(0, 0 , 0, 1));
        clone.AddRelativeForce(new Vector3(x,0,y) * vel_mult);

    }
    void SetReloaded() {
        reloading = false;
    }
}