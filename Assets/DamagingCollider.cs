using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(AudioSource))]
public class DamagingCollider : MonoBehaviour
{  
    public AudioClip sound;
    public ParticleSystem explosion;

    // Start is called before the first frame update
    void Start()
    {
        // Destroy projectiles after time
        Destroy(gameObject, 25);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //Detect collisions between the GameObjects with Colliders attached
    void OnTriggerEnter(Collider collider)
    {   

        //Check for a match with the specific tag on any GameObject that collides with your GameObject
        if (collider.tag == "Ship")
        {
            // Destroy cannon balls on collision with ship.
            if(gameObject.name == "cannon_ball_rigid(Clone)" || gameObject.name == "flaming_cannon_ball_rigid(Clone)") {
                Destroy(gameObject);
            }
            else if(gameObject.name == "Explosive Barrel(Clone)") {
                Explode();  
            }
        }
    }

    void Explode() {
        // Play explosion effects;
        ParticleSystem[] effects = GetComponents<ParticleSystem>();
        foreach (ParticleSystem effect in effects) {
            effect.Play();
        }
        AudioSource auso = gameObject.AddComponent<AudioSource>();
        explosion.Play();

        auso.clip = sound;
        auso.Play();
        
        Destroy(gameObject, 1);   
    }
}
