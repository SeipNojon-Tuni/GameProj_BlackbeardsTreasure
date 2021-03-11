using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Floater : MonoBehaviour
{   
    public Rigidbody rigidBody;
    public WaveManager manager;
    public float depthBeforeSub = 1f;
    public float dispAmo = 3f;
    public float water_level = 0;
    public float level = 15;
    public float level_offset = 1f;
    public int floaterCount = 1;
    public float waterDrag = 0.99f;
    public float waterAngularDrag = 0.5f;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {      
        // Get water level in position from WaveManager if available.
        if (!manager) {
            water_level = level + level_offset;            
        }
        else {
            water_level = manager.getLevel(new Vector2(transform.position.x, transform.position.z)) + level_offset;
        }

        rigidBody.AddForceAtPosition(Physics.gravity / floaterCount, transform.position, ForceMode.Acceleration);

        // Add buoyancy force based on how deep object is in water.
        if(transform.position.y < water_level) {
            float displacementMult = Mathf.Clamp01(water_level - transform.position.y / depthBeforeSub) * dispAmo;
            rigidBody.AddForceAtPosition(new Vector3(0f, Mathf.Abs(Physics.gravity.y) * displacementMult, 0f), transform.position, ForceMode.Acceleration);
            rigidBody.AddForce(displacementMult * -rigidBody.velocity * waterDrag * Time.fixedDeltaTime, ForceMode.VelocityChange);
            rigidBody.AddTorque(displacementMult * -rigidBody.angularVelocity * waterAngularDrag * Time.fixedDeltaTime, ForceMode.VelocityChange);
        }
    }
}
