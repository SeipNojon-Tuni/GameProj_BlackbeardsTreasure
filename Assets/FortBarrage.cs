using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FortBarrage : MonoBehaviour
{
    public Rigidbody pallo;
    public float vel_mult = 5000;
    public float reload_time = 10;
    public AudioSource auso;  
    public float x_offset = 15;

    private float x;
    private float y;

    private bool reloading;
    private ParticleSystem barrage;

    private GameObject p1_manowar;
    private GameObject p1_cannonboat;
    private GameObject p2_manowar;
    private GameObject p2_cannonboat;

    public float min_firing_dist = 200f;

    void Start()
    {   
        // Get player controller units.
        p1_manowar = GameObject.Find("ManOWarPlayer1");
        p1_cannonboat = GameObject.Find("CannonBoatPlayer1");
        p2_manowar = GameObject.Find("ManOWarPlayer2");
        p2_cannonboat = GameObject.Find("CannonBoatPlayer2");

        reloading = false;

        // Get cannon fire particles.
        ParticleSystem[] smokes = GetComponentsInChildren<ParticleSystem>();
        barrage = smokes[0];
    }
    void Update()
    {       
        
        Transform target = playerClose();

        // Check player distance
        if (target && !reloading) {
         
            // Check player side relative to ship to shoot
            Vector3 rightFlat = transform.forward;
            rightFlat.y = 0;
            
            Vector3 targetDirectionFlat = target.position - transform.position;
            targetDirectionFlat.y = 0;
            
            Vector3 unitRight = rightFlat / rightFlat.magnitude;
            Vector3 unitTarget = targetDirectionFlat / targetDirectionFlat.magnitude;
            float direction = Vector3.Dot(unitRight, unitTarget);

            if(direction >= 0 ) {
                
                auso.Play();

                reloading = true;

                for (int i = 0; i < 15; i++) {
                    x = transform.TransformDirection(0, 0, 5).x;
                    y = transform.TransformDirection(0, 0, 5).z;
                    
                    float time = i*0.2f;

                    Invoke("ExecuteAfterTime", time);
                }
                // Set weapons reloaded after delay.
                Invoke("SetReloaded", reload_time);
            }
        }
    }

    // Return whether or not player is close enough to shoot.
    Transform playerClose() {
        if (p1_manowar && Vector3.Distance(p1_manowar.transform.position, transform.position) < min_firing_dist) {
            return p1_manowar.transform;
        }
        else if (p1_cannonboat && Vector3.Distance(p1_cannonboat.transform.position, transform.position) < min_firing_dist) {
            return p1_cannonboat.transform;
        }
        else if (p2_manowar && Vector3.Distance(p2_manowar.transform.position, transform.position) < min_firing_dist) {
            return p2_manowar.transform;
        }
        else if (p2_cannonboat && Vector3.Distance(p2_cannonboat.transform.position, transform.position) < min_firing_dist) {
            return p2_cannonboat.transform;
        }

        return null;
    }

    void ExecuteAfterTime()
    {   
        float x_off = Random.Range(10.0f, 100.0f);

        Rigidbody clone;

        barrage.Play();
        
        float z_off = 3;

        Vector3 offset = new Vector3 (x_off, 0, z_off);
        Vector3 pos = transform.position + transform.TransformDirection(offset);

        clone = Instantiate(pallo, pos, new Quaternion(0, 0 , 0, 1));
        clone.AddRelativeForce(new Vector3(x,0,y) * vel_mult * clone.mass);

    }
    void SetReloaded() {
        reloading = false;
    }
}