using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MineLayer : MonoBehaviour
{   
    public Rigidbody mine;
    public AudioSource auso;
    public AudioClip sound;

    public float treshold = 0.3f;
    private bool reloading = false;
    private float reload_time = 5.0f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        {   
            if (!reloading && shoot() ) {
                auso.clip = sound;
                reloading = true;
                auso.Play(0);
        
                for (int i = 0; i < 1; i++) {

                    float time = i*0.2f;

                    Invoke("ExecuteAfterTime", time);
                }
                // Set weapons reloaded after delay.
                Invoke("SetReloaded", reload_time);
            }
        }
    }

    // Drop barrels randomly.
    bool shoot() {
        Random.InitState( (int)transform.position.x + (int)transform.position.z );
        float value = Random.Range(0.0f, 1.0f);
        if(value < treshold) {
            return true;
        }
        else {
            return false;
        }
    }

    void ExecuteAfterTime()
    {   
        float z_off = Random.Range(-10.0f, 10.0f);

        Rigidbody clone;
        Vector3 pos = transform.position -20*transform.forward;

        clone = Instantiate(mine, pos, new Quaternion(0, 0 , 0, 1));
        clone.AddRelativeForce(-transform.forward*120);

    }
    void SetReloaded() {
        reloading = false;
    }
}
