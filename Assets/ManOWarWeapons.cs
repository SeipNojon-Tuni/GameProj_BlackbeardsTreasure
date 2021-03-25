using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ManOWarWeapons : MonoBehaviour
{   
    public float projectileVelMult = 5000;
    public AudioSource auso; 
    public UIHandler ui;

    // Primary weapon stats
    public Rigidbody primaryProjectile;
    public float primaryReloadTime = 3;
    public AudioClip primarySound;
    private bool primaryReloading = false;

    string activeWeapon = "Primary";

    // Secondary weapon stats
    public Rigidbody secondaryProjectile;
    public float secondaryReloadTime = 8;
    public AudioClip secondarySound;
    private bool secondaryReloading = false;
 
    public ParticleSystem smoke_left;
    public ParticleSystem fire_left;

    public ParticleSystem smoke_right;
    public ParticleSystem fire_right;

    public float x_offset = 15;

    private bool changing = false;

    // Start is called before the first frame update
    void Start()
    {
        if(!auso) {
            auso = gameObject.GetComponent<AudioSource>();
        }
    }

    // Update is called once per frame
    void Update()
    {       
            // Change active weapon
            if( ((Input.GetAxis("Change_Weapon_P1") > 0) && (gameObject.name == "ManOWarPlayer1")) || ((Input.GetAxis("Change_Weapon_P2") > 0) && (gameObject.name == "ManOWarPlayer2")) ) {
                if(!changing) {
                    changing = true;
                    if(activeWeapon == "Primary") {
                        activeWeapon = "Secondary";
                    }
                    else {
                        activeWeapon = "Primary";
                    }

                    ui.changeWeapon(activeWeapon);
                }
            }
            if( ((Input.GetAxis("Change_Weapon_P1") == 0) && (gameObject.name == "ManOWarPlayer1")) || ((Input.GetAxis("Change_Weapon_P2") == 0) && (gameObject.name == "ManOWarPlayer2")) ) {
                changing = false;
            }

            // Shooting right in Man O War
            if( ((Input.GetAxis("Shoot_Weapon_P1") > 0) && (gameObject.name == "ManOWarPlayer1")) || ((Input.GetAxis("Shoot_Weapon_P2") > 0) && (gameObject.name == "ManOWarPlayer2")) ) {
                if( (activeWeapon == "Primary") && !primaryReloading ) {
                    primaryReloading = true;
                    Fire(1);

                    Invoke("primaryReloaded", primaryReloadTime);
                    ui.reloadCycle(primaryReloadTime);
                }
                else if( (activeWeapon == "Secondary") && !secondaryReloading) {
                    secondaryReloading = true;
                    Fire(1);

                    Invoke("secondaryReloaded", secondaryReloadTime);
                    ui.reloadCycle(secondaryReloadTime);
                }
            }
            // Shooting left in Man O War
            else if ( ((Input.GetAxis("Shoot_Weapon_P1") < 0) && (gameObject.name == "ManOWarPlayer1")) || ((Input.GetAxis("Shoot_Weapon_P2") < 0) && (gameObject.name == "ManOWarPlayer2")) ) {
                if( (activeWeapon == "Primary") && !primaryReloading ) {
                    primaryReloading = true;
                    Fire(-1);

                    Invoke("primaryReloaded", primaryReloadTime);
                    ui.reloadCycle(primaryReloadTime);
                }
                
                else if( (activeWeapon == "Secondary") && !secondaryReloading) {
                    secondaryReloading = true;
                    Fire(-1);

                    Invoke("secondaryReloaded", secondaryReloadTime);
                    ui.reloadCycle(secondaryReloadTime);
                }
            }
    }

    void Fire(float xDirection) {
        Rigidbody projectile;
        if(activeWeapon == "Primary") {
            auso.clip = primarySound;
            projectile = primaryProjectile;
        }
        else {
            auso.clip = secondarySound;
            projectile = secondaryProjectile;
        }

        auso.Play();

        // Get which side of the ship is firing.
        if ( (xDirection < 0) && (activeWeapon == "Primary") ) {
            smoke_left.Play();
        }
        else if( (xDirection < 0) && (activeWeapon == "Secondary") ){
            fire_left.Play();
        }
        else if( (xDirection > 0) && (activeWeapon == "Primary") ) {
            smoke_right.Play();
        }
        else if( (xDirection > 0) && (activeWeapon == "Secondary") ) {
            fire_right.Play();
        }

        StartCoroutine(ExecuteAfterTime(xDirection, projectile));
    }

    IEnumerator ExecuteAfterTime(float xDirection, Rigidbody projectile)
    {   
        float x_off = 0;

        if(xDirection > 0) {
            x_off = x_offset;        
        } 
        else {
            x_off = -1*x_offset;
        }

        for (int i = 0; i < 10; i++) {

            // Shoot projectile
            yield return new WaitForSeconds(0.2f);

            float z_off = Random.Range(-10.0f, 10.0f);

            Rigidbody clone;

            Vector3 shootDirection = transform.TransformDirection(xDirection, 0, 0);

            Vector3 offset = new Vector3 (x_off, 0, z_off);
            Vector3 pos = transform.position + transform.TransformDirection(offset);

            clone = Instantiate(projectile, pos, new Quaternion(0, 0 , 0, 1));
            clone.AddRelativeForce(shootDirection * projectileVelMult * clone.mass);
        }
    }

    void primaryReloaded() {
        primaryReloading = false;
    }

    void secondaryReloaded() {
        secondaryReloading = false;
    }

}
