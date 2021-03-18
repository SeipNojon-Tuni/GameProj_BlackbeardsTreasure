using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControl : MonoBehaviour
{   
    public Camera worldCam;
    public Camera chaseCam;

    // Start is called before the first frame update
    void Start()
    {
        worldCam.enabled = false;
        chaseCam.enabled = true;
    }

    // Update is called once per frame
    void Update()
    {   
        // Change between active camera with 'C'.
        if (Input.GetKeyDown(KeyCode.C)) {
            worldCam.enabled = !worldCam.enabled;
            chaseCam.enabled = !chaseCam.enabled;
        }
    }
}
