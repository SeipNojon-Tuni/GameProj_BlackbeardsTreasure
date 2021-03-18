using System.Collections;
using System.Collections.Generic;
using UnityEngine;

 public class CachalotMove : MonoBehaviour {
    private float timeToChangeDirection = 4.0f;
    public Rigidbody rigidBody;

    // Use this for initialization
    public void Start () {
        
        // Get rigid body component if not defined.
        if (rigidBody == null) {
            rigidBody = GetComponent<Rigidbody>();
        }

        rigidBody.detectCollisions = false;

        ChangeDirection();
    }
    
    // Update is called once per frame
    public void Update () {
        timeToChangeDirection -= Time.deltaTime;

        if (timeToChangeDirection <= 0) {
            ChangeDirection();
        }

        rigidBody.velocity = 15 * transform.forward;
    }

    // Change direction randomly.
    private void ChangeDirection() {

        float angle = Random.Range(-25.0f, 25.0f);
        //transform.Rotate(0, angle, 0);

        rigidBody.AddTorque(0, angle, 0);

        float time = Random.Range(5.0f, 10.0f);
        timeToChangeDirection = time;


    }

    void OnTriggerEnter(Collider collider) {
        ChangeDirection();

    }
}