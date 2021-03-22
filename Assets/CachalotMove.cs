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

        ChangeDirection();
    }
    
    // Update is called once per frame
    void Update () {
        timeToChangeDirection -= Time.deltaTime;

        if (timeToChangeDirection <= 0) {
            ChangeDirection();
        }
        rigidBody.AddForce(1500 * Time.deltaTime * transform.forward * rigidBody.mass);
    }

    // Change direction randomly.
    private void ChangeDirection(float angle = 0) {
        if (angle == 0) {
            angle = Random.Range(-25.0f, 5.0f);
        }

        rigidBody.AddTorque(0, angle, 0);

        float time = Random.Range(5.0f, 10.0f);
        timeToChangeDirection = time;


    }

    void OnTriggerEnter(Collider collider) {
        if(collider.tag == "Terrain") {
            float direction = Mathf.Sign(Random.Range(-1, 1));
            float angle = direction * Random.Range(60f, 90f);
            ChangeDirection(angle);
        }
        else if(collider.tag == "Ship") {
            float direction = Mathf.Sign(Random.Range(-1, 1));
            float angle = direction * Random.Range(15, 30f);
            ChangeDirection(angle);
        }
    }

}