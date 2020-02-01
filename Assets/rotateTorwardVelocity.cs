using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rotateTorwardVelocity : MonoBehaviour
{

    Vector3 position;
    Vector3 velocity;
    Vector3 direction;
    public float speed = 1.0f;

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

        transform.LookAt(position + Vector3.forward, direction);
        
    }
}
