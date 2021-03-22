using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionManager : MonoBehaviour
{
    public Rigidbody rigidBody;
    // Start is called before the first frame update
    void Start()
    {
        if(rigidBody == null) {
            rigidBody = GetComponent<Rigidbody>();
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter(Collider collider) {
        if(collider.tag == "Terrain" || collider.tag == "Ship") {

            if ( collider.tag != "Terrain" && (Mathf.Abs(rigidBody.velocity.x) < 2 && Mathf.Abs(rigidBody.velocity.z) < 2)) {
                rigidBody.velocity = -1.3f * collider.GetComponent<Rigidbody>().velocity;
                Debug.Log(rigidBody.velocity);
            }
            else {
                rigidBody.velocity = -1.3f * rigidBody.velocity;
            }

        }
    }

}
