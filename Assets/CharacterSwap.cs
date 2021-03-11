using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterSwap : MonoBehaviour
{   
    public GameObject manOWar;
    public GameObject cannonBoat;
    public Camera chaseCam;

    private Vector3 manOWarSpawn;
    private Vector3 cannonBoatSpawn;
    // Start is called before the first frame update
    void Start()
    {   
        manOWarSpawn = manOWar.transform.position;
        cannonBoatSpawn = cannonBoat.transform.position;

        manOWar.SetActive(true);
        cannonBoat.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {   
        // Swap character
        if(Input.GetKeyDown(KeyCode.K)) {
            manOWar.SetActive(!manOWar.activeSelf);
            cannonBoat.SetActive(!cannonBoat.activeSelf);
            
            // Switch between camera target.
            if (manOWar.activeSelf) {
                manOWar.transform.position = manOWarSpawn;
                chaseCam.GetComponent<FollowCamera>().target = manOWar.transform;
            }
            else {
                cannonBoat.transform.position = cannonBoatSpawn;
                chaseCam.GetComponent<FollowCamera>().target = cannonBoat.transform;
            }

        }
    }
}
