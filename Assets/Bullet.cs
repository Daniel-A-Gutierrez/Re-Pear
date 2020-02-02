using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Bullet : MonoBehaviour
{
    public float speed;
    public Vector3 direction;
    public float lifetime;
    public float birthTime;
    public abstract void Terminate();
}
