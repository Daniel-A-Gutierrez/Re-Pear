using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    public GameObject player;
    //The offset of the camera to centrate the player in the X axis
    public float offsetX = 0;
    //The offset of the camera to centrate the player in the Z axis
    public float offsetZ = -0;
    //The offset of the camera to centrate the player in the Y axis
    public float offsetY = 0;
    //The maximum distance permited to the camera to be far from the player, its used to     make a smooth movement
    public float maximumDistance = 2;
    //The velocity of your player, used to determine que speed of the camera
    public float playerVelocity = 10;
 
    private float _movementX;
    private float _movementZ;
    private float _movementY;
   
    void Start() {
    }
     
     
    // This function is called every fixed framerate frame, if the MonoBehaviour is enabled.
    void FixedUpdate()
    {
        _movementX = ((player.transform.position.x + offsetX - this.transform.position.x))/maximumDistance;
        _movementZ = 0;
        _movementY = ((player.transform.position.y + offsetY - this.transform.position.y))/maximumDistance;
 
        // Next position of camera
        Vector3 targetPos = this.transform.position + new Vector3((_movementX * playerVelocity * Time.deltaTime),
                                                              (_movementY * playerVelocity * Time.deltaTime),
                                                               (_movementZ * playerVelocity * Time.deltaTime));
        // Distance between old position and new position
        float distanceVec = Vector3.Distance(this.transform.position, targetPos);
        // Linearly interpolates between two vectors.
        this.transform.position = Vector3.Lerp(this.transform.position, targetPos, distanceVec);
    }
 
}
