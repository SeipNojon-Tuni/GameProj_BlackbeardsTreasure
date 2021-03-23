using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;

public class CharacterSwap : MonoBehaviour
{   
    public GameObject manOWar;
    public GameObject cannonBoat;
    public Camera chaseCam;
    public string key;
    public Canvas ui;

    private Vector3 manOWarSpawn;
    private Vector3 cannonBoatSpawn;
    // Start is called before the first frame update
    void Start()
    {   
        manOWarSpawn = manOWar.transform.position;
        cannonBoatSpawn = cannonBoat.transform.position;

        manOWar.SetActive(true);
        cannonBoat.SetActive(false);
        chaseCam.GetComponent<FollowCamera>().changeTarget(manOWar.transform);
        ui.GetComponent<UIHandler>().setCurrentTarget(manOWar);
    }

    // Update is called once per frame
    void Update()
    {   
        // Get bound control
        KeyCode keyCode = (KeyCode)Enum.Parse(typeof(KeyCode), key);

        // Swap character
        if(Input.GetKeyDown(keyCode)) {
            manOWar.SetActive(!manOWar.activeSelf);
            cannonBoat.SetActive(!cannonBoat.activeSelf);
            
            // Switch between camera target.
            if (manOWar.activeSelf) {
                manOWar.transform.position = manOWarSpawn;
                chaseCam.GetComponent<FollowCamera>().changeTarget(manOWar.transform);
                ui.GetComponent<UIHandler>().setCurrentTarget(manOWar);
            }
            else {
                cannonBoat.transform.position = cannonBoatSpawn;
                chaseCam.GetComponent<FollowCamera>().changeTarget(cannonBoat.transform);
                ui.GetComponent<UIHandler>().setCurrentTarget(cannonBoat);
            }

        }
    }
}
