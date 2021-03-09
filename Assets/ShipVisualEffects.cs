using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipVisualEffects : MonoBehaviour
{   
    public ParticleSystem debris;
    public ParticleSystem flames;

    // Start is called before the first frame update
    void Start()
    {
        if (debris == null || flames == null) {
            ParticleSystem[] systems = GetComponents<ParticleSystem>();

            foreach(ParticleSystem system in systems) {
                if (system.name == "Debris") {
                    debris = system;
                }
                if (system.name == "Flames") {
                    flames = system;
                }
            }

        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // Play effect of debris flying around because of cannon ball hit.
    public void playFlyingDebris(float dur = 0.5f) 
    {   
        debris.Stop();
        var durvar = debris.main.duration;
        durvar = dur;
        debris.Play();

        
    }

    // Play effect of fire burning on ship.
    public void playShipBurning(float dur) 
    {   
        flames.Stop();
        var durvar = flames.main.duration;
        durvar = dur;
        flames.Play();
    }

    public void stopShipBurning() 
    {   
        flames.Stop();
    }

}
