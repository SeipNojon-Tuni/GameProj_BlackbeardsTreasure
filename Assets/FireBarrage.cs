using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBarrage : MonoBehaviour
{
    private Camera cam;
    public Rigidbody pallo;
    public float vel_mult = 5000;
    public float reload_time;
    public AudioClip sound;
    public AudioSource auso;

    public float x_offset = 10;

    private float diffx;
    private float diffy;
    private float x;
    private float y;
    private float kulma;
    private float distance;
    private bool reloading;
    
    private ParticleSystem fire_left;
    private ParticleSystem fire_right;

    void Start()
    { cam =Camera.main;

        reloading = false;

        // Get cannon fire particles.
        ParticleSystem[] smokes = GetComponentsInChildren<ParticleSystem>();

        foreach(var system in smokes) {

            if (system.gameObject.name == "Fire_left") {
                fire_left = system;
            }
            else if(system.gameObject.name == "Fire_right") {
                fire_right = system;
            }
        }
    }
    void Update()
    {
    // Check camera every update if it changes.
    cam =Camera.main;

    Vector2 mousePos = new Vector2();
    // Secondary fire ball barrage
    if (Input.GetButtonDown("Fire2"))
        {   
            if (!reloading) {
                auso.clip = sound;
                reloading = true;
                auso.Play(0);
        
                for (int i = 0; i < 10; i++) {
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
                    
                    float time = i*0.05f;

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

        float x_off = 0;

        Vector3 left = transform.TransformDirection( new Vector3(-x_offset, 0, 0) ) - new Vector3(x, 0, y);
        Vector3 right = transform.TransformDirection( new Vector3(x_offset, 0, 0) ) - new Vector3(x, 0, y);
        
        // Get which side of the ship is firing.
        if ( left.magnitude < right.magnitude ) {
            x_off = -1 * x_offset;
            fire_left.Play();
        }
        else {
            x_off = x_offset;
            fire_right.Play();
        }

        Vector3 offset = new Vector3 (x_off, 0, z_off);
        Vector3 pos = transform.position + transform.TransformDirection(offset);

        clone = Instantiate(pallo, pos, new Quaternion(0, 0 , 0, 1));
        clone.AddRelativeForce(new Vector3(x,0,y) * vel_mult * clone.mass);

    }
    void SetReloaded() {
        reloading = false;
    }
}