using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Wasp : MonoBehaviour
{
    public float speedMod;
    public GameObject wasp;
    public GameObject stinger;
    public GameObject waspGun;

    public float rotationRadius;
    public float fireDelay;
    public float stingerSpeed;
    public float stingerLife;

    public float damage;
    public float hp;

    public Transform stationCore;
    private Rigidbody2D waspBody;
    private Transform waspTransform;
    private bool rotating;
    private float nextFire;

    public AudioClip deathSound;
    public AudioClip fireSound;
    AudioSource enemySource;


    // Start is called before the first frame update
    void Start()
    {
        if (wasp == null)
        {
            print("No wasp object set to wasp script in prefab");
        }

        //set the ray on the waspto point to the center of the map (ie, the station core)
        waspTransform = wasp.GetComponent<Transform>();
        waspTransform.up = (waspTransform.position - stationCore.position) * -1;

        //get the wasp rigidbody
        waspBody = wasp.GetComponent<Rigidbody2D>();

        //these are extremely static enemies so the velocity is just gonna be instanced here
        Vector2 vel = new Vector2(waspTransform.up.x, waspTransform.up.y);

        //vel.Normalize();
        waspBody.velocity = vel * speedMod;

        rotating = false;
        rotationRadius = 3;
        nextFire = 0;

        AudioSource enemySource = new AudioSource();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (((Vector3.Distance(stationCore.position, wasp.GetComponent<Transform>().position)) <= rotationRadius) && !rotating)
        {
            waspBody.velocity = Vector2.zero;
            rotating = true;
        }

        if(rotating)
        {
            wasp.GetComponent<Transform>().RotateAround(stationCore.position, stationCore.forward, 1);
            if(Time.time > nextFire)
            {
                nextFire = Time.time + fireDelay;
                GameManager.instance.EnemyShoot(waspGun.transform.position, waspGun.transform.up, stingerLife);
                GetComponent<AudioManager>().Play("fire");
            }               
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
       
    }

    public void setStationCore(Transform core)
    {
        stationCore = core;
    }

    public void takeDamage(float amount)
    {
        hp -= amount;
        if(hp <= 0)
        {
            Destroy(gameObject);
            GetComponent<AudioManager>().Play("die");
        }
    }
}
