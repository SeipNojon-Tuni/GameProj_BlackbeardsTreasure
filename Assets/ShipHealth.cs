using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipHealth : MonoBehaviour
{   
    public float baseHealth = 100.0f;
    private float current_health;
    private float dot_duration = 0.0f;
    private float dot_mod = 1.0f; // Basic damage over time 1/s;
    public ShipVisualEffects effects;

    // Start is called before the first frame update
    void Start()
    {
        current_health = baseHealth;
    }

    // Update is called once per frame.
    void Update()
    {
        // If uncalculated damage ticks remain take damage.
        if (dot_duration > 0) {
            StartCoroutine("takeDmgOverTime");
            effects.playShipBurning(dot_duration);
        }

        if (current_health <= 0) {
            commitDie();
        }

    }

    // Get ship health
    public float getCurrentHealth() {
        return current_health;
    }

    void commitDie() {
        /* === PLACE HOLDER DEATH EVENT ===
         *  Replace with something that enables
         *  respawning etc.
         */

        Rigidbody rig = gameObject.GetComponent<Rigidbody>();

    }

    // Take basic damage instance.
    void modifyHealth(float amount) {
        current_health += amount;
        return;
    }

    IEnumerator takeDmgOverTime() 
    {
        for (float ft = dot_duration; ft >= 0; ft -= 0.2f) 
        {
            modifyHealth(0.2f * dot_mod); // Dot mod remains dmg/s ticks every .2 seconds
            yield return new WaitForSeconds(.2f); // Tick damage every .2 seconds
        }

        effects.stopShipBurning();

        // Return to default values.
        dot_duration = 0.0f;
        dot_mod = 1.0f;
    }

    // Start ticking damage over time.
    void dmgOverTime(float duration, float dot_mag) {
        dot_duration = duration;
        dot_mod = dot_mag;
        return;
    }

    void damageCalc(Collider collider) 
    {   
        DamageValues dmgVals = collider.GetComponent<DamageValues>();
        float duration = dmgVals.getDuration();
        float magnitude = dmgVals.getDamage();

        Debug.Log(duration + " " + magnitude);

        // Play hit effect.
        effects.playFlyingDebris();

        // Determine is this single instance or damage over time.
        if (duration > 1) {
            dmgOverTime(duration, magnitude);
        }
        else {
            modifyHealth(magnitude);
        }
        return;
    }

    //Detect collisions between the GameObjects with Colliders attached
    void OnTriggerEnter(Collider collider)
    {   

        //Check for a match with the specific tag on any GameObject that collides with your GameObject
        if (collider.tag == "Projectile")
        {
            damageCalc(collider);
        }
        return;

    }

}
