using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestoyAfterTime : MonoBehaviour
{

    public float time = 1.0f;
    private float timer = 0.0f;

    void Update()
    {
        timer += Time.deltaTime;
        if (timer > time) {
            GameObject.Destroy(gameObject);
        }
    }
}
