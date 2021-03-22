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

            float horizontal = 0.0f;
            float vertical = 0.0f;

            // Get the right player input
            if(gameObject.name == "ManOWarPlayer1" || gameObject.name == "CannonBoatPlayer1") {
                horizontal = Input.GetAxis("Player1_Horizontal");
                vertical = Input.GetAxis("Player1_Vertical");
            }
            else if(gameObject.name == "ManOWarPlayer2" || gameObject.name == "CannonBoatPlayer2") {
                horizontal = Input.GetAxis("Player2_Horizontal");
                vertical = Input.GetAxis("Player2_Vertical");
            }

            rigidBody.AddTorque(0,horizontal*turn_rate*Time.deltaTime,0);

            Vector3 move = new Vector3(0, 0, vertical);
            move = transform.TransformDirection(move);
            move.y = 0; // Disable vertical acceleration to avoid flying.
            currentMove = move * Time.deltaTime * playerSpeed * mass;
            rigidBody.AddForce(currentMove);

        }
    }

}
