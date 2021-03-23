using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShipBarrage : MonoBehaviour
{
    public Rigidbody pallo;
    public float vel_mult = 5000;
    public float reload_time = 10;
    public AudioSource auso;  
    public float x_offset = 15;

    private float x;
    private float y;
    private float x_off = 0;

    private bool reloading;

    private ParticleSystem smoke_left;
    private ParticleSystem smoke_right;

    private GameObject p1_manowar;
    private GameObject p1_cannonboat;
    private GameObject p2_manowar;
    private GameObject p2_cannonboat;

    public float min_firing_dist = 170f;

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
        Transform target = playerClose();

        // Check player distance 
        if (target && !reloading) {
            
            auso.Play();

            reloading = true;

            // Check player side relative to ship to shoot
            Vector3 rightFlat = transform.right;
            rightFlat.y = 0;
            
            Vector3 targetDirectionFlat = target.position - transform.position;
            targetDirectionFlat.y = 0;
            
            Vector3 unitRight = rightFlat / rightFlat.magnitude;
            Vector3 unitTarget = targetDirectionFlat / targetDirectionFlat.magnitude;
            float direction = Vector3.Dot(unitRight, unitTarget);

            if( direction >= 0.35 ){
                //Player is to the right
                x = transform.TransformDirection(1, 0, 0).x;
                y = transform.TransformDirection(1, 0, 0).z;
                x_off = 3;

                smoke_right.Play();
                
            } else if( direction < -0.35 ) {
                //Player is to the left
                x = transform.TransformDirection(-1, 0, 0).x;
                y = transform.TransformDirection(-1, 0, 0).z;
                x_off = -3;

                smoke_left.Play();
            }
            else {
                x_off = 0;
                x = 0;
                y = 0;
            }
            if(x_off != 0) {
                for (int i = 0; i < 15; i++) {
                    
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
        Rigidbody clone;
        
        float z_off = Random.Range(-10.0f, 10.0f);

        Vector3 offset = new Vector3 (x_off, 0, z_off);
        Vector3 pos = transform.position + transform.TransformDirection(offset);

        clone = Instantiate(pallo, pos, new Quaternion(0, 0 , 0, 1));
        clone.AddRelativeForce(new Vector3(x,0,y) * vel_mult * clone.mass);

    }
    void SetReloaded() {
        reloading = false;
    }
}