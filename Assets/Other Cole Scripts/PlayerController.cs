﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    public AudioSource BeamSound, BeamDrop, AttachSound;
    private Rigidbody2D rigidBody;
    Vector2 inputDirection;

    public float speed = 1.0f; // The max speed per second.
    public float startTime = 1.0f; // Time in seconds to reach speed.
    public float stopTime = 1.0f; // Time in seconds to stop.
    private float timer;

    public float fireRate = 4f;
    float fireInterval;
    float lastShotTime = 0;
    float bulletLife = 2;

    public float hp;

    GameObject inventory;
    Vector3 direction;

    Use use ;

    void Start() {
        rigidBody = gameObject.GetComponent<Rigidbody2D>();
        fireInterval = 1/fireRate;
        use = transform.Find("probe").GetComponent<Use>();
    }

    void FixedUpdate() 
    {    

        
        inputDirection = new Vector2(
            Input.GetAxis("Horizontal"),
            Input.GetAxis("Vertical")
        ).normalized;
        

        //currently buggy , taking an impulse sends the player spinning forever
        // Convert input to a direction vector
        

        // Get the angle between the current direction and the velocity direction
        // float angle = Vector3.Angle(transform.up, rigidBody.velocity);

        // // We multiply by the sign of the cross product to use the closest direction
        // float sign = Mathf.Sign(Vector3.Cross(transform.up, rigidBody.velocity).z);

        // // Apply the rotation over time
        // angle *= Time.fixedDeltaTime / speed * sign;
        // transform.Rotate(0.0f, 0.0f, angle);
        // if (inputDirection.magnitude > 0.0) {
        //     transform.up = inputDirection;
        // }


        if (inputDirection.magnitude > 0.0) {
            // transform.up = Vector3.RotateTowards(transform.up, inputDirection, Time.fixedDeltaTime / 0.1f, 0.0f);
            direction = inputDirection;
        }
        float angle = Vector3.Angle(transform.up, inputDirection) * Mathf.Rad2Deg;
        float sign = Mathf.Sign(Vector3.Cross(inputDirection, transform.up).z);
        angle = Mathf.Min(angle * Time.fixedDeltaTime / 0.5f, angle);
        angle *= sign;
        Quaternion rotateVector = Quaternion.AngleAxis(angle, Vector3.forward);
        transform.up = rotateVector * direction;

        // If buttons are pressed, accelerate
        if (inputDirection.magnitude > 0.0) {
            timer += Time.fixedDeltaTime/startTime;
            timer = Mathf.Max(1.0f, timer);
            // rigidBody.MovePosition( rigidBody.position + Vector2.Lerp(Vector2.zero, inputDirection * speed, timer) * Time.fixedDeltaTime);
            rigidBody.velocity = Vector2.Lerp(Vector2.zero, inputDirection * speed, timer);
            // rigidBody.velocity = inputDirection * speed;

        // Otherwise deccelerate
        } else {
            timer -= Time.fixedDeltaTime/stopTime;
            timer = Mathf.Max(0.0f, timer);
            // rigidBody.MovePosition( rigidBody.position + Vector2.Lerp(rigidBody.velocity, Vector2.zero, 1-timer)* Time.fixedDeltaTime);
            rigidBody.velocity = Vector2.Lerp(inputDirection * speed, Vector2.zero, 1-timer);
            // rigidBody.velocity = Vector2.zero;
        }

        // inputDirection = (Vector2.right * (Input.GetKey(KeyCode.D) ? 1: 0) +
        //                  Vector2.left * (Input.GetKey(KeyCode.A) ? 1: 0) +
        //                  Vector2.up * (Input.GetKey(KeyCode.W) ? 1: 0) +
        //                  Vector2.down  * (Input.GetKey(KeyCode.S) ? 1: 0) ).normalized; 
        
        // rigidBody.MovePosition(rigidBody.position + inputDirection*speed*Time.fixedDeltaTime);
    }
    

    void Update()
    {

        // Vector3 campoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        // campoint.z = 0;
        // transform.up = (campoint -transform.position).normalized;


       
        if(Time.time - lastShotTime> fireInterval && Input.GetKey(KeyCode.Space))
        {
            Shoot();
        }


        //USE THING
        if(use.selecting != null && Input.GetKeyDown(KeyCode.E))
        {
            //check what use is selecting
                //current possibilities
                    //gun pickup, many kinds of fruit, 
                    //things you pickup, and highlight blocks where you place stuff
            if(use.selecting.GetComponent<Interactable>().topickup)
            {
                if(inventory==null)
                {
                    BeamSound.Play();
                    use.selecting.GetComponent<Interactable>().Use(this);
                }
            }
            else // its a placing place
            {
                if(inventory!=null)
                {
                    AttachSound.Play();
                    use.selecting.GetComponent<Interactable>().Use(this); //FIXME, sometimes tile doesn't show
                }
            }
            //do a thing
        }

        if(Input.GetKeyDown(KeyCode.Q) && inventory!=null)//drop stuff when u press q
        {
            BeamDrop.Play();
            Drop();
        }
        
    }

    public GameObject GetTile()
    {
        return inventory.GetComponent<Interactable>().tile;
    }

    void Shoot()
    {
        //Vector3 target = transform.position + transform.up;
        lastShotTime = Time.time;
        GameManager.instance.PlayerShoot( (transform.position +
             transform.up * 1f ),
             transform.up,
             bulletLife);
    }

    //these functions are called by the probe 
    public void Pickup(GameObject g)
    {
        transform.Find("Tractor Beam Left").gameObject.SetActive(true);
        transform.Find("Tractor Beam Right").gameObject.SetActive(true);
        inventory = g;
        g.transform.parent = transform.Find("Attach Point");
        g.transform.localPosition = Vector3.zero;
    }

    public void Drop()
    {
        inventory.transform.parent = null;
        inventory = null;
        transform.Find("Tractor Beam Left").gameObject.SetActive(false);
        transform.Find("Tractor Beam Right").gameObject.SetActive(false);
    }

    public void DropAndDestroy()
    {
        inventory.transform.parent = null;
        transform.Find("Tractor Beam Left").gameObject.SetActive(false);
        transform.Find("Tractor Beam Right").gameObject.SetActive(false);
        Destroy(inventory);
        inventory = null;
    }

    public bool HasTileInInventory()
    {
        //rudimentary
        if(inventory!= null)
        {
            return true;
        }
        return false;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        GameObject bullet = collision.gameObject;
        if (bullet.gameObject.layer == LayerMask.NameToLayer("Enemy Bullet") || bullet.gameObject.layer == LayerMask.NameToLayer("Ant"))
        {
            //takeDamage(1);
        }

    }

    public void takeDamage(float amount)
    {
        hp -= amount;
        if (hp <= 0)
        {
            Destroy(gameObject);
            SceneManager.LoadScene(3, LoadSceneMode.Single);
        }
    }


}
