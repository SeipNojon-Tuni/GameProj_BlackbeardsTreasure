﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShipHealth : MonoBehaviour
{   
    public float baseHealth = 100.0f;
    private float current_health;
    private float dot_duration = 0.0f;
    private float dot_mod = 1.0f; // Basic damage over time 1/s;
    public ShipVisualEffects effects;
    public int respawn_time = 5;
    public Text health_display;
    public EnemyScore score;
    public DisplayDialogue dialogueTrigger;

    public Rigidbody treasure;

    private bool dropLootOnDeath = false;
    private Vector3 spawn_point;
    private Quaternion orig_rot;
    private float orig_mass;
    private Floater floater;
    private Rigidbody rig;
    private bool dead = false;
    private Animator anim_control;
    private ShipRigid controlScript;
    private float timeSpan = 1.0f;
    private float time = 0.0f;

    // Keep track of used dialogue.
    private bool firstHit = false;
    private bool midHealth = false;
    private bool lowHealth = false;
    private bool noHealth = false;

    // Start is called before the first frame update
    void Start()
    {   
        current_health = baseHealth;
        rig = GetComponent<Rigidbody>();
        floater = gameObject.GetComponent<Floater>();
        anim_control = gameObject.GetComponent<Animator>();
        orig_mass = rig.mass;
        controlScript = GetComponent<ShipRigid>();

        if(!score) {
            score = gameObject.GetComponent<EnemyScore>();
        }
        // Prevent dropping loot if no EnemyScore component
        if(!score) {
            dropLootOnDeath = false;
        }
        else {
            dropLootOnDeath = true;
        }

        // Get spawn point
        spawn_point = transform.position;
        orig_rot = transform.rotation;

        dialogueTrigger = GameObject.Find("DialogueController").GetComponent<DisplayDialogue>();

    }

    // Update is called once per frame.
    void Update()
    {
        // If uncalculated damage ticks remain take damage.
        if (dot_duration > 0) {
            StartCoroutine(takeDmgOverTime());
        }

        if (current_health <= 0 && !dead) {
            dead = true;
            StartCoroutine(commitDie());
        }


        // Hold R to die and respawn
        if (Input.GetKey(KeyCode.R) && !dead) {
            time += Time.deltaTime;
        }
        if(time > timeSpan && !dead) {
            transform.position = spawn_point;
        }

        // Button released
        if (Input.GetKeyUp(KeyCode.R)) {
            time = 0.0f;
        }

        // Trigger dialogues
        if(dialogueTrigger) {

            // For player 
            if(!dropLootOnDeath) {
                if(!firstHit && current_health < baseHealth) {
                    dialogueTrigger.displayDialogue("FirstHit");
                    firstHit = true;
                }
                else if(!midHealth && current_health < (0.5f * baseHealth) ) {
                    dialogueTrigger.displayDialogue("MidHealth");
                    midHealth = true;
                }
                else if(!lowHealth && current_health < (0.15f * baseHealth) ) {
                    dialogueTrigger.displayDialogue("AlmostDead");
                    lowHealth = true;
                }
                else if(!noHealth && dead ) {
                    dialogueTrigger.displayDialogue("Dead");
                    noHealth = true;
                }
            }

            // For Enemy
            else {
                if(!midHealth && current_health < (0.5f * baseHealth) ) {
                    dialogueTrigger.displayDialogue("MidHealth");
                    midHealth = true;
                }
                else if(!noHealth && dead ) {
                    dialogueTrigger.displayDialogue("EnemyHit");
                    noHealth = true;
                }
            }
        }


        // Reset dialogues
        if (current_health >= baseHealth) {
            firstHit = false;
            midHealth = false;
            lowHealth = false;
            noHealth = false;
        }
    }

    // Get ship health
    public float getCurrentHealth() {
        return current_health;
    }

    IEnumerator commitDie() {
        // Don't have several deaths running at a time.
        dead = true;

        // Drop loot if instructed to.
        dropLoot();

        // Sink ship
        float offset = floater.level_offset;

        // Disable movement during spawning
        controlScript.canMove = false;

        // Play sinking animation.
        anim_control.SetInteger("isSinking", 1);
        
        // Wait for ship to respawn.
        yield return new WaitForSeconds(respawn_time);
        
        anim_control.SetInteger("isSinking", 0);

        // Wait transition
        yield return new WaitForSeconds(0.1f);

        // Move to spawn.
        rig.isKinematic = true;
        transform.position = spawn_point;

        // Reset dmg animations.
        effects.stopShipBurning();
        effects.stopFlyingDebris();

        // Reset velocity and health.
        
        current_health = baseHealth;
        floater.level_offset = offset;
        rig.velocity = Vector3.zero;
        rig.angularVelocity = Vector3.zero;

        transform.rotation = Quaternion.Euler(0, 0, 0);
        rig.isKinematic = false;

        controlScript.canMove = true;

        // Reset dialogue
        firstHit = false;
        midHealth = false;
        lowHealth = false;
        noHealth = false;

        dead = false;
    }

    void dropLoot() {
        if(dropLootOnDeath) {
            int dropAmo = score.getScore();

            for (int i = 0; i < 1; i++) {

                float time = i*0.05f;

                Invoke("ExecuteAfterTime", time);
            }

            // Reset ship score
            score.resetScore();

        }
    }

    // Take basic damage instance.
    void modifyHealth(float amount) {
        current_health += amount;
        return;
    }

    IEnumerator takeDmgOverTime() 
    {       
        // Get modifiers for this damage source.
        float dur = dot_duration;
        float current_mod = dot_mod;

        // Reset duration so this dmg source won't get reapplied.
        dot_duration = 0.0f;
        dot_mod = 1.0f;

        for (float ft = 0; ft < dur; ft += 0.2f) 
        {   
            float amo = 0.2f * current_mod;
            modifyHealth(amo); // Dot mod remains dmg/s ticks every .2 seconds
            yield return new WaitForSeconds(.2f); // Tick damage every .2 seconds
        }
        effects.stopShipBurning();
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

        // Play hit effect.
        if (collider.tag != "Salvage") {
            effects.playFlyingDebris();
        }

        // Determine is this single instance or damage over time.
        if (duration >= 1) {
            dmgOverTime(duration, magnitude); 
            effects.playShipBurning(duration);
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
        if (collider.tag == "Projectile" || collider.tag == "Salvage")
        {
            damageCalc(collider);
        }
        return;

    }

    // Drop loot
    void ExecuteAfterTime()
    {   
        float z_off = Random.Range(-10.0f, 10.0f);

        Rigidbody clone;
        Vector3 pos = transform.position -20*transform.forward;

        clone = Instantiate(treasure, pos, new Quaternion(0, 0 , 0, 1));
        clone.AddRelativeForce(-transform.forward*120);

    }

}
