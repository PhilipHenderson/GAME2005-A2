using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollowObject : MonoBehaviour
{
    public Transform target; // Reference to the object the camera should follow

    void Update()
    {
        if (target != null)
        {
            // Calculate the direction from the camera to the target
            Vector3 directionToTarget = target.position - transform.position;

            // Calculate the rotation that looks in the direction of the target
            Quaternion targetRotation = Quaternion.LookRotation(directionToTarget);

            // Apply the rotation to the camera
            transform.rotation = targetRotation;
        }
    }
}
