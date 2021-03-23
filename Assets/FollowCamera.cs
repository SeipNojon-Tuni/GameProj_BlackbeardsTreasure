using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowCamera : MonoBehaviour
{   
    public Transform target;
    GameObject CameraGoalLookAt;
    GameObject CameraGoalPosition;
    public Vector3 CameraCurrentLookAt;

    public float MovementDividor = 1;
    public float LookAtDividor = 1  ;

    void Start() {
        CameraGoalLookAt = target.transform.Find("GoalLookAt").gameObject;
        CameraGoalPosition = target.transform.Find("GoalPosition").gameObject;
    }

    void LateUpdate() {
        // Camera Movement
        Vector3 movement = CameraGoalPosition.transform.position - transform.position;
        movement = movement / MovementDividor;
        transform.position += movement;

        // Camera Look At
        Vector3 movementA = CameraGoalLookAt.transform.position - CameraCurrentLookAt;
        movementA = movementA / LookAtDividor;
        CameraCurrentLookAt += movementA;
        transform.LookAt(CameraCurrentLookAt);

        // If you dont' want the camera to have a smooth panning / turning one, then just replace the camera look at code with:
        //  transform.lookt(CameraGoalLookAt);
    }

    public void changeTarget(Transform newTarget) {
        target = newTarget;
        CameraGoalLookAt = target.transform.Find("GoalLookAt").gameObject;
        CameraGoalPosition = target.transform.Find("GoalPosition").gameObject;
    }

    /*
    public Transform target;
    public Vector3 target_Offset;
    public float smoothing = 0.2f;
    private void Start()
    {
        target_Offset = transform.position - target.position;
    }
    void Update()
    {
     if (target)
    {
        transform.position = Vector3.Lerp(transform.position, target.position+target_Offset, smoothing);
        transform.LookAt(target.transform.position + new Vector3(0f, 15.0f, 0f));

    }
    */
}