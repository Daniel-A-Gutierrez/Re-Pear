using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(Rigidbody2D))]
public class StationBullet : Bullet
{   
    public Animator animator;
    new Rigidbody2D rigidbody;
    private bool terminating = false;

    void Start()
    {
        rigidbody = GetComponent<Rigidbody2D>();
        rigidbody.velocity = transform.up * speed;
    }

    void Update()
    {
        if(Time.time-birthTime > lifetime)
            Terminate();
    }

    void OnEnable()
    {
        rigidbody = GetComponent<Rigidbody2D>();
        rigidbody.velocity = transform.up*speed;
        animator.SetTrigger("OnAwake");
    }

    public override void Terminate()
    {
        if (terminating == false){
            terminating = true;
            animator.SetTrigger("OnImpact");
            rigidbody.velocity = Vector2.zero;
            StartCoroutine(TerminateAfterSeconds());
        }
    }

    IEnumerator TerminateAfterSeconds()
    {
        yield return new WaitForSeconds(1);
        terminating = false;
        GameManager.instance.TerminateStationBullet(gameObject);
    }

    void OnCollisionEnter2D(Collision c)
    {
        Terminate();
    }
}