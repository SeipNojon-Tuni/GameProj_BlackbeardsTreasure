using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControl : MonoBehaviour
{   
    public Camera worldCam;
    public Camera chaseCamP1;
    public Camera chaseCamP2;

    // Start is called before the first frame update
    void Start()
    {
        worldCam.enabled = false;
        chaseCamP1.enabled = true;
        chaseCamP2.enabled = true;
    }

    // Update is called once per frame
    void Update()
    {   
        // Change between active camera with 'C'.
        if (Input.GetKeyDown(KeyCode.C)) {
            worldCam.enabled = !worldCam.enabled;
            chaseCamP1.enabled = !chaseCamP1.enabled;
            chaseCamP2.enabled = !chaseCamP2.enabled;
        }
    }
}
