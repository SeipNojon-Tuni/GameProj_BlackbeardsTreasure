using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SailManager : MonoBehaviour
{   
    private Vector3 last_pos;
    private Vector3 new_pos;
    private Cloth[] sails;

    public float sail_acc;

    // Start is called before the first frame update
    void Start()
    {   
        last_pos = transform.position;
        sails = GetComponents<Cloth>();

        // Default sail acceleration
        if (sail_acc == 0) {
            sail_acc = 25;
        }
    }

    // Update is called once per frame
    void Update()
    {
        new_pos = transform.position;

        // If ship moved, move sails.
        if ( new_pos != last_pos ) {
            foreach (Cloth sail in sails) {
                sail.externalAcceleration = new Vector3(0, 0, sail_acc);
                Debug.Log("Moving");
            }
        }
        else {
            foreach (Cloth sail in sails) {
                sail.externalAcceleration = new Vector3(0, 0, 0);
            }    
        }

    }
}
