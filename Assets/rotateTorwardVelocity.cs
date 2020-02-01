using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rotateTorwardVelocity : MonoBehaviour
{

    Vector3 position;
    Vector3 velocity;
    Vector3 direction;
    public float speed = 1.0f;  // The rotation speed in seconds.

    void Start()
    {
        position = transform.position;
        direction = transform.up;
    }

    void LateUpdate()
    {
        velocity = (transform.position - position);
        position = transform.position;
        
        if (velocity.magnitude > 0.01f){
            direction = velocity.normalized;
        }

        // Get the angle between the current direction and the velocity direction
        float angle = Vector3.Angle(transform.up, direction);

        // We multiply by the sign of the cross product to use the closest direction
        float sign = Mathf.Sign(Vector3.Cross(transform.up, direction).z);

        // Apply the rotation over time
        angle *= Time.deltaTime / speed * sign;
        transform.Rotate(0.0f, 0.0f, angle);

    }
}
