using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Bullet : MonoBehaviour
{
    public float velocity;
    public Vector3 direction;
    public float range;
    public Vector3 startingPosition;
    public abstract void Terminate() ;
}
