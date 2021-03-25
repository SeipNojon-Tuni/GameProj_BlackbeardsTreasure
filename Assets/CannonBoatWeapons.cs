using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CannonBoatWeapons : MonoBehaviour
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
 
    public ParticleSystem smoke;

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
        if( ((Input.GetAxis("Change_Weapon_P1") > 0) && (gameObject.name == "CannonBoatPlayer1")) || ((Input.GetAxis("Change_Weapon_P2") > 0) && (gameObject.name == "CannonBoatPlayer2"))) {
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
        if( ((Input.GetAxis("Change_Weapon_P1") == 0) && (gameObject.name == "CannonBoatPlayer1")) || ((Input.GetAxis("Change_Weapon_P2") == 0) && (gameObject.name == "CannonBoatPlayer2")) ) {
            changing = false;
        }

        // Shooting forward in CannonBoat
        if( ((Input.GetAxis("Shoot_Weapon_P1") != 0) && (gameObject.name == "CannonBoatPlayer1")) || ((Input.GetAxis("Shoot_Weapon_P2") != 0) && (gameObject.name == "CannonBoatPlayer2")) )
            {
            if( (activeWeapon == "Primary") && !primaryReloading ) {
                primaryReloading = true;
                Fire();

                Invoke("primaryReloaded", primaryReloadTime);
                ui.reloadCycle(primaryReloadTime);
            }
            else if( (activeWeapon == "Secondary") && !secondaryReloading) {
                secondaryReloading = true;
                Fire();

                Invoke("secondaryReloaded", secondaryReloadTime);
                ui.reloadCycle(secondaryReloadTime);
            }
        }
    }

    void Fire() {
        Rigidbody projectile;
        float direction = 0;

        if(activeWeapon == "Primary") {
            auso.clip = primarySound;
            projectile = primaryProjectile;
            direction = 1;
            projectileVelMult = 5000;
        }
        else {
            auso.clip = secondarySound;
            projectile = secondaryProjectile;
            direction = -1;
            projectileVelMult = 160;
        }

        auso.Play();

        // Get which side of the ship is firing.
        if ( (activeWeapon == "Primary") ) {
            smoke.Play();
        }

        ExecuteAfterTime(direction, projectile);
    }

    void ExecuteAfterTime(float zDirection, Rigidbody projectile)
    {  

        // Shoot projectile
        float z_off = zDirection * 25;

        Rigidbody clone;

        Vector3 shootDirection = transform.TransformDirection(0, 0, zDirection);

        Vector3 offset = new Vector3 (0, 5, z_off);
        Vector3 pos = transform.position + transform.TransformDirection(offset);

        clone = Instantiate(projectile, pos, new Quaternion(0, 0 , 0, 1));
        clone.AddRelativeForce(shootDirection * projectileVelMult * clone.mass);
    }

    void primaryReloaded() {
        primaryReloading = false;
    }

    void secondaryReloaded() {
        secondaryReloading = false;
    }

}

