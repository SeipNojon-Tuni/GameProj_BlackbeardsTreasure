using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipRigid : MonoBehaviour
{
    public Rigidbody rigidBody;
    public float playerSpeed;
    public float turn_rate;
    private float mass;
    public bool canMove = true;
    public Terrain land;
    private Vector3 currentMove;

    // Start is called before the first frame update
    void Start()
    {   
        mass = GetComponent<Rigidbody>().mass;

        // Basic turnrate to 20.
        if (turn_rate == 0.0f) {
            turn_rate = 20.0f;
        }
        if (playerSpeed == 0.0f) {
            playerSpeed = 220.0f;
        }
    }

    // Update is called once per frame
    void Update()
    {       
        if (canMove) {
            // Rigidbody version of ship controller
            transform.Rotate(0,Input.GetAxis("Horizontal")*turn_rate*Time.deltaTime,0);
            Vector3 move = new Vector3(0, 0, Input.GetAxis("Vertical"));
            move = transform.TransformDirection(move);
            move.y = 0; // Disable vertical acceleration to avoid flying.
            currentMove = move * Time.deltaTime * playerSpeed * mass;
            rigidBody.AddForce(currentMove);

            // Don't let ship on land.
            if(land && land.SampleHeight(transform.position + move * 5.0f ) - 50 > 0 ) {
                rigidBody.AddForce(-1.2f * currentMove);
            }

        }
    }
}
