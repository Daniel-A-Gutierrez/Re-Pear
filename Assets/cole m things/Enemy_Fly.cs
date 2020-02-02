using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Fly : MonoBehaviour
{
    public float speedMod;
    public float damage;
    public GameObject fly;
    public float minNextDive;
    public float rotationRadius;

    public Transform stationCore;

    private Rigidbody2D flyBody;
    private Transform flyTransform;

    private bool rotating;
    private bool retreating;

    private float lastDive;
    


    // Start is called before the first frame update
    void Start()
    {
        if (fly == null)
        {
            print("No fly object set to fly script in prefab");
        }

        //getting the fly rigid body
        flyBody = fly.GetComponent<Rigidbody2D>();
        flyTransform = fly.GetComponent<Transform>();

        rotating = false;
        rotationRadius = 3;

    }

    ///<summary>Update is called once per frame</summary> 
    void FixedUpdate()
    {
        //this is the default movement inwards
        //yes I know I could've done this in an if-else statement
        //yes I know my parents don't love me and I'm bitter
        //no I DO NOT need help
        if (!retreating && !rotating)
        {
            //refresh the velocity vector of the fly and send it at the station
            flyTransform.up = (flyTransform.position - stationCore.position) * -1;
            Vector2 vel = new Vector2(flyTransform.up.x, flyTransform.up.y);
            flyBody.velocity = vel * speedMod;
        }

        if (retreating)
        {
            //do velocity but backwards this time huah hoo!
            Vector2 vel = new Vector2(flyTransform.up.x, flyTransform.up.y);
            flyBody.velocity = vel * speedMod;

            if (((Vector3.Distance(stationCore.position, fly.GetComponent<Transform>().position)) >= rotationRadius))
            {
                rotating = true;
                flyBody.velocity = Vector2.zero;
                retreating = false;
                flyTransform.up *= -1;
            }
        }

        if (rotating)
        {
            //rotate around, start the dive at a fixed time
            fly.GetComponent<Transform>().RotateAround(stationCore.position, stationCore.forward, 1);
            if (Time.time >= minNextDive + lastDive)
            {
                rotating = false;
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        GameObject toDestroy = collision.gameObject;
        if (toDestroy.tag == "StationArmor")
        {
           // Destroy(toDestroy);
        }

        //tell the fly to retreat out to the rotation circle
        retreating = true;
        lastDive = Time.time;
        flyTransform.up *= -1;
    }

    public void setStationCore(Transform core)
    {
        stationCore = core;
    }
}
