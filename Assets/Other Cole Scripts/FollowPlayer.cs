using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayer : MonoBehaviour
{

    public Vector3 offset;
    public float speed = 0.1f;
    public Transform player;


    void LateUpdate()
    {
        transform.position = new Vector3(
            Mathf.Lerp(transform.position.x, player.position.x + offset.x, speed),
            Mathf.Lerp(transform.position.y, player.position.y + offset.y, speed),
            transform.position.z
        );
    }
}
