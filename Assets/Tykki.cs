using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tykki : MonoBehaviour
{
    private Camera cam;
    public Rigidbody pallo;
    public float vel_mult;
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
        if (vel_mult == 0) {
            vel_mult = 5000;
        }
        reloading = false;
    }
    void Update()
    {

    // Check camera every update if it changes.
    cam =Camera.main;

    Vector2 mousePos = new Vector2();
    if (Input.GetButtonDown("Fire1"))
        {
        if (!reloading) 
            {
            // Play sound and set reloading status.
            auso.clip = sound;
            auso.Play(0);
            reloading = true;

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
            Rigidbody clone;
            clone = Instantiate(pallo, transform.position + transform.TransformDirection(new Vector3(0, 7, 15)), new Quaternion(0, 0 , 0, 1));
            clone.AddRelativeForce(new Vector3(x,0,y) * vel_mult * clone.mass);

            // Set reloaded after delay.
            Invoke("SetReloaded", reload_time);
            }

        }
    }
    void SetReloaded() {
        reloading = false;
    }
}