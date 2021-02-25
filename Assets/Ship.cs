using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
public class Ship : MonoBehaviour
{
    private CharacterController controller;
    private float playerSpeed = 50.0f;
    // Start is called before the first frame update
    void Start()
    {
        controller = gameObject.AddComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
    transform.Rotate(0,Input.GetAxis("Horizontal")*20*Time.deltaTime,0);
    Vector3 move = new Vector3(0, 0, Input.GetAxis("Vertical"));
    move = transform.TransformDirection(move);
    controller.Move(move * Time.deltaTime * playerSpeed);
    }
}