using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
public class Ship : MonoBehaviour
{
    private CharacterController controller;
    public float playerSpeed;
    public float turn_rate;

    // Start is called before the first frame update
    void Start()
    {
        controller = gameObject.AddComponent<CharacterController>();

        // Basic turnrate to 20.
        if (turn_rate == 0.0f) {
            turn_rate = 20.0f;
        }
        if (playerSpeed == 0.0f) {
            playerSpeed = 50.0f;
        }
    }

    // Update is called once per frame
    void Update()
    {
    transform.Rotate(0,Input.GetAxis("Horizontal")*turn_rate*Time.deltaTime,0);
    Vector3 move = new Vector3(0, 0, Input.GetAxis("Vertical"));
    move = transform.TransformDirection(move);
    controller.Move(move * Time.deltaTime * playerSpeed);
    }
}