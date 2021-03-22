using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowCamera : MonoBehaviour
{   
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
     }
 }
}
