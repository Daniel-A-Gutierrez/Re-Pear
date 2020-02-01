using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    private Rigidbody2D rigidBody;
    Vector2 inputDirection;

    public float speed = 1.0f; // The max speed per second.
    public float startTime = 1.0f; // Time in seconds to reach speed.
    public float stopTime = 1.0f; // Time in seconds to stop.
    private float timer;

    void Start() {
        rigidBody = gameObject.GetComponent<Rigidbody2D>();
    }

    void FixedUpdate() {
        
        // Convert input to a direction vector
        inputDirection = new Vector2(
            Input.GetAxis("Horizontal"),
            Input.GetAxis("Vertical")
        ).normalized;

        // If buttons are pressed, accelerate
        if (inputDirection.magnitude > 0.0) {
            timer = Mathf.Min(1.0f, timer + Time.fixedDeltaTime / startTime);
            rigidBody.velocity = Vector2.Lerp(Vector2.zero, inputDirection * speed, timer);

        // Otherwise deccelerate
        } else {
            timer = Mathf.Max(0.0f, timer - Time.fixedDeltaTime / stopTime);
            rigidBody.velocity = Vector2.Lerp(rigidBody.velocity, Vector2.zero, 1-timer);
        }

    }


}
